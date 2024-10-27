using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class Product
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        [Required]
        [StringLength(200)]
        public string? ProductName { get; set; }
        [Required]
        [StringLength(255)]
        public string? ProductImage { get; set; }
        [StringLength(255)]
        public string? Description { get; set; }
        [StringLength(255)]
        public string? BrandName { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public double? UnitPrice {  get; set; }
        public int? NumberOfStock { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdateAt {  get; set; }
        public bool? IsDeleted { get; set; }

        [JsonIgnore]
        public virtual ICollection<CartItem>? CartItems { get; set; }
        public virtual Category? Category { get; set; }
    }
}
