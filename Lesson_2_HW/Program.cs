using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

//BUG: починить работу вызова sp_alter_quantity
namespace Lesson_2_HW
{
    internal class Program
    {
        public static void Main(string[] args)
        {string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                
                /* поиск в БД товара по названию
                //-------------
                // create procedure sp_find_good_by_name @name nvarchar(200)
                //     as
                //     select *
                // from Goods
                // where name like @name;
                 //-------------
                 */
                Console.WriteLine("Введите название товара:");
                string goodName = Console.ReadLine();
                string findGoods = "sp_find_good_by_name";
                SqlCommand cmd = new SqlCommand(findGoods, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter param = new SqlParameter("@name",goodName);
                cmd.Parameters.Add(param);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    Console.WriteLine($"{reader.GetName(0)}\t{reader.GetName(1)}\t{reader.GetName(2)}\t{reader.GetName(3)}");
                    while (reader.Read())
                    {
                        Console.WriteLine(
                            $"{reader.GetValue(0)}\t{reader.GetValue(1)}\t{reader.GetValue(2)}\t{reader.GetValue(3)}");
                    }
                }
                reader.Close();
                
                /* изменение количества на складе
                 -----------------
                 create procedure sp_alter_quantity @good_id int,
                 @stock_quantity int
                     as
                     begin try
                 begin transaction
                 update Goods
                 set stock_qty = @stock_quantity
                 where id = @good_id
                 commit
                     end try
                 begin catch
                 print N'Ошибка'
                 end catch
                 go
                -----------------
                */
                string modifyStock = "sp_alter_quantity";
                string qweryStock = "select * from Goods where id=@id";
                Console.Write("Введи id товара:");
                int id = Convert.ToInt32(Console.ReadLine());
                Console.Write("Введи количество:");
                int qty = Convert.ToInt32(Console.ReadLine());
                
                SqlCommand cmd2 = new SqlCommand(modifyStock, connection);
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.AddWithValue("@good_id", id);
                cmd2.Parameters.AddWithValue("@stock_quantity", qty);
                cmd2.ExecuteNonQuery();
                
                // прочитаем содержимое
                SqlCommand qweryStock = new SqlCommand(qweryStock, connection);
                SqlParameter goodId = new SqlParameter("@id", id);
                qweryStock.Parameters.Add(goodId);
                qweryStock.CommandType = CommandType.Text;
                
                SqlDataReader stock = qweryStock.ExecuteReader();
                if (stock.HasRows)
                {
                    Console.WriteLine($"{stock.GetName(1)}\t{stock.GetName(3)}\t{stock.GetName(4)}");
                }
                while (stock.Read())
                {
                    Console.WriteLine($"{stock.GetValue(1)}\t{stock.GetValue(3)}\t{stock.GetValue(4)}");
                }
                
                /* товары по категории
                 ------
                create procedure sp_get_goods_by_category @id_category int --товары по категории
                    as
                        begin try
                            begin transaction
                            select *
                                from Goods as g
                                join Categories_Goods as cg on g.id = cg.id_good
                                where cg.id_category = @id_category
                            commit
                        end try
                        begin catch
                            print N'Ошибка'
                        end catch;
                    go
                    -------
                */
                string spGoodsCategory = "sp_get_goods_by_category";
                Console.Write("Введи категорию товара:");
                
                SqlCommand qweryCategory = new SqlCommand(spGoodsCategory, connection);
                qweryCategory.CommandType = CommandType.StoredProcedure;
                SqlParameter categoryId = new SqlParameter("@id_category", id);
                qweryCategory.Parameters.Add(categoryId);
                qweryCategory.CommandType = CommandType.Text;
                
                SqlDataReader category = qweryCategory.ExecuteReader();
                if (stock.HasRows)
                {
                    Console.WriteLine($"{category.GetName(1)}\t{category.GetName(3)}\t{category.GetName(4)}");
                }
                while (category.Read())
                {
                    Console.WriteLine($"{category.GetValue(1)}\t{category.GetValue(3)}\t{category.GetValue(4)}");
                }
                
            }
        }
    }
}