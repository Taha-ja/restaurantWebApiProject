using BLL.Contracts;
using DAL.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace RestaurantWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantsController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Restaurant>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<Restaurant>>> GetRestaurants()
        {
            var restaurants = await _restaurantService.GetRestaurantsAsync();
            return Ok(restaurants);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Restaurant), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<Restaurant>> GetRestaurantById(int id)
        {
            var restaurant = await _restaurantService.GetRestaurantByIdAsync(id);

            if (restaurant is null)
                return NotFound("restaurant not found.");

            return Ok(restaurant);
        }

        [HttpPost(Name = nameof(GetRestaurantById))]
        [ProducesResponseType(typeof(Restaurant), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> AddRestaurant(Restaurant restaurant)
        {
            await _restaurantService.AddRestaurantAsync(restaurant);
            return CreatedAtRoute(nameof(GetRestaurantById), new { id = restaurant.Id }, restaurant);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRestaurant(int id, Restaurant restaurant)
        {
            if (id != restaurant.Id)
                return BadRequest("Id don't match any restaurant.");

            await _restaurantService.UpdateRestaurantAsync(restaurant);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurant(int id)
        {
            var restaurant = await _restaurantService.GetRestaurantByIdAsync(id);

            if (restaurant is null)
                return NotFound("Can not delete a not existing restaurant");

            await _restaurantService.DeleteRestaurantAsync(id);

            return NoContent();
        }
    }
}
