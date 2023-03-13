﻿using PlatformService.Application.Commands;
using PlatformService.Application.Queries;
using System.Text;
using System.Text.Json;

namespace PlatformService.SyncDataServices.Http
{
    public class HttpCommandDataClient : ICommandDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public HttpCommandDataClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task SendPlatformToCommand(CreatePlatformRequest plat)
        {
            var httpContent = new StringContent(JsonSerializer.Serialize(plat), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_configuration["CommandService"]}/api/c/platforms", httpContent);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Beautiful");
            }
            else
            {
                Console.WriteLine("Not beautiful");
            }
        }
    }
}
