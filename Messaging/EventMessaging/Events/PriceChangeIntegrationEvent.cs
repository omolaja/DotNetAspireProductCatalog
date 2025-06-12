using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventMessaging.Events
{
   public class PriceChangeIntegrationEvent : EventIntegration
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; } = default;

        public string ProductDescription { get; set; } = default;

        public decimal Price { get; set; }

        public string ProductImage { get; set; } = default;
    }
}
