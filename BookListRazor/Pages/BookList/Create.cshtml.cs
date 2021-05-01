using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookListRazor.Pages.BookList
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }
        //this gets the book and gives it to onGet controll automatically, but needs to be called
        [BindProperty]
        public Book Book { get; set; }
        public void OnGet()
        {
        }

        //[BindProperty] allows OnPost to get Book Model auto, not calling needed
        public async Task<IActionResult> OnPost() 
        {
            if (ModelState.IsValid)
            {
                //adds to queue
                await _db.Book.AddAsync(Book);
                //add to db
                await _db.SaveChangesAsync();

                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
