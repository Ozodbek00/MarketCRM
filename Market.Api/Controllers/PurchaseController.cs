using Market.Domain.Configurations;
using Market.Domain.Entities;
using Market.Service.DTOs.PurchaseDtos;
using Market.Service.Interfaces.IPurchaseServices;
using Microsoft.AspNetCore.Mvc;

namespace Market.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseService purchaseService;

        public PurchaseController(IPurchaseService purchaseService)
        {
            this.purchaseService = purchaseService;
        }

        [HttpGet]
        public async ValueTask<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
            => Ok(await purchaseService.GetAllAsync(@params));

        [HttpGet("{Id}")]
        public async ValueTask<IActionResult> GetAsync([FromRoute(Name = "Id")] long id)
            => Ok(await purchaseService.GetAsync(u => u.Id == id));

        [HttpPost]
        public async ValueTask<ActionResult<Purchase>> AddAsync(PurchaseForCreation dto)
            => Ok(await purchaseService.AddAsync(dto));

        [HttpPut("{Id}")]
        public async ValueTask<ActionResult<Purchase>> UpdateAsync([FromRoute(Name = "Id")] long id, PurchaseForCreation dto)
            => Ok(await purchaseService.UpdateAsync(id, dto));

        [HttpDelete("{Id}")]
        public async ValueTask<ActionResult<bool>> DeleteAsync([FromRoute(Name = "Id")] long id)
            => await purchaseService.DeleteAsync(p => p.Id == id);
    }
}
