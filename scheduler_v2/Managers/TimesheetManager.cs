using scheduler_v2.Models;
using scheduler_v2.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Web;

namespace scheduler_v2.Managers
{
    public class TimesheetManager
    {
        /*
         * 
         * Timesheet variabler: Date, StartTime, EndTime, Duration, Rate, Customer, Project, Activity, Description, Tags.
         * Timesheet funcs: Getters/setters, GetHours(day, week, month, year, alltime) - Done
         * GetUserTeams
         * Add methods to get all activities for Day/Week/Month/Year - done
         * Get timesheets by userId - done
         * get timesheets by teamlead id
         * 
         * Add to timesheet interface:
         *      getCurrentDate
         */

        private scheduler_v2Entities db = new scheduler_v2Entities();

        public timesheet CreateTimeSheet(int User_id, string StartDate, string StartTime, int Project_id, int Activity_id, string EndTime = "", string Rate = "", string Description = "")
        {

            //var fullDateFormat = "yyyy-MM-dd HH:mm:ss";

            //string fullStringDate = StartDate + " " + StartTime;
            //DateTime fullDate = new DateTime();
            float rate = float.Parse(Rate);

            //fullDate = DateTime.ParseExact(fullStringDate, fullDateFormat, CultureInfo.InvariantCulture);

            DateTime fullDate = getCurrentDate();
            DateTime endDate = getDateFormated(EndTime);

            timesheet sheet = new timesheet()
            {
                user_id = User_id,
                activity_id = Activity_id,
                project_id = Project_id,
                start_time = Convert.ToDateTime(fullDate),
                end_time = Convert.ToDateTime(endDate),
                rate = rate,
                description = Description
            };

            return sheet;
        }
        /*
         * returns current Date in "yyyy-MM-dd HH:mm:ss" format as DateTime variable
         */
        public DateTime getCurrentDate()
        {

            string fullStringDate = DateTime.Now.ToShortDateString();


            DateTime fullDate = getDateFormated(fullStringDate);

            return fullDate;
        }

        /*
         * returns given date string in "yyyy-MM-dd HH:mm:ss" format as DateTime variable
         * or in "yyyy-MM-dd" if provided string is less or equal to 11 characters
         */
        public DateTime getDateFormated(string DateString)
        {
            var dateFormat = "yyyy-MM-dd HH:mm:ss";

            DateTime date = new DateTime();
            if(DateString.Length <= 11)
            {
                dateFormat = "yyyy-MM-dd";
            }
            date = DateTime.ParseExact(DateString, dateFormat, CultureInfo.InvariantCulture);
            return date;
        }
        public List<UserTimesheetDTO> userTimesheetDTOListByUserID(int id)
        {
            List<TimeSheetDTO> timesheetDTO = QueryableListToDTO(IQueryableToList(QueryableTimesheetByUserID(id)));
            List<UserTimesheetDTO> userTimesheetDTOlist = TimesheetListToDTOlist(timesheetDTO);

            return userTimesheetDTOlist;

        }
        public UserTimesheetDTO GetTimesheetsByUserId(int id) // UserTimesheetDTO
        {
            var queryableSheet = QueryableTimesheetByUserID(id);
            var timesheetDTO = QueryableToDTO(queryableSheet, id);
            UserTimesheetDTO sentObject= DTOtoDTO(timesheetDTO);

            return sentObject;
        }

        public List<string> GetNamesToList(TimeSheetDTO dto) // gets Customer, Project and Activity names from TimesheetDTO object
        {
            CustomerManager cmngr = new CustomerManager();
            ProjectManager pmngr = new ProjectManager();
            ActivityManager amngr = new ActivityManager();

            string activityName = amngr.GetActivityById(dto.Activity_id);
            string projectName = pmngr.GetProjectNameById(dto.Project_id);
            string customerName = cmngr.GetCustomerNameByProjectId(dto.Project_id);

            List<string> names = new List<string>();

            names.Add(activityName);
            names.Add(projectName);
            names.Add(customerName);

            return names;
        }

        public IQueryable<timesheet> QueryableTimesheetByUserID(int userId)
        {
            return db.timesheet.Where(x => x.user_id == userId);
        }

        public List<timesheet> IQueryableToList(IQueryable<timesheet> queryableTimesheet)
        {
            return queryableTimesheet.ToList();
        }



        //converts List of TimesheetDTOs to a list of UserTimesheetDTOs
        public List<UserTimesheetDTO> TimesheetListToDTOlist(List<TimeSheetDTO> timesheetList)
        {
            List<UserTimesheetDTO> userTimesheetList = new List<UserTimesheetDTO>();
            foreach(var item in timesheetList)
            {
                userTimesheetList.Add(DTOtoDTO(item));

            }
            return userTimesheetList;
        }

        // converts TimesheetDTO to UserTimesheetDTO
        public UserTimesheetDTO DTOtoDTO(TimeSheetDTO firstDTO)
        {
            List<string> names = GetNamesToList(firstDTO);
            UserTimesheetDTO sheet = new UserTimesheetDTO
            {
                User_id = firstDTO.User_id,
                Project_name = names[1],
                Activity_name = names[0],
                Customer_name = names[2],
                Start_time = firstDTO.Start_time,
                Start_date = firstDTO.Start_date,
                End_time = firstDTO.End_time,
                Duration = firstDTO.Duration,
                Description = firstDTO.Description,
                Rate = firstDTO.Rate,
                Exported = firstDTO.Exported,
                Fixed_rate = firstDTO.Fixed_rate,
                Hourly_rate = firstDTO.Hourly_rate
            };
            return sheet;
        }

        // converts IQueryable<timesheet> to a TimesheetDTO and stores them to List
        public List<TimeSheetDTO> QueryableListToDTO(List<timesheet> queryableList) 
        {
            List<TimeSheetDTO> sheet = new List<TimeSheetDTO>();
            foreach(var item in queryableList)
            {
                TimeSheetDTO dto = new TimeSheetDTO
                {
                    User_id = item.user_id,
                    Project_id = item.project_id,
                    Activity_id = item.activity_id,
                    Start_time = item.start_time,
                    Start_date = item.start_time.ToString(),
                    End_time = item.end_time.ToString(),
                    Duration = item.duration ?? default(int),
                    Description = item.description,
                    Rate = item.rate ?? default(double),
                    Exported = item.exported ?? default(short),
                    Fixed_rate = item.fixed_rate ?? default(double),
                    Hourly_rate = item.hourly_rate ?? default(double)
                };
                sheet.Add(dto);
            }
            return sheet;
        }

        public TimeSheetDTO QueryableToDTO(IQueryable<timesheet> queryableTimesheet, int userId)
        {
            TimeSheetDTO obj = (from items in db.timesheet
                                where items.user_id == userId
                                select new TimeSheetDTO
                                {
                                    User_id = items.user_id,
                                    Project_id = items.project_id,
                                    Activity_id = items.activity_id,
                                    Start_time = items.start_time,
                                    Start_date = items.start_time.ToString(),
                                    End_time = items.end_time.ToString(),
                                    Duration = items.duration ?? default(int),
                                    Description = items.description,
                                    Rate = items.rate ?? default(double),
                                    Exported = items.exported ?? default(short),
                                    Fixed_rate = items.fixed_rate ?? default(double),
                                    Hourly_rate = items.hourly_rate ?? default(double)
                                }
                          ).FirstOrDefault();
            return obj;
        }

        public IQueryable<timesheet> GetTimesheetsByDate(string StartDate)
        {
            var dateFormat = "yyyy-MM-dd";
            DateTime date = new DateTime().Date;
            date = DateTime.ParseExact(StartDate, dateFormat, CultureInfo.InvariantCulture);
            Debug.WriteLine(weekNumber(date));
            return db.timesheet.Where(p => DbFunctions.TruncateTime(p.start_time) == date).OrderByDescending(p => p.start_time);
        }

        private int weekNumber(DateTime fromDate)
        {
            // Get jan 1st of the year
            DateTime startOfYear = fromDate.AddDays(-fromDate.Day + 1).AddMonths(-fromDate.Month + 1);
            // Get dec 31st of the year
            DateTime endOfYear = startOfYear.AddYears(1).AddDays(-1);
            // ISO 8601 weeks start with Monday 
            // The first week of a year includes the first Thursday 
            // DayOfWeek returns 0 for sunday up to 6 for saturday
            int[] iso8601Correction = { 6, 7, 8, 9, 10, 4, 5 };
            int nds = fromDate.Subtract(startOfYear).Days + iso8601Correction[(int)startOfYear.DayOfWeek];
            int wk = nds / 7;
            switch (wk)
            {
                case 0:
                    // Return weeknumber of dec 31st of the previous year
                    return weekNumber(startOfYear.AddDays(-1));
                case 53:
                    // If dec 31st falls before thursday it is week 01 of next year
                    if (endOfYear.DayOfWeek < DayOfWeek.Thursday)
                        return 1;
                    else
                        return wk;
                default: return wk;
            }
        }

        public IQueryable<timesheet> GetTimesheetThisWeek()
        {
            int currentWeek = weekNumber(getCurrentDate());
            var date = getDateFormated("2019-12-04");
            Debug.WriteLine(FirstDateOfWeekISO8601(2019, weekNumber(date)));
            var lowerDateBound = FirstDateOfWeekISO8601(2019, weekNumber(date));
            var upperDateBound = lowerDateBound.AddDays(6);
            return db.timesheet.Where(x => x.start_time >= lowerDateBound && x.start_time <= upperDateBound).OrderByDescending(x => x.start_time);
        }

        public IQueryable<timesheet> GetTimesheetThisMonth()
        {
            int currentMonth = getCurrentDate().Month;
            return db.timesheet.Where(x => x.start_time.Month == currentMonth).OrderByDescending(x => x.start_time);
        }

        public IQueryable<timesheet> GetTimesheetThisYear()
        {
            int currentYear = getCurrentDate().Year;
            return db.timesheet.Where(x => x.start_time.Year == currentYear).OrderByDescending(x => x.start_time);
        }

        public static DateTime FirstDateOfWeekISO8601(int year, int weekOfYear)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;

            // Use first Thursday in January to get first week of the year as
            // it will never be in Week 52/53
            DateTime firstThursday = jan1.AddDays(daysOffset);
            var cal = CultureInfo.CurrentCulture.Calendar;
            int firstWeek = cal.GetWeekOfYear(firstThursday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            var weekNum = weekOfYear;
            // As we're adding days to a date in Week 1,
            // we need to subtract 1 in order to get the right date for week #1
            if (firstWeek == 1)
            {
                weekNum -= 1;
            }

            // Using the first Thursday as starting week ensures that we are starting in the right year
            // then we add number of weeks multiplied with days
            var result = firstThursday.AddDays(weekNum * 7);

            // Subtract 3 days from Thursday to get Monday, which is the first weekday in ISO8601
            return result.AddDays(-3);
        }

        public int getAllHours()
        {
            int hours = 0;
            foreach(var item in db.timesheet)
            {
                if(item.duration!= null)
                {
                    hours += (int)item.duration;
                }
            }
            return hours;
        }

        public int getAllHours(IQueryable<timesheet> timesheets)
        {
            int hours = 0;
            foreach(var item in timesheets)
            {
                if (item.duration != null)
                {
                    hours += (int)item.duration;
                }
            }
            return hours;
        }

        public IQueryable<timesheet> search(string text)
        {
            return db.timesheet.Where(
            x => x.activities.name.Contains(text)
            || x.description.Contains(text)
            || x.projects.name.Contains(text));
        }
    }
}