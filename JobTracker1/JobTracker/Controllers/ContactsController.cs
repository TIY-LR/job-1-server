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
    public class ContactsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Contacts
        public object GetContacts()
        {
            var displaylist = from c in db.Contacts
                          select new
                          {
                              id = c.Id,
                              firstName = c.FirstName,
                              lastName = c.LastName,
                              organization = c.Org.Name,
                              email = c.Email,
                              officeNumber = c.OfficeNumber,
                              cellNumber = c.CellNumber,
                              addressOne = c.Address1,
                              addressTwo = c.Address2,
                              city = c.City,
                              state = c.State,
                              zip = c.Zip

                          };
            return new { contact = displaylist };
        }

        // GET: api/Contacts/5
        [ResponseType(typeof(object))]
        public object GetContact(int id)
        {
            var display = from c in db.Contacts
                          where c.Id == id
                          select new
                          {
                              id = c.Id,
                              firstName = c.FirstName,
                              lastName = c.LastName,
                              organization = c.Org.Name,
                              email = c.Email,
                              officeNumber = c.OfficeNumber,
                              cellNumber = c.CellNumber,
                              addressOne = c.Address1,
                              addressTwo = c.Address2,
                              city = c.City,
                              state = c.State,
                              zip = c.Zip
                          };

            if (display == null)
            {
                return NotFound();
            }
            return Ok(new {contact = display });
        }

        // PUT: api/Contacts/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutContact(int id, RootObject rootobject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != rootobject.Contact.Id)
            {
                return BadRequest();
            }

            db.Entry(rootobject.Contact).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactExists(id))
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

        // POST: api/Contacts
        [ResponseType(typeof(RootObject))]
        public IHttpActionResult PostContact(RootObject rootcontact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);      
            }
           
                db.Contacts.Add(rootcontact.Contact);
                db.SaveChanges();
            
            return CreatedAtRoute("DefaultApi", new { id = rootcontact.Contact.Id }, rootcontact.Contact);

        }

        // DELETE: api/Contacts/5
        [ResponseType(typeof(Contact))]
        public IHttpActionResult DeleteContact(int id)
        {
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return NotFound();
            }

            db.Contacts.Remove(contact);
            db.SaveChanges();
            RootObject rootobject = new RootObject();
            rootobject.Contact = contact;
            return Ok(rootobject.Contact);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ContactExists(int id)
        {
            return db.Contacts.Count(e => e.Id == id) > 0;
        }
    }
}