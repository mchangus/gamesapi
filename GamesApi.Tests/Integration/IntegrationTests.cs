using Games.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace GamesApi.Tests.Integration
{
    public class IntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _httpClient;
        public IntegrationTests()
        {
            var application = new WebApplicationFactory<Program>().WithWebHostBuilder(builder => { });

            _httpClient = application.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7191/");
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        [Fact]        
        public async Task GamesReturnSuccessAndCorrectContentType()
        {
            // Act
            var response = await _httpClient.GetAsync("games?q=mario");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal("application/json", response.Content.Headers.ContentType.MediaType);

        }

        [Fact]
        public async Task CreateUserReturn_Status_Code_Created()
        {
            // Act
            var response = await _httpClient.PostAsync("users", null);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task CreateUser_InParallel()
        {
            // Act

            var list = new List<int>();
            for (int i = 0; i < 10000; i++) {
                list.Add(i);
            }

            var postTasks = list.Select(async p => await _httpClient.PostAsync("users", null));

            var posts = await Task.WhenAll(postTasks);


            // Assert

            var getTasks = list.Select(async id => {

               var response = await _httpClient.GetAsync($"users/{id}");
              
                Assert.True(response.IsSuccessStatusCode);

            });

        }
    }

}
