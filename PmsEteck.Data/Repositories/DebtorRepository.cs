using Microsoft.EntityFrameworkCore;
using PmsEteck.Data.Models;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace PmsEteck.Data.Repositories
{
    public interface IDebtorRepository
    {
        void DeleteDouble(Debtor entity, Debtor duplicate);
    }

    public class DebtorRepository : Repository<Debtor>, IDebtorRepository
    {
        public DebtorRepository(DbContext dataContext) : base(dataContext) { }

        public new IQueryable<Debtor> SearchFor(Expression<Func<Debtor, bool>> predicate)
        {
            return DbSet.Where(predicate).Where(w => !w.bIsBlocked);
        }

        public void DeleteDouble(Debtor entity, Debtor duplicate)
        {
            // Move all tickets, invoices etc. from duplicate to entity
            // Remove duplicate
        }
    }
}
