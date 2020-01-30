using scheduler_v2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace scheduler_v2.Managers
{
    public class CustomerManager // add method overloads - done (mostly)
    {
        public customers CreateCustomer(string CustomerName, string Country, string Currency, int Budget, int TimeBudget, string Number = "", string Comment ="", string Company = "", string Contact ="", string Address="", string Phone = "", string Email = "")
        {
            customers customer = new customers()
            {
                name = CustomerName,
                country = Country,
                currency = Currency,
                budget = Budget,
                time_budget = TimeBudget,
                number = Number, 
                comment = Comment,
                company = Company,
                contact = Contact,
                address = Address,
                phone = Phone,
                email = Email
            };

            return customer;
        }

        public string GetCustomerNameByProjectId(int projectId)
        {
            using (var db = new scheduler_v2Entities())
            {
                var customerId = (from item in db.projects
                            where item.id == projectId
                            select item.customer_id).FirstOrDefault();

                var name = (from item in db.customers
                            where item.id == customerId
                            select item.name).FirstOrDefault();
                return name;
            }
        }
    }
}