using System.Net.Http.Json;
using GeminiCatalog.Infrastructure.Services.Inventory.Contracts;

namespace GeminiCatalog.Infrastructure.Services.Inventory;

public sealed class InventoryServiceClient
{
    private readonly HttpClient _httpClient;

    public InventoryServiceClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<InventoryResponse>> GetInventoryByProductsAsync(IEnumerable<Guid> productIds, CancellationToken cancellationToken = default)
    {
        var payload = new { ProductIds = productIds };
        var request = new HttpRequestMessage(HttpMethod.Post, "/inventory/by-products")
        {
            Content = JsonContent.Create(payload)
        };

        var response = await _httpClient.SendAsync(request, cancellationToken);
        response.EnsureSuccessStatusCode();

        var inventoryData = await response.Content.ReadFromJsonAsync<IEnumerable<InventoryResponse>>(cancellationToken: cancellationToken);
        return inventoryData ?? Enumerable.Empty<InventoryResponse>();
    }
}