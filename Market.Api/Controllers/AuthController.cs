using Market.Service.DTOs.UserDtos;
using Market.Service.Interfaces.IAuthServices;
using Microsoft.AspNetCore.Mvc;

namespace Market.Api.Controllers
{
    [ApiController, Route("[controller]")]
    public class AuthController: ControllerBase
    {
        private readonly IAuthService authService;
        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserForLogin dto)
        {
            var token = await authService.GenerateTokenAsync(dto.Login, dto.Password);

            return Ok(new
            {
                Token = token
            });
        }
    }
}
