using Sandwish.Server.Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sandwish.Server.Repository
{
    public interface ISandwishRepository
    {
        Task AddCart(Cart card);
    }
}
