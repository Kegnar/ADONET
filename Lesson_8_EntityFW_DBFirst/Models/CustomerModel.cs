using System;

namespace Lesson_8_EntityFW_DBFirst.Models
{
    public class CustomerModel
    {
        public int id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public byte[] Picture { get; set; }
      
        public override string ToString()
        {
            return $"{id,5} {LastName,20}, {FirstName,20} {Convert.ToDateTime(DateOfBirth).ToShortDateString(),20}";
        }
    }
}
   