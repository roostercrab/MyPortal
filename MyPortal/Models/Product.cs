using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace MyPortal.Models
{
    [Table("Finance_Products")]
    public class Product
    {
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            BasketItems = new HashSet<BasketItem>();
            Sales = new HashSet<Sale>();
        }

        [Display(Name = "ID")] public int Id { get; set; }

        [Required] [StringLength(255)] public string Description { get; set; }

        public decimal Price { get; set; }

        [Display(Name = "Available on Store")] public bool Visible { get; set; }

        [Display(Name = "One-Time Purchase")] public bool OnceOnly { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BasketItem> BasketItems { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sale> Sales { get; set; }
    }
}