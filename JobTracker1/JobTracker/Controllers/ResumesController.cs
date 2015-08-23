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
    public class ResumesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Resumes
        public object GetResumes()
        {
            var result = from r in db.Resumes
                         select new {id = r.Id, title = r.Title, date = r.Date, info = r.Content, file = r.File };
            return new {resume = result };
        }

        // GET: api/Resumes/5
        [ResponseType(typeof(Resume))]
        public IHttpActionResult GetResume(int id)
        {
            var display = from r in db.Resumes
                          where r.Id == id
                          select new
                          {
                              id = r.Id,
                              title = r.Title,
                              date = r.Date,
                              info = r.Title,
                              file = r.File
                          };

            if (display == null)
            {
                return NotFound();
            }
            return Ok(new { resume = display });
        }

        // PUT: api/Resumes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutResume(int id, Resume resume)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != resume.Id)
            {
                return BadRequest();
            }

            db.Entry(resume).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResumeExists(id))
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

        // POST: api/Resumes
        [ResponseType(typeof(RootObject))]
        public IHttpActionResult PostResume(RootObject rootresume)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Resumes.Add(rootresume.Resume);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = rootresume.Resume.Id }, rootresume.Resume);
        }

        // DELETE: api/Resumes/5
        [ResponseType(typeof(Resume))]
        public IHttpActionResult DeleteResume(int id)
        {
            Resume resume = db.Resumes.Find(id);
            if (resume == null)
            {
                return NotFound();
            }

            db.Resumes.Remove(resume);
            db.SaveChanges();

            return Ok(resume);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ResumeExists(int id)
        {
            return db.Resumes.Count(e => e.Id == id) > 0;
        }
    }
}