using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookListRazor.Pages.BookList
{
    public class EditModel : PageModel
    {
        private ApplicationDbContext _db;

        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Book Book { get; set; }

        /*It will pass Book to IActionResult automatically*/
        public async Task OnGet(int id)
        {
            Book = await _db.Book.FindAsync(id);

        }

        //binding the property for Book, no need  to get params, modelisvalid will check
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                Book bookFromDb = await _db.Book.FindAsync(Book.Id);
                bookFromDb.Author = Book.Author;
                bookFromDb.Name= Book.Name;
                bookFromDb.ISBN = Book.ISBN;
                await _db.SaveChangesAsync();

                return RedirectToPage("Index");

            }
            //returns to previous page auto, no need to add extra middleware
            return RedirectToPage();

        }
    }
}
