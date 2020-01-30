using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using scheduler_v2.Managers;
using scheduler_v2.Models;
using scheduler_v2.Models.DTOs;

namespace scheduler_v2.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")] // tune to your needs
    [RoutePrefix("")]
    public class customersController : ApiController
    {
        private scheduler_v2Entities db = new scheduler_v2Entities();
        private HttpRequestMessage message = new HttpRequestMessage();

        // GET: api/customers
        [Route("api/customers")]
        [HttpGet]
        public IQueryable<customers> Getcustomers()
        {
            return db.customers;
        }

        // GET: api/customers/5
        [ResponseType(typeof(customers))]
        public IHttpActionResult Getcustomers(int id)
        {
            customers customers = db.customers.Find(id);
            if (customers == null)
            {
                return NotFound();
            }

            return Ok(customers);
        }

        // PUT: api/customers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putcustomers(int id, customers customers)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customers.id)
            {
                return BadRequest();
            }

            db.Entry(customers).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!customersExists(id))
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

        // POST: api/customers
        [Route("api/customers")]
        [HttpPost]
        [ResponseType(typeof(CustomerDTO))]
        public IHttpActionResult Postcustomers(CustomerDTO input)
        {
            TextResult httpResponse = new TextResult("There is already a customer with that name!", message);

            CustomerManager manager = new CustomerManager();

            var customerObject = manager.CreateCustomer(input.Name, input.Country, input.Currency, input.Budget, input.TimeBudget);


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                db.customers.Add(customerObject);
                db.SaveChanges();
            }
            catch
            {
                httpResponse.ChangeHTTPMessage("Failed to create customer!", message); // HTTP response if fails to savechanges to DB
                return httpResponse;
            }


            return Ok();
        }


        // DELETE: api/customers/RemoveCustomerFromTeam
        //deletes customer from customer_teams table.
        // Takes customerDto as input. Ex {
	        //"CustomerId" : 2,
	        //"TeamId" : 4
            //}

    [ActionName("RemoveCustomerFromTeam")]
        [HttpDelete]
        public IHttpActionResult RemoveCustomerFromTeam(CustomerTeamDTO customerTeam)
        {
            int customer_team_id = db.customer_teams.Where(x => x.customer_id == customerTeam.CustomerId && x.team_id == customerTeam.TeamId).Select(x => x.id).SingleOrDefault();
            Debug.WriteLine(customer_team_id);
            var customer_team = db.customer_teams.Find(customer_team_id);

            if(customer_team == null)
            {
                return NotFound();
            }

            db.customer_teams.Remove(customer_team);
            db.SaveChanges();

            return Ok(customer_team);
        }
        // DELETE: api/customers/5
        [ResponseType(typeof(customers))]
        public IHttpActionResult Deletecustomers(int id)
        {
            customers customers = db.customers.Find(id);
            if (customers == null)
            {
                return NotFound();
            }

            db.customers.Remove(customers);
            db.SaveChanges();

            return Ok(customers);
        }

        //GET: returns customer name as string by customer id witch response status code (200 if success/ 404 if not found)
        [ActionName("CustomerName")]
        [HttpGet]
        public IHttpActionResult GetName(int customerId)
        {
            var name = db.customers.Where(x => x.id == customerId).Select(x => x.name).SingleOrDefault();

            if(name == null)
            {
                return NotFound();
            }
            return Ok(name);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool customersExists(int id)
        {
            return db.customers.Count(e => e.id == id) > 0;
        }
    }
}