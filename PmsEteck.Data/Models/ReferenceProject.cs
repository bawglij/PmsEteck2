using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("ReferenceProjects")]
    public class ReferenceProject
    {
        [Key]
        public int iReferenceProjectKey { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [MaxLength(100, ErrorMessage = "Dit veld mag maximaal {1} tekens bevatten.")]
        [Display(Name = "Naam")]
        public string sReferenProjectName { get; set; }

        [Display(Name = "Regels")]
        public List<ReferenceProjectRow> ReferenceProjectRows { get; set; }
    }
}