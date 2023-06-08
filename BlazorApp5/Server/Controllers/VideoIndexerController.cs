using BlazorApp5.Shared;
using BlazorApp5.Shared.Models.AzureVideoIndexer.ListVideos;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;
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



    }
}
