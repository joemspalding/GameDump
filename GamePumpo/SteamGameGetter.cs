using System;
using System.Collections.Generic;
using System.Net.Http;

namespace GamePumpo
{
    public class SteamGameGetter : IGameGetter
    {
        public List<Game> GetGames()
        {
            RetrieveGameList();
            return new List<Game>();
        }

        private void RetrieveGameList()
        {
            const string URL = @"http://api.steampowered.com/ISteamApps/GetAppList/v0002/?key=STEAMKEY&format=json";
            try
            {

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);
            HttpResponseMessage message = client.GetAsync(URL).Result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        
    }
}
