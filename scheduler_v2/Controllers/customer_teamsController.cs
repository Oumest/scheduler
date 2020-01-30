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
    public class customer_teamsController : ApiController
    {
        private scheduler_v2Entities db = new scheduler_v2Entities();

        // GET: api/customer_teams
        public IQueryable<customer_teams> Getcustomer_teams()
        {
            return db.customer_teams;
        }

        // GET: api/customer_teams/5
        [ResponseType(typeof(customer_teams))]
        public IHttpActionResult Getcustomer_teams(int id)
        {
            customer_teams customer_teams = db.customer_teams.Find(id);
            if (customer_teams == null)
            {
                return NotFound();
            }

            return Ok(customer_teams);
        }

        // PUT: api/customer_teams/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putcustomer_teams(int id, customer_teams customer_teams)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customer_teams.id)
            {
                return BadRequest();
            }

            db.Entry(customer_teams).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!customer_teamsExists(id))
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

        // POST: api/customer_teams
        [ResponseType(typeof(customer_teams))]
        public IHttpActionResult Postcustomer_teams(customer_teams customer_teams)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.customer_teams.Add(customer_teams);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = customer_teams.id }, customer_teams);
        }

        // DELETE: api/customer_teams/5
        [ResponseType(typeof(customer_teams))]
        public IHttpActionResult Deletecustomer_teams(int id)
        {
            customer_teams customer_teams = db.customer_teams.Find(id);
            if (customer_teams == null)
            {
                return NotFound();
            }

            db.customer_teams.Remove(customer_teams);
            db.SaveChanges();

            return Ok(customer_teams);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool customer_teamsExists(int id)
        {
            return db.customer_teams.Count(e => e.id == id) > 0;
        }
    }
}