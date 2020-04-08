using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MOIRO_SRV.Models
{
    public class AppDbInitializer : CreateDatabaseIfNotExists<ServerContext>
    {
        protected override void Seed(ServerContext context)
        {
            ServerContext db = new ServerContext();
            db.ServerUsers.Add(new ServerUser { Email = "root", Password = "235Cryptocertus" });
            db.Statuses.Add(new Status { Name = "В обработке" });
            db.Statuses.Add(new Status { Name = "Выполняется" });
            db.Statuses.Add(new Status { Name = "Выполнено" });
            db.Statuses.Add(new Status { Name = "Требуется ремонт/закупка" });
            db.Statuses.Add(new Status { Name = "Отменено" });
            db.SaveChanges();

            base.Seed(context);
        }
    }
}