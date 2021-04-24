using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.ViewModels
{
    public class DishesView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime UpdatedOn { get; set; }
        public List<DishIngredientsView> Ingredients { get; set; }
    }
}
