/*
 * Entity FW
 * Объектная модель - 
 * Object Relation Mapping -
 * Entity Data Model
 *      Conceptual Model - классы и их взаимоотношения
 *      Mapping - инфо 
 *      Storage Model - инфо о структуре БД
 *      file.edmx - xaml файл с описанием
 *  LINQ to Entities расширение доступа к концептуальной модели
 *  Entity SQL 
 *
 *  Способы:
 *      DBFirst - БД -> EDM -> Сущность
 *      ModelFirst - EDM -> DB -> Query
 *      CodeFirst -> C# -> Classes -> EDM -> DB
 *      
 *      
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using Lesson_8_EntityFW_DBFirst.Models;
using Lesson_8_EntityFW_DBFirst.DataLayer;

namespace Lesson_8_EntityFW_DBFirst
{
    internal class Program
    {
        public static void Main(string[] args)
        {

            {
                var db = new ADONETEntities();
                //All
                IEnumerable<CustomerModel> models = DL.Customer.CustomerAll();
                foreach (CustomerModel model in models)
                {
                    Console.WriteLine(model);
                }
                //ById
                CustomerModel cm = DL.Customer.ById(1);
                Console.WriteLine(cm);

                //Add. Вернет id нового пользователя

                Console.WriteLine(DL.Customer.AddCustomer("111", "222", DateTime.Now));

            }
        }
    }
}