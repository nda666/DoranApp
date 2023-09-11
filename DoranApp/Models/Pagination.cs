using System;
using System.Collections.Generic;

namespace DoranApp.Models
{
    public class PaginationData
    {
        public long TotalPage { get; set; } = 0;
        public long TotalRow { get; set; } = 0;
        public long Page { get; set; } = 1;
        public long PageSize { get; set; } = 20;
    }
}