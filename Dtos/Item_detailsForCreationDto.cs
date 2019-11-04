using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static OnlineStore.Helpers.Enums;

namespace OnlineStore.Dtos
{
    public class Item_detailsForCreationDto
    {
        public decimal Price { get; set; }
        public int Color { get; set; }
        public Size Size { get; set; }
        public string Material { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public double Weight { get; set; }
        public int Quantity { get; set; }
    }
}
