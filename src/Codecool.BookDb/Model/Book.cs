namespace Codecool.BookDb.Model
{
    public class Book
    {
        private static int NextId = 1;
        public int Id { get; set; }
        public Author Author { get; set; }
        public string Title { get; set; }

        public char Temporary { get; set; }
        public Book(Author author, string title, char temporary)
        {
            Author = author;
            Title = title;
            if (temporary != 'Y')
            {
                Id = NextId++;
            }
        }

        public override string ToString()
        {
            return new string($"Id: {Id}, Title: {Title}, Author: {Author.FirstName} {Author.LastName}");
        }
    }
}
