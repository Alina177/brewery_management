using System.Linq;
using BreweryManagement.Data;
using BreweryManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BreweryManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BreweryController : ControllerBase
    {
        private readonly BreweryContext _context;

        public BreweryController(BreweryContext context)
        {
            _context = context;
        }

        [HttpGet("{breweryId}/beers")]
        public IActionResult GetBeersByBrewery(int breweryId)
        {
            var beers = _context.Beers
                                .Where(b => b.BreweryId == breweryId)
                                .ToList();
            if (beers.Count == 0)
                return NotFound("No beers found for this brewery.");

            return Ok(beers);
        }

        [HttpPost("{breweryId}/beers")]
        public IActionResult AddBeer(int breweryId, [FromBody] Beer newBeer)
        {
            var brewery = _context.Breweries
                                  .AsNoTracking()
                                  .FirstOrDefault(b => b.Id == breweryId);

            if (brewery == null)
                return NotFound("Brewery not found.");

            newBeer.BreweryId = breweryId;

            _context.Beers.Add(newBeer);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetBeersByBrewery), new { breweryId = breweryId }, newBeer);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBeer(int id)
        {
            var beer = _context.Beers.FirstOrDefault(b => b.Id == id);
            if (beer == null)
            {
                return NotFound();
            }

            _context.Beers.Remove(beer);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
