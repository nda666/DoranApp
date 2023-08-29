using System;
using System.Collections.Generic;

namespace DoranApp.Models
{
    internal class Pagination
    {
        public int current_page { get; set; }
        public List<dynamic> data { get; set; }
        public String first_page_url { get; set; }
        public int from { get; set; }
        public int last_page { get; set; }
        public String last_page_url { get; set; }
        public String next_page_url { get; set; }
        public String path { get; set; }
        public int per_page { get; set; }
        public int prev_per_page { get; set; }
        public int to { get; set; }
        public int total { get; set; }
    }
}