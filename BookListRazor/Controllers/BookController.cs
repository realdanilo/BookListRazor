using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
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
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json( new { data = _db.Book.ToList()});
        }
    }
}
