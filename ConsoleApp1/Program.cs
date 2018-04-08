using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GamePumpo;
namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            GameList gameList = new GameList();
            SteamGameGetter steam  = new SteamGameGetter();

            gameList.AddGamesBySystem(steam);

        }
    }
}
