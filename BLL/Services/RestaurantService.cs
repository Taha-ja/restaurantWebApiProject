using BLL.Contracts;
using DAL.Entities.Config;
using DAL.Entities.Models;
using Microsoft.EntityFrameworkCore;


namespace BLL.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly RepositoryContext _repositoryContext;

        public RestaurantService(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public async Task<IEnumerable<Restaurant>> GetRestaurantsAsync()
        {
            return await _repositoryContext.Restaurants.Include(r => r.Cuisine).ToListAsync();
        }

        public async Task<Restaurant?> GetRestaurantByIdAsync(int id)
        {
            return await _repositoryContext.Restaurants.Include(r => r.Cuisine).FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task AddRestaurantAsync(Restaurant restaurant)
        {
            await _repositoryContext.Restaurants.AddAsync(restaurant);
            await _repositoryContext.SaveChangesAsync();
        }

        public async Task UpdateRestaurantAsync(Restaurant restaurant)
        {
            _repositoryContext.Entry(restaurant).State = EntityState.Modified;
            await _repositoryContext.SaveChangesAsync();
        }

        public async Task DeleteRestaurantAsync(int id)
        {
            var restaurant = await _repositoryContext.Restaurants.FindAsync(id);
            if (restaurant is null)
                return;

            _repositoryContext.Restaurants.Remove(restaurant);
            await _repositoryContext.SaveChangesAsync();
        }
    }
}
