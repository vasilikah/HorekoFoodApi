using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.DbModels
{
        public class Dishes
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public DateTime UpdatedOn { get; set; }
        public List<DishIngredient> Ingredients { get; set; }
    }
}



