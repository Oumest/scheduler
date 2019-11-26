using scheduler_v2.Managers;
using scheduler_v2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace scheduler_v2.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {

            //          registration with user manager example. Takes username, password and email as input
            //UserManager user = new UserManager();
            ////user.CreateAccount("teset", "Tester2", "teseet@mail.com");
            //var userObj = user.CreateAccount("second", "Tester20", "second@mail.com");
            //scheduler_v2Entities ent = new scheduler_v2Entities();
            //ent.users.Add(userObj);
            //ent.SaveChanges();
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
