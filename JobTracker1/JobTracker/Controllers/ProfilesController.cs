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

namespace JobTracker.Controllers
{
    public class ProfilesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Profiles
        public object GetProfiles()
        {
            var displaylist = from p in db.Profiles
                              select new
                              {
                                  id = p.Id,
                                  firstName = p.FirstName,
                                  lastName = p.LastName,
                                  phone = p.Phone,
                                  email = p.Email,
                                  address = p.Address,
                                  org = p.Org.Name
                              };
            return new { profile = displaylist };
        }

        // GET: api/Profiles/5
        [ResponseType(typeof(object))]
        public IHttpActionResult GetProfile(int id)
        {
            var display = from p in db.Profiles
                          where p.Id == id
                          select new
                          {
                              id = p.Id,
                              firstName = p.FirstName,
                              lastName = p.LastName,
                              phone = p.Phone,
                              email = p.Email,
                              address = p.Address,
                              org = p.Org.Name
                          };

            if (display == null)
            {
                return NotFound();
            }
            return Ok(new { contact = display });
        }

        // PUT: api/Profiles/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProfile(int id, RootObject rootobject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != rootobject.Profile.Id)
            {
                return BadRequest();
            }

            db.Entry(rootobject.Profile).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfileExists(id))
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

        // POST: api/Profiles
        [ResponseType(typeof(RootObject))]
        public IHttpActionResult PostProfile(RootObject rootobject)
        {
            if (!ModelState.IsValid || rootobject.Profile == null)
            {
                return BadRequest(ModelState);
            }

            db.Profiles.Add(rootobject.Profile);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = rootobject.Profile.Id }, rootobject.Profile);
        }

        // DELETE: api/Profiles/5
        [ResponseType(typeof(RootObject))]
        public IHttpActionResult DeleteProfile(int id)
        {
            Profile profile = db.Profiles.Find(id);
            if (profile == null)
            {
                return NotFound();
            }

            db.Profiles.Remove(profile);
            db.SaveChanges();
            RootObject rootobject = new RootObject();
            rootobject.Profile = profile;

            return Ok(rootobject);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProfileExists(int id)
        {
            return db.Profiles.Count(e => e.Id == id) > 0;
        }
    }
}