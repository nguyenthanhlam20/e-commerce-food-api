using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        [ForeignKey("Account")]
        public int AccountId { get; set; }
        [ForeignKey("Role")]
        public int RoleId { get; set; }
        [StringLength(200)]
        public string? FirstName { get; set; }
        [StringLength(200)]
        public string? LastName { get; set; }
        [StringLength(200)]
        public string? Address { get; set; }
        [Required]
        [EmailAddress]
        [StringLength(200)]
        public string? Email { get; set; }
        [Required]
        [StringLength(200)]
        public string? Phone { get; set; }
        [StringLength(200)]
        public bool Gender { get; set; }
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        public virtual Account? Account { get; set; }
        public virtual Role? Role { get; set; }

        public User() { }
        public User(string email, string phone) {
            this.Email = email;
            this.Phone = phone;
        }
    }
}
