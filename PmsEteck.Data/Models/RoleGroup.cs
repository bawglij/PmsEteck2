
using PmsEteck.Data.Models.Results;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("AspNetRoleGroups")]
    public class RoleGroup
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Rolegroup")]
        [MaxLength(100, ErrorMessage = "Dit veld mag maximaal {1} tekens bevatten.")]
        public string Name { get; set; }

        [Display(Name = "Users")]
        //public List<ApplicationUser> Users { get; set; }
        public virtual List<ApplicationUserRoleGroup> Users { get; set; }

        [Display(Name = "Roles")]
        public virtual ICollection<ApplicationRoleGroup> Roles { get; set; }
        //public virtual List<ApplicationRole> Roles { get; set; }
    }
}