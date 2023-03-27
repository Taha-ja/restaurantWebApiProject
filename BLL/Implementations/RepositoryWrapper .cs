using BLL.Contracts;
using BLL.Implementations.Services;
using DAL.Entities.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Implementations
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private RepositoryContext _repoContext;
        private IRestaurantService? _restaurant;
        public IRestaurantService Restaurant
        {
            get
            {
                if (_restaurant == null)
                {
                    _restaurant = new RestaurantService(_repoContext);
                }
                return _restaurant;
            }
        }

        public RepositoryWrapper(RepositoryContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }
        public void Save()
        {
            _repoContext.SaveChanges();
        }
    }
}
