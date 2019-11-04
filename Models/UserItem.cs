using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Models
{
    public class UserItem
    {
        [Required]
        [Column(TypeName = "decimal(6,2)")]
        public decimal FinalPrice { get; set; }
        [Required]
        public int Quantity { get; set; }
        public DateTime DateAdded { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int ItemId { get; set; }
        public Item Item { get; set; }
    }
}
