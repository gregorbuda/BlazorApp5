using BlazorApp5.Server.Controllers;
using BlazorApp5.Shared;
using BlazorApp5.Shared.Models.AzureVideoIndexer.ListVideos;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using LV = BlazorApp5.Shared.Models.AzureVideoIndexer.ListVideos;

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

        [TestMethod]
        public async Task GetVideoAccessTokenAsync()
        {
            VideoIndexerController controller =
                new VideoIndexerController(this.AzureConfiguration);
            OkObjectResult videoListResult = (OkObjectResult)await controller.ListVideos();
            var videList =
                (LV.ListVideosResponse)videoListResult.Value;
            var firstVideo = videList.results.First();
            var result = await controller.GetVideoAccessToken(firstVideo.id, false);
            Assert.IsTrue(result is OkObjectResult, "Invalid Result");
        }

        [TestMethod]
        public async Task GetVideoThumbnailAsync()
        {
            VideoIndexerController controller =
                new VideoIndexerController(this.AzureConfiguration);
            OkObjectResult videoListResult = (OkObjectResult)await controller.ListVideos();
            var videList = 
                (LV.ListVideosResponse)videoListResult.Value;
            var firstVideo = videList.results.First();
            var result = await controller.GetVideoThumbnail(videoId: firstVideo.id, thumbnailId: firstVideo.thumbnailId);
                 Assert.IsTrue(result is OkObjectResult, "Invalid Result");

        }

        [TestMethod]
        public void GetLocationAsync()
        {
            VideoIndexerController controller =
                new VideoIndexerController(this.AzureConfiguration);
            var result =  controller.GetLocation();
            Assert.IsTrue(result is OkObjectResult, "Invalid Result");

        }


        [TestMethod]
        public async Task GetLanguajesAsync()
        {
            VideoIndexerController controller =
          new VideoIndexerController(this.AzureConfiguration);
            var result = await controller.GetLanguajes();
            Assert.IsTrue(result is OkObjectResult, "Invalid Result");
        }

        //[TestMethod]
        //public async Task UploadVideo()
        //{
        //    VideoIndexerController controller =
        //    new VideoIndexerController(this.AzureConfiguration);
        //    var result = await controller.UploadVideo(language: "test");
        //    Assert.IsTrue(result is OkObjectResult, "Invalid Result");
        //}

    }
}
