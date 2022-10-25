using AxxesMarket.Shared.WebModels;
using MediatR;
using System.Net.Http.Json;

namespace AxxesMarket.SPA.Client.Utils;

public class BlazorHttpClient
{
    private readonly HttpClient _httpClient;
    private IMediator _mediator;

    public BlazorHttpClient(HttpClient httpClient, IMediator mediator)
    {
        _httpClient = httpClient;
        _mediator = mediator;
    }

    public async Task<JsonResponse<T>> GetAsync<T>(string route, string? successMessage = null)
    {
        var httpResponse = await _httpClient.GetFromJsonAsync<JsonResponse<T>>(route);
        return httpResponse;
    }

    public async Task<JsonResponse<TResponse>> PostAsync<TBody, TResponse>(string route, TBody body, string? successMessage = null)
    {
        var httpResponse = await _httpClient.PostAsJsonAsync(route, body);
        var response = await httpResponse.Content.ReadFromJsonAsync<JsonResponse<TResponse>>();
        return response;
    }

    public async Task<JsonResponse<TResponse>> PutAsync<TBody, TResponse>(string route, TBody body, string? successMessage = null)
    {
        var httpResponse = await _httpClient.PutAsJsonAsync(route, body);
        var response = await httpResponse.Content.ReadFromJsonAsync<JsonResponse<TResponse>>();
        return response;
    }

    private async Task HandleResponse(JsonResponse response, string? SuccessMessage = null)
    {
    }
}
