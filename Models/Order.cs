using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore.Models
{
    public class Order
    {
        public int Id { get; set; }
        [Required]
        public string PaymentMethod { get; set; }
        [Required]
        public string Owner { get; set; }
        [Required]
        public string Number { get; set; }
        [Required]
        [Column(TypeName = "decimal(6,2)")]
        public decimal ShippingCost { get; set; }
        [Required]
        public string OrdersStatus { get; set; }
        [Required]
        public string ShippingMethod { get; set; }
        public string Comments { get; set; }

        public DateTime ModifiedAt { get; set; }
        public DateTime PurchasedAt { get; set; }
        // Need invistigation
        public DateTime orders_date_finished { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}