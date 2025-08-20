using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Lesson_2_StoredProceduresCall
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                //     string customerAll = "dbo.sp_Customer_All";
                //     SqlCommand cmd = new SqlCommand(customerAll, connection);
                //     cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //     SqlDataReader reader = cmd.ExecuteReader();
                //     reader = cmd.ExecuteReader();
                //     while (reader.Read())
                //     {
                //         Console.WriteLine($"{reader["id"]} {reader["LastName"]}");
                //     }
                //     reader.Close();
                //
                //
                //insert_add

                string cust_add = "dbo.stp_CustomerAdd";
                SqlCommand cmd = new SqlCommand(cust_add, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FirstName", "Вася");
                cmd.Parameters.AddWithValue("@LastName", "Пупкин");
                cmd.Parameters.AddWithValue("@BirthDate", DateTime.Now.ToShortDateString());
                SqlParameter cust_id = cmd.Parameters.Add("@CustomerID", SqlDbType.Int);
                cust_id.Direction = ParameterDirection.Output; // выходной параметр
                cmd.ExecuteNonQuery();
                Console.WriteLine((int)cust_id.Value);

                // поиск по id
                string searchEmployeeId = "sp_Employee_By_Id";
                SqlCommand cmd2 = new SqlCommand(searchEmployeeId, connection);
                cmd2.CommandType = CommandType.StoredProcedure;
                // вариант 1
                // SqlParameter employeeId = cmd2.Parameters.Add("@employee_id", SqlDbType.Int);
                // employeeId.Value = 4;

                // вариант 2
                // cmd2.Parameters.AddWithValue("@customer_id", 3);
                // SqlDataReader reader = cmd2.ExecuteReader();
                //
                // while (reader.Read())
                // {
                //     Console.WriteLine($"{reader.GetValue(0)}\t{reader.GetValue(2)}\t{reader.GetValue(5)}");
                // }

                // вариант 3
                string deleteString = "sp_customer_delete";
                
                SqlCommand cmd3 = new SqlCommand(deleteString, connection);
                cmd3.CommandType = CommandType.StoredProcedure;
                cmd3.Parameters.AddWithValue("@customer_id", 1012);
                int affectedRecords = cmd3.ExecuteNonQuery();
                if (affectedRecords > 0)Console.WriteLine($"Удалено записей:{affectedRecords}");
                else
                {
                    Console.WriteLine("нечего удалять");
                }
            }
        }
    }
}