using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static OnlineStore.Helpers.Enums;

namespace OnlineStore.Models
{
    public class Item_details
    {
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "decimal(6,2)")]
        public decimal Price { get; set; }
        [Required]
        public int Color { get; set; }
        [Required]
        public Size Size { get; set; }
        [Required]
        public string Material { get; set; }
        [Required]
        public int Height { get; set; }
        [Required]
        public int Width { get; set; }
        [Required]
        public double Weight { get; set; }
        [Required]
        public int Quantity { get; set; }
        public int Viewed { get; set; }
        [Required]
        public bool Status { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        public Item Item { get; set; }
        public int ItemId { get; set; }
    }
}