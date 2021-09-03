using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Codecool.BookDb.Model;

namespace Codecool.BookDb.Controller
{
    public class AuthorDao : IAuthorDao
    {
        private string connectionString;
        private List<Author> Authors { get; set; } = new List<Author>();

        public AuthorDao()
        {
            connectionString = new BookDbManager().Connect();
        }

        public void Add(Author author)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string queryString = $"Insert Into dbo.author (first_name, last_name, birth_date) " +
                    $"Values ('{author.FirstName}', '{author.LastName}', '{author.BirthDate}');";
                SqlCommand command = new SqlCommand(queryString, connection);
                SqlDataReader reader = command.ExecuteReader();
                reader.Close();
            }
        }

        public Author Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Author> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.ConnectionString = connectionString;
                connection.Open();
                string queryString = "Select * From dbo.author;";
                SqlCommand command = new SqlCommand(queryString, connection);
                SqlDataReader reader = command.ExecuteReader();             
                while (reader.Read())
                {
                    var id = (int)reader["id"];
                    var firstname = (string)reader["first_name"];
                    var lastname = (string)reader["last_name"];
                    var birthdate = (DateTime)reader["birth_date"];
                    var author = new Author(firstname, lastname, birthdate, 'N');
                    author.Id = id;
                    Authors.Add(author);
                }
                return Authors;
            }
        }

        public void Update(Author author)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string queryString =
                    $"Update dbo.author Set first_name ='{author.FirstName}', " +
                    $"last_name='{author.LastName}', " +
                    $"birth_date='{author.BirthDate}' where id = {author.Id};";
                SqlCommand command = new SqlCommand(queryString, connection);
                SqlDataReader reader = command.ExecuteReader();
                reader.Close();
            }
        }

        public bool Delete(Author selectedAuthor)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string queryString =
                    $"Delete dbo.author where id = {selectedAuthor.Id};";
                SqlCommand command = new SqlCommand(queryString, connection);
                try
                {
                    SqlDataReader reader = command.ExecuteReader();
                }catch(SqlException e)
                {
                    return false;
                }
                return true;
            }
        }
    }
}
