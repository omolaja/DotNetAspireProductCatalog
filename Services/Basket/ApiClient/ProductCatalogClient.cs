

namespace Basket.ApiClient
{
    public class ProductCatalogClient(HttpClient httpClient)
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<Product>GetProductById(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<Product>($"api/ProductCatalog/{id}");//ProductCatalog
            return response!;
        }
    }
}
