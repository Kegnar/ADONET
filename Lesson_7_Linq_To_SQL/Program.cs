using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data.SqlClient;
using System.Linq;
using Lesson_7_Linq_To_XML.Entity;

namespace Lesson_7_Linq_To_SQL
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (DataContext dataContext = new DataContext(connectionString))
            {
                //All customers
                Table<Customer> customers = dataContext.GetTable<Customer>();
                foreach (Customer customer in customers)
                {
                    Console.WriteLine(customer);
                }
                
                Console.WriteLine("------------");
                
                //top 2
                List<Customer> res = customers.Take(2).ToList();
                foreach (Customer customer in res)
                {
                    Console.WriteLine(customer);
                }
                //customer by id
                
                Console.WriteLine("---3-----");
                var query = from c in customers 
                    where c.id ==3 
                        select c;
                foreach (Customer customer in query)
                {
                    Console.WriteLine(customer);
                }
                // customer year
                Console.WriteLine("-----2001----");
                var query1 = from c in customers
                    where c.DateOfBirth.Year ==2001
                    select c;
                foreach (Customer customer in query1)
                {
                    Console.WriteLine(customer);
                }
                
                // sda
                Console.WriteLine("-----I-----");
                var query2 = from c in customers
                    where c.FirstName.StartsWith("I")
                        select c;
                foreach (Customer customer in query2)
                {
                    Console.WriteLine(customer);
                }
                // insert
                Console.WriteLine("----new----");
                var cust =  new Customer();
                cust.FirstName = "Ivan";
                cust.LastName = "Petrov";
                cust.DateOfBirth = new DateTime(2023, 04, 03);
                customers.InsertOnSubmit(cust);
                dataContext.SubmitChanges();
                foreach (Customer customer in query2)
                {
                    Console.WriteLine(customer);
                }
                
                // edit
                Console.WriteLine("----edit----");
                Customer c_edit = customers.Where(c => c.id == 2).First();
                c_edit.LastName += "_redacted";
                Console.WriteLine(c_edit);
                dataContext.SubmitChanges();
                Console.WriteLine("+++++++++++++++++++++++++++++++++++");
                foreach (Customer item in customers)
                {
                    Console.WriteLine(item);
                }
            }
            
        }
    }
}