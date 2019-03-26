using Microsoft.EntityFrameworkCore;
using PmsEteck.Data.Models;
using PmsEteck.Data.Models.Results;
using System;
using System.Threading.Tasks;

namespace PmsEteck.Data.Services
{
    public class ServiceTicketService
    {
        private readonly PmsEteckContext context;
        private string UserId;
        private readonly WorkOrderService workOrderService;

        public ServiceTicketService(string userId = null)
        {
            context = new PmsEteckContext();
            workOrderService = new WorkOrderService(context);
            UserId = userId;
        }

        public ServiceTicketService(PmsEteckContext context, string userId)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            UserId = userId ?? throw new ArgumentNullException(nameof(userId));
            workOrderService = new WorkOrderService(context);
        }

        public async Task<Result> CreateServiceTicket(ServiceTicket serviceTicket)
        {
            if (serviceTicket.FinishedDateTime.HasValue)
            {
                // serviceTicket is directly closed
                serviceTicket.Status = await context.ServiceTicketStatuses.FirstOrDefaultAsync(f => f.StatusCode == 400);
                context.ServiceTickets.Add(serviceTicket);
                if (!string.IsNullOrWhiteSpace(UserId))
                    await context.SaveChangesAsync(UserId);
                else
                    await context.SaveChangesAsync();
            }
            else
            {
                // serviceTicket is not finished
                WorkOrder workOrder = new WorkOrder
                {
                    CreatedDateTime = DateTime.UtcNow,
                    Instruction = serviceTicket.Description,
                    Status = await context.WorkOrderStatuses.FirstOrDefaultAsync(f => f.StatusCode == 100),
                    WorkOrderCode = await context.WorkOrders.FirstOrDefaultAsync() != null ? await context.WorkOrders.MaxAsync(m => m.WorkOrderCode) + 1 : 1,
                    WorkOrderID = Guid.NewGuid(),
                    ServiceTicket = serviceTicket
                };
                serviceTicket.WorkOrders.Add(workOrder);

                context.ServiceTickets.Add(serviceTicket);
                if (!string.IsNullOrWhiteSpace(UserId))
                    await context.SaveChangesAsync(UserId);
                else
                    await context.SaveChangesAsync();

                await workOrderService.ShareWithMaintenanceContact(workOrder.WorkOrderID);

                if (!string.IsNullOrWhiteSpace(UserId))
                    await context.SaveChangesAsync(UserId);
                else
                    await context.SaveChangesAsync();
            }
            return Result.Success;
        }
    }
}
