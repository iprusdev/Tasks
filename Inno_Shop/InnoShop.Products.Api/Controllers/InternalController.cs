using InnoShop.Products.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace InnoShop.Products.Api.Controllers
{
    [Route("api/internal")] // Особый маршрут
    [ApiController]
    public class InternalController : ControllerBase
    {
        private readonly IProductService _productService;

        public InternalController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPut("user/{userId}/visibility")]
        public async Task<IActionResult> ChangeVisibility(Guid userId, [FromQuery] bool isVisible)
        {
            await _productService.SwitchUserProductsAvailabilityAsync(userId, isVisible);
            return Ok();
        }
    }
}