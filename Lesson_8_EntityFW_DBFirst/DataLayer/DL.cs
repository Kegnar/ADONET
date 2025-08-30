using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using Lesson_8_EntityFW_DBFirst.Models;

namespace Lesson_8_EntityFW_DBFirst.DataLayer
{
    public class DL
    {
        public static class Customer
        {
            public static IEnumerable<CustomerModel> CustomerAll()
            {
                using (var db = new ADONETEntities())
                {
                    List<CustomerModel> customers = new List<CustomerModel>();
                    var result = db.sp_customer_all().ToList();
                    foreach (var customer in result)
                    {
                        CustomerModel tmp = new CustomerModel();
                        tmp.id = customer.id;
                        tmp.LastName = customer.LastName;
                        tmp.FirstName = customer.FirstName;
                        tmp.DateOfBirth = customer.DateOfBirth;
                        customers.Add(tmp);

                    }
                    return customers;
                }
            }

            public static CustomerModel ById(int id)
            {
                using (var db = new ADONETEntities())
                {
                    CustomerModel tmp = new CustomerModel();
                    var res = db.stp_CustomerByID(id).First();
                    tmp.id = res.id;
                    tmp.LastName = res.LastName;
                    tmp.FirstName = res.FirstName;
                    tmp.DateOfBirth = res.DateOfBirth;
                    return tmp;

                }
            }

            public static int AddCustomer(string firstName, string lastName, DateTime dateOfBirth)
            {
                int id = -1; // заглушка, нужна для передачи в stp_CustomerAdd. 

                var userId = new ObjectParameter("CustomerID", id);
                using (var db = new ADONETEntities())
                {
                    db.stp_CustomerAdd(firstName, lastName, dateOfBirth, userId); 
                    return Convert.ToInt32(userId.Value);
                    
                }
            }
        }
    }
}