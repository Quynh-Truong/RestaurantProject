using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RestaurantProject.Data.Repos.IRepos;
using RestaurantProject.Exceptions;
using RestaurantProject.Models;
using RestaurantProject.Models.DTOs;
using RestaurantProject.Services.IServices;

namespace RestaurantProject.Services
{
    public class DishService : IDishService
    {
        private readonly IDishRepository _dishRepository;//middle man to database and repo,
                                                         //add error handling


        public DishService(IDishRepository dishRepository)
        {
            _dishRepository = dishRepository;
        }


        //using DTO to add new dish
        public async Task AddDishAsync(DishDTO dish)
        {
            var existingDish = await _dishRepository.FindDishByNameAsync(dish.Name);

            if (existingDish != null)
            {
                throw new ValidationException("This dish name already exists!");
            }

            var dishAdded = new Dish
            {
                Name = dish.Name,
                Price = dish.Price,
                Availability = dish.Availability
            };

            await _dishRepository.AddDishAsync(dishAdded);
        }

        public async Task DeleteDishAsync(int dishId)
        {
            //find the dish we want to delete, if existing => delete
            var dish = await _dishRepository.FindDishByIdAsync(dishId);

            if (dish == null)
            {
                throw new NotFoundException($"Dish with ID {dishId} not found.");
            }

            await _dishRepository.DeleteDishAsync(dish);

        }

        public async Task<Dish> FindDishByIdAsync(int dishId)
        {
            var dishChosen = await _dishRepository.FindDishByIdAsync(dishId);

            if (dishChosen == null)
            {
                throw new NotFoundException($"Dish with ID {dishId} not found.");
            }

            return dishChosen;

        }

        public async Task<Dish> FindDishByNameAsync(string name)
        {
            var existingDish = await _dishRepository.FindDishByNameAsync(name);
            return existingDish;
        }

        public async Task<IEnumerable<Dish>> GetAllDishesAsync()
        {
            var dishes = await _dishRepository.GetAllDishesAsync();

            return dishes;
        }



        public async Task UpdateDishAsync(int dishId, DishDTO dish)
        {
            var chosenDish = await _dishRepository.FindDishByIdAsync(dishId);

            if (chosenDish == null)
            {
                throw new NotFoundException($"Dish with ID {dishId} not found.");
            }

            //check if the name exists
            var existingDish = await _dishRepository.FindDishByNameAsync(dish.Name);

            //... and check (if it exists) that it is NOT the dish being updated now
            if (existingDish != null && existingDish.DishId != dishId)
            {
                throw new ValidationException("This dish name already exists!");
            }

            //re-writing the old values
            chosenDish.Name = dish.Name;
            chosenDish.Price = dish.Price;
            chosenDish.Availability = dish.Availability;

            await _dishRepository.UpdateDishAsync(chosenDish);

        }
    }
}
