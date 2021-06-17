using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Provider
    {
        public string name { get; set; }
        public float score { get; set; }
        public string[] specialties { get; set; }
        public class Availability
        {
            public long from { get; set; }
            public long to { get; set; }
            //public DateTime dateFrom { get => DateTimeOffset.FromUnixTimeMilliseconds(from).DateTime; }
            //public DateTime dateTo { get => DateTimeOffset.FromUnixTimeMilliseconds(to).DateTime; }
        }

        public IEnumerable<Availability> availableDates { get; set; }
    }
}