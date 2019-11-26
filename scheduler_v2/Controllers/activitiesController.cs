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
using scheduler_v2.Models;

namespace scheduler_v2.Controllers
{
    public class ActivitiesController : ApiController
    {
        private scheduler_v2Entities db = new scheduler_v2Entities();

        // GET: api/activities
        public IQueryable<activities> Getactivities()
        {
            return db.activities;
        }

        // GET: api/activities/5
        [ResponseType(typeof(activities))]
        public IHttpActionResult Getactivities(int id)
        {
            activities activities = db.activities.Find(id);
            if (activities == null)
            {
                return NotFound();
            }

            return Ok(activities);
        }

        // PUT: api/activities/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putactivities(int id, activities activities)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != activities.id)
            {
                return BadRequest();
            }

            db.Entry(activities).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!activitiesExists(id))
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

        // POST: api/activities
        [ResponseType(typeof(activities))]
        public IHttpActionResult Postactivities(activities activities)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.activities.Add(activities);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = activities.id }, activities);
        }

        // DELETE: api/activities/5
        [ResponseType(typeof(activities))]
        public IHttpActionResult Deleteactivities(int id)
        {
            activities activities = db.activities.Find(id);
            if (activities == null)
            {
                return NotFound();
            }

            db.activities.Remove(activities);
            db.SaveChanges();

            return Ok(activities);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool activitiesExists(int id)
        {
            return db.activities.Count(e => e.id == id) > 0;
        }
    }
}