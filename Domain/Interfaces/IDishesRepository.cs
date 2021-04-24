using Domain.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IDishesRepository
    {
        Task<IEnumerable<Dishes>> GetDishes();
        Task<Dishes> GetDishById(int id);
        Task<Dishes> Create(Dishes dish);
        Task Update(Dishes dish);
        Task Delete(int id);
        Task Save();
        Task<bool> CheckByName(string name);
        Task<List<Dishes>> GetDishByParentId(int? parentId);
        Task<bool> ExistsCheckById(int id);
        Task<bool> CheckIfExistsItems();
    }
}
