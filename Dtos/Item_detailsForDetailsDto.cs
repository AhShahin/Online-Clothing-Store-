using OnlineStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static OnlineStore.Helpers.Enums;

namespace OnlineStore.Dtos
{
    public class Item_detailsForDetailsDto
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public int Color { get; set; }
        public string Size { get; set; }
        public string Material { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public double Weight { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
