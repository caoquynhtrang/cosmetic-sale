using CSharpAssignment.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Web;

namespace CSharpAssignment.Utils
{
    public class GoogleUtil
    {
        public static readonly string CLIENT_ID = "1024825622153-u5s97h8b1g2podu0pd6cq2babmo0kcs2.apps.googleusercontent.com";
        public static readonly string CLIENT_SECRET = "0rRNMP6K4xHk71YLYbUJqNTY";
        public static readonly string GRANT_TYPE = "authorization_code";
        //public static readonly string REDIRECT_URI = "https://localhost:44316/LoginWithGoogle.aspx";
        public static readonly string REDIRECT_URI = "http://cosmetic-sale.gearhostpreview.com/LoginWithGoogle.aspx";
        public static readonly string LINK_GET_CODE = "https://accounts.google.com/o/oauth2/auth";
        public static readonly string LINK_GET_TOKEN = "https://accounts.google.com/o/oauth2/token";
        public static readonly string LINK_GET_INFO = "https://www.googleapis.com/oauth2/v1/userinfo";


        public static string GetRedirectLink()
        {
            string redirect = LINK_GET_CODE
                + "?scope=email profile"
                + "&redirect_uri=" + REDIRECT_URI
                + "&response_type=code"
                + "&client_id=" + CLIENT_ID;
            return redirect;
        }

        public static async Task<string> GetAccessToken(string code)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
                client.DefaultRequestHeaders.Add("Accept", "*/*");

                var Parameters = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("client_id", CLIENT_ID),
                        new KeyValuePair<string, string>("client_secret", CLIENT_SECRET),
                        new KeyValuePair<string, string>("code", code),
                        new KeyValuePair<string, string>("grant_type", GRANT_TYPE),
                        new KeyValuePair<string, string>("redirect_uri", REDIRECT_URI)
                    };

                var Request = new HttpRequestMessage(HttpMethod.Post, LINK_GET_TOKEN)
                {
                    Content = new FormUrlEncodedContent(Parameters)
                };

                ResponseGetAccessToken res = await client.SendAsync(Request).Result.Content.ReadAsAsync<ResponseGetAccessToken>();

                return res.AccessToken;
            }
        }

        public static async Task<AccountDTO> GetUserInfo(string accessToken)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Accept", "application/json");

                var Request = new HttpRequestMessage(HttpMethod.Get, LINK_GET_INFO + "?access_token=" + accessToken);
                string account = await client.SendAsync(Request).Result.Content.ReadAsStringAsync();
                GooglePojo googlePojo = JsonConvert.DeserializeObject<GooglePojo>(account);
                return new AccountDTO(googlePojo.Name, googlePojo.Email, googlePojo.Picture);
            }
        }
    }

    public class GooglePojo
    {
        public string ID { get; set; }
        public string Email { get; set; }
        public bool Verified_Email { get; set; }
        public string Name { get; set; }
        public string Given_Name { get; set; }
        public string Family_Name { get; set; }
        public string Picture { get; set; }
    }

    public class ResponseGetAccessToken
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        public ResponseGetAccessToken()
        {
        }
    }

    public class RequestGetAccessToken
    {
        [JsonProperty("client_id")]
        public string ClientID { get; set; }

        [JsonProperty("client_secret")]
        public string ClientSecret { get; set; }

        [JsonProperty("redirect_uri")]
        public string RedirectUri { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("grant_type")]
        public string GrantType { get; set; }

        public RequestGetAccessToken()
        {
        }

        public RequestGetAccessToken(string clientID, string clientSecret, string redirectUri, string code, string grantType)
        {
            ClientID = clientID;
            ClientSecret = clientSecret;
            RedirectUri = redirectUri;
            Code = code;
            GrantType = grantType;
        }
    }
}