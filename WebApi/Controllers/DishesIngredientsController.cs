using Domain.Interfaces;
using Domain.Models;
using Domain.Models.DbModels;
using Domain.Models.ViewModels;
using Domain.Models.ViewModels.CountingDishesIngredients;
using Domain.Models.ViewModels.CreateNewDish;
using Domain.Models.ViewModels.IngredientsUsage;
using Domain.Models.ViewModels.ViewModelsById;
using Domain.SeedServices.Interfaces;
using Domain.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DishesIngredientsController : ControllerBase
    {
        private readonly IIngredientsRepository _ingredientsRepository;
        private readonly IDishesIngredientsRepository _dishesIngredientsRepository;
        private readonly IDishesRepository _dishesRepository;
        private readonly IDishesIngredientsService _dishIngredientsService;
        public DishesIngredientsController(IIngredientsRepository ingredientsRepository, IDishesIngredientsRepository dishesIngredientsRepository, IDishesRepository dishesRepository, IDishesIngredientsService dishIngredientsService)
        {
            _ingredientsRepository = ingredientsRepository;
            _dishesIngredientsRepository = dishesIngredientsRepository;
            _dishesRepository = dishesRepository;
            _dishIngredientsService = dishIngredientsService;
        }
       
        [HttpGet]
        public async Task<List<DishesView>> GetListDishes()
        {
            var data = await _dishIngredientsService.GetListDishes();
            return data;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DishesById>> GetDishById(int id)
        {
            var exist = await _dishesRepository.ExistsCheckById(id);
            if (!exist)
            {
                return StatusCode((int)HttpStatusCode.Conflict, "Dish with id: " + id + " is not found");
            }
            var data = await _dishIngredientsService.GetDishById(id);
            return data;
        }

        [HttpPost]
        public async Task<ActionResult<CreateNewDish>> AddDish(CreateNewDish newDish)
        {
            
           var dishName =await _dishesRepository.CheckByName(newDish.Name);
            if(dishName)
            {
                 return StatusCode((int)HttpStatusCode.Conflict, "Dish with name: " + newDish.Name +  " already exists");
            }
           
            var postDish = await _dishIngredientsService.AddNewDish(newDish);

            return CreatedAtAction(nameof(GetDishById), new { id = postDish.Id }, postDish.Id);
        }
        [HttpGet]
        public async Task<List<DishesCountPrice>> CalculateIngredients()
        {
            var data = await _dishIngredientsService.CalculateIngredients();
            return data;
        }
        [HttpGet]
        public async Task<List<IngredientsUsageViewModel>> IngredientsUsage()
        {
            var data = await _dishIngredientsService.IngredientsUsage();
            return data;
        }
    }
    
}

