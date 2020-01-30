using scheduler_v2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace scheduler_v2.Managers
{
    
    public class ProjectManager // add method overloads - done
    {
        private scheduler_v2Entities db = new scheduler_v2Entities();

        public projects CreateNewProject(int CustomerId, string ProjectName, string OrderNumber = "", string Comment = "", float Fixed_rate = 0, float Hourly_rate = 0, float Budget = 0, int Time_budget = 0, string Color = "")
        {
            projects project = new projects()
            {
                customer_id = CustomerId,
                name = ProjectName,
                order_number = OrderNumber,
                comment = Comment,
                fixed_rate = Fixed_rate,
                hourly_rate = Hourly_rate,
                budget = Budget,
                time_budget = Time_budget,
                color = Color,
                visible = 1
            };

            return project;
        }

        public string GetProjectNameById(int projectId)
        {
            using (var db = new scheduler_v2Entities())
            {
                var name = (from item in db.projects
                            where item.id == projectId
                            select item.name).FirstOrDefault();
                return name;
            }
        }

        public IQueryable<projects> GetProjectsByCustomerId(int id)
        {

            return db.projects.Where(x => x.customer_id == id);
        }
    }
}