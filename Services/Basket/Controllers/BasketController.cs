using Microsoft.AspNetCore.Mvc;

namespace Basket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController(BasketService basketService) : Controller
    {
        private readonly BasketService _basketService = basketService;

        [HttpGet]
        public async Task<IActionResult> GetBasket([FromQuery] string userId)
        {
            var response = await _basketService.GetBasket(userId);
            if (response == null)
            {
                //return Results.NotFound(); Minimal Api
                return NotFound($"Basket for user {userId} not found.");
            }
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBasket([FromBody] ShoppingCart shoppingCart)
        {
            await _basketService.UpdateBasket(shoppingCart);
            //return Results.Created("GetBasket", shoppingCart); Minimal Api
            return CreatedAtAction("GetBasket", new { id = shoppingCart.UserId }, shoppingCart);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBasket([FromQuery] string userId)
        {
            await _basketService.DeleteBasket(userId);
            //return Results.NoContent(); Minimal Api
            return NoContent();
        }
    }
}
