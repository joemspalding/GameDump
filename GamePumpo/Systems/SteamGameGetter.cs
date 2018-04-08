using System;
using System.Collections.Generic;
using System.IO;
using log4net;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace GamePumpo
{
    public class SteamGameGetter : IGameGetter
    {
        private List<Game> _gameList = new List<Game>();
        private static ILog log = LogManager.GetLogger("");

        public List<Game> GetGames()
        {
            Console.WriteLine(  log.IsInfoEnabled);// ("testing");
            VoidGetGamesAsync();
            return _gameList;
        }

        private async void VoidGetGamesAsync()
        {
            string tmp = await RetrieveGameListAsync();
            ParseFirstResponse(tmp);

            //test logs
            foreach (Game g in _gameList)
                Console.WriteLine(g.ToString());
        }


        private async Task<string> RetrieveGameListAsync()
        {
            const string URL = @"http://api.steampowered.com/ISteamApps/GetAppList/v0002/?key=STEAMKEY&format=json";
            return await APICaller.ApiCallAsync(URL);

        }

        private async void ParseFirstResponse(string gamesAsJson)
        {
            using (var reader = new JsonTextReader(new StringReader(gamesAsJson)))
            {
                int i = 0; // remember to delete this
                int idTracker = 0;
                while (reader.Read() && i < 1000)
                {
                    if (idTracker == 1)
                    {
                        await SteamGameAsync(reader.Value.ToString());
                        idTracker = 0;
                        i++;
                    }

                    if (string.Equals(reader.Value, "appid"))
                    {
                        idTracker = 1;
                    }

                }
            }
        }

        private async Task SteamGameAsync(string id) 
        {
            const string URL = @"http://store.steampowered.com/api/appdetails?appids=";

            string result = await APICaller.ApiCallAsync(URL + id);
            try
            {
                using (var reader = new JsonTextReader(new StringReader(result)))
                {
                    Game game = new Game();
                    int tracker = 0;
                    bool isGame = false;
                    while (reader.Read())
                    {

                        if (reader.Value == "null")
                            return;

                        if (tracker == -1)
                            break;

                        if (tracker == 1)
                        {
                            if (string.Equals(reader.Value, "game"))
                            {
                                isGame = true;
                            }
                            tracker = 0;
                        }

                        if (isGame)
                        {
                            switch (tracker)
                            {
                                case 2:
                                    game.Name = reader.Value.ToString();
                                    tracker = 0;
                                    break;
                                case 3:
                                    game.ID = int.Parse(id);
                                    tracker = -1;
                                    break;
                            }
                        }
                        switch (reader.Value)
                        {
                            case "type":
                                tracker = 1;
                                break;
                            case "name":
                                tracker = 2;
                                break;
                            case "steam_appid":
                                tracker = 3;
                                break;

                        }

                    }
                    if (isGame)
                    {
                        //test logs
                        Console.WriteLine(game);
                        _gameList.Add(game);
                    }
                }
            } catch(Exception e)
            {
                //TODO: Create a permanent list that ignores null id pages.

                //test logs
                Console.WriteLine("ID: " + id + " " + e.Message);
                
            }

        }


    }
}
