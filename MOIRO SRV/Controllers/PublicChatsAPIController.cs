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
    public class PublicChatsAPIController : ApiController
    {
        private ServerContext db = new ServerContext();

        // GET: api/PublicChats
        public IQueryable<PublicChat> GetPublicChats()
        {
            return db.PublicChats;
        }

        // GET: api/PublicChats/5
        [ResponseType(typeof(PublicChat))]
        public async Task<IHttpActionResult> GetPublicChat(int id)
        {
            PublicChat publicChat = await db.PublicChats.FindAsync(id);
            if (publicChat == null)
            {
                return NotFound();
            }

            return Ok(publicChat);
        }

        // GET orders by idUser (Personal user orders) and request for a specific user by Admin
        public IQueryable<PublicChat> GetPublicChats(int userId, int count)
        {
            IQueryable<PublicChat> publicChats = db.PublicChats;

            publicChats = publicChats.Where(user => user.UserId == userId).OrderByDescending(user => user.Date).Take(count);
            return publicChats;
        }

        // PUT: api/PublicChats/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPublicChat(int id, PublicChat publicChat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != publicChat.Id)
            {
                return BadRequest();
            }

            db.Entry(publicChat).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PublicChatExists(id))
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

        // POST: api/PublicChats
        [ResponseType(typeof(PublicChat))]
        public async Task<IHttpActionResult> PostPublicChat(PublicChat publicChat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PublicChats.Add(publicChat);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = publicChat.Id }, publicChat);
        }

        // DELETE: api/PublicChats/5
        [ResponseType(typeof(PublicChat))]
        public async Task<IHttpActionResult> DeletePublicChat(int id)
        {
            PublicChat publicChat = await db.PublicChats.FindAsync(id);
            if (publicChat == null)
            {
                return NotFound();
            }

            db.PublicChats.Remove(publicChat);
            await db.SaveChangesAsync();

            return Ok(publicChat);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PublicChatExists(int id)
        {
            return db.PublicChats.Count(e => e.Id == id) > 0;
        }
    }
}