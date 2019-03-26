using PmsEteck.Data.Models;
using PmsEteck.Data.Repositories;
using System;

namespace PmsEteck.Data.Services
{
    public class UnitOfWork : IDisposable
    {
        private PmsEteckContext context = new PmsEteckContext();
        private Repository<Debtor> debtorRepository;

        public Repository<Debtor> DebtorRepository
        {
            get
            {
                if(debtorRepository == null)
                {
                    debtorRepository = new Repository<Debtor>(context);
                }
                return debtorRepository;
            }
        }



        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
