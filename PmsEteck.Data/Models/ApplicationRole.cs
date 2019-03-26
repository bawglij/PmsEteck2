
using Microsoft.AspNetCore.Identity;
using PmsEteck.Data.Models.Results;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [NotMapped]
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() : base() { }

        public ApplicationRole(string name)
            : base(name)
        {
        }

        //vld
        [Display(Name = "Rolegroups")]
        public virtual ICollection<ApplicationRoleGroup> RoleGroups { get; set; }
        //public virtual ICollection<RoleGroup> RoleGroups { get; set; }
    }
}