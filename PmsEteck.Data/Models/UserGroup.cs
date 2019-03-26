using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("AspNetUserGroups")]
    public class UserGroup
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Usergroup")]
        [MaxLength(100, ErrorMessage = "Dit veld mag maximaal {1} tekens bevatten.")]
        public string Name { get; set; }

        [Display(Name = "Users")]
        //public ICollection<ApplicationUser> Users { get; set; }
        public ICollection<ApplicationUserGroup> Users { get; set; }

        [Display(Name = "Projects")]
        //public ICollection<ProjectInfo> Projects { get; set; }
        public ICollection<ProjectInfoUserGroup> Projects { get; set; }
    }
}