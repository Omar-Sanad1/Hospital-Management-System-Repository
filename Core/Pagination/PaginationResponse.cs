using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Pagination
{
    public class PaginationResponse<T>
    {
        public IEnumerable<T> Data { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public PaginationResponse(IEnumerable<T> data, int pageNumber, int pageSize , int totalitems)
        {
            Data = data;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalItems = totalitems;
            TotalPages = pageSize > 0 ? ((int)Math.Ceiling((double)totalitems / pageSize)) : 0;
        }
    }
}
