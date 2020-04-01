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
    public class StatusAPIController : ApiController
    {
        private ServerContext db = new ServerContext();

        // GET: api/StatusAPI
        public IQueryable<Status> GetStatuses()
        {
            return db.Statuses;
        }

        // GET: api/StatusAPI/5
        [ResponseType(typeof(Status))]
        public async Task<IHttpActionResult> GetStatus(int id)
        {
            Status status = await db.Statuses.FindAsync(id);
            if (status == null)
            {
                return NotFound();
            }

            return Ok(status);
        }

        // PUT: api/StatusAPI/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutStatus(int id, Status status)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != status.Id)
            {
                return BadRequest();
            }

            db.Entry(status).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StatusExists(id))
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

        // POST: api/StatusAPI
        [ResponseType(typeof(Status))]
        public async Task<IHttpActionResult> PostStatus(Status status)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Statuses.Add(status);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = status.Id }, status);
        }

        // DELETE: api/StatusAPI/5
        [ResponseType(typeof(Status))]
        public async Task<IHttpActionResult> DeleteStatus(int id)
        {
            Status status = await db.Statuses.FindAsync(id);
            if (status == null)
            {
                return NotFound();
            }

            db.Statuses.Remove(status);
            await db.SaveChangesAsync();

            return Ok(status);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StatusExists(int id)
        {
            return db.Statuses.Count(e => e.Id == id) > 0;
        }
    }
}