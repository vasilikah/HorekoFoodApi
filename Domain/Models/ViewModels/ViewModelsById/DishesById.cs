using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.ViewModels.ViewModelsById
{
    public class DishesById
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime UpdatedOn { get; set; }
        public List<ParentDish> ParentDish { get; set; }
        public List<IngredientsWithAmount> Ingredients { get; set; }
    }
}
