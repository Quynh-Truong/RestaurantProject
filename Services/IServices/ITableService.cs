using RestaurantProject.Models;
using RestaurantProject.Models.DTOs;

namespace RestaurantProject.Services.IServices
{
    public interface ITableService
    {
        Task<IEnumerable<Table>> GetAllTablesAsync();
        Task AddTableAsync(TableDTO tableDto);
        Task<Table> FindTableByIdAsync(int tableId);
        Task UpdateTableAsync(int tableId, TableDTO tableDto);
        Task DeleteTableAsync(int tableId);
        //Task<Table> FindTableByTableNoAsync(int tableNo);

    }
}
