using Market.Domain.Configurations;
using Market.Domain.Entities;
using Market.Service.DTOs.OrderDtos;
using Market.Service.Interfaces.IOrderService;
using Microsoft.AspNetCore.Mvc;

namespace Market.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpGet]
        public async ValueTask<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
            => Ok(await orderService.GetAllAsync(@params));

        [HttpGet("{Id}")]
        public async ValueTask<IActionResult> GetAsync([FromRoute(Name = "Id")] long id)
            => Ok(await orderService.GetAsync(u => u.Id == id));

        [HttpPost]
        public async ValueTask<ActionResult<Order>> AddAsync(OrderForCreation dto)
            => Ok(await orderService.AddAsync(dto));

        [HttpPut("{Id}")]
        public async ValueTask<ActionResult<Order>> UpdateAsync([FromRoute(Name = "Id")] long id, OrderForCreation dto)
            => Ok(await orderService.UpdateAsync(id, dto));

        [HttpDelete("{Id}")]
        public async ValueTask<ActionResult<bool>> DeleteAsync([FromRoute(Name = "Id")] long id)
            => await orderService.DeleteAsync(p => p.Id == id);
    }
}
