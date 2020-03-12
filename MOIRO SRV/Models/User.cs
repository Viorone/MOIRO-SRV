using System;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MOIRO_SRV.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string FullName { get; set; }
        public string OrganizationalUnit { get; set; }
        public int Room { get; set; }
        public DateTime LastLogin { get; set; }
        public bool Admin { get; set; }

        public List<Order> Orders { get; set; }
        public List<Event> Events { get; set; }
        public List<PublicChat> PublicChats{ get; set; }

    }
}