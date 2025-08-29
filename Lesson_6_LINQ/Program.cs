using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;

namespace Lesson_6_LINQ
{
    public class Student
    {
        public int Age { get; set; }
        public string FN { get; set; }
        public string LN { get; set; }
        public string City { get; set; }

        public override string ToString()
        {
            return $"{FN,15} {LN,15} {City,15} {Age,5}";
        }
    }

    internal class Program
    {
        public static void Func_student_create(List<Student> students)
        {
            Random rnd = new Random();
            string[] city = { "c2", "c4", "c5", "c10", "c1", "c123", "c45" };

            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(200);
                students.Add(new Student
                    { FN = "N" + i, LN = "LN" + i, City = city[rnd.Next(0, city.Length - 1)], Age = rnd.Next(16, 20) });
            }
        }

        public static void Find_linq_student(List<Student> students)
        {
            var res = from s in students
                where s.Age >= 17
                orderby s.Age
                select new { s.LN, s.Age };

            foreach (var r in res)
            {
                Console.WriteLine(r);
            }
        }

        public static double AverageAge(List<Student> students)
        {
            var avg = students.Select(s => s.Age).Average();
            return avg;
        }

        public static int MinAge1(List<Student> students)
        {
            var min = students.Select(s => s.Age).Min();
            return min;
        }

        public static int MinAge2(List<Student> students)
        {
            var minAge = students.Min(s => s.Age);
            Console.WriteLine(students.First(s => s.Age == minAge));
            return minAge;
        }

        public static List<Student> MinAgeStudents(List<Student> students)
        {
            var minAge = MinAge1(students);
            var result = from student in students
                where student.Age > minAge
                orderby student.Age
                select student;
            List<Student> resultStudents = result.ToList();
            return resultStudents;
        }


        public static void Main(string[] args)
        {
            // string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            // using (SqlConnection connection = new SqlConnection(connectionString)) ;

            // Linq To Object

            // List<Student> students = new List<Student>();
            // Func_student_create(students);
            // foreach (Student student in students)
            // {
            //     Console.WriteLine(student);
            // }
            // Find_linq_student(students);
            //
            // Console.WriteLine($"Средний возраст студентов: {AverageAge(students)}");
            // Console.WriteLine($"Минимальный возраст студента: {MinAge1(students)}");
            // Console.WriteLine($"Минимальный возраст студента#2: {MinAge2(students)}");
            // Console.WriteLine("---");
            // List<Student> students2 = MinAgeStudents(students);
            // foreach (Student student in students2)
            // {
            //     Console.WriteLine(student);
            // }

            string[] color = { "red", "green", "blue", "red", "yellow", "black", "blue", "brown", "green", "yellow" };
            var res = from c in color
                where c.Length > 3
                select c;
            foreach (var item in res)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("----");
            var res1 = (from c in color
                where c.Length > 3
                orderby c.Length descending //asc desc
                select c).Distinct();
            foreach (var item in res1)
            {
                Console.WriteLine(item);
            }
        }
    }
}