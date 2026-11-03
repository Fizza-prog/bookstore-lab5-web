using System.Diagnostics;
using bookstoreLab5.Models;
using Microsoft.AspNetCore.Mvc;
using bookstoreLab5.Models;
using bookstoreLab5.ViewModels;
using System.Linq;
using System.Collections.Generic;

namespace bookstoreLab5.Controllers
{
    public class HomeController : Controller
    {

        public static List<Author> Authors = new List<Author>
        {
         new Author { Id = 1, Name = "J.K. Rowling", Country = "United Kingdom" },
         new Author { Id = 2, Name = "George Orwell", Country = "United Kingdom" }
        };

        public static List<Book> Books = new List<Book>
        {
         new Book { Id = 1, Title = "Harry Potter", Price = 1500, AuthorId = 1 },
         new Book { Id = 2, Title = "1984", Price = 1200, AuthorId = 2 },
         new Book { Id = 3, Title = "Animal Farm", Price = 1000, AuthorId = 2 }
        };

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        public IActionResult Index()
        {
            HomeViewModel homeViewModel = new HomeViewModel
            {
                Authors = Authors,

                Books = Books.Select(book => new BookCardViewModel
                {
                    Id = book.Id,
                    Title = book.Title,
                    Price = book.Price,

                    AuthorName = Authors
                        .FirstOrDefault(a => a.Id == book.AuthorId)?.Name
                }).ToList()
            };

            return View(homeViewModel);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    
    
[HttpPost]
        public IActionResult AddBook(string title, decimal price, int authorId)
        {
            int newId = Books.Count == 0 ? 1 : Books.Max(b => b.Id) + 1;

            Books.Add(new Book
            {
                Id = newId,
                Title = title,
                Price = price,
                AuthorId = authorId
            });

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult AddAuthor(string name, string country)
        {
            int newId = Authors.Count == 0 ? 1 : Authors.Max(a => a.Id) + 1;

            Authors.Add(new Author
            {
                Id = newId,
                Name = name,
                Country = country
            });

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult RemoveBook(int id)
        {
            var book = Books.FirstOrDefault(b => b.Id == id);

            if (book != null)
            {
                Books.Remove(book);
            }

            return RedirectToAction("Index");
        }

    }
}
