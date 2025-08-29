﻿using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;
namespace Lesson_7_Linq_To_XML.Entity
{
    [Table(Name = "Customers")] 
    public class Customer
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int id { get; set; }
        [Column]
        public string FirstName { get; set; }
        [Column]
        public string LastName { get; set; }
        [Column(CanBeNull = true)]
        public DateTime DateOfBirth { get; set; }
        public override string ToString()
        {
            return $"{id,5} {LastName,20}, {FirstName,20} {Convert.ToDateTime(DateOfBirth).ToShortDateString(),20}";
        }
    }
}