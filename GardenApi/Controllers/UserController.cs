using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GardenApi.Dtos;
using GardenApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace GardenApi.Controllers
{
    public class UserController : ApiController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("register")]
        public async Task<ActionResult> RegisterAsync(RegisterDto model)
        {
            var result = await _userService.RegisterAsync(model);
            return Ok(result);
        }
        [HttpPost("token")]
        public async Task<ActionResult> GetTokenAsync(LoginDto model)
        {
            var result = await _userService.GetTokenAsync(model);
            SetRefreshTokenInCookie(result.RefreshToken);
            return Ok(result);
        }
        [HttpPost("addrol")]
        public async Task<ActionResult> AddRolAsync(AddRolDto model)
        {
            var result = await _userService.AddRolAsync(model);
            return Ok(result);
        }
        [HttpPost("refresh-token")]
        public async Task<ActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var result = await _userService.RefreshTokenAsync(refreshToken);
            if (!string.IsNullOrEmpty(result.RefreshToken))
            {
                SetRefreshTokenInCookie(result.RefreshToken);
            }
            return Ok(result);
        }

        private void SetRefreshTokenInCookie(string refreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(2),
            };
            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }
    }
}