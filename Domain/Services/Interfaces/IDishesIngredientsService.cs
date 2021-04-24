using Domain.Models.DbModels;
using Domain.Models.ViewModels;
using Domain.Models.ViewModels.CountingDishesIngredients;
using Domain.Models.ViewModels.CreateNewDish;
using Domain.Models.ViewModels.IngredientsUsage;
using Domain.Models.ViewModels.ViewModelsById;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Interfaces
{
    public interface IDishesIngredientsService
    {
        Task<List<DishesView>> GetListDishes();
        Task<DishesById> GetDishById(int id);
        Task<CreateNewDish> AddNewDish(CreateNewDish newDish);
        Task<List<DishesCountPrice>> CalculateIngredients();
        Task<List<IngredientsUsageViewModel>> IngredientsUsage();
    }
}
