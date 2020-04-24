using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MOIRO_SRV.Models
{
    public class Platform
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SessionLimit { get; set; } //максимальное количество одновременных трансляций
        public int ListenersCount { get; set; } // максимальное количество слушателей
    }
}