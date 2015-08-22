using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobTracker.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public bool IsOpen { get; set; }
        public string Notes { get; set; }
        public Contact Contact { get; set; }
        public Org Organization { get; set; }
        public Position Position { get; set; }
        public Resume Resume { get; set; }
    }
}