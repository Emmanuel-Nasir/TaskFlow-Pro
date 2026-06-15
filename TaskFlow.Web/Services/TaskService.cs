using System.Net.Http.Json;
using System.Net.Http.Headers;
using TaskFlow.Web.Models;
//using TaskFlow.Web.Services;

namespace TaskFlow.Web.Services;

public class TaskService
{
    private readonly HttpClient _http;
    private readonly AuthService _auth;

    public TaskService(HttpClient http, AuthService auth) 
    {
        _http = http;
        _auth = auth;
    }

    // Attach token to every request
    private async Task SetAuthHeaderAsync()
    {
        var token = await _auth.GetTokenAsync();
        _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);
    }

    public async Task<List<TaskItem>> GetAllAsync()
    {
        await SetAuthHeaderAsync();
        return await _http.GetFromJsonAsync<List<TaskItem>>("api/tasks") ?? new();
    }

    public async Task<TaskItem?> GetByIdAsync(int id)
    {
        await SetAuthHeaderAsync();
        return await _http.GetFromJsonAsync<TaskItem>($"api/tasks/{id}");
    }

    public async Task CreateAsync(TaskItem task)
    {
        await SetAuthHeaderAsync();
        await _http.PostAsJsonAsync("api/tasks", task);
    }

    public async Task UpdateAsync(int id, TaskItem task)
    {
        await SetAuthHeaderAsync();
        await _http.PutAsJsonAsync($"api/tasks/{id}", task);
    }

    public async Task DeleteAsync(int id)
    {
        await SetAuthHeaderAsync();
        await _http.DeleteAsync($"api/tasks/{id}");
    }

    public async Task ToggleCompleteAsync(int id)
    {
        await SetAuthHeaderAsync();
        await _http.PatchAsync($"api/tasks/{id}/complete", null);
    }
}