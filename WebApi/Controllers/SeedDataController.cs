using Domain.SeedServices.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SeedDataController : ControllerBase
    {

        private readonly ISeedService _seedService;
        public SeedDataController(ISeedService seedService)
        {
           
            _seedService = seedService;
        }

        [HttpGet]
        public async Task<IActionResult> SeedIngredientsDataBase()
        {
            await _seedService.InsertIngredientsIntoDB();
            return Content("Database was succesfully updated");
        }

        [HttpGet]
        public async Task<IActionResult> SeedDishesDataBase()
        {
            await _seedService.InsertDishedIntoDb();
            return Content("Database was succesfully updated");

        }
    }
}
