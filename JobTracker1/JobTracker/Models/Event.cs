using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobTracker.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public DateTime Initiated { get; set; }
        public DateTime FollowUp { get; set; }
        public bool IsOpen { get; set; }
        public string Notes { get; set; }
        public List<Contact> Contacts { get; set; }
        public Org Organization { get; set; }
        public Position Position { get; set; }
        public Resume Resume { get; set; }
    }
}