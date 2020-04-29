using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MOIRO_SRV.Models
{
    public class WebinarSettings
    {
        public int Id { get; set; }
        [Required]
        public int WebCamerasCount { get; set; }
        [Required]
        public int MaxWebinarsCount { get; set; }
    }
}