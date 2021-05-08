using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace UploadFilesServer.Tests
{
    public class FileUploadTests
    {
        private readonly HttpClient _client;

        public FileUploadTests()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var server = new TestServer(new WebHostBuilder()
                .UseConfiguration(configuration)
                .UseStartup<Startup>());
            _client = server.CreateClient();
        }

        [Fact(DisplayName = "Upload should read the contents from the file stream")]
        public async Task Upload_ReadsFileStream()
        {
            // Act
            HttpResponseMessage response;

            using (var file1 = File.OpenRead(@"//Users/ozgur/Documents/TradingViewTactics.png"))
            using (var content1 = new StreamContent(file1))
            using (var formData = new MultipartFormDataContent())
            {
                // Add file (file, field name, file name)
                formData.Add(content1, "files", "TradingViewTactics.png");
                response = await _client.PostAsync("/api/files/create", formData);
            }

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            //Assert.NotEmpty(responseString);
            //Assert.Equal(expectedContentType, response.Content.Headers.ContentType.ToString());

            response.Dispose();
        }
    }
}
