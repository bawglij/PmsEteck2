using Microsoft.EntityFrameworkCore;
using Microsoft.Exchange.WebServices.Data;
using PmsEteck.Data.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Data.EmailService
{
    public class Email
    {
        private static PmsEteckContext db = new PmsEteckContext();

        private async System.Threading.Tasks.Task GetTicketsAsync()
        {
            List<MailConfig> mailConfigs = db.MailConfigs.ToList();

            foreach (MailConfig mailConfig in mailConfigs)
            {
                string username = mailConfig.Username;
                byte[] bytePassword = Convert.FromBase64String(mailConfig.Password);
                string password = Encoding.UTF8.GetString(bytePassword);
                string mailbox = mailConfig.Mailbox;
                string readFolder = mailConfig.ReadFolder;
                bool archiveMail = mailConfig.ArchiveMail;
                string serviceUrl = mailConfig.ServiceUrl ?? ConfigurationManager.AppSettings["exchangeUrl"];

                ExchangeService service = new ExchangeService(ExchangeVersion.Exchange2013_SP1)
                {
                    Credentials = new WebCredentials(username, password),
                    TraceEnabled = true,
                    TraceFlags = TraceFlags.All
                };

                if (string.IsNullOrWhiteSpace(serviceUrl))
                    service.AutodiscoverUrl(mailbox, RedirectionUrlValidationCallback);
                else
                    service.Url = new Uri(serviceUrl);

                FindFoldersResults folders = await service.FindFolders(WellKnownFolderName.Root, new FolderView(int.MaxValue)
                {
                    PropertySet = new PropertySet(BasePropertySet.IdOnly)
                {
                    FolderSchema.DisplayName
                },
                    Traversal = FolderTraversal.Deep
                });

                Folder targetFolder = folders.First(w => w.DisplayName == readFolder);
                PropertySet propertySet = new PropertySet(BasePropertySet.FirstClassProperties, ItemSchema.UniqueBody);
                //propertySet.RequestedBodyType = BodyType.Text;
                FindItemsResults<Item> items = await targetFolder.FindItems(new ItemView(int.MaxValue));
                foreach (EmailMessage item in items)
                {
                    EmailMessage email = await EmailMessage.Bind(service, item.Id, propertySet);
                    await email.Load(propertySet);
                    string emailBody = email.Body.Text ?? string.Empty;
                    if (emailBody.Contains("<body") && emailBody.Contains("</body>"))
                    {
                        int bodyBeginning = emailBody.IndexOf("<body") + "<body".Length;
                        int to = emailBody.LastIndexOf("</body>");
                        string body = emailBody.Substring(bodyBeginning, to - bodyBeginning);
                        int from = body.IndexOf(">") + bodyBeginning + 1;
                        emailBody = emailBody.Substring(from, to - from);
                    }

                    while (emailBody.Contains("<style") && emailBody.Contains("</style>"))
                    {
                        int from = emailBody.IndexOf("<style");
                        int to = emailBody.LastIndexOf("</style>") + 9;
                        emailBody = emailBody.Remove(from, to - from);
                    }
                    //Ticket ticket = new Ticket();
                    if (emailBody.Contains("e-aj.my.com"))
                        emailBody.Replace("e-aj.my.com", "pms.eteck.nl");

                    if (string.IsNullOrWhiteSpace(email.Subject))
                        email.Subject = "<geen onderwerp>";
                    if (string.IsNullOrWhiteSpace(emailBody) || emailBody == "\r\n")
                        emailBody = "<geen inhoud>";

                    //emailBody = emailBody.Replace("\r\n", "<br />");
                    int ticketID = 0;
                    try
                    {
                        ticketID = Convert.ToInt32(email.Subject.Split(new[] { "[nr:" }, StringSplitOptions.None)[1].Split(']')[0]);
                    }
                    catch
                    {

                    }
                    Ticket ticket = !email.Subject.Contains("[nr:") ? new Ticket() : db.Tickets.SingleOrDefault(s => s.iTicketID == ticketID);

                    if (!email.Subject.Contains("[nr:"))
                    {
                        int? occupantID = GetOccupantID(email.Sender?.Address);

                        Occupant occupant = new Occupant();
                        if (occupantID.HasValue)
                        {
                            occupant = db.Occupants.Find(occupantID);
                            if (occupant == null)
                                occupant = new Occupant();
                        }

                        ticket = new Ticket
                        {
                            iOccupantID = occupantID,
                            iTicketStatusID = 1,
                            iTicketTypeID = 1,
                            sTitle = email.Subject,
                            sMessage = emailBody,
                            bAssigned = false,
                            bFinished = false,
                            dtCreateDateTime = email.DateTimeSent.ToUniversalTime(),
                            sEmail = email.Sender?.Address,
                            MailConfigID = mailConfig.MailConfigID
                        };

                        if (occupant.iDebtorID != 0)
                            ticket.iDebtorID = occupant.iDebtorID;

                        if (occupant.AddressOccupants != null)
                        {
                            if (occupant.AddressOccupants.Any(w => w.bIsActive))
                            {
                                ticket.iProjectID = occupant.AddressOccupants.FirstOrDefault().Address.iProjectKey;
                                ticket.iAddressID = occupant.AddressOccupants != null ? occupant.AddressOccupants.FirstOrDefault(f => f.bIsActive).iAddressID : (int?)null;
                            }
                        }

                        db.Tickets.Add(ticket);
                        db.SaveChanges();
                    }
                    else
                    {
                        if (ticket == null)
                        {
                            int? occupantID = GetOccupantID(email.Sender?.Address);
                            Occupant occupant = new Occupant();
                            if (occupantID.HasValue)
                                occupant = db.Occupants.Include(i => i.AddressOccupants).SingleOrDefault(s => s.iOccupantID == occupantID);

                            ticket = new Ticket
                            {
                                iOccupantID = GetOccupantID(email.Sender.Address),
                                iTicketStatusID = 1,
                                iTicketTypeID = 1,
                                sTitle = email.Subject,
                                sMessage = emailBody,
                                bAssigned = false,
                                bFinished = false,
                                dtCreateDateTime = email.DateTimeSent.ToUniversalTime(),
                                sEmail = email.Sender?.Address,
                                MailConfigID = mailConfig.MailConfigID
                            };

                            if (occupant.iDebtorID != 0)
                                ticket.iDebtorID = occupant.iDebtorID;

                            if (occupant.AddressOccupants != null)
                                if (occupant.AddressOccupants.Any(w => w.bIsActive))
                                {
                                    ticket.iProjectID = occupant.AddressOccupants.FirstOrDefault(f => f.bIsActive).Address.iProjectKey;
                                    ticket.iAddressID = occupant.AddressOccupants.FirstOrDefault(f => f.bIsActive).iAddressID;
                                }

                            db.Tickets.Add(ticket);
                        }
                        else
                        {
                            ticket.bFinished = false;
                            ticket.iTicketStatusID = 2;
                            db.Entry(ticket).State = EntityState.Modified;
                        }

                        db.SaveChanges();
                    }

                    Response response = new Response
                    {
                        iTicketID = ticket.iTicketID,
                        iResponseTypeID = 2,
                        dtCreateDateTime = email.DateTimeSent.ToUniversalTime(),
                        sMessage = emailBody,
                        bIncoming = true,
                        sFromEmail = email.From.Address,
                        sFromName = email.From.Name,
                        sToEmail = "klantzaken@eteck.nl",
                        sToName = "Eteck Energie Bedrijven"
                    };

                    db.Responses.Add(response);
                    db.SaveChanges();

                    if (email.CcRecipients != null)
                    {
                        foreach (var CC in email.CcRecipients)
                        {
                            db.EmailAddresses.Add(new PmsEteck.Data.Models.EmailAddress
                            {
                                iResponseID = response.iResponseID,
                                sEmailAddress = CC.Address
                            });
                        }
                    }

                    int lastTicketID = db.Tickets
                        .OrderByDescending(o => o.dtCreateDateTime)
                        .First().iTicketID;

                    db.TicketLogs.Add(new TicketLog()
                    {
                        iTicketID = lastTicketID,
                        sActivity = "Melding is aangekomen vanuit " + targetFolder.DisplayName,
                        dtTimestamp = DateTime.UtcNow
                    });

                    foreach (Attachment attachment in email.Attachments)
                    {
                        if (attachment is FileAttachment fileAttachment)
                        {
                            try
                            {
                                await fileAttachment.Load();
                            }
                            catch
                            {
                                continue;
                            }

                            MailAttachment mailAttachment = new MailAttachment
                            {
                                sFileName = fileAttachment.Name,
                                sContentType = fileAttachment.ContentType,
                                bByteArray = fileAttachment.Content,
                                iResponseID = response.iResponseID
                            };

                            db.MailAttachments.Add(mailAttachment);
                        }
                    }

                    db.SaveChanges();

                    if (archiveMail)
                    {
                        Folder destinationFolder = folders.First(w => w.DisplayName == mailConfig.ArchiveFolder);
                        await email.Move(destinationFolder.Id);
                    }
                }
            }
        }

        private static int? GetOccupantID(string email)
        {
            Occupant occupant = db.Occupants
                .FirstOrDefault(f => f.sEmailAddress == email);

            if (occupant == null || db.Occupants.Where(w => w.sEmailAddress == email).Count() > 1)
                return null;

            return occupant.iOccupantID;
        }

        private static bool RedirectionUrlValidationCallback(string redirectionUrl)
        {
            bool result = false;

            Uri redirectionUri = new Uri(redirectionUrl);

            if (redirectionUri.Scheme == "https")
                result = true;

            return result;
        }
    }
}