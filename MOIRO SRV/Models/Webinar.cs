using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MOIRO_SRV.Models
{
    public class Webinar
    {
        public int Id { get; set; }
        [Required]
        public string NameWebinar { get; set; }
        public string Description { get; set; }
        public string Place { get; set; }
        public DateTime DateStart { get; set; } //вермя начала 
        public DateTime DateEnd { get; set; } //время конца
        public DateTime Date { get; set; } //время подачи заявки на вебинар

        public int StatusId { get; set; } // статус : В ожидании, Отменено, Выполнено
        public Status Status { get; set; }

        public int UserId { get; set; } // пользователь который создал заявку на вебинар
        public User User { get; set; }
        [Required]
        public int PlatformId { get; set; } // платформа на которой будет проводиться вебинар
        public Platform Platform { get; set; }
    }
}