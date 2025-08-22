using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Lesson_3_HW
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
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
                SqlCommandBuilder.DeriveParameters(cmd);
                cmd.Parameters[1].Value = goodName;
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    Console.WriteLine(
                        $"{reader.GetName(0)}\t{reader.GetName(1)}\t{reader.GetName(2)}\t{reader.GetName(3)}");
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
                 rollback
                 end catch
                 go
                -----------------
                */
                string modifyStock = "sp_alter_quantity";
                Console.Write("Введи id товара:");
                int id = Convert.ToInt32(Console.ReadLine());
                Console.Write("Введи количество:");
                int qty = Convert.ToInt32(Console.ReadLine());

                SqlCommand cmd2 = new SqlCommand(modifyStock, connection);
                cmd2.CommandType = CommandType.StoredProcedure;
                SqlCommandBuilder.DeriveParameters(cmd2);
                cmd2.Parameters[1].Value =  id;
                cmd2.Parameters[2].Value =  qty;
                cmd2.ExecuteNonQuery();

                // прочитаем содержимое
                string qweryStockString = "select * from goods where id=@good_id";
                SqlCommand qweryStock = new SqlCommand(qweryStockString, connection);
                SqlParameter goodId = new SqlParameter("@good_id", id);
                qweryStock.Parameters.Add(goodId);
                qweryStock.CommandType = CommandType.Text;

                SqlDataReader stock = qweryStock.ExecuteReader();
                if (stock.HasRows)
                {
                    Console.WriteLine($"{stock.GetName(1),-30}{stock.GetName(3),-7}\t{stock.GetName(4)}");
                }

                while (stock.Read())
                {
                    Console.WriteLine($"{stock.GetValue(1),-30}{stock.GetValue(3),-7}\t{stock.GetValue(4)}");
                }

                stock.Close();

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
                            rollback
                        end catch;
                    go
                    -------
                */
                string spGoodsCategory = "sp_get_goods_by_category";
                Console.Write("Введи id категории:");
                int idCategory = Convert.ToInt32(Console.ReadLine());
                SqlCommand qweryCategory = new SqlCommand(spGoodsCategory, connection);
                qweryCategory.CommandType = CommandType.StoredProcedure;
                SqlCommandBuilder.DeriveParameters(qweryCategory);
                qweryCategory.Parameters[1].Value = idCategory;
                
                


                SqlDataReader category = qweryCategory.ExecuteReader();
                if (category.HasRows)
                {
                    Console.WriteLine($"{category.GetName(1),-30}{category.GetName(3),-7}{category.GetName(4)}");
                }

                while (category.Read())
                {
                    Console.WriteLine($"{category.GetValue(1),-30}{category.GetValue(3),-7}{category.GetValue(4)}");
                }

                category.Close();
            }
        }
    }
}