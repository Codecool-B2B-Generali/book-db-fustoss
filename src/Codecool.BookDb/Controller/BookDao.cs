using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Codecool.BookDb.Model;

namespace Codecool.BookDb.Controller
{
    public class BookDao : IBookDao
    {
        private string connectionString;
        private List<Book> Books { get; set; } = new List<Book>();
        private List<Author> Authors { get; set; } = new List<Author>();
        private AuthorDao authorDao = new AuthorDao();

        public BookDao()
        {
            connectionString = new BookDbManager().Connect();
        }

        public void Add(Book book)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string queryString = $"Insert Into dbo.book (author_id, title) " +
                    $"Values ('{book.Author.Id}', '{book.Title}');";
                SqlCommand command = new SqlCommand(queryString, connection);
                SqlDataReader reader = command.ExecuteReader();
                reader.Close();
            }
        }

        public Book Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Book> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                Authors = authorDao.GetAll();
                connection.ConnectionString = connectionString;
                connection.Open();
                string queryString = "Select * From dbo.book;";
                SqlCommand command = new SqlCommand(queryString, connection);
                SqlDataReader reader = command.ExecuteReader();
                {
                    while (reader.Read())
                    {
                        var id = (int)reader["id"];
                        var authorId = (int)reader["author_id"];
                        var book = new Book(Authors.Find(author => author.Id == authorId),
                                           (string)reader["title"],'N');
                        book.Id = id;
                        Books.Add(book);
                    }
                }
                reader.Close();
                return Books;
            }
        }

        public void Update(Book book)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string queryString =
                    $"Update dbo.book Set author_id ='{book.Author.Id}', " +
                    $"title='{book.Title}' " +
                    $"where id = {book.Id};";
                SqlCommand command = new SqlCommand(queryString, connection);
                SqlDataReader reader = command.ExecuteReader();
                reader.Close();
            }
        }

        public bool Delete(Book selectedBook)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string queryString =
                    $"Delete dbo.book where id = {selectedBook.Id};";
                SqlCommand command = new SqlCommand(queryString, connection);
                try
                {
                    SqlDataReader reader = command.ExecuteReader();
                    return true;
                }
                catch (SqlException e)
                {
                    return false;
                }
            }
        }
    }
}
