using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Booker.Models;
using Booker.Repositories;
using Microsoft.AspNetCore.Authorization;
using StackExchange.Redis;

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
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(bookRepository.GetBooks());
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

    }
}
