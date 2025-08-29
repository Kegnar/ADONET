using System;

namespace Lesson_4_DataLayer.Models
{
    public class CustomerModel
    {
        public int Id { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }

        public CustomerModel(int id, string firstName, string lastName, DateTime dateOfBirth)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
        }

        public override string ToString()
        {
            return $"{Id,4} {FirstName,-15} {LastName,-15} {DateOfBirth.ToShortDateString(),-15}";
        }
        
    }
}