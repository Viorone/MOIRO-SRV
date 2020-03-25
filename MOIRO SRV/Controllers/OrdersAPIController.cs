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

        // GET orders by idUser (Personal user orders) and request for a specific user by Admin
        public IQueryable<Order> GetOrders(int userId, int count)
        {
            IQueryable<Order> orders = db.Orders;

            orders = orders.Where(user => user.UserId == userId).OrderByDescending(user => user.Date).Take(count);
            return orders;
        }

        public IEnumerable<Order> GetOrders(int userId, string date)
        {
            DateTime date1 = Convert.ToDateTime(date);
            IEnumerable<Order> orders = db.Orders;

            orders = orders.Where(user => user.UserId == userId && user.Date.Date == date1.Date);
            return orders;
        }

        public IEnumerable<object> GetOrders(string date)
        {
            DateTime date1 = Convert.ToDateTime(date);
            IEnumerable<Order> orders = db.Orders;
            IEnumerable<User> users = db.Users;

            var ord = orders.Where(user => user.Date.Date == date1.Date).Join(users, p => p.UserId, t => t.Id, (p, t) => new { p.Description, p.Problem, p.Id, p.UserId, p.Status, p.Date, UserName = t.FullName, t.Room});
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