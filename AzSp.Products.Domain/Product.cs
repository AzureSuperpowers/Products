using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AzSp.Products.Domain
{
    public class Product
    {
        public int ProductId { get; set; }
        [Column("CategoryID")]
        public int? CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public bool Discontinued { get; set; }

        [Required]
        [MaxLength(40)]
        public string ProductName { get; set; }

        [MaxLength(20)]
        public string QuantityPerUnit { get; set; }
        public short? ReorderLevel { get; set; }
        public decimal? UnitPrice { get; set; }
        public short? UnitsInStock { get; set; }
        public short? UnitsOnOrder { get; set; }
    }
}
