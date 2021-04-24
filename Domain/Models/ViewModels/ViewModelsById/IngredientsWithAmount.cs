using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.ViewModels.ViewModelsById
{
   public class IngredientsWithAmount
    {
        public int IngredientId { get; set; }
        public string Name { get; set; }
        public double Amount { get; set; }
    }
}
