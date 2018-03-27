using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePumpo
{
    public class GameList
    {
        private List<Game> _gameList = new List<Game>();

        private void AddGames()
        {
            List<Game> tempList = null;
            tempList = AddGamesByConsole(new SteamGameGetter());

            foreach(Game g in tempList)
            {
                _gameList.Add(g);
            }


        }


        private List<Game> AddGamesByConsole (IGameGetter gameGetter)
        {
            return gameGetter.GetGames();
        }
        
    }
}
