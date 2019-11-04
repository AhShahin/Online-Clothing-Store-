using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Dtos
{
    public class ItemForCreationDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Viewed { get; set; }
        public bool Status { get; set; }
    }
}
