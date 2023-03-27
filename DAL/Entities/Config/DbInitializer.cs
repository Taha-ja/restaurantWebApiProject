using DAL.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities.Config
{
    public class DbInitializer
    {
        private readonly ModelBuilder _modelBuilder;

        public DbInitializer(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
        }

        public void Seed()
        {
            SeedCuisines();
            SeedRestaurants();
        }

        private void SeedCuisines()
        {
            _modelBuilder.Entity<Cuisine>().HasData(
            new Cuisine
            {
                Id = 1,
                Name = "Italian"
            },
            new Cuisine
            {
                Id = 2,
                Name = "French"
            },
            new Cuisine
            {
                Id = 3,
                Name = "Japanese"
            }
            );
        }



        private void SeedRestaurants()
        {
            _modelBuilder.Entity<Restaurant>().HasData(

            new Restaurant
            {
                Id = 1,
                Name = "Pizza Hut",
                Description = "A classic Italian pizza",
                CuisineId = 1
            },
            new Restaurant
            {
                Id = 2,
                Name = "French Bistro",
                Description = "A classic French bistro",
                CuisineId = 2
            },
            new Restaurant
            {
                Id = 3,
                Name = "Sushi Bar",
                Description = "A trendy Japanese sushi bar",
                CuisineId = 3
            }
            );
        }
    }
}
