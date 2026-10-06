using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using IDEManager.Models;

namespace IDEManager.Services
{
    public class GitHubService
    {
        private readonly HttpClient _httpClient;

        public GitHubService()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("IDEManager");
        }

        public async Task<RepositoryInfo?> GetRepositoryAsync(string owner, string repo)
        {
            var url = $"https://api.github.com/repos/{owner}/{repo}";

            try
            {
                var response = await _httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                    return null;

                var json = await response.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(json);
                var root = doc.RootElement;

                return new RepositoryInfo
                {
                    Id = 0,
                    Name = root.GetProperty("name").GetString() ?? string.Empty,
                    Owner = root.GetProperty("owner").GetProperty("login").GetString() ?? string.Empty,
                    RemoteUrl = root.GetProperty("html_url").GetString() ?? string.Empty,
                    Provider = "GitHub",
                    Branch = "main"
                };
            }
            catch
            {
                return null;
            }
        }
    }
}
