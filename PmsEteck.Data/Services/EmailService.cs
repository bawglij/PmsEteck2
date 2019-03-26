
using Microsoft.EntityFrameworkCore;
using Microsoft.Exchange.WebServices.Data;
using PmsEteck.Data.Models;
using PmsEteck.Data.Models.Results;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
namespace PmsEteck.Data.Services
{
    public struct TicketEmail
    {
        public List<string> Receiver;
        public List<string> CC;
        public List<string> BCC;
        public string Subject;
        public string From { get; set; }
        public string Body;
        public bool Urgent { get; set; }
        public int? MailConfigID { get; set; }
        public IEnumerable<MailAttachment> Attachments;
        public DateTime? CreatedDateTime { get; set; }
    }

    public class EmailService
    {
        public PmsEteckContext db = new PmsEteckContext();

        #region GetTickets

        public async System.Threading.Tasks.Task GetTicketsAsync(IEnumerable<MailConfig> mailConfigs)
        {
            //mailConfigs = mailConfigs.Where(w => w.MailConfigID == 1); // Tijdelijk
            foreach (MailConfig mailConfig in mailConfigs)
            {
                ExchangeService service = new ExchangeService(ExchangeVersion.Exchange2013_SP1)
                {
                    Credentials = new WebCredentials(mailConfig.Username, Encoding.UTF8.GetString(Convert.FromBase64String(mailConfig.Password))),
                    TraceEnabled = true,
                    TraceFlags = TraceFlags.All
                };

                string serviceUrl = mailConfig.ServiceUrl ?? ConfigurationManager.AppSettings["exchangeUrl"];
                if (string.IsNullOrWhiteSpace(serviceUrl))
                    service.AutodiscoverUrl(mailConfig.Mailbox, RedirectionUrlValidationCallback);
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

                PropertySet propertySet = new PropertySet(BasePropertySet.FirstClassProperties, ItemSchema.TextBody)
                {
                    RequestedBodyType = BodyType.Text
                };

                foreach (EmailMessage item in await folders.FirstOrDefault(w => w.DisplayName == mailConfig.ReadFolder).FindItems(new ItemView(int.MaxValue)))
                {
                    EmailMessage email = await EmailMessage.Bind(service, item.Id, propertySet);
                    await email.Load(propertySet);

                    if (string.IsNullOrWhiteSpace(email.Subject))
                        email.Subject = "<geen onderwerp>";

                    string message = email.TextBody.Text?.Replace("\r\n", "<br>");
                    if (string.IsNullOrWhiteSpace(message) || message == "<br>")
                        message = "<geen inhoud>";

                    if (message.Contains("--#-- Graag boven deze regel reageren --#--"))
                    {
                        int index = message.IndexOf("--#-- Graag boven deze regel reageren --#--");
                        message = message.Substring(0, index);
                    }

                    Ticket ticket = (email.Subject.Contains("[nr:") && int.TryParse(email.Subject.Split(new[] { "[nr:" }, StringSplitOptions.None)[1].Split(']').First(), out int ticketID))
                        ? GetTicket(ticketID) ?? CreateTicket(email, message)
                        : CreateTicket(email, message);

                    ticket.MailConfigID = mailConfig.MailConfigID;
                    Response response = new Response()
                    {
                        iResponseTypeID = 2,
                        dtCreateDateTime = email.DateTimeCreated.ToUniversalTime(),
                        sMessage = message,
                        bIncoming = true,
                        sFromEmail = email.Sender.Address,
                        sFromName = email.Sender.Name,
                        sToEmail = mailConfig.Username,
                        sToName = mailConfig.Description,
                        EmailAddresses = email.CcRecipients.Select(s => new Models.EmailAddress
                        {
                            sEmailAddress = s.Address
                        }).ToList(),
                        Attachments = email.Attachments.Select(f => f is FileAttachment ? GetAttachment((FileAttachment)f) : null).ToList()
                    };
                    ticket.Responses.Add(response);
                    ticket.TicketLogs.Add(new TicketLog()
                    {
                        sActivity = string.Format("Melding is binnengekomen vanuit de {0} mailbox", mailConfig.Description),
                        dtTimestamp = DateTime.UtcNow
                    });

                    bool sendReply = false;
                    string responseMessage = "Uw bericht is in goede orde ontvangen";
                    if (db.Entry(ticket).State == EntityState.Added)
                    {
                        db.Tickets.Add(ticket);
                        //sendReply = true;
                        //ticket.Responses.Add(new Response()
                        //{
                        //    iResponseTypeID = 2,
                        //    dtCreateDateTime = DateTime.UtcNow,
                        //    sMessage = responseMessage,
                        //    bIncoming = false,
                        //    sToEmail = email.Sender.Address,
                        //    sToName = email.Sender.Name,
                        //    sFromEmail = mailConfig.Username,
                        //    sFromName = "Automatisch"
                        //});
                    }
                    else
                        db.Entry(ticket).State = EntityState.Modified;

                    db.SaveChanges();
                    email.Subject = "[nr:" + ticket.iTicketID + "] " + ticket.sTitle;
                    await email.Update(ConflictResolutionMode.AutoResolve);

                    if (sendReply)
                    {
                        try
                        {
                            ResponseMessage reply = item.CreateReply(false);
                            reply.Subject = string.Format("[nr:{0}] {1}", ticket.iTicketID, ticket.sTitle);
                            reply.BodyPrefix = responseMessage;
                            await reply.SendAndSaveCopy();
                        }
                        catch
                        {
                            db.Responses.Remove(response);
                            db.SaveChanges();
                        }
                    }

                    if (mailConfig.ArchiveMail)
                    {
                        Folder destinationFolder = folders.FirstOrDefault(w => w.DisplayName == mailConfig.ArchiveFolder);
                        if (destinationFolder != null)
                            await email.Move(destinationFolder.Id);
                    }
                }
            }
        }

        private Ticket GetTicket(int ticketID)
        {
            Ticket ticket = db.Tickets
                .Include(i => i.Responses)
                .Include(i => i.TicketLogs)
                .SingleOrDefault(s => s.iTicketID == ticketID);
            if (ticket != null)
            {
                ticket.bFinished = false;
                ticket.iTicketStatusID = 2;
                db.Entry(ticket).State = EntityState.Modified;
            }

            return ticket;
        }

        private Ticket CreateTicket(EmailMessage email, string message)
        {
            IQueryable<Occupant> occupants = db.Occupants
                .Include(i => i.AddressOccupants)
                .Where(s => s.sEmailAddress == email.Sender.Address);

            Occupant occupant = occupants.Count() == 1
                ? occupants.First()
                : null;

            Ticket ticket = new Ticket()
            {
                iTicketStatusID = 1,
                iTicketTypeID = 1,
                sTitle = email.Subject,
                sMessage = message,
                bAssigned = false,
                bFinished = false,
                dtCreateDateTime = email.DateTimeSent.ToUniversalTime(),
                sEmail = email.Sender.Address,
                iOccupantID = occupant?.iOccupantID,
                iDebtorID = occupant?.iDebtorID,
                iAddressID = occupant?.AddressOccupants.SingleOrDefault(s => s.bIsActive)?.iAddressID,
                iProjectID = occupant?.AddressOccupants.SingleOrDefault(s => s.bIsActive)?.Address.iProjectKey,
                Responses = new List<Response>(),
                TicketLogs = new List<TicketLog>(),
            };
            db.Entry(ticket).State = EntityState.Added;

            return ticket;
        }

        private MailAttachment GetAttachment(FileAttachment fileAttachment)
        {
            fileAttachment.Load();

            return new MailAttachment()
            {
                sFileName = fileAttachment.Name,
                sContentType = fileAttachment.ContentType,
                bByteArray = fileAttachment.Content,
            };
        }

        #endregion

        public async System.Threading.Tasks.Task<Result> SendMailAsync(TicketEmail email)
        {
            //MailConfig mailConfig = email.MailConfigID != 1 ? db.MailConfigs.Find(1) : db.MailConfigs.Find(email.MailConfigID); // Tijdelijk
            MailConfig mailConfig = db.MailConfigs.Find(email.MailConfigID);

            string username = mailConfig.Username;
            string base64passWord = mailConfig.Password;
            string mailbox = mailConfig.Mailbox;
            string serviceUrl = mailConfig.ServiceUrl ?? ConfigurationManager.AppSettings["exchangeUrl"];

            byte[] bytePassword = Convert.FromBase64String(base64passWord);

            ExchangeService service = new ExchangeService(ExchangeVersion.Exchange2013_SP1)
            {
                Credentials = new WebCredentials(username, Encoding.UTF8.GetString(bytePassword)),
                TraceEnabled = true
            };
            if (string.IsNullOrEmpty(serviceUrl))
                service.AutodiscoverUrl(mailbox, RedirectionUrlValidationCallback);
            else
                service.Url = new Uri(serviceUrl);

            SearchFilter searchFilter = new SearchFilter.SearchFilterCollection(LogicalOperator.And, new List<SearchFilter>
            {
                new SearchFilter.ContainsSubstring(ItemSchema.Subject, email.Subject)
            });
            PropertySet propertySet = new PropertySet(BasePropertySet.IdOnly)
            {
                FolderSchema.DisplayName
            };
            FindFoldersResults folders = await service.FindFolders(WellKnownFolderName.Root, new FolderView(int.MaxValue)
            {
                PropertySet = propertySet,
                Traversal = FolderTraversal.Deep
            });
            Folder targetFolder = folders.FirstOrDefault(w => w.DisplayName == (mailConfig.ArchiveFolder ?? mailConfig.ReadFolder));
            FindItemsResults<Item> items = await targetFolder.FindItems(searchFilter, new ItemView(int.MaxValue));

            string body = email.From == "Ticket" ? "<span style='font-size: 9pt; font-family: &quot;Lucida Grande&quot;, sans-serif, serif, EmojiFont; color: rgb(181, 181, 181);'>--#-- Graag boven deze regel reageren --#--</span> <br />" : "";
            body += email.Body;
            if (items.Count() > 0)
            {
                EmailMessage emailMessage = await EmailMessage.Bind(service, items.OrderByDescending(o => o.DateTimeReceived).First().Id, propertySet);
                await emailMessage.Load(propertySet);
                ResponseMessage reply = emailMessage.CreateReply(false);
                reply.Body = body;
                try
                {
                    EmailMessage mailMessage = await reply.Save();
                    if (email.Receiver != null)
                        email.Receiver.ForEach(e => mailMessage.ToRecipients.Add(e));

                    if (email.Urgent)
                        mailMessage.Importance = Importance.High;

                    if (email.Attachments != null)
                        foreach (MailAttachment attachment in email.Attachments)
                            mailMessage.Attachments.AddFileAttachment(attachment.sFileName, attachment.bByteArray);
                    if (email.CC != null)
                        email.CC.ForEach(e => mailMessage.CcRecipients.Add(e));
                    if (email.BCC != null)
                        email.BCC.ForEach(e => mailMessage.BccRecipients.Add(e));
                    await mailMessage.SendAndSaveCopy();
                    return Result.Success;
                }
                catch (Exception ex)
                {
                    return Result.Failed(ex.Message + ", " + ex.InnerException?.Message);
                }
            }
            else
            {
                EmailMessage message = new EmailMessage(service)
                {
                    Subject = email.Subject,
                    Body = body
                };
                if (email.Receiver != null)
                    email.Receiver.ForEach(e => message.ToRecipients.Add(e));

                if (email.Urgent)
                    message.Importance = Importance.High;

                if (email.Attachments != null)
                    foreach (MailAttachment attachment in email.Attachments)
                        message.Attachments.AddFileAttachment(attachment.sFileName, attachment.bByteArray);
                if (email.CC != null)
                    email.CC.ForEach(e => message.CcRecipients.Add(e));
                if (email.BCC != null)
                    email.BCC.ForEach(e => message.BccRecipients.Add(e));
                try
                {
                    await message.Send();
                    return Result.Success;
                }
                catch (Exception ex)
                {
                    return Result.Failed(ex.Message + ", " + ex.InnerException?.Message);
                }
            }

            //EmailMessage message = new EmailMessage(service)
            //{
            //    Subject = email.Subject,
            //    Body = email.Body
            //};
            //if (email.Receiver != null)
            //    email.Receiver.ForEach(e => message.ToRecipients.Add(e));

            //if (email.Urgent)
            //    message.Importance = Importance.High;

            //if (email.Attachments != null)
            //    foreach (Models.MailAttachment attachment in email.Attachments)
            //        message.Attachments.AddFileAttachment(attachment.sFileName, attachment.bByteArray);
            //if (email.CC != null)
            //    email.CC.ForEach(e => message.CcRecipients.Add(e));
            //if (email.BCC != null)
            //    email.BCC.ForEach(e => message.BccRecipients.Add(e));
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