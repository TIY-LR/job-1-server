using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobTracker.Models
{
    public class Position
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Org Org { get; set; }
        public int? Org_Id { get; set; }
        public string Education { get; set; }
        public string Experience { get; set; }
        public decimal SalaryMin { get; set; }
        public decimal SalaryMax { get; set; }
        public string Description { get; set; }
        public Contact Contact { get; set; }
        public int? Contact_Id { get; set; }
    }
}