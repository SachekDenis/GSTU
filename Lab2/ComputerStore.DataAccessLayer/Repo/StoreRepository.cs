using System.Collections.Generic;
using System.Threading.Tasks;
using ComputerStore.DataAccessLayer.Context;
using ComputerStore.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace ComputerStore.DataAccessLayer.Repo
{
    public class StoreRepository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly StoreContext _context;

        public StoreRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task Add(T item)
        {
            await _context.Set<T>().AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var item = await _context.FindAsync<T>(id);
            if (item == null)
            {
                return;
            }

            _context.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _context.Set<T>()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task Update(T item)
        {
            var entry = await _context.Set<T>().FirstAsync(e => e.Id == item.Id);
            _context.Entry(entry).CurrentValues.SetValues(item);
            await _context.SaveChangesAsync();
        }
    }
}