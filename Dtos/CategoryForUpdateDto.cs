using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Dtos
{
    public class CategoryForUpdateDto
    {
        public string Name { get; set; }
        public string ParentId { get; set; }
    }
}
