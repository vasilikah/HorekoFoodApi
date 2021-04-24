using Domain.Models.DbModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Models.ViewModels.CreateNewDish
{
    public class CreateNewDish
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Maximum 50 characters")]
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public DateTime UpdatedOn { get; set; }

        [MinLength(1)]
        public List<CreateNewIngredient> Ingredients { get; set; }
    }
}
