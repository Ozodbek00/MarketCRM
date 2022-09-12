using Market.Domain.Configurations;
using Market.Domain.Entities;
using Market.Service.DTOs.UserDtos;
using Market.Service.Interfaces.IUserServices;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Market.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly ILogger<UserController> logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            this.userService = userService;
            this.logger = logger;
        }

        [HttpGet]
        public async ValueTask<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
            => Ok(await userService.GetAllAsync(@params));

        [HttpGet("{Id}")]
        public async ValueTask<IActionResult> GetAsync([FromRoute(Name = "Id")] long id)
            => Ok(await userService.GetAsync(u => u.Id == id));

        [HttpPost]
        public async ValueTask<ActionResult<User>> AddAsync(UserForCreation dto)
            => Ok(await userService.AddAsync(dto));

        [HttpPut("{Id}")]
        public async ValueTask<ActionResult<User>> UpdateAsync([FromRoute(Name = "Id")] long id, UserForCreation dto)
            => Ok(await userService.UpdateAsync(id, dto));

        [HttpDelete("{Id}")]
        public async ValueTask<ActionResult<bool>> DeleteAsync([FromRoute(Name = "Id")] long id)
            => await userService.DeleteAsync(p => p.Id == id);
    }
}
