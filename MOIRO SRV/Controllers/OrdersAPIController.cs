using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using MOIRO_SRV.Models;

namespace MOIRO_SRV.Controllers
{
    public class OrdersAPIController : ApiController
    {
        private ServerContext db = new ServerContext();

        // GET: api/Orders
        public IQueryable<Order> GetOrders()
        {
            return db.Orders;
        }

        // GET: api/Orders/5
        [ResponseType(typeof(Order))]
        public async Task<IHttpActionResult> GetOrder(int id)
        {
            Order order = await db.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        public IEnumerable<object> GetOrders(int statusId, string dateStart, string dateEnd)
        {
            DateTime tmpDateStart = Convert.ToDateTime(dateStart);
            DateTime tmpDateEnd = Convert.ToDateTime(dateEnd);         
            IEnumerable<Order> orders = db.Orders;
            IEnumerable<User> users = db.Users;
            IEnumerable<Status> statuses = db.Statuses;

            var ord = from first in orders.Where(a => a.StatusId == statusId && a.Date.Date >= tmpDateStart && a.Date.Date <= tmpDateEnd)
                      join second in users on first.UserId equals second.Id
                      join third in statuses on first.StatusId equals third.Id
                      join fourth in users on first.AdminId equals fourth.Id into temp
                      from fourth in temp.DefaultIfEmpty()
                      select new
                      {
                          first.Id,
                          first.AdminComment,
                          first.AdminId,
                          first.CompletionDate,
                          first.Date,
                          first.Description,
                          first.Problem,
                          first.StatusId,
                          first.UserId,
                          UserName = second.FullName,
                          second.Room,
                          StatusName = third.Name,
                          UserLogin = second.Login,
                          AdminName = first.AdminId == null ? null : fourth.FullName
                      };

            return ord;
        }

        public int GetOrdersCount(int statusId)
        {
            IQueryable<Order> orders = db.Orders;

            var status = orders.Where(a => a.StatusId == statusId).Count();

            return status;
        }

        public int GetOrdersCount(int statusId, int userId)
        {
            IQueryable<Order> orders = db.Orders;

            var status = orders.Where(a => a.StatusId == statusId && a.UserId == userId).Count();

            return status;
        }

        public IEnumerable<object> GetOrders(int userId, string date)
        {
            DateTime date1 = Convert.ToDateTime(date);
            IEnumerable<Order> orders = db.Orders;
            IEnumerable<User> users = db.Users;
            IEnumerable<Status> statuses = db.Statuses;

            var ord = from first in orders.Where(user => user.UserId == userId && user.Date.Date == date1.Date)
                      join last in users on first.AdminId equals last.Id into temp
                      from z in temp.DefaultIfEmpty()
                      join last in statuses on first.StatusId equals last.Id
                      select new
                      {
                          first.Id,
                          first.AdminComment,
                          first.AdminId,
                          first.CompletionDate,
                          first.Date,
                          first.Description,
                          first.Problem,
                          first.StatusId,
                          first.UserId,
                          StatusName = last.Name,
                          AdminName = first.AdminId == null ? null : z.FullName
                      };

            return ord;
        }

        public IEnumerable<object> GetOrders(int userId, int statusId)
        {            
            IEnumerable<Order> orders = db.Orders;
            IEnumerable<User> users = db.Users;
            IEnumerable<Status> statuses = db.Statuses;

            var ord = from first in orders.Where(user => user.UserId == userId && user.StatusId == statusId)
                      join last in users on first.AdminId equals last.Id into temp
                      from z in temp.DefaultIfEmpty()
                      join last in statuses on first.StatusId equals last.Id
                      select new
                      {
                          first.Id,
                          first.AdminComment,
                          first.AdminId,
                          first.CompletionDate,
                          first.Date,
                          first.Description,
                          first.Problem,
                          first.StatusId,
                          first.UserId,
                          StatusName = last.Name,
                          AdminName = first.AdminId == null ? null : z.FullName
                      };

            return ord;
        }

        public IEnumerable<object> GetOrders(string date)
        {
            DateTime date1 = Convert.ToDateTime(date);
            IEnumerable<Order> orders = db.Orders;
            IEnumerable<User> users = db.Users;
            IEnumerable<Status> statuses = db.Statuses;

            var ord = from first in orders.Where(user => user.Date.Date == date1.Date)
                      join second in users on first.UserId equals second.Id 
                      join third in statuses on first.StatusId equals third.Id
                      join fourth in users on first.AdminId equals fourth.Id into temp
                      from fourth in temp.DefaultIfEmpty()
                      select new
                      {
                          first.Id,
                          first.AdminComment,
                          first.AdminId,
                          first.CompletionDate,
                          first.Date,
                          first.Description,
                          first.Problem,
                          first.StatusId,
                          first.UserId,
                          UserName = second.FullName,
                          second.Room,
                          StatusName = third.Name,
                          UserLogin = second.Login,
                          AdminName = first.AdminId == null ? null : fourth.FullName
                      };
     
            return ord;
        }

        // PUT: api/Orders/5 
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutOrder(int id, Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != order.Id)
            {
                return BadRequest();
            }

            db.Entry(order).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Orders
        [ResponseType(typeof(Order))]
        public async Task<IHttpActionResult> PostOrder(Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Orders.Add(order);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = order.Id }, order);
        }

        // DELETE: api/Orders/5
        [ResponseType(typeof(Order))]
        public async Task<IHttpActionResult> DeleteOrder(int id)
        {
            Order order = await db.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            db.Orders.Remove(order);
            await db.SaveChangesAsync();

            return Ok(order);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrderExists(int id)
        {
            return db.Orders.Count(e => e.Id == id) > 0;
        }
    }
}