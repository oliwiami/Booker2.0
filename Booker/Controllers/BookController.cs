using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Booker.Models;
using Booker.Repositories;
using Microsoft.AspNetCore.Authorization;
using StackExchange.Redis;
using System;
using System.Linq;



namespace Booker.Controllers
{
    public class BookController : Controller
    {
        private readonly IBooksRepository bookRepository;

        public BookController(IBooksRepository booksRepository)
        {
            bookRepository = booksRepository;
        }

        // GET: BookController
        [Authorize]
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.TitleSortParm = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewBag.AuthorSortParm = String.IsNullOrEmpty(sortOrder) ? "author_desc" : "";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            var books = from b in bookRepository.GetBooks()
                           select b;
            if (!String.IsNullOrEmpty(searchString))
            {
                books = books.Where(b => b.Title.ToUpper().Contains(searchString.ToUpper())
                                       || b.Author.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "title_desc":
                    books = books.OrderByDescending(b => b.Title);
                    break;
                case "author_desc":
                    books = books.OrderByDescending(b => b.Author);
                    break;
                default:
                    books = books.OrderBy(b => b.Title);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(books);
        }


        // GET: BookController/Details/5
        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            return View(bookRepository.GetBookByID(id));
        }


        // GET: BookController/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View(new Books());
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Books bookModel)
        {
            bookRepository.InsertBook(bookModel);
            return RedirectToAction(nameof(Index));
        }
       // [Authorize(Roles = "admin")]
        // GET: BookController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(bookRepository.GetBookByID(id));
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Books bookModel)
        {
           bookRepository.UpdateBook(id, bookModel);
           return RedirectToAction(nameof(Index));
           
        }

        // GET: BookController/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int id)
        {
            return View(bookRepository.GetBookByID(id));
        }

        // POST: BookController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Books bookModel)
        {
            bookRepository.DeleteBook(id);
            return RedirectToAction(nameof(Index));
        }

        //GET: BookController/Buy/5
        [Authorize]
        public ActionResult Sell(int id)
        {
            return View(bookRepository.GetBookByID(id));
        }

        //POST: BookController/Buy/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Sell(int id, Books bookModel)
        {
            if (bookRepository.SellBook(id, bookModel))
            {
                return RedirectToAction(nameof(Index));
            }
            TempData["Message"] = "This book is not in stock";
            return RedirectToAction(nameof(Index));
        }

        // GET: BookController/Catalog
        [AllowAnonymous]
        public ActionResult Catalog(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.TitleSortParm = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewBag.AuthorSortParm = String.IsNullOrEmpty(sortOrder) ? "author_desc" : "";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            var books = from b in bookRepository.GetBooksInStock()
                        select b;
            if (!String.IsNullOrEmpty(searchString))
            {
                books = books.Where(b => b.Title.ToUpper().Contains(searchString.ToUpper())
                                       || b.Author.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "title_desc":
                    books = books.OrderByDescending(b => b.Title);
                    break;
                case "author_desc":
                    books = books.OrderByDescending(b => b.Author);
                    break;
                default:
                    books = books.OrderBy(b => b.Title);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(books);
        }


    }
}
