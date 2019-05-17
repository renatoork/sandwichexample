using Microsoft.EntityFrameworkCore;
using Sandwish.Server.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandwish.Server.Repository
{
    public class SandwishRepository : ISandwishRepository
    {
        private SandwishDbContext _context;

        public SandwishRepository(SandwishDbContext context)
        {
            _context = context;
        }
        public async Task AddCart(Cart cart)
        {
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();
            return;
        }

    }
}
