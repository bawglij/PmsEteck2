using PmsEteck.Data.Models.Results;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace PmsEteck.Data.Models
{
    [Table(name: "ServiceRuns", Schema = "meter")]
    public class ServiceRun
    {
        #region Constructor
        private PmsEteckContext db = new PmsEteckContext();
        #endregion

        #region Properties

        [Key]
        public int iServiceRunKey { get; set; }

        public int iServiceKey { get; set; }

        public DateTime dtServiceRunStartDate { get; set; }

        public DateTime? dtServiceRunEndDate { get; set; }

        public int iServiceRunStatus { get; set; }

        public int iServiceRunRowsUpdated { get; set; }

        [MaxLength(1000)]
        public string sServiceRunMessage { get; set; }

        [MaxLength(250)]
        public string sFileName { get; set; }

        [Display(Name = "In wachtrij")]
        public bool bInQueue { get; set; }

        [Display(Name = "# keer geprobeerd")]
        public int? iNumberOfReprocessed { get; set; }

        public virtual Service Service { get; set; }

        public List<ServiceRunError> ServiceRunErrors { get; set; }
        #endregion

        #region Methods
        public async Task<Result> CreateAsync()
        {
            switch (iServiceKey)
            {
                case 1:
                    //SmartDataSolutions

                    break;
                case 2:
                    //Fudura
                    break;
                case 7:
                // Fudura Blindconsumption
                case 8:
                    // Fudura Max Power
                default:
                    break;
            }
            return await Task.FromResult(Result.Success);
        }
        #endregion
    }
}