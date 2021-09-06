using System;
using System.Collections.Generic;
using Codecool.BookDb.Model;
using Codecool.BookDb.View;

namespace Codecool.BookDb.Controller
{
    public class MainViewController
    {
        public List<Book> Books { get; set; } = new List<Book>();
        public List<Author> Authors { get; set; } = new List<Author>();
        
        public MainView mainView = new MainView();
        public BookDao bookDao = new BookDao();
        public AuthorDao authorDao = new AuthorDao();

        public MainViewController()
        {
            ShowMenu();
        }
        public void ShowMenu()
        {
            Authors = authorDao.GetAll();
            Books = bookDao.GetAll();

            char choice;
            do
            {
                mainView.ClearScr();
                choice = mainView.MainMenu();
                switch (choice)
                {
                    case 'a':
                        mainView.ClearScr();
                        ProcessAuthors();
                        break;
                    case 'b':
                        mainView.ClearScr();
                        ProcessBooks();
                        break;
                }
            } while (choice != 'q');
        }
        private void ProcessBooks()
        {
            char choice;
            do
            {
                mainView.ClearScr();
                mainView.showSelection("books");
                choice = mainView.ResourcesMenu();
                switch (choice)
                {
                    case 'l':
                        mainView.ListBooks(Books);
                        mainView.waitForKey();
                        break;
                    case 'a':
                        Book addedBook = mainView.GetNewBook(Authors);
                        Books.Add(addedBook);
                        bookDao.Add(addedBook);
                        Books.Clear();
                        Books = bookDao.GetAll();
                        mainView.waitForKey();
                        break;
                    case 'e':
                        EditBook();
                        mainView.waitForKey();
                        break;
                    case 'd':
                        DeleteBook();
                        Books.Clear();
                        Books = bookDao.GetAll();
                        mainView.waitForKey();
                        break;
                }
            } while (choice != 'q');
        }

        private void EditBook()
        {
            var selectedBook = mainView.SelectBook(Books);
            selectedBook = mainView.GetNewBookData(selectedBook, Authors);
            var index = Books.FindIndex(book => book.Id == selectedBook.Id);
            Books[index] = selectedBook;
            bookDao.Update(selectedBook);
        }

        private void DeleteBook()
        {
            var selectedBook = mainView.SelectBook(Books);
            if (mainView.AreYouShure() == true)
            {
                var index = Books.FindIndex(book => book.Id == selectedBook.Id);
                Books.RemoveAt(index);
                if (bookDao.Delete(selectedBook))
                {
                    mainView.showInfo("Success");
                }
                else
                {
                    mainView.showInfo("Delete failed");
                }
            }
        }

        private void ProcessAuthors()
        {
            char choice;
            do
            {
                mainView.ClearScr();
                mainView.showSelection("authors");
                choice = mainView.ResourcesMenu();
                switch (choice)
                {
                    case 'l':
                        mainView.ListAuthors(Authors);
                        mainView.waitForKey();
                        break;
                    case 'a':
                        Author addedAuthor = mainView.GetNewAuthor();
                        Authors.Add(addedAuthor);
                        authorDao.Add(addedAuthor);
                        Authors.Clear();
                        Authors = authorDao.GetAll();
                        mainView.waitForKey();
                        break;
                    case 'e':
                        EditAuthor();
                        mainView.waitForKey();
                        break;
                    case 'd':
                        DeleteAuthor();
                        Authors.Clear();
                        Authors = authorDao.GetAll();
                        mainView.waitForKey();
                        break;
                }
            } while (choice != 'q');
        }

        private void EditAuthor()
        {
            var selectedAuthor = mainView.SelectAuthor(Authors, Authors[0]);
            selectedAuthor = mainView.GetNewAuthorData(selectedAuthor);   
            var index = Authors.FindIndex(author => author.Id == selectedAuthor.Id);
            Authors[index] = selectedAuthor;
            authorDao.Update(selectedAuthor);
        }

        private void DeleteAuthor()
        {
            var selectedAuthor = mainView.SelectAuthor(Authors, Authors[0]);
            if (mainView.AreYouShure() == true)
            {
                var index = Authors.FindIndex(author => author.Id == selectedAuthor.Id);
                Authors.RemoveAt(index);
                if (authorDao.Delete(selectedAuthor))
                {
                    mainView.showInfo("Success");
                }else
                {
                    mainView.showInfo("Delete failed, delete Author's books first");
                }
            }
        }
    }
}