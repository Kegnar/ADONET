using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Lesson_3_CommandBuilder
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                
                // sp_cust_add
                
                // string cust_add = "dbo.stp_CustomerAdd";
                // SqlCommand cmd = new SqlCommand(cust_add, connection);
                // cmd.CommandType = CommandType.StoredProcedure;
                // SqlCommandBuilder.DeriveParameters(cmd);
                // cmd.Parameters[4].Value = DBNull.Value;
                // cmd.Parameters[1].Value = "NewFName";
                // cmd.Parameters[2].Value = "NewLName";
                // cmd.Parameters[3].Value = DateTime.Now.AddYears(-1).ToShortDateString();
                // cmd.ExecuteNonQuery();
                // int newId = (int)cmd.Parameters[4].Value;
                // Console.WriteLine(newId);
                
                //sp_cust_add2 with return
                
                string cust_add2 = "dbo.stp_CustomerAdd_2";
                SqlCommand cmd2 = new SqlCommand(cust_add2, connection);
                cmd2.CommandType = CommandType.StoredProcedure;
                SqlCommandBuilder.DeriveParameters(cmd2);
                cmd2.Parameters[0].Value = DBNull.Value;
                cmd2.Parameters[1].Value = "NewFName12";
                cmd2.Parameters[2].Value = "NewLName12";
                cmd2.Parameters[3].Value = DateTime.Now.AddYears(-12).ToShortDateString();
                cmd2.ExecuteNonQuery();
                int newId = (int)cmd2.Parameters[0].Value;
                Console.WriteLine(newId);
                
                
                
            }
        }
    }
}