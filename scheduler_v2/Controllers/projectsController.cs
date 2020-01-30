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
    public class projectsController : ApiController
    {
        private scheduler_v2Entities entities = new scheduler_v2Entities();
        private ProjectManager manager = new ProjectManager();
        private HttpRequestMessage message = new HttpRequestMessage();

        // GET: api/projects
        [Route("api/projects")]
        [HttpGet]
        public IQueryable<projects> Getprojects()
        {

            // below code up until return goes into post method route
            ProjectManager mngr = new ProjectManager();
            var projectObj = mngr.CreateNewProject(1, "testing", "12345", "testing method with overloads", 150, 155, 150000, 150, "blue");
            try { entities.projects.Add(projectObj); entities.SaveChanges(); }
            catch
            {
                Debug.WriteLine("error");
            }
            return entities.projects;
        }
        //POST: api/projects/Create
        [ActionName("Create")]
        [HttpPost]
        public IHttpActionResult CreateProject(ProjectDTO project)
        {
            TextResult httpResponse = new TextResult("Something went wrong", message);
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var projectObj = manager.CreateNewProject(project.Customer_id, project.Name, project.Order_number, project.Comment, project.Fixed_rate, project.Hourly_rate, project.Budget, project.Time_budget, project.Color);
            try
            {
                entities.projects.Add(projectObj);
                entities.SaveChanges();
            }
            catch
            {
                return httpResponse;
            }
            return Ok(projectObj);
        }

        // GET: api/projects/5
        [ResponseType(typeof(projects))]
        public IHttpActionResult Getprojects(int id)
        {
            projects projects = entities.projects.Find(id);
            if (projects == null)
            {
                return NotFound();
            }

            return Ok(projects);
        }

        // PUT: api/projects/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putprojects(int id, projects projects)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != projects.id)
            {
                return BadRequest();
            }

            entities.Entry(projects).State = EntityState.Modified;

            try
            {
                entities.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!projectsExists(id))
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

        // POST: api/projects
        [ResponseType(typeof(projects))]
        public IHttpActionResult Postprojects(projects projects)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            entities.projects.Add(projects);
            entities.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = projects.id }, projects);
        }

        // DELETE: api/projects/5
        [ResponseType(typeof(projects))]
        public IHttpActionResult Deleteprojects(int id)
        {
            projects projects = entities.projects.Find(id);
            if (projects == null)
            {
                return NotFound();
            }

            entities.projects.Remove(projects);
            entities.SaveChanges();

            return Ok(projects);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                entities.Dispose();
            }
            base.Dispose(disposing);
        }

        // GET: api/projects
        //[Route("api/projects")]
        //[HttpGet]
        [ActionName("projectByCustomerId")]
        [ResponseType(typeof(projects))]
        public IQueryable<projects> GetProjectsByCustomerId(int id)
        {

            return manager.GetProjectsByCustomerId(id);
        }

        [ActionName("ProjectsByTeamId")]
        [ResponseType(typeof(projects))]
        [HttpGet]
        public IQueryable<projects> GetProjectsByTeamId(int id)
        {
            return entities.project_teams.Where(x => x.team_id == id).Select(x => x.projects);
        }
        private bool projectsExists(int id)
        {
            return entities.projects.Count(e => e.id == id) > 0;
        }
    }
}