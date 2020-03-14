﻿using DataAccesLayer.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer.Repo
{
    public class StoreRepository<T> : IRepository<T> where T : class
    {
        private readonly StoreContext _context;

        public StoreRepository(StoreContext context)
        {
            _context = context;
        }

        public async void Add(T item)
        {
            await _context.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async void Delete(int id)
        {
            var item = await _context.FindAsync<T>(id);
            if (item == null)
                return;
            _context.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            var items = await _context.Set<T>().ToListAsync();
            return items;
        }

        public async Task<T> GetById(int id)
        {
            var item = await _context.FindAsync<T>(id);
            return item;
        }

        public async void Update(T item)
        {
            _context.Update(item);
            await _context.SaveChangesAsync();
        }
    }
}
