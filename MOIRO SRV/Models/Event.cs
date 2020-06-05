using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MOIRO_SRV.Models
{
    public class Event
    {
        public int Id { get; set; }
        [Required]
        public string NameEvent { get; set; }
        public string Description { get; set; }
        public string Place { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public DateTime Date { get; set; }
        public bool IsCanceled { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

    }
}