using RestaurantProject.Models;
using RestaurantProject.Models.DTOs;

namespace RestaurantProject.Data.Repos.IRepos
{
    public interface IDishRepository
    {
        Task<IEnumerable<Dish>> GetAllDishesAsync();
        Task AddDishAsync(Dish dish);
        Task<Dish> FindDishByIdAsync(int dishId);
        Task UpdateDishAsync(Dish dish);
        Task DeleteDishAsync(Dish dish);

        Task<Dish> FindDishByNameAsync(string name);

    }
}

