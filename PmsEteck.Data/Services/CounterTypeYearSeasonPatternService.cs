using Microsoft.EntityFrameworkCore;
using PmsEteck.Data.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;

namespace PmsEteck.Data.Services
{
    public class CounterTypeYearSeasonPatternService : BaseService<CounterTypeYearSeasonPattern>
    {
        public IQueryable<CounterTypeYearSeasonPattern> GetCounterTypeYearSeasonsByProjectYear(Guid projectYearDetailId)
        {
            return dbSet.Where(w => w.ProjectYearDetailId == projectYearDetailId).AsQueryable();
        }

        public CounterTypeYearSeasonPattern FindById(Guid id)
        {
            return dbSet.FirstOrDefault(f => f.Id == id);
        }

        public List<PeriodPercentage> GetPeriodValuesById(CounterTypeYearSeasonPattern counterTypeYearSeasonPattern)
        {
            List<PeriodPercentage> periodPercentageList = new List<PeriodPercentage>();
            using (PmsEteckContext _context = new PmsEteckContext())
            {
                _context.Database.SetCommandTimeout(300);
                _context.Database.OpenConnection();
                using (DbCommand _command = _context.Database.GetDbConnection().CreateCommand())
                {
                    _command.CommandTimeout = 300;
                    _command.CommandText = "SELECT *" +
                                        "FROM[pms].[vw_SeasonalPatternPercentages]" +
                                        "WHERE ProjectId = @ProjectID and PatternType = @PatternType and YEAR(SeasonalPatternDate) = @Year " +
                                        "ORDER BY SeasonalPatternDate asc";
                    _command.CommandType = CommandType.Text;
                    _command.Parameters.Add(new SqlParameter("@ProjectID", counterTypeYearSeasonPattern.ProjectYearDetail.ProjectId));
                    _command.Parameters.Add(new SqlParameter("@PatternType", (int)counterTypeYearSeasonPattern.SeasonalPattern.PatternType));
                    _command.Parameters.Add(new SqlParameter("@Year", counterTypeYearSeasonPattern.ProjectYearDetail.Year));

                    using (DbDataReader reader = _command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            periodPercentageList.Add(new PeriodPercentage
                            {
                                Id = Guid.NewGuid(),
                                PeriodNumber = string.IsNullOrEmpty(reader["SeasonalPatternDate"].ToString()) ? 0 : DateTime.Parse(reader["SeasonalPatternDate"].ToString()).Month,
                                Percentage = string.IsNullOrEmpty(reader["SeasonalPatternPercentage"].ToString()) ? 0 : decimal.Parse(reader["SeasonalPatternPercentage"].ToString()),
                            });
                        }
                    }
                }
            }

            return periodPercentageList;
        }
    }
}
