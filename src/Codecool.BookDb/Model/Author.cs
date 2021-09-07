using System;

namespace Codecool.BookDb.Model
{
    public class Author
    {
        private static int NextId = 1;
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public char Temporary { get; set; }

        public Author(string firstName, string lastName, DateTime birthDate)
        {
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
            Id = NextId++; 
        }
        
        public override string ToString()
        {
            return new string($"Id: {Id}, Name: {FirstName} {LastName}, Birth date: {BirthDate:yyyy/MM/dd}"); 
        }
    }
}
