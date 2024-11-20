using System.Collections.Generic;
using System.Linq;
using BreweryManagement.Data;
using BreweryManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BreweryManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WholesalerController : ControllerBase
    {
        private readonly BreweryContext _context;

        public WholesalerController(BreweryContext context)
        {
            _context = context;
        }

        [HttpPost("{wholesalerId}/stock")]
        public IActionResult AddBeerToWholesaler(int wholesalerId, [FromBody] WholesalerStock stock)
        {
            var wholesaler = _context.Wholesalers.Find(wholesalerId);
            if (wholesaler == null)
                return NotFound("Wholesaler not found.");

            var beer = _context.Beers.Find(stock.BeerId);
            if (beer == null)
                return NotFound("Beer not found.");

            stock.WholesalerId = wholesalerId;
            _context.WholesalerStocks.Add(stock);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetStockByWholesaler), new { wholesalerId = wholesalerId }, stock);

        }

        [HttpPut("{wholesalerId}/stock/{beerId}")]
        public IActionResult UpdateStock(int wholesalerId, int beerId, [FromBody] int newQuantity)
        {
            var stock = _context.WholesalerStocks
                                .FirstOrDefault(s => s.WholesalerId == wholesalerId && s.BeerId == beerId);
            if (stock == null)
                return NotFound("Stock not found.");

            stock.Quantity = newQuantity;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpPost("{wholesalerId}/quote")]
        public IActionResult RequestQuote(int wholesalerId, [FromBody] Dictionary<int, int> order) // BeerId -> Quantity
        {
            if (order == null || !order.Any())
                return BadRequest("The order cannot be empty.");

            var wholesaler = _context.Wholesalers
                                     .Include(w => w.WholesalerStocks)
                                     .ThenInclude(s => s.Beer)
                                     .FirstOrDefault(w => w.Id == wholesalerId);
            if (wholesaler == null)
                return NotFound("Wholesaler not found.");

            var response = new List<object>();
            decimal totalPrice = 0;

            foreach (var item in order)
            {
                var stock = wholesaler.WholesalerStocks.FirstOrDefault(s => s.BeerId == item.Key);
                if (stock == null)
                    return BadRequest($"Beer with ID {item.Key} is not sold by this wholesaler.");

                if (item.Value > stock.Quantity)
                    return BadRequest($"Insufficient stock for Beer ID {item.Key}.");

                var beerPrice = stock.Beer.Price * item.Value;
                totalPrice += beerPrice;

                response.Add(new
                {
                    Beer = stock.Beer.Name,
                    Quantity = item.Value,
                    Price = beerPrice
                });
            }

            // Apply discounts
            if (order.Values.Sum() > 20)
                totalPrice *= 0.8m;
            else if (order.Values.Sum() > 10)
                totalPrice *= 0.9m;

            return Ok(new { TotalPrice = totalPrice, Details = response });
        }


        [HttpGet("{wholesalerId}/stock")]
        public IActionResult GetStockByWholesaler(int wholesalerId)
        {
            // Fetch the wholesaler and include their stock with beer details
            var wholesaler = _context.Wholesalers
                                     .Include(w => w.WholesalerStocks)
                                     .ThenInclude(s => s.Beer)
                                     .FirstOrDefault(w => w.Id == wholesalerId);

            if (wholesaler == null)
                return NotFound("Wholesaler not found.");

            // Transform the stock data into a simplified response format
            var stockDetails = wholesaler.WholesalerStocks.Select(s => new
            {
                BeerName = s.Beer.Name,
                s.Quantity,
                PricePerUnit = s.Beer.Price
            });

            return Ok(stockDetails);
        }
    }
}
