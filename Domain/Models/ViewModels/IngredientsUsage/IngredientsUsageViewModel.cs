using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.ViewModels.IngredientsUsage
{
    public class IngredientsUsageViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double TotalAmount { get; set; }
        public double NumberOfDishes { get; set; }
    }
}
