using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using scheduler_v2.Managers;
using scheduler_v2.Models;

namespace scheduler_v2.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")] // tune to your needs
    [RoutePrefix("")]
    public class ActivitiesController : ApiController
    {
        private scheduler_v2Entities db = new scheduler_v2Entities();
        private ActivityManager activity = new ActivityManager();

        // GET: api/activities
        [Route("api/activities")]
        [HttpGet]
        public IQueryable<activities> Getactivities()
        {
            var items = activity.GetAllActivities();
            return items;
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


        // GET: api/activities/activityByProjectId?id=*your id*
        //[Route("api/activities/activityByProjectId/")]
        [ActionName("activityByProjectId")]
        [ResponseType(typeof(activities))]
        public IQueryable GetactivitiesByProjectId(int id)
        {
            return activity.GetActivitiesByProjectId(id);
        }

        private bool activitiesExists(int id)
        {
            return db.activities.Count(e => e.id == id) > 0;
        }
    }
}