﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityApp.Pagination
{
    public class PagedModel<T>
    {
        public int PageSize { get; set; }
        public int PageNo { get; set; }
        public int TotalRecordCount { get; set; }
        public List<T> Data { get; set; } = new List<T>();

        public PagedModel(List<T> data, int pageNo, int pageSize, int totalRecordCount)
        {
            Data = data;
            PageNo = pageNo;
            PageSize = pageSize;
            TotalRecordCount = totalRecordCount;
        }
    }
}
