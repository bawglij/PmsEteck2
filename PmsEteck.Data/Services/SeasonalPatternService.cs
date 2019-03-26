using PmsEteck.Data.Models;
using System;
using System.Linq;

namespace PmsEteck.Data.Services
{
    public class SeasonalPatternService : BaseService<SeasonalPattern>
    {
        public override void Delete(SeasonalPattern seasonalPattern, string userID)
        {
            if(!context.CounterTypeYearCurves.Any(u => u.SeasonalPatternId == seasonalPattern.Id))
            {
                base.Delete(seasonalPattern, userID);
            }
            throw new Exception("Cannot be deleted. Is referenced by one or more project counter types");
        }
    }
}
