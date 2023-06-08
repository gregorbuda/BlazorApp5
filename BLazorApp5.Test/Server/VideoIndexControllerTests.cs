using BlazorApp5.Server.Controllers;
using BlazorApp5.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLazorApp5.Test.Server
{
    [TestClass]
    public class VideoIndexControllerTests
    {
        private AzureConfiguration AzureConfiguration { get; set; }
        [TestInitialize]
        public void InitializeTests()
        {
            ConfigurationBuilder configurationBuilder =
                new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("appsettings.json");
            IConfiguration configuration = configurationBuilder.Build();
            var azureConfiguration = configuration.GetSection("AzureConfiguration").Get<AzureConfiguration>();
            AzureConfiguration = azureConfiguration;
        }


        [TestMethod]
        public async Task ListVideosTestAsync()
        {
            VideoIndexerController controller =
                new VideoIndexerController(this.AzureConfiguration);

            var result = await controller.ListVideos();
            Assert.IsNotNull(result, "Invalid Result");

        }
        [TestMethod]
        public async Task GetAccountAccessToken()
        {
            VideoIndexerController controller =
                new VideoIndexerController(this.AzureConfiguration);
            var result = await controller.GetAccountAccessToken(false);
            Assert.IsTrue(result is OkObjectResult, "Invalid Result");
        }
    }
}
