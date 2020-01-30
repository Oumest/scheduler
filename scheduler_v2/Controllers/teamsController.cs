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
    public class teamsController : ApiController
    {
        private scheduler_v2Entities db = new scheduler_v2Entities();
        private HttpRequestMessage message = new HttpRequestMessage();
        private TeamManager team = new TeamManager();

        // GET: api/teams
        [Route("api/teams")]
        public IQueryable<teams> Getteams()
        {   
            return db.teams.Include(x=> x.user_teams);
        }


        //Returns all teams that are working on given project Id
        //GET: api/teams/ProjectTeams?ProjectId=5
        [ActionName("ProjectTeams")]
        [HttpGet]
        public IQueryable<teams> GetProjectTeams(int ProjectId)
        {
            //var project_teams = db.project_teams.Where(x => x.project_id == ProjectId).Include(x => x.teams);
            //return project_teams.Select(x => x.teams);
            return db.project_teams.Where(x=> x.project_id == ProjectId).Select(x => x.teams);
        }

        [ActionName("CustomerTeams")]
        [HttpGet]
        public IQueryable<teams> GetCustomerTeams(int CustomerId)
        {
            return db.customer_teams.Where(x => x.customer_id == CustomerId).Select(x => x.teams);
        }

        // GET: api/teams/5
        [ResponseType(typeof(teams))]
        public IHttpActionResult Getteams(int id)
        {
            teams teams = db.teams.Find(id);
            if (teams == null)
            {
                return NotFound();
            }

            return Ok(teams);
        }

        // PUT: api/teams/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putteams(int id, teams teams)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != teams.id)
            {
                return BadRequest();
            }

            db.Entry(teams).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!teamsExists(id))
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

        // POST: api/teams
        /*
         * Input example : 
         * {
	"TeamName" : "TNT",
	"Teamlead_id" : "5",
	"Users" : [2, 3, 4]
}
*/


        // POST: api/teams/CreateNewTeam
        [ActionName("CreateNewTeam")]
        [ResponseType(typeof(TeamDTO))]
        public IHttpActionResult Postteams(TeamDTO teamdto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TeamManager mngr = new TeamManager();
            var team = mngr.CreateTeam(teamdto.TeamName, teamdto.Teamlead_id);
            db.teams.Add(team);

            var teamleadTeam = mngr.AddUserTeam(team.id, team.teamlead_id);
            db.user_teams.Add(teamleadTeam);

            foreach(int id in teamdto.Users)
            {
                var userTeam = mngr.AddUserTeam(team.id, id);
                db.user_teams.Add(userTeam);
            }

            db.SaveChanges();
            return Ok();
            ///return CreatedAtRoute("DefaultApi", new { id = teams.id }, teams);
        }

        // DELETE: api/teams/5
        [ResponseType(typeof(teams))]
        public IHttpActionResult Deleteteams(int id)
        {
            teams teams = db.teams.Find(id);
            if (teams == null)
            {
                return NotFound();
            }

            db.teams.Remove(teams);
            db.SaveChanges();

            return Ok(teams);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool teamsExists(int id)
        {
            return db.teams.Count(e => e.id == id) > 0;
        }

        //[Route("api/teams")]
        //[HttpPost]

    }
}
 