using OnlineStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Dtos
{
    public class OrderItemForDetailsDto
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public decimal Tax { get; set; }
        public decimal FinalePrice { get; set; }
        public int Quantity { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int ItemId { get; set; }
        public Item Item { get; set; }
    }
}
