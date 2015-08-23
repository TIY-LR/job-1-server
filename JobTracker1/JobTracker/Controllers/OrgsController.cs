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
    public class OrgsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Orgs
        public object GetOrgs()
        {
            var displaylist = from o in db.Orgs
                              select new
                              {
                                  id = o.Id,
                                  name = o.Name,
                                  website = o.Website,
                                  phone = o.Phone,
                                  addressOne = o.Address1,
                                  addressTwo = o.Address2,
                                  city = o.City,
                                  state = o.State,
                                  zip = o.Zip,
                                  email = o.Email,
                                  contacts = o.Contacts,
                                  positions = o.Positions
                              };
            return new { organization = displaylist };
        }

        // GET: api/Orgs/5
        [ResponseType(typeof(Org))]
        public object GetOrg(int id)
        {
            var display = from o in db.Orgs
                          where o.Id == id
                          select new
                          {
                              id = o.Id,
                              name = o.Name,
                              website = o.Website,
                              phone = o.Phone,
                              addressOne = o.Address1,
                              addressTwo = o.Address2,
                              city = o.City,
                              state = o.State,
                              zip = o.Zip,
                              email = o.Email,
                              contacts = o.Contacts,
                              positions = o.Positions
                          };

            if (display == null)
            {
                return NotFound();
            }
            return Ok(new { organization = display });
        }

        // PUT: api/Orgs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOrg(int id, RootObject rootobject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != rootobject.Org.Id)
            {
                return BadRequest();
            }

            db.Entry(rootobject.Org).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrgExists(id))
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

        // POST: api/Orgs
        [ResponseType(typeof(RootObject))]
        public IHttpActionResult PostOrg(RootObject rootorg)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Orgs.Add(rootorg.Org);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = rootorg.Org.Id }, rootorg.Org);
        }

        // DELETE: api/Orgs/5
        [ResponseType(typeof(Org))]
        public IHttpActionResult DeleteOrg(int id)
        {
            Org org = db.Orgs.Find(id);
            if (org == null)
            {
                return NotFound();
            }

            db.Orgs.Remove(org);
            db.SaveChanges();
            RootObject rootobject = new RootObject();
            rootobject.Org = org;

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

        private bool OrgExists(int id)
        {
            return db.Orgs.Count(e => e.Id == id) > 0;
        }
    }
}