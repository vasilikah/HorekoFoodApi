using Domain.Models;
using Domain.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IIngredientsRepository
    {
        Task<IEnumerable<Ingredients>> GetIngredients();
        Task<Ingredients> Create(Ingredients ingredient);
        Task Update(Ingredients ingredient);
        Task Delete(int id);
        Task Save();
        Task<bool> CheckIfExistsItems();
        Task<List<Ingredients>> GetIngredientsById(int id);
        Task<List<Ingredients>> GetDishIngredientsByIngredientsId(int ingredientId);

    }
}
