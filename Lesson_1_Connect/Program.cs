/*
 * DbConnection - подключение к БД
 * DbCommand - команды БД
 * DbDataReader - чтение данных
 * DbDataAdapter - работа с отсоединенным режимом
 *
 * DataTable
 * DataSet
 *
 * режимы работы
 * DBFirst - работа с существующей БД
 * ModelFirst - создание моделей в существующей БД
 * CodeFirst - чистый код с созданием БД
 *
 */

using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics.Eventing;

namespace Lesson_1_Connect
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            // первый вариант
            
            // string connectionString = "Server=localhost;Database=ADONET;Trusted_Connection=True;";
            // SqlConnection connection =
            //     new SqlConnection();
            // connection.ConnectionString = connectionString;
            // connection.Open();
            
            //
            
            // string connectionString = "Server=localhost\\TEST;Database=ADONET;Trusted_Connection=True;";
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                //ExecuteReader
                // string sqlCommand = "SELECT * FROM dbo.Employee";
                // SqlCommand command = new SqlCommand(sqlCommand, connection);
                // SqlDataReader reader = command.ExecuteReader();
                //
                // while (reader.Read())
                // {
                //     var f0 = reader[0];
                //     var f1 = reader[1];
                //     var f2 = reader[2];
                //     var f3 = reader[3];
                //     var f4 = reader[4];
                //     var f5 = reader[5];
                //
                //     Console.WriteLine($"{f0}\t{f2,10}\t{f5}");
                // }
                // connection.Close();
               
                // ExecuteScalar
            //     string sqlComm2 = "select sum(Salary) as SalaryTotal from Employee";
            //     SqlCommand cmd2 = new SqlCommand(sqlComm2, connection);
            //     object result = cmd2.ExecuteScalar();
            //     Console.WriteLine($"Salary = {result}");
            //
            
            // ExecuteNonQuery
            string sqlComm3 = "insert into Position (PositionName) values ('Director')";
            SqlCommand command = new SqlCommand(sqlComm3, connection);
            command.ExecuteNonQuery();
            int result = command.ExecuteNonQuery();
            Console.WriteLine(result);
            }
        }
    }
}