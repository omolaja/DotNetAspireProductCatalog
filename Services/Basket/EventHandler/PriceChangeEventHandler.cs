using EventMessaging.Events;
using MassTransit;

namespace Basket.EventHandler
{
    public class PriceChangeEventHandler(BasketService basketService) : IConsumer<PriceChangeIntegrationEvent>
    {
       public async Task Consume(ConsumeContext<PriceChangeIntegrationEvent> context)
        {
            await basketService.UpdateProductPriceInAllBasketsAsync(context.Message.ProductId, context.Message.Price);
        }
    }

}
