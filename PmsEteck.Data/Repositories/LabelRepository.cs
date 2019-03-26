using Microsoft.EntityFrameworkCore;
using PmsEteck.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace PmsEteck.Data.Repositories
{
    public interface ILabelRepository
    {
        IQueryable<Label> Update(string[] labels);
    }

    public class LabelRepository : Repository<Label>, ILabelRepository
    {
        public LabelRepository(DbContext dataContext) : base(dataContext)
        {
            DbSet = dataContext.Set<Label>();
        }

        public IQueryable<Label> Update(string[] labels)
        {
            IQueryable<Label> selectedLabels = DbSet.Where(w => labels.Contains(w.sDescription));
            IEnumerable<string> newLabels = labels.Except(selectedLabels.Select(s => s.sDescription));
            foreach (string label in newLabels)
                Insert(new Label { sDescription = label.ToLower() });
            return selectedLabels;
        }
    }
}