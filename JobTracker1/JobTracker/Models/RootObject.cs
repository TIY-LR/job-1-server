using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobTracker.Models
{
    
    public class RootObject
    {
        public Contact Contact { get; set; }
        public Event Event { get; set; }
        public Org Org { get; set; }
        public Position Position { get; set; }
        public Resume Resume { get; set; }
    }
        
}