using System;
using Tweetinvi.Core.Web;
using Tweetinvi.Models;
using Tweetinvi;
using FunctionApp1.Helpers;

namespace FunctionApp1.Servicios
{
    internal class MensajeService
    {
        private Credentials ObjCredentials;

        private string consumerKey;
        private string consumerSecret;
        private string accessToken;
        private string accessTokenSecret;

        public MensajeService()
        {
            ObjCredentials = new Credentials();

            consumerKey = ObjCredentials.consumerKey;
            consumerSecret = ObjCredentials.consumerSecret;
            accessToken = ObjCredentials.accessToken;
            accessTokenSecret = ObjCredentials.accessTokenSecret;

        }

        public async void  MakeATweet(string message)
        {
            ITwitterCredentials credentials = new TwitterCredentials(consumerKey, consumerSecret, accessToken, accessTokenSecret);
            ITwitterClient client = new TwitterClient(credentials);

            var authenticatedUser = await client.Users.GetAuthenticatedUserAsync();
            Console.WriteLine("usuario autenticado -> " + authenticatedUser);


            TweetsV2Poster poster = new TweetsV2Poster(client);

            ITwitterResult result = await poster.PostTweet(
                new TweetV2PostRequest
                {
                    Text = message
                }
            );

            if (result.Response.IsSuccessStatusCode == false)
            {
                throw new Exception(
                    "Error when posting tweet: " + Environment.NewLine + result.Content
                );
            }
            Console.WriteLine("Tweet Creado Correctamente");

        }
    }
}
