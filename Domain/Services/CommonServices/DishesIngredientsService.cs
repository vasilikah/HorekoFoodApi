using Domain.Interfaces;
using Domain.Models.DbModels;
using Domain.Models.ViewModels;
using Domain.Models.ViewModels.CountingDishesIngredients;
using Domain.Models.ViewModels.CreateNewDish;
using Domain.Models.ViewModels.IngredientsUsage;
using Domain.Models.ViewModels.ViewModelsById;
using Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.CommonServices
{
    public class DishesIngredientsService : IDishesIngredientsService
    {
        private readonly IDishesRepository _dishRepository;
        private readonly IIngredientsRepository _ingredientsRepository;
        private readonly IDishesIngredientsRepository _dishesIngredientsRepository;

        public DishesIngredientsService(IDishesRepository dishesRepository, IIngredientsRepository ingredientsRepository, IDishesIngredientsRepository dishesIngredientsRepository)
        {
            _dishRepository = dishesRepository;
            _ingredientsRepository = ingredientsRepository;
            _dishesIngredientsRepository = dishesIngredientsRepository;
        }
        public async Task<List<DishesView>> GetListDishes()
        {
            var dishDb = await _dishRepository.GetDishes();
            var dishModel = new List<DishesView>();
            foreach (var item in dishDb)
            {
                var dish = new DishesView();
                dish.Id = item.Id;
                dish.Name = item.Name;
                dish.UpdatedOn = item.UpdatedOn;
                dish.Ingredients = new List<DishIngredientsView>();
                var disheIngredients = await _dishesIngredientsRepository.GetDishIngredientsByDishesId(item.Id);
                foreach (var item2 in disheIngredients)
                {
                    var ingredient = await _ingredientsRepository.GetIngredientsById(item2.IngredientId);
                    foreach (var item3 in ingredient)
                    {
                        var ingredientList = new DishIngredientsView()
                        {
                            IngredientId = item2.IngredientId,
                            Name = item3.Name,
                        };

                        dish.Ingredients.Add(ingredientList);
                    }

                }
                var dishWithParentId = await _dishRepository.GetDishByParentId(item.ParentId);
                if (dishWithParentId != null)
                {
                    foreach (var item4 in dishWithParentId)
                    {
                        var dishesByIngredients = await _dishesIngredientsRepository.GetDishIngredientsByDishesId(item4.Id);
                        foreach (var item5 in dishesByIngredients)
                        {
                            var ingredientByParent = await _ingredientsRepository.GetIngredientsById(item5.IngredientId);
                            {
                                foreach (var item6 in ingredientByParent)
                                {
                                    var ingredientList = new DishIngredientsView();
                                    ingredientList.IngredientId = item5.IngredientId;
                                    ingredientList.Name = item6.Name;
                                    dish.Ingredients.Add(ingredientList);
                                }

                            }
                        }

                    }

                }
                dishModel.Add(dish);
            }
            return dishModel;
        }



        public async Task<DishesById> GetDishById(int id)
        {
            var dishDb = await _dishRepository.GetDishById(id);
            var dishById = new DishesById();
            dishById.Id = dishDb.Id;
            dishById.Name = dishDb.Name;
            dishById.UpdatedOn = dishDb.UpdatedOn;
            dishById.ParentDish = new List<ParentDish>();
            dishById.Ingredients = new List<IngredientsWithAmount>();

            var dishesIngredients = await _dishesIngredientsRepository.GetDishIngredientsByDishesId(dishDb.Id);
            foreach (var item in dishesIngredients)
            {
                var ingredientsById = await _ingredientsRepository.GetIngredientsById(item.IngredientId);
                foreach (var item2 in ingredientsById)
                {
                    var ingridientWithAmount = new IngredientsWithAmount
                    {
                        IngredientId = item.IngredientId,
                        Name = item2.Name,
                        Amount = item.Amount
                    };
                    dishById.Ingredients.Add(ingridientWithAmount);
                }
            }
            var dishWithParentId = await _dishRepository.GetDishByParentId(dishDb.ParentId);
            if (dishWithParentId != null)
            {
                foreach (var item3 in dishWithParentId)
                {
                    var dishesByIngredients = await _dishesIngredientsRepository.GetDishIngredientsByDishesId(item3.Id);
                    foreach (var item4 in dishesByIngredients)
                    {
                        var ingredientByParent = await _ingredientsRepository.GetIngredientsById(item4.IngredientId);
                        foreach (var item5 in ingredientByParent)
                        {
                            var ingridientWithAmount = new IngredientsWithAmount();
                            ingridientWithAmount.IngredientId = item4.IngredientId;
                            ingridientWithAmount.Name = item5.Name;
                            ingridientWithAmount.Amount = item4.Amount;
                            dishById.Ingredients.Add(ingridientWithAmount);
                        }
                    }
                }
                foreach (var parent in dishWithParentId)
                {
                    var parentDish = new ParentDish();
                    parentDish.ParentId = parent.Id;
                    parentDish.ParentDishName = parent.Name;
                    dishById.ParentDish.Add(parentDish);
                }

            }

            return dishById;
        }
        public async Task<CreateNewDish> AddNewDish(CreateNewDish dish)
        {
            var newDish = new Dishes();
            newDish.Id = dish.Id;
            newDish.Name = dish.Name;
            newDish.ParentId = dish.ParentId;
            newDish.UpdatedOn = dish.UpdatedOn;
            newDish.Ingredients = new List<DishIngredient>();
            foreach (var item in dish.Ingredients)
            {
                var ingredients = new DishIngredient()
                {
                    IngredientId = item.IngredientId,
                    Amount = item.Amount
                };
                newDish.Ingredients.Add(ingredients);
            }
            await _dishRepository.Create(newDish);
            await _dishRepository.Save();
            return dish;
        }

        public async Task<List<DishesCountPrice>> CalculateIngredients()
        {
            var listOfDishes = await GetListDishes();
            var dishesCountPrice = new List<DishesCountPrice>();
            foreach (var item in listOfDishes)
            {
                var ingredientsList = new List<IngredientsView>();
                foreach (var item2 in item.Ingredients)
                {
                    var dishIngredientById = await _ingredientsRepository.GetDishIngredientsByIngredientsId(item2.IngredientId);
                    
                    var ingredientPrice = new IngredientsView();
                    foreach (var item3 in dishIngredientById)
                    {
                        ingredientPrice.Price = item3.Price;
                    }
                      ingredientsList.Add(ingredientPrice);
                }

                double price = ingredientsList.Sum(x => x.Price);
                var dishCountPrice = new DishesCountPrice();
                dishCountPrice.Id = item.Id;
                dishCountPrice.Name = item.Name;
                dishCountPrice.Price = price;

                dishesCountPrice.Add(dishCountPrice);
            }
            return dishesCountPrice;
        }

        public async Task<List<IngredientsUsageViewModel>> IngredientsUsage()
        {
            var ingredientsUsageList = new List<IngredientsUsageViewModel>();
            var ingredients = await _ingredientsRepository.GetIngredients();
           
            //"CreateNewIngredient" - model for get amount as property
            foreach (var item in ingredients)
            {
                var findIngredientsIntoDishes =await _dishesIngredientsRepository.UsageOfIngredients(item.Id);
                var listOfIngredients = new List<IngredientsView>();
                var listOfAmounts = new List<CreateNewIngredient>();
                foreach (var item2 in findIngredientsIntoDishes)
                {
                    var ingredient = new IngredientsView();
                    ingredient.Id = item2.Id;                    
                    listOfIngredients.Add(ingredient);

                    var ingredientPerDish = new CreateNewIngredient();
                    ingredientPerDish.Amount = item2.Amount;
                    listOfAmounts.Add(ingredientPerDish);
                }
                int numberOfIngredientsInDishes = listOfIngredients.Count();
                var amoutOfIngredientsInDishes = listOfAmounts.Sum(x => x.Amount);
                var ingredientsUsage = new IngredientsUsageViewModel()
                {
                    Id = item.Id,
                    Name=item.Name,
                    TotalAmount=amoutOfIngredientsInDishes,
                    NumberOfDishes= numberOfIngredientsInDishes
                };
                ingredientsUsageList.Add(ingredientsUsage);
                
            }
            return ingredientsUsageList;


        }
    }
}

