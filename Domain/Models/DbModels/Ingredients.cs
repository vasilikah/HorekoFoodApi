using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models.DbModels
{
    public class Ingredients
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
    }


}
