using Domain.DbContextPersistence;
using Domain.Interfaces;
using Domain.Models;
using Domain.Models.DbModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public class IngredientsRepository : IIngredientsRepository
    {
        private readonly DataContext _context;

        public IngredientsRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Ingredients> Create(Ingredients ingredient)
        {
            _context.Ingredients.Add(ingredient);
            await _context.SaveChangesAsync();
            return ingredient;
        }

        public async Task Delete(int id)
        {
            var ingredient = await _context.Ingredients.FindAsync(id);
            _context.Ingredients.Remove(ingredient);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Ingredients>> GetIngredientsById(int id)
        {
            return await _context.Ingredients.Where(x => x.Id == id).ToListAsync();
        }
      
        public async Task<IEnumerable<Ingredients>> GetIngredients()
        {
            return await _context.Ingredients.ToListAsync();
        }
        public async Task<bool> CheckIfExistsItems()
        {
            return await _context.Ingredients.AnyAsync();
        }
        public async Task Update(Ingredients ingredient)
        {
            _context.Entry(ingredient).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<List<Ingredients>> GetDishIngredientsByIngredientsId(int ingredientId)
        {
            return await _context.Ingredients
                .Where(x => x.Id == ingredientId)
                .ToListAsync();
        }
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
