using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Dtos
{
    public class CategoryForDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ParentId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        public ICollection<ItemForListDto> Items { get; set; }
    }
}
