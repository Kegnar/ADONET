using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using Lesson_4_DataLayer.Models;
using Lesson_4_DataLayer.DataLayer;
namespace Lesson_4_DataLayer
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            {
                CustomerModel customer1 = DL.Customer.GetCustomerById(1);
                CustomerModel customer2 = DL.Customer.GetCustomerById(2);
                Console.WriteLine(customer1);
                List<CustomerModel> customers = DL.Customer.GetAllCustomers();
                foreach (CustomerModel customer in customers)
                {
                    Console.WriteLine(customer);
                }
                //delete 
                //update - еще и sp
            }
        }
    }
}