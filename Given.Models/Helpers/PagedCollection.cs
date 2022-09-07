using System.Collections.Generic;
using System.Linq;

namespace Given.Models.Helpers
{
    public class PagedCollection<T>
    {
        public int Page { get; set; }

        public int Count
        {
            get
            {
                return (null != this.Items) ? this.Items.Count() : 0;
            }
        }

        public int TotalPages { get; set; }
        public int TotalCount { get; set; }

        public IEnumerable<T> Items { get; set; }
    }

    public class QueryParam
    {                                 
        public string Query { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string OrderBy { get; set; }
        public SorderOrder OrderDir { get; set; }
    }
}
