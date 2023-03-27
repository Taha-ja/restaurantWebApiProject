﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Contracts
{
    public interface IRepositoryWrapper
    {
        IRestaurantService Restaurant { get; }
        void Save();
    }
}
