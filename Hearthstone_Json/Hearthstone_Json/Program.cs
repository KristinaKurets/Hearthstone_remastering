using Hearthstone_Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace Hearthstone
{
    class Program
    {
        static void Main(string[] args)
        {
            var rnd = new Random();
            var Player = new Player();
            var Ai = new Player();
            Player.CreatePlayer(Player, "Deck1.json");
            Ai.CreatePlayer(Ai, "Deck2.json");
            //GAME
            Player.AddMinion(Player);
            Ai.AddRandomMinion(Ai);
            while (Player.Hp > 0 && Ai.Hp > 0)
            {
                Thread.Sleep(1000);
                Player.ChooseTargetAndAttack(Player, Ai);
                Player.AddMinion(Player);
                Ai.Attack(Ai, Player);
                Ai.AddRandomMinion(Ai);
            }

        }

    }
}
