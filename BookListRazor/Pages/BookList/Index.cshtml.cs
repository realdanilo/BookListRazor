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
    public class IndexModel : PageModel
    {
        
        private readonly ApplicationDbContext _db;

        //constructor gets db context from pipeline, then assign to private var so we can use database
        public IndexModel(ApplicationDbContext db)
        {
            _db = db; 
        }

        public IEnumerable<Book> Books { get; set; }
        public async Task OnGet()
        {   
            //using Entity FrameworkCore
            Books = await _db.Book.ToListAsync();
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            Book toDeleteBook = await _db.Book.FindAsync(id);
            if (toDeleteBook==null) { return NotFound(); }
            _db.Book.Remove(toDeleteBook);
            await _db.SaveChangesAsync();
            return RedirectToPage("Index");

        }
    }
}
