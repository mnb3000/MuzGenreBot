using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using RestSharp;
using RestSharp.Authenticators;
using System.Net;
using Newtonsoft.Json;
using System.Collections.Specialized;
using System.Text;
using SpotifyAPI;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Auth;
using SpotifyApi.NetCore.Authorization;
using System.Net.Http;
using SpotifyApi.NetCore;
using System.Diagnostics;

namespace MuzGenreBot.PublicApi
{
    class Program
    {
        
        //static void Main(string[] args)
        //{
        //    Program.RandomGenre();
        //    Program.RandomStory();
        //    Program.SpotifyApi("Blues");
        //}
        static public async Task<string> RandomGenre() // random genre
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(
                "https://binaryjazz.us/wp-json/genrenator/v1/genre/");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader sr = new StreamReader(stream);

            string ReadData = sr.ReadToEnd();
            response.Close();
            return ReadData;
        }
        static public string RandomStory() // random story
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(
                "https://binaryjazz.us/wp-json/genrenator/v1/story/1");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader sr = new StreamReader(stream);

            string ReadData = sr.ReadToEnd();
            response.Close();
            return ReadData;
        }
        static public string SpotifyApi(string s) // playlists 
        {
           
                Environment.SetEnvironmentVariable("SpotifyApiClientId", "3b7334201f9a420d9c7e16fdc98ba24a");
                Environment.SetEnvironmentVariable("SpotifyApiClientSecret", "354798de11e94314a347b686e7bb6c6c");

                Console.WriteLine("Nice1");
                var token = GetSpotifyAccessToken();
                SpotifyClient spotifyClient = new SpotifyClient(token);

                var request = spotifyClient.Search.Item(new SearchRequest(SearchRequest.Types.Playlist, s)).Result;
                var playlist1 = request.Playlists.Items[0];
                var playlist2 = request.Playlists.Items[1];
                var playlist3 = request.Playlists.Items[2];
                //var track = spotifyClient.Tracks.Get("1s6ux0lNiTziSrd7iUAADH").Result;
                


            string res = $"1) {playlist1.Name}\n{playlist1.ExternalUrls.Values.FirstOrDefault()}\n2) {playlist2.Name}\n{playlist2.ExternalUrls.Values.FirstOrDefault()}\n3) {playlist3.Name}\n{playlist3.ExternalUrls.Values.FirstOrDefault()}\n";
                return res;
            
        }


        static private string GetSpotifyAccessToken()
        {
            var spotifyClient = "3b7334201f9a420d9c7e16fdc98ba24a";
            var spotifySecret = "354798de11e94314a347b686e7bb6c6c";

            var webClient = new WebClient();

            var postparams = new NameValueCollection();
            postparams.Add("grant_type", "client_credentials");

            var authHeader = Convert.ToBase64String(Encoding.Default.GetBytes($"{spotifyClient}:{spotifySecret}"));
            webClient.Headers.Add(HttpRequestHeader.Authorization, "Basic " + authHeader);

            var tokenResponse = webClient.UploadValues("https://accounts.spotify.com/api/token", postparams);

            var textResponse = Encoding.UTF8.GetString(tokenResponse);
            dynamic parsed = JsonConvert.DeserializeObject(textResponse);

            return parsed.access_token;
        }






    }
}



