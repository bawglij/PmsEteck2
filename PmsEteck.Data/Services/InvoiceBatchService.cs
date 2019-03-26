using Microsoft.EntityFrameworkCore;
using PmsEteck.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace PmsEteck.Data.Services
{
    public class InvoiceBatchService : BaseService<InvoiceBatch>
    {
        public IQueryable<InvoiceBatch> GetInvoiceBatchesWithInvoices()
        {
            builder.AsNoTracking();
            builder.Include(i => i.Invoices);
            return builder;
        }

        public void CreateInvoiceBatchesForProjects(List<InvoiceBatch> invoiceBatches)
        {
            foreach (InvoiceBatch invoiceBatch in invoiceBatches)
            {
                CreateInvoiceBatchForProject(invoiceBatch);
            }
        }

        public void CreateInvoiceBatchForProject(InvoiceBatch invoiceBatch)
        {
            if(!dbSet.Any(u => u.iInvoiceTypeID == invoiceBatch.iInvoiceTypeID && u.InvoicePeriodID == invoiceBatch.InvoicePeriodID && u.iPeriod == invoiceBatch.iPeriod && u.iYear == invoiceBatch.iYear && u.iProjectID == invoiceBatch.iProjectID))
            {
                Add(invoiceBatch);
            }
        }
    }
}
