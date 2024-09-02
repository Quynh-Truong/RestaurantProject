
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantProject.Data.Repos.IRepos;
using RestaurantProject.Models;

namespace RestaurantProject.Data.Repos
{
    public class DishRepository : IDishRepository 
    {
        private readonly RestaurantContext _context;//dep injection

        public DishRepository(RestaurantContext context)
        {
            _context = context;
        }


        public async Task AddDishAsync(Dish dish)
        {
            await _context.Dishes.AddAsync(dish);
            await _context.SaveChangesAsync();
        }


        public async Task DeleteDishAsync(Dish dish)
        {
            _context.Dishes.Remove(dish);
            await _context.SaveChangesAsync();
        }

        public async Task<Dish> FindDishByIdAsync(int dishId)
        {
            //this top version is not as effective/"good for performance)
            //var dish = _context.Dishes.SingleOrDefault(d => d.DishId == dishId);
            //return dish;
            return await _context.Dishes.SingleOrDefaultAsync(d => d.DishId == dishId);
        }

        public async Task<Dish> FindDishByNameAsync(string name)
        {
            return await _context.Dishes.SingleOrDefaultAsync(d => d.Name.ToLower() == name.ToLower());
        }

        public async Task<IEnumerable<Dish>> GetAllDishesAsync()
        {
            return await _context.Dishes.ToListAsync();
        }

        public async Task UpdateDishAsync(Dish dish)
        {
            _context.Dishes.Update(dish);
            await _context.SaveChangesAsync();
        }
    }
}
