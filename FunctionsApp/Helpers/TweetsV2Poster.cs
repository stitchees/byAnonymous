using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Tweetinvi;
using Tweetinvi.Core.Web;

namespace FunctionApp1.Helpers
{
    public class TweetsV2Poster
    {
        // ----------------- Fields ----------------

        private readonly ITwitterClient client;

        // ----------------- Constructor ----------------

        public TweetsV2Poster(ITwitterClient client)
        {
            this.client = client;
        }

        public Task<ITwitterResult> PostTweet(TweetV2PostRequest tweetParams)
        {
            return client.Execute.AdvanceRequestAsync(
                (request) =>
                {
                    var jsonBody = client.Json.Serialize(tweetParams);
                    
                    var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                    request.Query.Url = "https://api.twitter.com/2/tweets";
                    request.Query.HttpMethod = Tweetinvi.Models.HttpMethod.POST;
                    request.Query.HttpContent = content;
                }
            );
        }
    }

    public class TweetV2PostRequest
    {
        [JsonProperty("text")]
        public string Text { get; set; } = string.Empty;
    }
}
