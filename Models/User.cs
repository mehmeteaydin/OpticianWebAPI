using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OpticianWebAPI.Models
{
    [Table("users")]
    public class User
    {
        [Key]
        [Column("id")]
        public Guid Id {get; set;}

        [Required]
        [Column("name")]
        public string Username {get; set;} = string.Empty;

        [Required]
        [Column("role_type")]
        public RoleType Role {get; set;}

        [Required]
        [Column("password")]
        public string Password {get; set;} = string.Empty;

    }

    public enum RoleType
    {
        Admin,
        Employee,
        Customer
    }
}