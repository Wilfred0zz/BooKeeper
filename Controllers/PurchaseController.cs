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
    public class PurchaseController : ControllerBase
    {
        private readonly APIDBContext _context;

        public PurchaseController(APIDBContext context)
        {
            _context = context;
        }

        // GET: api/Purchase
        [HttpGet]
        public async Task<ActionResult<Response>> GetPurchases()
        {
            var purchases = await _context.Purchases.ToListAsync();

            var result = new Response();

            result.StatusCode = 400;
            result.StatusDescription = " Cannot fetch list of purchases check database if table exists";

            if (purchases != null)
            {
                result.StatusCode = 200;
                result.StatusDescription = "Sucess. List of purchases fetched!";

                foreach(Purchase purchased in purchases)
                {
                    result.PurchasesResponse.Add(purchased);
                }
                //return result;
            }
            else
            { result.PurchasesResponse = null; }
            return result;
            //return purchases;
            
        }

        // GET: api/Purchase/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Response>> GetPurchase(int id)
        {
            var purchase = await _context.Purchases.FindAsync(id);

            var result = new Response();
            result.StatusCode = 404;
            result.StatusDescription = " Cannot find specificed purchase for ID given. ";
            


            if (purchase != null)
            {
                result.StatusCode = 202;
                result.StatusDescription = "Sucess. List of purchase information fetched!";
                result.PurchasesResponse.Add(purchase);

            }else
            { result.PurchasesResponse = null; }

                return result;
        }

        // PUT: api/Purchase/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<Response>> PutPurchase(int id, Purchase purchase)
        {
            var result = new Response();

            result.StatusCode = 404;
            result.StatusDescription = " Cannot find specificed purchase for ID given. ";

            if (id != purchase.PurchaseId)
            {
                result.StatusDescription = " ID does not match one in database ";
                return result;
            }

            _context.Entry(purchase).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                result.StatusCode = 200;
                result.StatusDescription = " Success. Updated information for purchase of that id. ";

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PurchaseExists(id))
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

        // POST: api/Purchase
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Response>> PostPurchase(Purchase purchase)
        {

            var result = new Response();


            result.StatusCode = 401;
            result.StatusDescription = "Bad Request Cannot Create new purchase make sure everything is filled out. ";


            if (purchase != null)
            {
                _context.Purchases.Add(purchase);
                await _context.SaveChangesAsync();
                result.StatusCode = 201;
                result.StatusDescription = "Created. Added a new purchase to database";
                result.PurchasesResponse.Add(purchase);
            }
            else
            {
                result.PurchasesResponse = null;

            }

            return result;   
        }

        // DELETE: api/Purchase/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Response>> DeletePurchase(int id)
        {
            var result = new Response();

            var purchase = await _context.Purchases.FindAsync(id);

            result.StatusCode = 404;
            result.StatusDescription = " Cannot find specificed purchase for ID given. ";

            if (purchase != null)
            {
                _context.Purchases.Remove(purchase);
                await _context.SaveChangesAsync();
                result.StatusCode = 202;
                result.StatusDescription = "Successfully deleted purchase";
            }
            else
            {
                result.PurchasesResponse = null;

            }
            return result;
        }

        private bool PurchaseExists(int id)
        {
            return _context.Purchases.Any(e => e.PurchaseId == id);
        }
    }
}
