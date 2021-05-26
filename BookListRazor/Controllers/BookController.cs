using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/*

 API JSON

 */
namespace BookListRazor.Controllers
{
    //add Route to call this controller
    [Route("api/Book")]
    //states that "BookContrller" class (this file) is a book controller
    [ApiController]
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _db;
        public BookController(ApplicationDbContext db)
        {
            _db = db;

        }

        /*Get All Books*/
        /*When using async, it needs entity framework core*/
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json( new { data = await _db.Book.ToListAsync()});
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id) 
        {
            Book bookToDelete = await _db.Book.FirstOrDefaultAsync(b => b.Id == id);
            if (bookToDelete == null) return Json(new { success = false, message = "Error while deleting" });

             _db.Book.Remove(bookToDelete);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Book was deleted" });

        }
    }
}
