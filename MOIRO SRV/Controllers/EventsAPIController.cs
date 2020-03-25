﻿using System;
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
    public class EventsAPIController : ApiController
    {
        private ServerContext db = new ServerContext();

        // GET: api/Events
        public IQueryable<Event> GetEvents()
        {
            return db.Events;
        }

        // GET events by idUser (Personal user evens) and request for a specific user by Admin
        public IQueryable<Event> GetEvents(int userId, int count)
        {
            IQueryable<Event> events = db.Events;

            events = events.Where(user => user.UserId == userId).OrderByDescending(user => user.Date).Take(count);
            return events;
        }

        // GET: api/Events/5
        [ResponseType(typeof(Event))]
        public async Task<IHttpActionResult> GetEvent(int id)
        {
            Event @event = await db.Events.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }

            return Ok(@event);
        }

        public IEnumerable<Event> GetEvents(int userId, string date)
        {
            DateTime date1 = Convert.ToDateTime(date);
            IEnumerable<Event> events = db.Events;

            events = events.Where(user => user.UserId == userId && user.Date.Date == date1.Date);
            return events;
        }

        public IEnumerable<object> GetEvents(string date)
        {
            DateTime date1 = Convert.ToDateTime(date);
            IEnumerable<Event> events = db.Events;
            IEnumerable<User> users = db.Users;

            var eve = events.Where(user => user.Date.Date == date1.Date).Join(users, p => p.UserId, t => t.Id, (p, t) => new { p.Description, p.DateStart, p.DateEnd, p.NameEvent, p.Place, p.Status, p.Date, UserName = t.FullName, t.Room });
            return eve;
        }

        // PUT: api/Events/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutEvent(int id, Event @event)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != @event.Id)
            {
                return BadRequest();
            }

            db.Entry(@event).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(id))
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

        // POST: api/Events
        [ResponseType(typeof(Event))]
        public async Task<IHttpActionResult> PostEvent(Event @event)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Events.Add(@event);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = @event.Id }, @event);
        }

        // DELETE: api/Events/5
        [ResponseType(typeof(Event))]
        public async Task<IHttpActionResult> DeleteEvent(int id)
        {
            Event @event = await db.Events.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }

            db.Events.Remove(@event);
            await db.SaveChangesAsync();

            return Ok(@event);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EventExists(int id)
        {
            return db.Events.Count(e => e.Id == id) > 0;
        }
    }
}