using BookMvc.ViewModels;
using Infrastracture.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookMvc.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _service;
        private readonly IAuthorService _authorService;
        private readonly IPublishingHouseService _pingHouseService;

        public BookController(IBookService service, IAuthorService authorService, IPublishingHouseService pingHouseService)
        {
            _service = service;
            _authorService = authorService;
            _pingHouseService = pingHouseService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var books = await _service.GetAll();
            return View(books);
        }
        [HttpGet("/Book/Details/{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            if (!User.IsInRole("Admin"))
            {
                TempData["ErrorMessage"] = "Nie masz uprawnień do wykonania tej operacji.";
                return RedirectToAction("Index", "Book");
            }
            var car = await _service.GetById(id);
            if (car is null) return NotFound();
            return View(car);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            if (!User.IsInRole("Admin"))
            {
                TempData["ErrorMessage"] = "Nie masz uprawnień do wykonania tej operacji.";
                return RedirectToAction("Index", "Book");
            }
            var authors = await _authorService.GetAll();
            if (authors == null)
            {
                return NotFound();
            }

            var viewModel = new BookViewModel
            {
                Authors = authors
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(BookViewModel model)
        {
            if (!User.IsInRole("Admin"))
            {
                TempData["ErrorMessage"] = "Nie masz uprawnień do wykonania tej operacji.";
                return RedirectToAction("Index", "Book");
            }
            try
            {
                if (ModelState.IsValid)
                {
                    var newBook = model.Book;
                    await _service.Create(newBook);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                var authors = await _authorService.GetAll();
                if (authors == null)
                    return NotFound();

                var viewModel = new BookViewModel
                {
                    Book = model.Book,
                    Authors = authors
                };

                return View(viewModel);
            }

            return View(model);
        }
        [HttpGet("/Book/Update/{id:int}")]
        public async Task<IActionResult> Update(int id)
        {
            if (!User.IsInRole("Admin"))
            {
                TempData["ErrorMessage"] = "Nie masz uprawnień do wykonania tej operacji.";
                return RedirectToAction("Index", "Book");
            }
            var book = await _service.GetById(id);
            var authors = await _authorService.GetAll();
            if (book is null || authors is null) return NotFound();
            return View(new BookViewModel { Book = book, Authors = authors });
        }

        [HttpPost]
        public async Task<IActionResult> Update(BookViewModel model)
        {
            if (!User.IsInRole("Admin"))
            {
                TempData["ErrorMessage"] = "Nie masz uprawnień do wykonania tej operacji.";
                return RedirectToAction("Index", "Book");
            }
            try
            {
                var book = model.Book;
                await _service.Update(book);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                var authors = await _authorService.GetAll();
                if (authors == null) return NotFound();
                return View(new BookViewModel { Book = model.Book, Authors = authors });
            }
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (!User.IsInRole("Admin"))
            {
                TempData["ErrorMessage"] = "Nie masz uprawnień do wykonania tej operacji.";
                return RedirectToAction("Index", "Book");
            }
            await _service.Delete(id);
            return RedirectToAction("Index");
        }
    }
}

