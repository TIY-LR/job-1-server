using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using JobTracker.Models;
using System.Web.Http.Cors;

namespace JobTracker.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class EventsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Events
        public object GetEvents()
        {
            var display = from e in db.Events
                          select new
                          {
                              id = e.Id,
                              title = e.Title,
                              date = e.Date,
                              time = e.Time,
                              location = e.Org.Name,
                              job = e.Position.Title,
                              contact = e.Contact.FirstName + " " + e.Contact.LastName,
                          };
            return new {events = display };
        }

        // GET: api/Events/5
        [ResponseType(typeof(object))]
        public object GetEvent(int id)
        {
            var display = from e in db.Events
                          where e.Id == id
                          select new
                          {
                              id = e.Id,
                              title = e.Title,
                              date = e.Date,
                              time = e.Time,
                              location = e.Org.Name,
                              orgId = e.Org.Id,
                              job = e.Position.Title,
                              positionId = e.Position.Id,
                              contact = e.Contact.FirstName + " " + e.Contact.LastName,
                              contactId = e.Contact.Id
                          };
            if (display == null)
            {
                return NotFound();
            }

            return Ok(new {@event = display });
        }

        // PUT: api/Events/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEvent(int id, Event @event)
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
                db.SaveChanges();
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
        [ResponseType(typeof(RootObject))]
        public IHttpActionResult PostEvent(RootObject rootevent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Events.Add(rootevent.Event);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = rootevent.Event.Id }, rootevent.Event);
        }

        // DELETE: api/Events/5
        [ResponseType(typeof(Event))]
        public IHttpActionResult DeleteEvent(int id)
        {
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return NotFound();
            }

            db.Events.Remove(@event);
            db.SaveChanges();

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