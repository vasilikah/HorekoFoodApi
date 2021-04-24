using Domain.DbContextPersistence;
using Domain.Interfaces;
using Domain.Models.DbModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public class DishesRepository : IDishesRepository
    {
        private readonly DataContext _context;

        public DishesRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Dishes> Create(Dishes dish)
        {
            _context.Dishes.Add(dish);
            await _context.SaveChangesAsync();
            return dish;
        }

        public async Task Delete(int id)
        {
            var dish = await _context.Dishes.FindAsync(id);
            _context.Dishes.Remove(dish);
            await _context.SaveChangesAsync();
        }

        public async Task<Dishes> GetDishById(int id)
        {
            return await _context.Dishes.FindAsync(id);
        }

        public async Task Update(Dishes dish)
        {
            _context.Entry(dish).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Dishes>> GetDishes()
        {
            return await _context.Dishes.ToListAsync();
        }

        public async Task<List<Dishes>> GetDishByParentId(int? parentId)
        {
            return await _context.Dishes
                .Where(x => x.Id == parentId)
                .ToListAsync();
        }
        public async Task<bool> CheckByName(string name)
        {
            return await _context.Dishes.AnyAsync(x=>x.Name==name);
        }

        public async Task<bool> ExistsCheckById(int id)
        {
            return await _context.Dishes.AnyAsync(x => x.Id == id);
        }
     
        public async Task<bool> CheckIfExistsItems()
        {
            return await _context.Dishes.AnyAsync();
        }
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        
    }
}

