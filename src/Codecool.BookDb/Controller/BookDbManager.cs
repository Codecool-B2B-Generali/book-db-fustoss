using System.Configuration;

namespace Codecool.BookDb.Controller
{
    class BookDbManager
    {
        public string Connect()
        {
            return ConfigurationManager.AppSettings["connectionString"];
        }
    }
}
