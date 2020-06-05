using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MOIRO_SRV.Models
{
    public class Status
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Order> Orders { get; set; }
    }
}