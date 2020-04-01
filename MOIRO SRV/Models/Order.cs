﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MOIRO_SRV.Models
{
    public class Order
    {
        public int Id { get; set; }
        [Required]
        public string Problem { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }

        public int StatusId { get; set; }
        public Status Status { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

    }
}