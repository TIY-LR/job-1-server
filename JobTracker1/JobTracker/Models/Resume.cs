using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobTracker.Models
{
    public class Resume
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public byte[] File { get; set; }
    }
}