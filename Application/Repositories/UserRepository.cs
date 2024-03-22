using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Persistence.Data;

namespace Application.Repositories
{
    public class UserRepository : GenericRepository<User>, IUser
    {
        private readonly GardenContext _context;
        public UserRepository(GardenContext context) : base(context)
        {
            _context = context;
        }

        public Task<User> GetByRefreshTokenAsync(string refreshToken)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByUsernameAsync(string userName)
        {
            throw new NotImplementedException();
        }
    }
}