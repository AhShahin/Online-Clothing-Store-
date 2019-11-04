using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Models
{
    public class OrderItem
    {
        [Required]
        [Column(TypeName = "decimal(6,2)")]
        public decimal Price { get; set; }
        [Required]
        [Column(TypeName = "decimal(6,2)")]
        public decimal Tax { get; set; }
        [Required]
        [Column(TypeName = "decimal(6,2)")]
        public decimal FinalePrice { get; set; }
        [Required]
        public int Quantity { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int ItemId { get; set; }
        public Item Item { get; set; }
    }
}
