using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities.Models
{
    public class Restaurant
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Restaurant name is required")]
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public int CuisineId { get; set; }

        public Cuisine? Cuisine { get; set; }
    }
}
