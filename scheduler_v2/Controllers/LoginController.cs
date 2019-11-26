using scheduler_v2.Managers;
using scheduler_v2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace scheduler_v2.Controllers
{
    public class LoginController : ApiController
    {
        private scheduler_v2Entities entities = new scheduler_v2Entities();
        private HttpRequestMessage message = new HttpRequestMessage();

        [Route("Login")]
        [HttpPost]
        [ResponseType(typeof(HttpResponseMessage))]
        public IHttpActionResult LoginUser(users userInput) // LOGIN ACCOUNT
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            };

            UserManager manager = new UserManager();

            bool loginOk = manager.Login(userInput.username, userInput.password);
            string token = manager.CreateToken(userInput.username);

            if(token is null && !loginOk)
            {
                return Unauthorized();
            }

            if (loginOk)
            {
                return Ok(token);
            }

            return Unauthorized();
        }
    }
}