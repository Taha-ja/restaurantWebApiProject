using DAL.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Contracts
{
    public interface IRestaurantService
    {
        Task AddRestaurantAsync(Restaurant restaurant);
        Task DeleteRestaurantAsync(int id);
        Task<Restaurant?> GetRestaurantByIdAsync(int id);
        Task<IEnumerable<Restaurant>> GetRestaurantsAsync();
        Task UpdateRestaurantAsync(Restaurant restaurant);
    }
}
