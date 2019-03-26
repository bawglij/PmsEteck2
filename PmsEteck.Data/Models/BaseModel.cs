using System;
using System.ComponentModel.DataAnnotations;

namespace PmsEteck.Data.Models
{
    public class BaseModel
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateModified { get; set; }
    }
}