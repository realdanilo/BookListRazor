using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookListRazor.Pages.BookList
{
    public class UpsertModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public UpsertModel(ApplicationDbContext db)
        {
            _db = db;
        }

        //binds property from frontend to backend, vice versa when sending info
        [BindProperty]
        public Book Book {get; set;} 
        public async Task<IActionResult> OnGet(int? id)
        {
            Book = new Book();
            if (id == null) return Page();


            //also works
            //Book = await _db.Book.FindAsync(id);
            Book = await _db.Book.FirstOrDefaultAsync(b => b.Id == id);
            if (Book == null) return NotFound();
            return Page();
        }


        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                if(Book.Id == 0)
                {
                    _db.Book.Add(Book);
                }
                else
                {
                    _db.Book.Update(Book);
                }
                await _db.SaveChangesAsync();
            }
            return RedirectToPage();
        }
    }
}