using RestaurantProject.Models;
using RestaurantProject.Models.ViewModels;

namespace RestaurantProject.Data.Repos.IRepos
{
    public interface ITableRepository
    {
        Task<IEnumerable<Table>> GetAllTablesAsync();
        Task AddTableAsync(Table table);
        Task<Table> FindTableByIdAsync(int  tableId);
        Task UpdateTableAsync(Table table);
        Task DeleteTableAsync(Table table);
        //Task<Table> FindTableByTableNoAsync(int tableNo);

    }
}
