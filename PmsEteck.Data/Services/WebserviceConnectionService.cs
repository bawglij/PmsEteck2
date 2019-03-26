using PmsEteck.Data.Models;
using System.Linq;

namespace PmsEteck.Data.Services
{
    public class WebserviceConnectionService : BaseService<WebserviceConnection>
    {
        public WebserviceConnection FindByMaintenanceContactId(int maintenanceContactId)
        {
            return dbSet.FirstOrDefault(f => f.MaintenanceContactID == maintenanceContactId);
        }

    }
}
