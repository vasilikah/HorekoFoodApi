using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.JsonModels
{
    public class ListIngredient
    {
        public int ingredientId { get; set; }
        public double amount { get; set; }
    }

    public class JsonDishes
    {
        public int id { get; set; }
        public string name { get; set; }
        public int? parentId { get; set; }
        public object updatedOn { get; set; }
        public List<ListIngredient> ingredients { get; set; }
    }

}
