using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("DocumentCategories")]
    public class DocumentCategory
    {
        [Key]
        public int iDocumentCategoryKey { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Categorienaam")]
        [StringLength(50, ErrorMessage = "{0} mag maximaal {1} tekens bevatten.")]
        public string sName { get; set; }

        [Display(Name = "Toelichting")]
        [StringLength(250, ErrorMessage = "{0} mag maximaal {1} tekens bevatten.")]
        public string sExplanation { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Sortering")]
        public int iSorting { get; set; }
        
        [Display(Name = "Documenten")]
        public ICollection<CustomerDocument> CustomerDocuments { get; set; }

    }
}