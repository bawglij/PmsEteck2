using PmsEteck.Data.Models;
using System;
using System.Collections.Generic;

namespace PmsEteck.Data.Repositories
{
    public interface ITicketRepository
    {
        void Create(Debtor debtor, string title, string body, string userID, int ticketTypeID, List<Label> labels, DateTime? suppress, bool sendMail, string solution);
    }
    /*
    public class TicketRepository : Repository<Ticket>, ITicketRepository
    {

        //IIdentity user = HttpContext.Current.User.Identity;

        public TicketRepository(DbContext dataContext) : base(dataContext)
        {
            DbSet = dataContext.Set<Ticket>();
        }
        /*
        public void Create(Debtor debtor, string title, string body, string userID, int ticketTypeID, List<Label> labels, DateTime? suppress, bool sendMail, string solution)
        {
            Ticket ticket = new Ticket()
            {
                sTitle = title,
                sMessage = body,
                sUserID = userID,
                iTicketTypeID = ticketTypeID,
                iTicketStatusID = string.IsNullOrWhiteSpace(solution) ? 2 : 3,
                Labels = labels,
                SuppressedUntil = suppress,
                dtCreateDateTime = DateTime.UtcNow,
                iDebtorID = debtor.iDebtorID,
                iProjectID = debtor.AddressDebtors?.Where(w => w.bIsActive)?.Count() == 1 ? debtor.AddressDebtors.Single(f => f.bIsActive).Address.iProjectKey : (int?)null,
                iAddressID = debtor.AddressDebtors?.Where(w => w.bIsActive)?.Count() == 1 ? debtor.AddressDebtors.Single(f => f.bIsActive).iAddressID : (int?)null,
                iOccupantID = debtor.Occupants?.Count() == 1 ? debtor.Occupants.Single().iOccupantID : (int?)null,
                sPhoneNumber = debtor.sPhoneNumber,
                sEmail = debtor.sEmailAddress,
                bAssigned = !string.IsNullOrWhiteSpace(userID),
                Suppressed = suppress.HasValue,
                Responses = new Collection<Response>
                {
                    new Response()
                    {
                        sUserID = user.GetUserID(),
                        iResponseTypeID = 1,
                        dtCreateDateTime = DateTime.UtcNow,
                        sMessage = body,
                        bIncoming = true
                    }
                },
                TicketLogs = new Collection<TicketLog>
                {
                    new TicketLog()
                    {
                        sUserID = user.GetUserID(),
                        sActivity = string.Format("{0} heeft deze melding aangemaakt", user.Name),
                        dtTimestamp = DateTime.UtcNow
                    }
                }
            };

            if (!string.IsNullOrWhiteSpace(solution))
            {
                ticket.dtFinishedDateTime = DateTime.UtcNow;
                ticket.bFinished = true;
                ticket.Responses.Add(new Response()
                {
                    sUserID = user.GetUserID(),
                    iResponseTypeID = 4,
                    dtCreateDateTime = DateTime.UtcNow,
                    sMessage = solution,
                    bIncoming = false,
                    sFromEmail = user.Name,
                    sFromName = user.Name
                });
                ticket.TicketLogs.Add(new TicketLog()
                {
                    sUserID = user.GetUserID(),
                    sActivity = string.Format("{0} heeft deze melding gesloten", user.Name),
                    dtTimestamp = DateTime.UtcNow
                });
            }

            Insert(ticket);
        }
    }
    
    }
*/
}

    