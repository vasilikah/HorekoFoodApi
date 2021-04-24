using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.DbModels
{
    public class DishIngredient
    {
        public int Id { get; set; }
        public int IngredientId { get; set; }
        public double Amount { get; set; }
        public int DishesId { get; set; }
    }
}
