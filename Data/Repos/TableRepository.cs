using Microsoft.EntityFrameworkCore;
using RestaurantProject.Data.Repos.IRepos;
using RestaurantProject.Models;
using RestaurantProject.Models.ViewModels;

namespace RestaurantProject.Data.Repos
{
    public class TableRepository : ITableRepository
    {
        private readonly RestaurantContext _context;//context dep inj

        public TableRepository(RestaurantContext context)
        {
            _context = context;
        }
        public async Task AddTableAsync(Table table)
        {
            await _context.Tables.AddAsync(table);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTableAsync(Table table)
        {
            _context.Tables.Remove(table);
            await _context.SaveChangesAsync();
        }

        public async Task<Table> FindTableByIdAsync(int tableId)
        {
            return await _context.Tables.SingleOrDefaultAsync(t => t.TableId == tableId);
        }

        //public async Task<Table> FindTableByTableNoAsync(int tableNo)
        //{
        //    return await _context.Tables.SingleOrDefaultAsync(t => t.TableNo == tableNo);
        //}

        public async Task<IEnumerable<Table>> GetAllTablesAsync()
        {
            return await _context.Tables.ToListAsync();
        }


        public async Task UpdateTableAsync(Table table)
        {
            _context.Tables.Update(table);
            await _context.SaveChangesAsync();
        }
    }
}
