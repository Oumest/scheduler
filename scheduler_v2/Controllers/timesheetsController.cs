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
using scheduler_v2.Models.DTOs;

namespace scheduler_v2.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")] // tune to your needs
    [RoutePrefix("")]
    public class timesheetsController : ApiController
    {
        private scheduler_v2Entities db = new scheduler_v2Entities();
        private HttpRequestMessage message = new HttpRequestMessage();
        private TimesheetManager timesheet = new TimesheetManager();

        // GET: api/timesheets
        // returns timesheets by descending order, showing newest first.
        [Route("api/timesheets")]
        [HttpGet]
        public IQueryable<timesheet> Gettimesheet()
        {
            return db.timesheet.OrderByDescending(x => x.start_time);
        }

        // GET: api/timesheets/5
        [ResponseType(typeof(timesheet))]
        public IHttpActionResult Gettimesheet(int id)
        {
            timesheet timesheet = db.timesheet.Find(id);
            if (timesheet == null)
            {
                return NotFound();
            }

            return Ok(timesheet);
        }

        // PUT: api/timesheets/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttimesheet(int id, timesheet timesheet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != timesheet.id)
            {
                return BadRequest();
            }

            db.Entry(timesheet).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!timesheetExists(id))
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

        // POST: api/timesheets

/*
 * Currently needs to take format like this:
 * {
"User_id" : "3",
"Start_date" : "2019-12-03",
"Start_time" : "08:30:00",
"Project_id" : "1",
"Activity_id" : "1"
}
*/

[Route("api/timesheets")]
[HttpPost]
[ResponseType(typeof(TimeSheetDTO))]
public IHttpActionResult Posttimesheet(TimeSheetDTO input)
{
    TextResult httpResponse = new TextResult("There is already a timesheet present", message);
    TimesheetManager manager = new TimesheetManager();

    var timesheetObject = manager.CreateTimeSheet(input.User_id, input.Start_date, input.Start_time.ToString(), input.Project_id, input.Activity_id); ;
    if (!ModelState.IsValid)
    {
        return BadRequest(ModelState);
    }
    try
    {
        db.timesheet.Add(timesheetObject);
        db.SaveChanges();
    }
    catch
    {
        httpResponse.ChangeHTTPMessage("Failed to create timesheet", message); // HTTP response if fails to savechanges to DB
        return httpResponse;
    }

    return Ok();
}

// DELETE: api/timesheets/5
[ResponseType(typeof(timesheet))]
public IHttpActionResult Deletetimesheet(int id)
{
    timesheet timesheet = db.timesheet.Find(id);
    if (timesheet == null)
    {
        return NotFound();
    }

    db.timesheet.Remove(timesheet);
    db.SaveChanges();

    return Ok(timesheet);
}
        // GET: api/timesheets/timesheetByDate?date=yyyy-MM-dd
        [ActionName("timesheetByDate")]
        [ResponseType(typeof(timesheet))]
        public IQueryable GetactivitiesByProjectId(string date)
        {
            return timesheet.GetTimesheetsByDate(date);
        }

        // GET: api/timesheets/descending?date=yyyy-MM-dd
        // Takes start time as string and returns all previous timesheets including given date.
        [ActionName("descending")]
        [ResponseType(typeof(timesheet))]
        public IQueryable GetTimesheetsByDescending(string date)
        {
            return db.timesheet.OrderByDescending(x => x.start_time);
        }

        // GET: api/timesheets/week
        [ActionName("week")]
        [ResponseType(typeof(timesheet))]
        public IQueryable GetTimesheetsForWeek()
        {
            return timesheet.GetTimesheetThisWeek();
        }
        // GET: api/timesheets/month
        [ActionName("month")]
        [ResponseType(typeof(timesheet))]
        public IQueryable GetTimesheetsForMonth()
        {
            return timesheet.GetTimesheetThisMonth();
        }

        // GET: api/timesheets/year
        [ActionName("year")]
        [ResponseType(typeof(timesheet))]
        public IQueryable GetTimesheetsForYear()
        {
            return timesheet.GetTimesheetThisYear();
        }

        // GET: api/timesheets/user?id=*
        [ActionName("user")]
        [ResponseType(typeof(List<UserTimesheetDTO>))]
        public List<UserTimesheetDTO> GetTimesheetsForUser(int id)
        {
            //timesheet.getAllHours(timesheet.GetTimesheetsByUserId(id));
            return timesheet.userTimesheetDTOListByUserID(id);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool timesheetExists(int id)
        {
            return db.timesheet.Count(e => e.id == id) > 0;
        }
    }
}
 