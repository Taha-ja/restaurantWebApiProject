using AutoMapper;
using BLL.Contracts;
using DAL.Entities.DTOs;
using DAL.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace RestaurantWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public RestaurantsController(IRepositoryWrapper repository , ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Restaurant>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<Restaurant>>> GetRestaurants()
        {
            try
            {
                var restaurants = await _repository.Restaurant.GetRestaurantsAsync();
                _logger.LogInfo($"Returned all restaurants from database with success.");
                return Ok(restaurants);
            }catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetRestaurants action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Restaurant), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<Restaurant>> GetRestaurantById(int id)
        {
            try
            {
                var restaurant = await _repository.Restaurant.GetRestaurantByIdAsync(id);
                if (restaurant is null)
                    return NotFound($"restaurant with id {id} not found.");
                else
                {
                    _logger.LogInfo($"Returned the restaurant with id {id} from database with success.");
                    return Ok(restaurant);
                }
            }
            catch(Exception ex) {
                _logger.LogError($"Something went wrong inside GetRestaurant action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }



           
        }

        [HttpPost(Name = nameof(GetRestaurantById))]
        [ProducesResponseType(typeof(Restaurant), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> AddRestaurant(Restaurant restaurant)
        {
            try
            {
                if (restaurant is null)
                {
                    _logger.LogError("Restaurant object sent from client is null.");
                    return BadRequest("Restaurant object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Restaurant object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var restaurantEntity = _mapper.Map<Restaurant>(restaurant);
                await _repository.Restaurant.AddRestaurantAsync(restaurantEntity);
                _repository.Save();
                var createdRestaurant = _mapper.Map<RestaurantDto>(restaurantEntity);
                return CreatedAtRoute("GetRestaurantById", new { id = restaurantEntity.Id }, createdRestaurant);
                //await _repository.Restaurant.AddRestaurantAsync(restaurant);
                //_repository.Save();
                //return CreatedAtRoute("GetRestaurantById", new { id = restaurant.Id }, restaurant);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside AddRestaurant action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRestaurant(int id, Restaurant restaurant)
        {
            try
            {
                if (restaurant is null)
                {
                    _logger.LogError("Restaurant object sent from client is null.");
                    return BadRequest("Restaurant object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Restaurant object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var restaurantEntity = _repository.Restaurant.GetRestaurantByIdAsync(id);
                if (restaurantEntity is null)
                {
                    _logger.LogError($"Restaurant with id: {id}, hasn't been found in database.");
                    return NotFound();
                }
                await _repository.Restaurant.UpdateRestaurantAsync(restaurant);
                _repository.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateRestaurant action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurant(int id)
        {
            try
            {
                var restaurant = _repository.Restaurant.GetRestaurantByIdAsync(id);
                if (restaurant == null)
                {
                    _logger.LogError($"Restaurant with id: {id}, hasn't been found in database.");
                    return NotFound();
                }
                await _repository.Restaurant.DeleteRestaurantAsync(id);
                _repository.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteRestaurant action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
