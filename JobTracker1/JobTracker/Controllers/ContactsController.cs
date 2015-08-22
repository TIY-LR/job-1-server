﻿using System;
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
                              organization = c.Organization.Name,
                              email = c.Email,
                              officeNumber = c.OfficeNumber,
                              cellNumber = c.CellNumber,
                              addressOne = c.Address1,
                              addressTwo = c.Address2,
                              city = c.City,
                              state = c.State,
                              zip = c.Zip

                          };
            return new { contacts = displaylist };
        }

        // GET: api/Contacts/5
        [ResponseType(typeof(object))]
        public object GetContact(int id)
        {
            var contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return NotFound();
            }
            var display = new
            {
                id = contact.Id,
                firstName = contact.FirstName,
                lastName = contact.LastName,
                organization = contact.Organization.Name,
                email = contact.Email,
                officeNumber = contact.OfficeNumber,
                cellNumber = contact.CellNumber,
                addressOne = contact.Address1,
                addressTwo = contact.Address2,
                city = contact.City,
                state = contact.State,
                zip = contact.Zip
            };
            return Ok(new {contact = display });
        }

        // PUT: api/Contacts/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutContact(int id, Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != contact.Id)
            {
                return BadRequest();
            }

            db.Entry(contact).State = EntityState.Modified;

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

            return Ok(contact);
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