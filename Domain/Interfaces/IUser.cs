using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUser : IGenericRepository<User>
    {
        Task<User> GetByRefreshTokenAsync(string refreshToken);
        Task<User> GetByUsernameAsync(string userName);
    }
}