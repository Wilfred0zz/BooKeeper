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
    public class AuthorController : ControllerBase
    {
        private readonly APIDBContext _context;

        public AuthorController(APIDBContext context)
        {
            _context = context;
        }

        // GET: api/Author
        [HttpGet]
        public async Task<ActionResult<Response>> GetAuthors()
        {
            var author = await _context.Authors.ToListAsync();
            var result = new Response();
            result.StatusCode = 400;
            result.StatusDescription = " Cannot fetch list of Authors check database if table exists";
            if (author != null)
            {
                result.StatusCode = 200;
                result.StatusDescription = "Sucess. List of authors fetched!";

                foreach (Author authorIt in author)
                {
                    result.AuthorResponse.Add(authorIt);
                }
                //return result;
            }
            else
            { result.AuthorResponse = null; }
            return result;


        }

        // GET: api/Author/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Response>> GetAuthor(int id)
        {
            var author = await _context.Authors.FindAsync(id);

            var result = new Response();
            result.StatusCode = 404;
            result.StatusDescription = " Cannot find specificed author for ID given. ";

            if (author != null)
            {
                result.StatusCode = 202;
                result.StatusDescription = "Sucess. List of author info fetched!";
                result.AuthorResponse.Add(author);

            }
            else
            { result.AuthorResponse = null; }

            return result;
        }

        // PUT: api/Author/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<Response>> PutAuthor(int id, Author author)
        {
            var result = new Response();

            result.StatusCode = 404;
            result.StatusDescription = " Cannot find specificed author for ID given. ";

            if (id != author.AuthorId)
            {
                result.StatusDescription = " ID does not match one in database ";
                return result;
            }

            _context.Entry(author).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                result.StatusCode = 200;
                result.StatusDescription = " Success. Updated information for author of that id. ";

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorExists(id))
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

        // POST: api/Author
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Response>> PostAuthor(Author author)
        {
            var result = new Response();
            result.StatusCode = 401;
            result.StatusDescription = "Bad Request Cannot Create new author make sure everything is filled out. ";

            if (author != null)
            {
                _context.Authors.Add(author);
                await _context.SaveChangesAsync();
                result.StatusCode = 201;
                result.StatusDescription = "Created. Added a new author to database";
                result.AuthorResponse.Add(author);
            }
            else
            {
                result.AuthorResponse = null;

            }

            return result;
        }

        // DELETE: api/Author/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Response>> DeleteAuthor(int id)
        {
            var result = new Response();

            var author = await _context.Authors.FindAsync(id);
            result.StatusCode = 404;
            result.StatusDescription = " Cannot find specificed author for ID given. ";

            if (author != null)
            {
                _context.Authors.Remove(author);
                await _context.SaveChangesAsync();
                result.StatusCode = 202;
                result.StatusDescription = "Successfully deleted author of that ID";
            }
            else
            {
                result.AuthorResponse = null;

            }

            return result;
        }

        private bool AuthorExists(int id)
        {
            return _context.Authors.Any(e => e.AuthorId == id);
        }
    }
}
