
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmazonLite
{

    public class Todo
    {
        public int id { get; set; }
        public string content { get; set; }
        public string due_at { get; set; }
        public int comments_count { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public string url { get; set; }
        public string app_url { get; set; }
        public int position { get; set; }
    }

}