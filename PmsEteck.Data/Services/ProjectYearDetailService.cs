using PmsEteck.Data.Models;
using System.Linq;

namespace PmsEteck.Data.Services
{
    public class ProjectYearDetailService : BaseService<ProjectYearDetail>
    {

        public bool Exists(int projectId, int year)
        {
            return dbSet.Any(u => u.ProjectId == projectId && u.Year == year);
        }
    }
}
