using System;
using System.Collections.Generic;
using System.Configuration;
using Lesson_4_DataLayer.Models;
using System.Data;
using System.Data.SqlClient;

namespace Lesson_4_DataLayer.DataLayer
{
    public class DL
    {
        public static string ConnectionString { get; private set; } =
            ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        static SqlConnection connection;

        public static class Customer
        {
            public static CustomerModel GetCustomerById(int customerId)
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    SqlCommand getCustomer = new SqlCommand("stp_CustomerByID", connection);
                    getCustomer.Parameters.AddWithValue("@customerID", customerId);
                    getCustomer.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = getCustomer.ExecuteReader();
                    CustomerModel customer = null;
                    while (reader.Read())
                    {
                        int id = (int)reader[0];
                        string firstName = reader[1].ToString();
                        string lastName = reader[2].ToString();
                        DateTime birthDate = DateTime.Parse(reader[3].ToString());
                        customer = new CustomerModel(id, firstName, lastName, birthDate);
                    }

                    reader.Close();
                 
                    return customer;
                }
            }

            public static int InsertCustomer(CustomerModel customerModel)
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    string customerAdd = "stp_CustomerAdd";
                    SqlCommand insertCustomer = new SqlCommand(customerAdd, connection);
                    insertCustomer.CommandType = CommandType.StoredProcedure;
                    SqlCommandBuilder.DeriveParameters(insertCustomer);
                    insertCustomer.Parameters[4].Value = DBNull.Value;
                    insertCustomer.Parameters[1].Value = customerModel.FirstName;
                    insertCustomer.Parameters[2].Value = customerModel.LastName;
                    insertCustomer.Parameters[3].Value = customerModel.DateOfBirth;
                    insertCustomer.ExecuteNonQuery();
                    int newId = (int)insertCustomer.Parameters[4].Value;
                    
                    return newId;
                }
            }

            public static List<CustomerModel> GetAllCustomers()
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString)) 
                {
                    List<CustomerModel> customers = new List<CustomerModel>();
                    connection.Open();
                    string customerAll = "dbo.sp_Customer_All";
                    SqlCommand cmd = new SqlCommand(customerAll, connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        CustomerModel customer = new CustomerModel(Convert.ToInt32(reader[0]), reader[1].ToString(),
                            reader[2].ToString(), DateTime.Parse(reader[3].ToString()));
                        customers.Add(customer);
                    }
                    reader.Close();
                    
                    return customers;
                }
            }

            public static void UpdateCustomer(int customerId, CustomerModel customerModel)
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    try
                    {
                        connection.Open();
                        string customerUpdate = "sp_update_customer";
                        SqlCommand updateCustomer = new SqlCommand(customerUpdate, connection);
                        updateCustomer.CommandType = CommandType.StoredProcedure;
                        SqlCommandBuilder.DeriveParameters(updateCustomer);
                        updateCustomer.Parameters[1].Value = customerModel.Id;
                        updateCustomer.Parameters[2].Value = customerModel.FirstName;
                        updateCustomer.Parameters[3].Value = customerModel.LastName;
                        updateCustomer.Parameters[4].Value = customerModel.DateOfBirth;
                        updateCustomer.ExecuteNonQuery();
                        
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        throw;
                    }
                    
                }
            }
        }
    }
}