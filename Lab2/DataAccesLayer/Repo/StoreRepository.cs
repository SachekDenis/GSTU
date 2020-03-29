using DataAccesLayer.Context;
using DataAccesLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccesLayer.Repo
{
    public class StoreRepository<T> : IRepository<T> where T : class,IEntity
    {
        private bool disposed = false;
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
                return;
            _context.Remove(item);
            await _context.SaveChangesAsync();
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>().AsNoTracking();
        }

        public async Task<T> GetById(int id)
        {
            return await _context.Set<T>()
                         .AsNoTracking()
                         .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task Update(T item)
        {
            var entry = _context.Set<T>().First(e=>e.Id == item.Id);
            _context.Entry(entry).CurrentValues.SetValues(item);
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual async void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    await _context.DisposeAsync();
                }
                disposed = true;
            }
        }
    }
}
