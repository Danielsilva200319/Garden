using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Persistence.Data;

namespace Application.Repositories
{
    public class RolRepository : GenericRepository<Rol>, IRol
    {
        private readonly GardenContext _context;
        public RolRepository(GardenContext context) : base(context)
        {
            _context = context;
        }
    }
}