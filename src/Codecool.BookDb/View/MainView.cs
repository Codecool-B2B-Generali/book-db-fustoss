using System;
using Codecool.BookDb.Model;
using Codecool.BookDb.View;
using Codecool.BookDb.Controller;
using System.Collections.Generic;

namespace Codecool.BookDb.View
{
    public class MainView
    {
        UserInterface userInterface = new UserInterface();

        public char MainMenu()
        {
            userInterface.PrintLn("Select items to work with:");
            userInterface.PrintOption('a', "Authors");
            userInterface.PrintOption('b', "Books");
            userInterface.PrintOption('q', "Quit");
            return userInterface.Choice("abq");
        }

        public char ResourcesMenu()
        {
            userInterface.PrintLn("What you want to do with?");
            userInterface.PrintOption('l', "List");
            userInterface.PrintOption('a', "Add");
            userInterface.PrintOption('e', "Edit");
            userInterface.PrintOption('d', "Delete");
            userInterface.PrintOption('q', "Quit");
            return userInterface.Choice("laedq");
        }

        public Author GetNewAuthor()
        {
            string firstName = userInterface.ReadString("Please enter author's first name:", "Unknown");
            string lastName = userInterface.ReadString("Please enter author's last name:", "Unknown");
            DateTime birthDate = userInterface.ReadDate("Please enter author's birth date:", DateTime.Parse("1900.01.01"));
            return (new Author(firstName, lastName, birthDate,'N'));
        }

        public void showSelection(string selectedItem)
        {
            userInterface.PrintLn($"You selected {selectedItem}.");
        }

        public void showInfo(string text)
        {
            userInterface.PrintLn($"{text}");
        }

        public Author GetNewAuthorData(Author author)
        {
            var changedAuthor = new Author(author.FirstName, author.LastName, author.BirthDate, 'Y');
            changedAuthor.Id = author.Id;
            changedAuthor.FirstName = userInterface.ReadString("Please enter author's first name, if changed:", author.FirstName);
            changedAuthor.LastName = userInterface.ReadString("Please enter author's last name if changed:", author.LastName);
            changedAuthor.BirthDate = userInterface.ReadDate("Please enter author's birth date if changed:", author.BirthDate);
            return (changedAuthor);
        }

        internal void waitForKey()
        {
            userInterface.PrintLn("\nPress a key to continue...");
            userInterface.ReadAnyKey();
        }

        public Book GetNewBook(List<Author> authors)
        {
            Author author = SelectAuthor(authors, authors[0]);
            string title = userInterface.ReadString("Please enter new book's title:", "Unknown");
            return (new Book(author, title,'N'));
        }

        public Book GetNewBookData(Book book, List<Author> authors)
        {
            var changedBook = new Book(book.Author, book.Title,'Y');
            changedBook.Author = SelectAuthor(authors, book.Author);
            changedBook.Title = userInterface.ReadString("Please enter title, if changed:", book.Title);
            changedBook.Id = book.Id;
            return (changedBook);
        }

        public Author SelectAuthor(List<Author> authors, Author defaultAuthor)
        {
            Author selectedAuthor;
            ListAuthors(authors);
            var selection = userInterface.ReadInt("Select Author ID", defaultAuthor.Id);
            if(selection == 0)
            {
                selectedAuthor = defaultAuthor;
                return selectedAuthor;
            }
            selectedAuthor = authors.Find(author => author.Id == selection);
            while (selectedAuthor == null)
            {
                userInterface.PrintLn("Invalid ID, please retry.");
                selection = userInterface.ReadInt("Select Author ID", 1);
                selectedAuthor = authors.Find(author => author.Id == selection);
            }
            return selectedAuthor;
        }

        public Book SelectBook(List<Book> books)
        {
            Book selectedBook;
            ListBooks(books);
            var selection = userInterface.ReadInt("Select Book ID to edit", 1);
            selectedBook = books.Find(book => book.Id == selection);
            while (selectedBook == null)
            {
                userInterface.PrintLn("Invalid ID, please retry.");
                selection = userInterface.ReadInt("Select Book ID to edit", 1);
                selectedBook = books.Find(book => book.Id == selection);
            }
            return selectedBook;
        }

        public void ListBooks(List<Book> books)
        {
            if (books.Count == 0)
            {
                userInterface.PrintLn("No book entered yet");
                return;
            }
            foreach (var book in books)
            {
                userInterface.PrintLn("List of books:");
                userInterface.PrintLn(book);
            }
        }
        public void ListAuthors(List<Author> authors)
        {
            if(authors.Count == 0)
            {
                userInterface.PrintLn("No authors entered yet");
                return;
            }
            userInterface.PrintLn("List of authors:");
            foreach (var author in authors)
            {
                userInterface.PrintLn(author);
            }
        }

        public bool AreYouShure()
        {
            string shure = string.Empty;
            while (shure != "y" && shure != "n")
            {
                shure = userInterface.ReadString("Are you shure? y/n", string.Empty);
            }
            if (shure == "y") 
                return true;
            else 
                return false;
        }
    }
}
