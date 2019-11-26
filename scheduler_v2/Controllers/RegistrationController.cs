using scheduler_v2.Models;
using scheduler_v2.Models.DTOs;
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
using scheduler_v2.Managers;

namespace scheduler_v2.Controllers
{
    public class RegistrationController : ApiController
    {
        private scheduler_v2Entities entities = new scheduler_v2Entities();
        private HttpRequestMessage message = new HttpRequestMessage();

        //POST
        [Route("Register")]
        [HttpPost]
        [ResponseType(typeof(RegistrationDTO))]
        public IHttpActionResult CreateAccount(RegistrationDTO userInput)
        {
            TextResult httpResponse = new TextResult("There is already an account with that name!", message);
            UserManager manager = new UserManager();

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (!manager.IsValidEmail(userInput.Email))
            {
                httpResponse.ChangeHTTPMessage("Enter valid email!", message);
                return httpResponse; // HTTP response if accountname already exists
            }

            if (manager.CheckIfAccountNameExists(userInput.Username))
            {
                return httpResponse;
            }

            if (manager.CheckIfEmailExists(userInput.Email))
            {
                httpResponse.ChangeHTTPMessage("Email already exists!", message); // If email exists, HTTP response
                return httpResponse;
            }

            var accountObject = manager.CreateAccount(userInput.Username, userInput.Password, userInput.Email);

            try
            {
                entities.users.Add(accountObject);
                entities.SaveChanges();
            }
            catch
            {
                httpResponse.ChangeHTTPMessage("Failed to create account!", message); // HTTP response if fails to savechanges to DB
                return httpResponse;
            }
            return Ok(); // returns login token if registration succesfull
        }

    }
}