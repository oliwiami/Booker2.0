using System.Collections.Generic;
using System.Linq;
using Booker.Models;

namespace Booker.Repositories
{
    public interface IBooksRepository
    {
       // Books Get(int bookId);
        IEnumerable<Books> GetBooks();
        Books GetBookByID(int bookId);
        void InsertBook(Books book);
        void DeleteBook(int bookID);
        void UpdateBook(int bookId, Books book);
        bool SellBook(int bookId, Books book);
        IQueryable<Books> GetBooksInStock();

    }
}
