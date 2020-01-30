using scheduler_v2.Managers;
using scheduler_v2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace scheduler_v2.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")] // tune to your needs
    [RoutePrefix("")]
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
            int userId = manager.GetAccountIdFromName(userInput.username);
            string userRole = manager.GetUserRole(userId);
            var map = new Dictionary<string, string>();
            map.Add("id", userId.ToString());
            map.Add("username", userInput.username);
            map.Add("role", userRole);
            map.Add("token", token);

            if(token is null && !loginOk)
            {
                return Unauthorized();
            }

            if (loginOk)
            {
                 return Ok(map);
            }

            return Unauthorized();
        }
    }
}