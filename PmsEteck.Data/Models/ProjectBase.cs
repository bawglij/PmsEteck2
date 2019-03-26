//using PmsEteck.Data.WSProject;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;

namespace PmsEteck.Data.Models
{
    [Table("DimProject", Schema="dbo")]
    public class ProjectBase
    {
        #region Constructor
        private readonly PmsEteckContext context = new PmsEteckContext();
        //WSProject_Service _service;
        #endregion

        #region Static Fields
        private string WebserviceUser = ConfigurationManager.AppSettings["WebserviceUser"];
        private string WebserviceDomain = ConfigurationManager.AppSettings["WebserviceDomain"];
        private string WebservicePassword = ConfigurationManager.AppSettings["WebservicePassword"];
        private string ServiceUrl = ConfigurationManager.AppSettings["ServiceUrl"];
        private string EETBV = ConfigurationManager.AppSettings["EETBV"];
        #endregion

        #region Properties
        [Key]
        public int iProjectKey { get; set; }

        [Display(Name ="Entiteit")]
        public int iCustomerID { get; set; }

        public int iPartnerID { get; set; }

        [MaxLength(50)]
        [Display(Name ="Projectnummer EET")]
        public string sProjectCode { get; set; }

        [MaxLength(255)]
        [Display(Name ="Projectnaam")]
        public string sProjectDescription { get; set; }

        [MaxLength(50)]
        public string sProjectCategoryCode { get; set; }

        [MaxLength(255)]
        public string sProjectCategoryDescription { get; set; }

        [MaxLength(255)]
        public string dStartDate { get; set; }

        [MaxLength(50)]
        public string dEndDate { get; set; }

        public virtual ProjectInfo ProjectInfo { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Opportunity Opportunity { get; set; }

        public ICollection<Budget> Budgets { get; set; }
        
        public ICollection<BudgetDimension> BudgetDimensions { get; set; }
        #endregion
        /*
        #region Methods
        public Task<Result> UpdateNav()
        {
            try
            {

                _service = new WSProject_Service();
                _service.Credentials = new System.Net.NetworkCredential(WebserviceUser, WebservicePassword, WebserviceDomain);
                _service.Url = string.Format("{0}/WS/{1}/Page/WSProject", ServiceUrl, Uri.EscapeDataString(EETBV));

                WSProject.WSProject project = _service.Read(sProjectCode);
                if (project == null)
                {
                    project = new WSProject.WSProject {
                        Project_No = sProjectCode,
                        Project_Description = sProjectDescription
                    };
                }
                else
                {
                    if (ProjectInfo != null)
                    {
                        // Check if ProjectInfo has FinProjectKey
                        if (ProjectInfo.iFinProjectKey.HasValue)
                        {
                            // Find finProject
                            ProjectBase finProject = context.ProjectBases.FirstOrDefault(f => f.iProjectKey == ProjectInfo.iFinProjectKey);
                            project.Shortcut_Dimension_1_Code = finProject.sProjectCode;
                            project.Description_Dimension_1_Code = finProject.sProjectDescription;
                            project.Shortcut_Dimension_2_Code = finProject.Customer.sClientcode;
                            project.Description_Dimension_2_Code = finProject.Customer.sName;
                        }
                    }
                }
                _service.Create(ref project);
                return Task.FromResult(Result.Success);
            }
            catch (System.Exception ex)
            {
                return Task.FromResult(Result.Failed(ex.Message, ex.InnerException?.Message));
            }
        }
        #endregion
    */
    }
}