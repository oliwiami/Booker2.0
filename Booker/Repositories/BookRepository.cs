using System.Collections.Generic;
using System.Linq;
using Booker.Models;
using Microsoft.EntityFrameworkCore;

namespace Booker.Repositories
{
    public class BookRepository : IBooksRepository
    {
        private BookContext context;

        public BookRepository(BookContext context)
        {
            this.context = context;
        }

        public void DeleteBook(int bookID)
        {
            Books book = context.Books.Find(bookID);
            context.Books.Remove(book);
            context.SaveChanges();
        }
        public Books GetBookByID(int bookId)
        {
            return context.Books.Find(bookId);
        }

        public IEnumerable<Books> GetBooks()
        {
            return context.Books.ToList();
        }

        public void InsertBook(Books book)
        {
            context.Books.Add(book);
            context.SaveChanges();
        }

        public void UpdateBook(int bookid, Books book)
        {
            var result = context.Books.SingleOrDefault(x => x.BookId == bookid);
            if (result != null) 
            {
                result.Title = book.Title;
                result.Publisher = book.Publisher;
                result.Publishdate = book.Publishdate;
                result.Author = book.Author;
                result.Quantity = book.Quantity;
                context.SaveChanges();
            }
        }
        public void Save()
        {
            context.SaveChanges();
        }

        public bool SellBook(int bookid, Books book)
        {
            var result = context.Books.SingleOrDefault(x => x.BookId == bookid);
            if (result != null)
            {
                if (result.Quantity > 0)
                {
                    result.Quantity = (result.Quantity - 1);

                    context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
       public IQueryable<Books> GetBooksInStock()
        {
            return context.Books.Where(b => b.Quantity > 0);
        }
    }
}
