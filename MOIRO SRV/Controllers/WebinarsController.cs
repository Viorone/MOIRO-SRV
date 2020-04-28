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
    public class WebinarsController : ApiController
    {
        private ServerContext db = new ServerContext();

        // GET: api/Webinars
        public IQueryable<Webinar> GetWebinars()
        {
            return db.Webinars;
        }

        // GET: api/Webinars/5
        [ResponseType(typeof(Webinar))]
        public async Task<IHttpActionResult> GetWebinar(int id)
        {
            Webinar webinar = await db.Webinars.FindAsync(id);
            if (webinar == null)
            {
                return NotFound();
            }

            return Ok(webinar);
        }

        // PUT: api/Webinars/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutWebinar(int id, Webinar webinar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != webinar.Id)
            {
                return BadRequest();
            }

            db.Entry(webinar).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WebinarExists(id))
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

        public IEnumerable<Webinar> GetWebinars(int userId, string date)
        {
            DateTime date1 = Convert.ToDateTime(date);
            IEnumerable<Webinar> webinars = db.Webinars;

            webinars = webinars.Where(user => user.UserId == userId && user.DateStart.Date == date1.Date);
            return webinars;
        }

        public IEnumerable<object> GetWebinars(string date)
        {
            DateTime date1 = Convert.ToDateTime(date);
            IEnumerable<Webinar> webinars = db.Webinars;
            IEnumerable<User> users = db.Users;

            var web = webinars.Where(user => user.DateStart.Date == date1.Date).Join(users, p => p.UserId, t => t.Id, (p, t) => new { p.Description, p.DateStart, p.DateEnd, p.NameWebinar, p.Place, p.Date, UserName = t.FullName, t.Room });
            return web;
        }

        // POST: api/Webinars
        [ResponseType(typeof(Webinar))]
        public async Task<IHttpActionResult> PostWebinar(Webinar webinar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Webinars.Add(webinar);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = webinar.Id }, webinar);
        }

        // DELETE: api/Webinars/5
        [ResponseType(typeof(Webinar))]
        public async Task<IHttpActionResult> DeleteWebinar(int id)
        {
            Webinar webinar = await db.Webinars.FindAsync(id);
            if (webinar == null)
            {
                return NotFound();
            }

            db.Webinars.Remove(webinar);
            await db.SaveChangesAsync();

            return Ok(webinar);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool WebinarExists(int id)
        {
            return db.Webinars.Count(e => e.Id == id) > 0;
        }
    }
}