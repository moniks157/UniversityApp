using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityApp.Pagination
{
    public class PagedModel<T>
    {
        const int maxPageSize = 50;

        public int PageSize { get; set; }
        public int PageNo { get; set; }
        public int TotalRecordCount { get; set; }
        public int PageCount { get; set; }
        public List<T> Data { get; set; } = new List<T>();

        public PagedModel(List<T> data, int pageNo, int pageSize, int totalRecordCount)
        {
            Data = data;
            PageNo = pageNo;
            PageSize = (pageSize > maxPageSize) ? maxPageSize : pageSize;
            TotalRecordCount = totalRecordCount;
            PageCount = totalRecordCount > 0 ? (int)Math.Ceiling((double)totalRecordCount / (double)pageSize) : 0;
        }
    }
}
