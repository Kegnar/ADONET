using System;
using System.Configuration;
using System.Data.SqlClient;

namespace Lesson_1_HW
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                // ExecuteReader
                SqlCommand command = new SqlCommand("SELECT * FROM Customers", connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    Console.WriteLine($"{reader.GetName(0)}\t{reader.GetName(1)}\t{reader.GetName(2)}\t{reader.GetName(3)}");
                    while (reader.Read())
                    {
                        Console.WriteLine(
                            $"{reader.GetValue(0)}\t{reader.GetValue(1)}\t\t{reader.GetValue(2)}\t\t{reader.GetValue(3)}");
                    }
                }   
                reader.Close();
                
                // ExecuteScalar
                    string sqlComm2 = "select sum(Salary) as SalaryTotal from Employee";
                    SqlCommand cmd2 = new SqlCommand(sqlComm2, connection);
                    object result = cmd2.ExecuteScalar(); 
                    Console.WriteLine($"Зарплатный фонд = {result}");
                    
                //ExecuteNonQuery
                string sqlComm3 = "delete from Employee where EmployeeID=1004"; // грохнем тестового сотрудника с id=1004
                SqlCommand cmd3 = new SqlCommand(sqlComm3, connection);
                int result2 = cmd3.ExecuteNonQuery();
                Console.WriteLine($"Удалено сотрудников:{result2}");
                
                connection.Close();
                
                
            }
        }
    }
}