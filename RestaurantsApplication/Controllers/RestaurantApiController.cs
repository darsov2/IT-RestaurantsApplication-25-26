using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantsApplication.Data;
using RestaurantsApplication.Models;
using RestaurantsApplication.Models.Dto;

namespace RestaurantsApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RestaurantApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/RestaurantApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RestaurantDto>>> GetRestaurants()
        {
            var result = await _context.Restaurants.ToListAsync();
            return result.Select(x => new RestaurantDto()
            {
                Name = x.Name,
                Address = x.Address,
                Location = x.City + ", " + x.Country,
                Rating = x.Rating,
                Id = x.Id
            }).ToList();
            
        }

        [HttpGet("paged")]
        public IActionResult GetPaged(int page, int size)
        {
            // last page
            // data
            
            var lastPage = _context.Restaurants.Count() /  size;
            var skip = (page - 1) * size; 

            var result = _context.Restaurants.OrderBy(x => x.Id).Skip(skip).Take(size).
                Select(x => new RestaurantDto()
                {
                    Name = x.Name,
                    Address = x.Address,
                    Location = x.City + ", " + x.Country,
                    Rating = x.Rating,
                    Id = x.Id
                }).ToList();

            return Ok(new TabulatorPagingDto()
            {
                LastPage = lastPage,
                Data = result
            });
        }

        // GET: api/RestaurantApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Restaurant>> GetRestaurant(int id)
        {
            var restaurant = await _context.Restaurants.FindAsync(id);

            if (restaurant == null)
            {
                return NotFound();
            }

            return restaurant;
        }

        // PUT: api/RestaurantApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRestaurant(int id, Restaurant restaurant)
        {
            if (id != restaurant.Id)
            {
                return BadRequest();
            }

            _context.Entry(restaurant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RestaurantExists(id))
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

        // POST: api/RestaurantApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Restaurant>> PostRestaurant(Restaurant restaurant)
        {
            _context.Restaurants.Add(restaurant);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRestaurant", new { id = restaurant.Id }, restaurant);
        }

        // DELETE: api/RestaurantApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurant(int id)
        {
            var restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant == null)
            {
                return NotFound();
            }

            _context.Restaurants.Remove(restaurant);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RestaurantExists(int id)
        {
            return _context.Restaurants.Any(e => e.Id == id);
        }
    }
}
