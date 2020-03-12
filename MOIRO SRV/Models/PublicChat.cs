using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MOIRO_SRV.Models
{
    public class PublicChat
    {
        public int Id { get; set; }
        [Required]
        public string Message { get; set; }
        public DateTime Date { get; set; }
        //public string Status { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

    }
}