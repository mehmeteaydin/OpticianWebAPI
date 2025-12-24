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
        public string Name {get; set;} = string.Empty;

        [Required]
        [Column("role_type")]
        public RoleType Role {get; set;}


    }

    public enum RoleType
    {
        Admin = 1,

        Employee = 2,

        Customer = 3
    }
}