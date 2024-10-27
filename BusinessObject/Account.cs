using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class Account
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AccountId { get; set; }
        [Required]
        [StringLength(100)]
        public string? Username { get; set; }
        [Required]
        [StringLength(255)]
        public string? Password { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdateAt { get; set; }
        [JsonIgnore]
        public virtual User? User { get; set; }

        public Account() { }
        public Account(string username, string password, bool isActive) {
            this.Username = username;
            this.Password = password;
            this.IsActive = isActive;
        }
    }
}
