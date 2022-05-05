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
    public class CustomerController : ControllerBase
    {
        private readonly APIDBContext _context;

        public CustomerController(APIDBContext context)
        {
            _context = context;
        }

        // GET: api/Customer
        [HttpGet]
        public async Task<ActionResult<Response>> GetCustomers()
        {
            var customer = await _context.Customers.ToListAsync();
            var result = new Response();
            result.StatusCode = 400;
            result.StatusDescription = " Cannot fetch list of Customers check database if table exists";

            if (customer != null)
            {
                result.StatusCode = 200;
                result.StatusDescription = "Sucess. List of customers fetched!";

                foreach (Customer customerIt in customer)
                {
                    result.CustomerResponse.Add(customerIt);
                }
                //return result;
            }
            else
            { result.CustomerResponse = null; }
            return result;

        }

        // GET: api/Customer/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Response>> GetCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            var result = new Response();
            result.StatusCode = 404;
            result.StatusDescription = " Cannot find specificed Customer for ID given. ";


            if (customer != null)
            {
                result.StatusCode = 202;
                result.StatusDescription = "Sucess. List of customer info fetched!";
                result.CustomerResponse.Add(customer);
            }
            else
            { result.CustomerResponse = null; }
            return result;
        }

        // PUT: api/Customer/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<Response>> PutCustomer(int id, Customer customer)
        {
            var result = new Response();

            result.StatusCode = 404;
            result.StatusDescription = " Cannot find specificed Customer for ID given. ";

            if (id != customer.CustomerId)
            {
                result.StatusDescription = " CustomerID does not match one in database ";
                return result;
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                result.StatusCode = 200;
                result.StatusDescription = " Success. Updated information for Customer of that id. ";

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Customer
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Response>> PostCustomer(Customer customer)
        {
            var result = new Response();
            result.StatusCode = 401;
            result.StatusDescription = "Bad Request Cannot Create new Customer make sure everything is filled out. ";
            
            if (customer != null)
            {
                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();
                result.StatusCode = 201;
                result.StatusDescription = "Created. Added a new customer";
                result.CustomerResponse.Add(customer);
            }
            else
            {
                result.AuthorResponse = null;
            }

            return result;
        }

        // DELETE: api/Customer/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Response>> DeleteCustomer(int id)
        {
            var result = new Response();
            var customer = await _context.Customers.FindAsync(id);
            result.StatusCode = 404;
            result.StatusDescription = " Cannot find specificed customer for ID given. ";

            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
                result.StatusCode = 202;
                result.StatusDescription = "Successfully deleted customer of that ID";
            }
            else
            {
                result.CustomerResponse = null;

            }

            return result;
            // NoContent();
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.CustomerId == id);
        }
    }
}
