using System.Linq;
using BookManagement.Data;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BookManagement.Models;
using System;


namespace BookManagement.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var books = _context.Books.ToList();
            return View(books);
        }

        [HttpGet]

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Book book)
        {
            if(ModelState.IsValid)
            {
                try{
                    _context.Books.Add(book);

                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index");
                }catch(Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Something went wrong, {ex.Message}");
                }
            }

            ModelState.AddModelError(string.Empty, $"Something went wrong");
            return View(book);
        }
    }
}