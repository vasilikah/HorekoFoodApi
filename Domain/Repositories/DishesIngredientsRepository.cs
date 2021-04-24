using Domain.DbContextPersistence;
using Domain.Interfaces;
using Domain.Models.DbModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Models.DbModels.Dishes;

namespace Domain.Repositories
{
    public class DishesIngredientsRepository : IDishesIngredientsRepository
    {
        private readonly DataContext _context;

        public DishesIngredientsRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<DishIngredient> Create(DishIngredient ingredientDish)
        {
            _context.DishIngredient.Add(ingredientDish);
            await _context.SaveChangesAsync();
            return ingredientDish;
        }

        public async Task Delete(int id)
        {
            var ingredientDish = await _context.DishIngredient.FindAsync(id);
            _context.DishIngredient.Remove(ingredientDish);
            await _context.SaveChangesAsync();
        }

        public async Task Update(DishIngredient ingredientDish)
        {
            _context.Entry(ingredientDish).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<List<DishIngredient>> GetDishIngredientsByDishesId(int dishesId)
        {
            return await _context.DishIngredient                
                .Where(x => x.DishesId == dishesId)
                .ToListAsync();
        }

        public async Task<List<DishIngredient>> UsageOfIngredients(int ingredientId)
        {
            return await _context.DishIngredient
                .Where(x => x.IngredientId == ingredientId)
                .ToListAsync();
        }


        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}

