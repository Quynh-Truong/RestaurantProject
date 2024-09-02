

using RestaurantProject.Models;
using RestaurantProject.Models.DTOs;

namespace RestaurantProject.Services.IServices
{
    public interface IDishService
    {
        Task<IEnumerable<Dish>> GetAllDishesAsync();
        Task AddDishAsync(DishDTO dish);
        Task<Dish> FindDishByIdAsync(int dishId);
        Task UpdateDishAsync(int dishId, DishDTO dish);
        Task DeleteDishAsync(int dishId);
        Task<Dish> FindDishByNameAsync(string name);

    }
}
