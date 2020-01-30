using scheduler_v2.Interfaces;
using scheduler_v2.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace scheduler_v2.Managers
{
    /*
     * Add method overloads - done
     * 
     * Add methods to get all activities for Day/Week/Month/Year -- only for timesheet
     * Get activities by project id - done
     * delete activities
     * update activities
     */


    public class ActivityManager
    {
        private scheduler_v2Entities db = new scheduler_v2Entities();

        public activities CreateNewActivity(int Project_id, string Name, string Comment ="", float Fixed_rate = 0, float Hourly_rate = 0, float Budget = 0, int Time_budget = 0, string Color = "")
        {
            activities activity = new activities()
            {
                project_id = Project_id,
                name = Name,
                visible = 1,
                comment = Comment,
                fixed_rate = Fixed_rate,
                hourly_rate = Hourly_rate,
                budget = Budget,
                time_budget = Time_budget,
                color = Color
            };

            return activity;
        }

        public string GetActivityById(int activityId)
        {
            using (var db = new scheduler_v2Entities())
            {
                var name = (from item in db.activities
                            where item.id == activityId
                            select item.name).FirstOrDefault();
                return name;
            }
        }

        public IQueryable<activities> GetAllActivities()
        {
            
            return db.activities;
        }

        public IQueryable<activities> GetActivitiesByProjectId(int id)
        {

            return db.activities.Where(x => x.project_id == id);
        }


    }
}