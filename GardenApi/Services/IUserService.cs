using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GardenApi.Dtos;

namespace GardenApi.Services;
public interface IUserService
{
    Task<string> RegisterAsync(RegisterDto model);
    Task<DataUserDto> GetTokenAsync(LoginDto model);
    Task<string> AddRolAsync(AddRolDto model);
    Task<DataUserDto> RefreshTokenAsync(string RefreshToken);
}