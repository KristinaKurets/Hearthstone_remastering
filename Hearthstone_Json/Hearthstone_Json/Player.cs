using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Hearthstone_Json
{
    public class Player
    {
        public int Hp { get; set; }
        public List<Card> Deck { get; set; }
        public List<Card> Hand { get; set; }
        public List<Card> Table { get; set; }

        public void CreatePlayer(Player player, string path)
        {
            var deck = new DeckService();
            player.Deck = deck.CreateDeck(path);
            player.Hp = 30;
            player.Hand = player.Deck.GetRange(0, 3);
            player.Deck.RemoveRange(0, 3);
            player.Table = new List<Card>();
        }
        public void AddMinion(Player player)
        {
            player.Hand.Add(player.Deck[0]);
            player.Deck.RemoveAt(0);
            while (true)
            {
                Console.WriteLine("\nChoose the minion you want to put on the table:");
                for (int i = 0; i < player.Hand.Count; i++)
                {
                    Console.WriteLine($"{i + 1} - {player.Hand[i].Name}, {player.Hand[i].Damage}/{player.Hand[i].Health}");
                }
                int ind = int.Parse(Console.ReadLine());
                if (ind > player.Hand.Count) Console.WriteLine("I don't understand you.");
                else
                {
                    player.Table.Add(player.Hand[ind - 1]);
                    Console.WriteLine($"You chose {player.Hand[ind - 1].Name}, {player.Hand[ind - 1].Damage}/{player.Hand[ind - 1].Health}. It's on your table. \n\nAi's turn!");
                    player.Hand.RemoveAt(ind - 1);
                    break;
                }
            }

        }

        public void AddRandomMinion(Player player)
        {
            var rnd = new Random();
            player.Hand.Add(player.Deck[0]);
            player.Deck.RemoveAt(0);
            int ind = rnd.Next(0, player.Hand.Count);
            player.Table.Add(player.Hand[ind]);
            Thread.Sleep(1000);
            Console.WriteLine($"\nAi chose {player.Hand[ind].Name}, {player.Hand[ind].Damage}/{player.Hand[ind].Health}. It's on his table. \n\nYour turn!");
            player.Hand.RemoveAt(ind);

        }
    }

    
}
