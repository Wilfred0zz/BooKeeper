#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BooKeeper.Models;

namespace BooKeeper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly APIDBContext _context;

        public BooksController(APIDBContext context)
        {
            _context = context;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<Response>> GetBooks()
        {
            var books = await _context.Books.ToListAsync();
            var result = new Response();
            result.StatusCode = 400;
            result.StatusDescription = " Cannot fetch list of Books check database if table exists";
            if (books != null)
            {
                result.StatusCode = 200;
                result.StatusDescription = "Sucess. List of books fetched!";

                foreach (Book bookIt in books)
                {
                    result.BookResponse.Add(bookIt);
                }
                //return result;
            }
            else
            { result.BookResponse = null; }
            return result;
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Response>> GetBooks(int id)
        {
            var books = await _context.Books.FindAsync(id);
            var result = new Response();
            result.StatusCode = 404;
            result.StatusDescription = " Cannot find specificed book for ID given. ";


            if (books != null)
            {
                result.StatusCode = 202;
                result.StatusDescription = "Sucess. List of author info fetched!";
                result.BookResponse.Add(books);
            }
            else
            { result.BookResponse = null; }


            return result;
        }

        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<Response>> PutBooks(int id, Book books)
        {
            var result = new Response();
            result.StatusCode = 404;
            result.StatusDescription = " Cannot find specified Book for ID given. ";

            if (id != books.BookId)
            {
                result.StatusDescription = " ID does not match one in database ";
                return result;
            }

            _context.Entry(books).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                result.StatusCode = 200;
                result.StatusDescription = " Success. Updated information for book of that id. ";

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BooksExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return result;
        }

        // POST: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Response>> PostBooks(Book books)
        {
            var result = new Response();
            result.StatusCode = 401;
            result.StatusDescription = "Bad Request Cannot Create new book make sure everything is filled out. ";

            if (books != null)
            {
                _context.Books.Add(books);
                await _context.SaveChangesAsync();
                result.StatusCode = 201;
                result.StatusDescription = "Created. Added a new book to database";
                result.BookResponse.Add(books);
            }
            else
            {
                result.BookResponse = null;

            }

            return result;
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Response>> DeleteBooks(int id)
        {
            var books = await _context.Books.FindAsync(id);
            var result = new Response();
            result.StatusCode = 404;
            result.StatusDescription = " Cannot find specificed book for ID given. ";

            if (books != null)
            {
                _context.Books.Remove(books);
                await _context.SaveChangesAsync();
                result.StatusCode = 202;
                result.StatusDescription = "Successfully deleted books of that ID";
            }
            else
            {
                result.BookResponse = null;
            }

            return result;

        }

        private bool BooksExists(int id)
        {
            return _context.Books.Any(e => e.BookId == id);
        }
    }
}
