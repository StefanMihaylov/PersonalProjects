namespace TweeterBackup.Logic
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Security.Cryptography;
    using System.Text;

    // source code from http://johnnewcombe.net/blog/mvc-4-part-5
    public class Twitter : ITwitter
    {
        public const string OauthVersion = "1.0";
        public const string OauthSignatureMethod = "HMAC-SHA1";

        public Twitter(string consumerKey, string consumerKeySecret, string accessToken, string accessTokenSecret)
        {
            this.ConsumerKey = consumerKey;
            this.ConsumerKeySecret = consumerKeySecret;
            this.AccessToken = accessToken;
            this.AccessTokenSecret = accessTokenSecret;
        }

        public string ConsumerKey { get; private set; }

        public string ConsumerKeySecret { get; private set; }

        public string AccessToken { get; private set; }

        public string AccessTokenSecret { get; private set; }

        public string GetMentions(int count)
        {
            const string ResourceUrl = "http://api.twitter.com/1/statuses/mentions.json";

            var requestParameters = new SortedDictionary<string, string>();
            requestParameters.Add("count", count.ToString());
            requestParameters.Add("include_entities", "true");

            var response = this.GetResponse(ResourceUrl, Method.GET, requestParameters);
            return response;
        }

        public string GetTweets(string userId, int count)
        {
            const string ResourceUrl = "https://api.twitter.com/1.1/statuses/user_timeline.json";

            var requestParameters = new SortedDictionary<string, string>();
            requestParameters.Add("user_id", userId);
            requestParameters.Add("count", count.ToString());

            var response = this.GetResponse(ResourceUrl, Method.GET, requestParameters);
            return response;
        }

        public string GetTweetById(long id)
        {
            const string ResourceUrl = "https://api.twitter.com/1.1/statuses/show.json";

            var requestParameters = new SortedDictionary<string, string>();
            requestParameters.Add("id", id.ToString());

            var response = this.GetResponse(ResourceUrl, Method.GET, requestParameters);
            return response;
        }

        public string RetweetById(long id)
        {
            const string ResourceUrl = "https://api.twitter.com/1.1/statuses/retweet/{0}.json";
            
            var requestParameters = new SortedDictionary<string, string>();

            var url = string.Format(ResourceUrl, id);

            var response = this.GetResponse(url, Method.POST, requestParameters);
            return response;
        }

        public string PostStatusUpdate(string status, double latitude, double longitude)
        {
            const string ResourceUrl = "http://api.twitter.com/1/statuses/update.json";

            var requestParameters = new SortedDictionary<string, string>();
            requestParameters.Add("status", status);
            requestParameters.Add("lat", latitude.ToString());
            requestParameters.Add("long", longitude.ToString());

            var response = this.GetResponse(ResourceUrl, Method.POST, requestParameters);
            return response;
        }

        public string SendDirectMessage(string screenName, string text)
        {
            string resourceUrl = "https://api.twitter.com/1.1/direct_messages/new.json";

            var requestParameters = new SortedDictionary<string, string>();
            requestParameters.Add("screen_name", screenName);
            requestParameters.Add("text", text);

            var response = this.GetResponse(resourceUrl, Method.POST, requestParameters);
            return response;
        }

        public string GetUserInfo(string screenName)
        {
            string resourceUrl = "https://api.twitter.com/1.1/users/show.json";

            var requestParameters = new SortedDictionary<string, string>();
            requestParameters.Add("screen_name", screenName);

            // requestParameters.Add("text", text);
            var response = this.GetResponse(resourceUrl, Method.GET, requestParameters);
            return response;
        }

        private static string CreateOAuthTimestamp()
        {
            var nowUtc = DateTime.UtcNow;
            var timeSpan = nowUtc - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            var timestamp = Convert.ToInt64(timeSpan.TotalSeconds).ToString();

            return timestamp;
        }

        private string GetResponse(string resourceUrl, Method method, SortedDictionary<string, string> requestParameters)
        {
            ServicePointManager.Expect100Continue = false;
            WebRequest request = null;
            string resultString = string.Empty;

            if (method == Method.POST)
            {
                var postBody = requestParameters.ToWebString();
                request = (HttpWebRequest)WebRequest.Create(resourceUrl);
                request.Method = method.ToString();
                request.ContentType = "application/x-www-form-urlencoded";

                using (var stream = request.GetRequestStream())
                {
                    byte[] content = Encoding.ASCII.GetBytes(postBody);
                    stream.Write(content, 0, content.Length);
                }
            }
            else if (method == Method.GET)
            {
                request = (HttpWebRequest)WebRequest.Create(resourceUrl + "?" + requestParameters.ToWebString());
                request.Method = method.ToString();
            }
            else
            {
                // other verbs can be addressed here... 
            }

            if (request != null)
            {
                var authHeader = this.CreateHeader(resourceUrl, method, requestParameters);
                request.Headers.Add("Authorization", authHeader);

                var response = request.GetResponse();

                using (var sd = new StreamReader(response.GetResponseStream()))
                {
                    resultString = sd.ReadToEnd();
                    response.Close();
                }
            }

            return resultString;
        }

        private string CreateOauthNonce()
        {
            return Convert.ToBase64String(new ASCIIEncoding().GetBytes(DateTime.Now.Ticks.ToString()));
        }

        private string CreateHeader(string resourceUrl, Method method, SortedDictionary<string, string> requestParameters)
        {
            var oauthNonce = this.CreateOauthNonce();
            var oauthTimestamp = CreateOAuthTimestamp();
            var oauthSignature = this.CreateOauthSignature(resourceUrl, method, oauthNonce, oauthTimestamp, requestParameters);

            // The oAuth signature is then used to generate the Authentication header. 
            const string HeaderFormat = "OAuth oauth_nonce=\"{0}\", oauth_signature_method=\"{1}\", " + "oauth_timestamp=\"{2}\", oauth_consumer_key=\"{3}\", " + "oauth_token=\"{4}\", oauth_signature=\"{5}\", " + "oauth_version=\"{6}\"";

            var authHeader = string.Format(HeaderFormat, Uri.EscapeDataString(oauthNonce), Uri.EscapeDataString(OauthSignatureMethod), Uri.EscapeDataString(oauthTimestamp), Uri.EscapeDataString(this.ConsumerKey), Uri.EscapeDataString(this.AccessToken), Uri.EscapeDataString(oauthSignature), Uri.EscapeDataString(OauthVersion));

            return authHeader;
        }

        private string CreateOauthSignature(string resourceUrl, Method method, string oauthNonce, string oauthTimestamp, SortedDictionary<string, string> requestParameters)
        {
            // firstly we need to add the standard oauth parameters to the sorted list 
            requestParameters.Add("oauth_consumer_key", this.ConsumerKey);
            requestParameters.Add("oauth_nonce", oauthNonce);
            requestParameters.Add("oauth_signature_method", OauthSignatureMethod);
            requestParameters.Add("oauth_timestamp", oauthTimestamp);
            requestParameters.Add("oauth_token", this.AccessToken);
            requestParameters.Add("oauth_version", OauthVersion);

            var sigBaseString = requestParameters.ToWebString();
            var signatureBaseString = string.Concat(method.ToString(), "&", Uri.EscapeDataString(resourceUrl), "&", Uri.EscapeDataString(sigBaseString.ToString()));

            // Using this base string, we then encrypt the data using a composite of the //secret keys and the HMAC-SHA1 algorithm. 
            var compositeKey = string.Concat(Uri.EscapeDataString(this.ConsumerKeySecret), "&", Uri.EscapeDataString(this.AccessTokenSecret));

            string oauthSignature;

            using (var hasher = new HMACSHA1(Encoding.ASCII.GetBytes(compositeKey)))
            {
                oauthSignature = Convert.ToBase64String(hasher.ComputeHash(Encoding.ASCII.GetBytes(signatureBaseString)));
            }

            return oauthSignature;
        }
    }
}