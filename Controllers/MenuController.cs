using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantProject.Exceptions;
using RestaurantProject.Models;
using RestaurantProject.Models.DTOs;
using RestaurantProject.Services.IServices;

namespace RestaurantProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IDishService _dishService;


        public MenuController(IDishService dishService)
        {
            _dishService = dishService;
        }

        [HttpGet("getAllDishes")]
        public async Task<ActionResult<IEnumerable<Dish>>> GetAllDishes()
        {
            var dishes = await _dishService.GetAllDishesAsync();
            return Ok(dishes);
        }

        [HttpPost("addDish")]
        public async Task<ActionResult> AddDish(DishDTO dish)
        {
            if (!ModelState.IsValid)//checks that all required fields are there/correct
                                    //- d annotations in DB help with this.. standard for API
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _dishService.AddDishAsync(dish);
                return Created();
            }
            catch(ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error in handling request");
            }

        }

        [HttpGet("getDish/{dishId}")]
        public async Task<ActionResult<DishDTO>> FindDishById(int dishId)
        {
            if (dishId == null)
            {
                return BadRequest("Input dish ID, please.");
            }

            var dish = await _dishService.FindDishByIdAsync(dishId);

            if (dish == null)
            {
                return NotFound();
            }

            return Ok(dish);
        }

        [HttpGet("getDishByName/{name}")]
        public async Task<ActionResult> FindDishByName(string name)
        {
            if (name == null)
            {
                return BadRequest("Input dish name, please.");
            }

            var dish = await _dishService.FindDishByNameAsync(name);

            if (dish == null)
            {
                return NotFound();
            }

            return Ok(dish);
        }

        [HttpPut("updateDish/{dishId}")]
        public async Task<ActionResult> UpdateDish(int dishId, DishDTO dish)
        {
            if (dishId == null)
            {
                return BadRequest("Input dish ID, please.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _dishService.UpdateDishAsync(dishId, dish);
                return NoContent(); //NoContent = status code 204, standard for API,
                                    //used for PUT/PATCH and when no data has to be returned
            }
            catch(ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception)
            {
                return StatusCode(500, "Error in handling request");
            }
        }

        [HttpDelete("deleteDish/{dishId}")]
        public async Task<ActionResult> DeleteDish(int dishId)
        {
            if (dishId == null)
            {
                return BadRequest("Input dish ID, please.");
            }

            try
            {
                await _dishService.DeleteDishAsync(dishId);
                return Ok();
            }
            catch(NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error in handling request");
            }
        }
    }
}
