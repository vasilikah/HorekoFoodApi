using Domain.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static Domain.Models.DbModels.Dishes;

namespace Domain.Interfaces
{
    public interface IDishesIngredientsRepository
    {
        Task<DishIngredient> Create(DishIngredient dish);
        Task<List<DishIngredient>> GetDishIngredientsByDishesId(int dishesId);
        Task<List<DishIngredient>> UsageOfIngredients(int ingredientId);
        Task Update(DishIngredient dish);
        Task Delete(int id);
        Task Save();
    }
}
