using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore.Models
{
    public class Item
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Viewed { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public ICollection<Item_details> Item_Details { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public ICollection<UserItem> UserItems { get; set; }
    }
}