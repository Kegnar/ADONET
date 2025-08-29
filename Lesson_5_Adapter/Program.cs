using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Lesson_5_Adapter
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                // через select
                string query = "select * from Position order by 2";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);
                DataSet ds = new DataSet();
                string dsTableName = "Positions";
                adapter.Fill(ds,dsTableName );
                DataTable dt = ds.Tables[dsTableName];
                foreach (DataRow dr in dt.Rows)
                {
                    Console.WriteLine($"{dr[0],5} - {dr[1],20}");
                }
                
                // через хп
                Console.WriteLine("-------------");
                SqlDataAdapter adapter2 = new SqlDataAdapter();
                string ds2TableName = "Customers";
                adapter2.InsertCommand = new SqlCommand("sp_customer_all", connection);
                adapter2.InsertCommand.CommandType = CommandType.StoredProcedure;
                adapter2.SelectCommand = adapter2.InsertCommand;
                SqlCommandBuilder adapterBuilder = new SqlCommandBuilder(adapter2);
                DataSet ds2 = new DataSet();
                adapter2.Fill(ds2, ds2TableName);
                DataTable dt2 = ds2.Tables[ds2TableName];
                foreach (DataRow dr in dt2.Rows)
                {
                    Console.WriteLine($"{dr[0],5} - {dr[1],20} - {Convert.ToDateTime(dr[3]).ToShortDateString(),15}");
                }
                
                //add customer
                // string srcTableName = "Customers";
                // SqlDataAdapter customerAdapter = new SqlDataAdapter("select * from Customers", connection);
                // SqlCommandBuilder customerBuilder = new SqlCommandBuilder(customerAdapter);
                // DataSet customers = new DataSet();
                // customerAdapter.Fill(customers, srcTableName);
                // DataTable customerTable = customers.Tables[srcTableName];
                // DataRow newCustomer = customerTable.NewRow();
                // newCustomer[1] = "Биба";
                // newCustomer[2] = "Бобов";
                // newCustomer[3] = DateTime.Now.ToShortDateString();
                // customerTable.Rows.Add(newCustomer);
                //
                // // отсюда начинается апдейт базы
                //
                // customerAdapter.Update(customers, srcTableName);
                // customerTable.Clear();
                // customerAdapter.Fill(customers, srcTableName);
                //
// delete
                string srcTableName = "Customers";
                SqlDataAdapter customerAdapter = new SqlDataAdapter("select * from Customers", connection);
                SqlCommandBuilder customerBuilder = new SqlCommandBuilder(customerAdapter);
                DataSet customers = new DataSet();
                customerAdapter.Fill(customers, srcTableName);
                DataTable customerTable = customers.Tables[srcTableName];
                customerTable.PrimaryKey = new DataColumn[] { customerTable.Columns["id"] };
                DataRow deleteRow = customerTable.Rows.Find(5);
                if (deleteRow != null)
                {
                    deleteRow.Delete();
                    Console.WriteLine("Row deleted");
                }
                else
                {
                    Console.WriteLine("Row not found");
                }
                // удоли!
                customerAdapter.Update(customers, srcTableName);
                customerTable.Clear();
                customerAdapter.Fill(customers, srcTableName);
                

            }
        }
    }
}