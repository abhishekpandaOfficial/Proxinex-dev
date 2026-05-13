using System.Net.Http.Json;
using Proxinex.Shared.Contracts.Rag;

namespace Proxinex.ChatService.Infrastructure.Rag;

public class RagServiceClient
    : IRagServiceClient
{
    private readonly HttpClient _httpClient;

    public RagServiceClient(
        HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<string>> SearchAsync(
        string query)
    {
        var response =
            await _httpClient.PostAsJsonAsync(
                "http://localhost:5240/api/rag/search",
                new
                {
                    query
                });

        response.EnsureSuccessStatusCode();

        var result =
            await response.Content
                .ReadFromJsonAsync<RagSearchResponse>();

        return result?.Chunks
            ?? new List<string>();
    }
}