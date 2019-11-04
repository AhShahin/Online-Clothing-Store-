using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Helpers.QueryParams
{
    public class ItemParams : IQueryObject
    {
        private const byte MaxPageSize = 50;
        private byte pageSize = 10;
        public int PageNumber { get; set; } = 1;
        public byte PageSize
        {
            get { return pageSize; }
            set { pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }
        public string OrderBy { get; set; }
        public string SearchTerm { get; set; }
        public bool IsSortAscending { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public int Viewed { get; set; } = 0;
        public bool? Status { get; set; }
        public string Size { get; set; }
    }
}
