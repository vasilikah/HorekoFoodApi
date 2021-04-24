using Domain.Interfaces;
using Domain.Models;
using Domain.Models.DbModels;
using Domain.Models.JsonModels;
using Domain.Paths;
using Domain.SeedServices.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Domain.SeedServices.Services
{
    public class SeedService : ISeedService
    {
        private readonly IIngredientsRepository _ingredientsRepository;
        private readonly IDishesRepository _dishesRepository;
        private readonly IDishesIngredientsRepository _dishesIngredientsRepository;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public SeedService(IIngredientsRepository ingredientsRepository, IDishesRepository dishesRepository, IWebHostEnvironment hostingEnvironment, IDishesIngredientsRepository dishesIngredientsRepository)
        {
            _ingredientsRepository = ingredientsRepository;
            _hostingEnvironment = hostingEnvironment;
            _dishesRepository = dishesRepository;
            _dishesIngredientsRepository = dishesIngredientsRepository;
        }

        public async Task InsertIngredientsIntoDB()
        {
            var ingredientDb =await _ingredientsRepository.CheckIfExistsItems();
            if (ingredientDb) return;
            var projectRootPath = _hostingEnvironment.ContentRootPath;
            var fullFileDirectory = Path.Combine(projectRootPath, StaticDocumentsDirectories.JsonFiles);
            if (!Directory.Exists(fullFileDirectory))
            {
                throw new ApplicationException(Errors.DirectoryDoesNotExist);
            }
            var fullFileName = Path.Combine(fullFileDirectory, "ingredients-sample-data.json");
            if (!File.Exists(fullFileName))
            {
                throw new ApplicationException(Errors.FileDoesNotExist);
            }
            var jsonString = File.ReadAllText(fullFileName);
            var jsonModel = JsonConvert.DeserializeObject<List<JsonIngredients>>(jsonString);
            var ingredient = new Ingredients();
            foreach (var item in jsonModel)
            {
                ingredient.Id = item.Id;
                ingredient.Name = item.Name;
                ingredient.Price = item.Price;
                await _ingredientsRepository.Create(ingredient);
            }
            await _ingredientsRepository.Save();
        }


        public async Task InsertDishedIntoDb()
        {
            var dishDb = await _dishesRepository.CheckIfExistsItems();
            if (dishDb) return;
            var projectRootPath = _hostingEnvironment.ContentRootPath;
            var fullFileDirectory = Path.Combine(projectRootPath, StaticDocumentsDirectories.JsonFiles);
            if (!Directory.Exists(fullFileDirectory))
            {
                throw new ApplicationException(Errors.DirectoryDoesNotExist);
            }
            var fullFileName = Path.Combine(fullFileDirectory, "dishes-sample-data.json");
            if (!File.Exists(fullFileName))
            {
                throw new ApplicationException(Errors.FileDoesNotExist);
            }
            var jsonString = File.ReadAllText(fullFileName);
            var jsonModel = JsonConvert.DeserializeObject<List<JsonDishes>>(jsonString);
            
            foreach (var item in jsonModel)
            {
                var dish = new Dishes
                {
                    Id = item.id,
                    Name = item.name,
                    ParentId = item.parentId
                };
                dish.UpdatedOn = dish.UpdatedOn = DateTime.Parse((item.updatedOn).ToString());
                await _dishesRepository.Create(dish);

                foreach (var item2 in item.ingredients)
                {
                    var ingredient = new DishIngredient
                    {
                        DishesId = dish.Id,
                        Amount = item2.amount,
                        IngredientId = item2.ingredientId
                    };

                    await _dishesIngredientsRepository.Create(ingredient);
                }
                await _dishesRepository.Save();
            }
        }

    }
}


