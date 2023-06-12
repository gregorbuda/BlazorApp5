﻿using BlazorApp5.Shared;
using BlazorApp5.Shared.Models.AzureVideoIndexer.ListVideos;
using BlazorApp5.Shared.Models.AzureVideoIndexer.SearchVideos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace BlazorApp5.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VideoIndexerController : ControllerBase
    {
        private AzureConfiguration AzureConfiguration { get; }
        public VideoIndexerController(AzureConfiguration azureConfiguration)
        {
            this.AzureConfiguration = azureConfiguration;
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> ListVideos()
        {
            var accountAccessToken = 
                await this.GetAccountAccessTokenString(false);
            string requestUrl = $"{this.AzureConfiguration.VideoIndexerConfiguration.BaseAPIUrl}" +
                $"/{this.AzureConfiguration.VideoIndexerConfiguration.Location}" +
                $"/Accounts/{this.AzureConfiguration.VideoIndexerConfiguration.AccountId}" +
                $"/Videos?accessToken={accountAccessToken}";
            HttpClient client = new HttpClient();
            var result = await client.GetFromJsonAsync<ListVideosResponse>(requestUrl);
            return Ok(result);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAccountAccessToken(bool allowEdit = false)
        {
            string result = await GetAccountAccessTokenString(allowEdit);
            return Ok(result);
        }

        public async Task<string> GetAccountAccessTokenString(bool allowEdit)
        {
            string requestUrl = $"https://api.videoindexer.ai" +
                $"/Auth/{this.AzureConfiguration.VideoIndexerConfiguration.Location}" +
                $"/Accounts/{this.AzureConfiguration.VideoIndexerConfiguration.AccountId}" +
                $"/AccessToken" +
                $"?allowEdit={allowEdit}";
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key",
                this.AzureConfiguration.VideoIndexerConfiguration.SubscriptionKey);
            var result = await client.GetStringAsync(requestUrl);
            return result.Replace("\"", "");
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetVideoAccessToken(string videoId, bool allowEdit=false)
        {
            var videoAccessToken = await this.GetVideoAccessTokenString(videoId, allowEdit);

            return Ok(videoAccessToken);
        }

        private async Task<string> GetVideoAccessTokenString(string videoId, bool allowEdit = false)
        {
            string requestUrl = $"{this.AzureConfiguration.VideoIndexerConfiguration.BaseAPIUrl}" +
            $"/Auth/{this.AzureConfiguration.VideoIndexerConfiguration.Location}" +
            $"/Accounts/{this.AzureConfiguration.VideoIndexerConfiguration.AccountId}" +
            $"/Videos/{videoId}/AccessToken" +
            $"?allowEdit={allowEdit}";
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key",
               this.AzureConfiguration.VideoIndexerConfiguration.SubscriptionKey);
            var result = await client.GetStringAsync(requestUrl);
            return result.Replace("\"", string.Empty);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetVideoThumbnail(string videoId, string thumbnailId)
        {
            string videoAccessToken = await this.GetVideoAccessTokenString(videoId, true);
            string format = "Base64";
            string requestUrl = $"{this.AzureConfiguration.VideoIndexerConfiguration.BaseAPIUrl}" +
                $"/{this.AzureConfiguration.VideoIndexerConfiguration.Location}" +
                $"/Accounts/{this.AzureConfiguration.VideoIndexerConfiguration.AccountId}" +
                $"/Videos/{videoId}" +
                $"/Thumbnails/{thumbnailId}" +
                $"?format={format}" +
                $"&accessToken={videoAccessToken}";
            HttpClient client = new HttpClient();
            var result = await client.GetStringAsync(requestUrl);
            return Ok(result);
        }

        [HttpGet("[action]")]
        public IActionResult GetLocation()
        {
            return Ok(this.AzureConfiguration.VideoIndexerConfiguration.Location);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> SearchVideos(string keyword)
        {
            var accountAccesstoken = await this.GetAccountAccessTokenString(false);
            string requestUrl = $"https://api.videoindexer.ai/" +
                $"{this.AzureConfiguration.VideoIndexerConfiguration.Location}" +
                $"/Accounts/{this.AzureConfiguration.VideoIndexerConfiguration.AccountId}" +
                $"/Videos/Search?" +
                $"query={keyword}" + 
            //$"[?sourceLanguage]" +
            //$"[&hasSourceVideoFile]" +
            //$"[&sourceVideoId]" +
            //$"[&privacy]" +
            //$"[&id]" +
            //$"[&partition]" +
            //$"[&externalId]" +
            //$"[&owner]" +
            //$"[&face]" +
            //$"[&animatedcharacter]" +
            //$"[&textScope]" +
            //$"[&language]" +
            //$"[&createdAfter]" +
            //$"[&createdBefore]" +
            //$"[&pageSize]" +
            //$"[&skip]" +
            $"&accessToken={accountAccesstoken}";

            HttpClient client = new HttpClient();
            var result = await client.GetFromJsonAsync<SearchVideosResponse>(requestUrl);
            return Ok(result);
        }
    }
}
