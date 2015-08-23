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
    public class PositionsController : ApiController
    {
      
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Positions
        public object GetPositions()
        {
            var display = from p in db.Positions
                          select new
                          {
                              id = p.Id,
                              title = p.Title,
                              org = p.Org.Name,
                              education = p.Education,
                              experience = p.Experience,
                              salaryMax = p.SalaryMax,
                              salaryMin = p.SalaryMin,
                              description = p.Description,
                              contact = p.Contact.FirstName + " " + p.Contact.LastName,
                          };
            return new { position = display };
        }

        // GET: api/Positions/5
        [ResponseType(typeof(Position))]
        public IHttpActionResult GetPosition(int id)
        {
            var display = from p in db.Positions
                          where p.Id == id
                         
                          select new
                          {
                              id = p.Id,
                              title = p.Title,
                              org = p.Org.Name,
                              education = p.Education,
                              experience = p.Experience,
                              salaryMax = p.SalaryMax,
                              salaryMin = p.SalaryMin,
                              description = p.Description,
                              contact = p.Contact.FirstName + " " + p.Contact.LastName,
                          };
            if (display == null)
            {
                return NotFound();
            }

            return Ok(new { position = display });
        }

        // PUT: api/Positions/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPosition(int id, Position position)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != position.Id)
            {
                return BadRequest();
            }

            db.Entry(position).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PositionExists(id))
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

        // POST: api/Positions
        [ResponseType(typeof(RootObject))]
        public IHttpActionResult PostPosition(RootObject rootposition)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Positions.Add(rootposition.Position);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = rootposition.Position.Id }, rootposition.Position);
        }

        // DELETE: api/Positions/5
        [ResponseType(typeof(Position))]
        public IHttpActionResult DeletePosition(int id)
        {
            Position position = db.Positions.Find(id);
            if (position == null)
            {
                return NotFound();
            }

            db.Positions.Remove(position);
            db.SaveChanges();

            return Ok(position);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PositionExists(int id)
        {
            return db.Positions.Count(e => e.Id == id) > 0;
        }
    }
}