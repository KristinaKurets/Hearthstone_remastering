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
            var deck = new DeckService();
            
            //Create players:
            var Player1 = new Player
            {
                Hp = 30,
                Deck = deck.CreateDeck("Deck1.json"),
                
            };
            Player1.Hand = Player1.Deck.GetRange(0, 3);
            Player1.Deck.RemoveRange(0, 3);
            Player1.Table = new List<Card>();

            var Player2 = new Player
            {
                Hp = 30,
                Deck = deck.CreateDeck("Deck2.json"),
            };
            Player2.Hand = Player2.Deck.GetRange(0, 3);
            Player2.Deck.RemoveRange(0, 3);
            Player2.Table = new List<Card>();

            
            //TRY GAME
            //Player1's turn: выбираем существо из руки и ставим его на стол
            Player1.Hand.Add(Player1.Deck[0]);
            Player1.Deck.RemoveAt(0);
            Console.WriteLine("\nThere are cards in your starting hand:");
            foreach (var card in Player1.Hand)
            {
                Console.WriteLine($"{ card.Name}, {card.Damage}/{card.Health}");
            }
            while (true)
            {
                Console.WriteLine("\nChoose the minion you want to put on the table:");
                for (int i = 0; i < Player1.Hand.Count; i++)
                {
                    Console.WriteLine($"{i + 1} - {Player1.Hand[i].Name}, {Player1.Hand[i].Damage}/{Player1.Hand[i].Health}");
                }

                int ind = int.Parse(Console.ReadLine());
                if (ind > Player1.Hand.Count) Console.WriteLine("I don't understand you.");
                else
                {
                    Player1.Table.Add(Player1.Hand[ind - 1]);
                    Console.WriteLine($"You chose {Player1.Hand[ind - 1].Name}, {Player1.Hand[ind - 1].Damage}/{Player1.Hand[ind - 1].Health}. It's on your table. \n\nPlayer2's turn!");
                    Player1.Hand.RemoveAt(ind - 1);
                    break;
                }
            }

            //Player2: ставит случайное существо из руки на стол
            Player2.Hand.Add(Player2.Deck[0]);
            Player2.Deck.RemoveAt(0);
            int ind2 = rnd.Next(0, Player2.Hand.Count);
            Player2.Table.Add(Player2.Hand[ind2]);
            Thread.Sleep(1000);
            Console.WriteLine($"\nPlayer2 chose {Player2.Hand[ind2].Name}, {Player2.Hand[ind2].Damage}/{Player2.Hand[ind2].Health}. It's on his table. \n\nYour turn!");
            Player2.Hand.RemoveAt(ind2);

            //цикл:
            while (Player1.Hp > 0 && Player2.Hp > 0)
            {
                //Player1: Выбираем, кого атаковать
                Thread.Sleep(1000);
                Console.WriteLine("\nIt's time to attack");
                for (int i = 0; i < Player1.Table.Count; i++)
                {
                    Console.WriteLine($"You have to choose a target to attack with {Player1.Table[i].Name}, {Player1.Table[i].Damage}/{Player1.Table[i].Health}:");
                    for (int s = 0; s < Player2.Table.Count; s++)
                    {
                        Console.WriteLine($"{s + 1} - {Player2.Table[s].Name}, {Player2.Table[s].Damage}/{Player2.Table[s].Health}");
                    }
                    Console.WriteLine($"{Player2.Table.Count + 1} - go to face. Now Player2 has {Player2.Hp} HP");
                    int attacked = int.Parse(Console.ReadLine());
                    //for (int j = 0; j < Player2.Table.Count; j++)
                    //{
                       
                        if (attacked == Player2.Table.Count + 1)
                        {
                            Player2.Hp -= Player1.Table[i].Damage;
                            Console.WriteLine($"You decided go to enemy face. Now Player2 has {Player2.Hp} HP");
                            if (Player2.Hp<=0)
                            {
                                Console.WriteLine("You won!");
                                break;
                            }
                           
                            
                        }
                        else if (attacked <= Player2.Table.Count)
                        {
                            Console.WriteLine($"{Player1.Table[i].Name}, {Player1.Table[i].Damage}/{Player1.Table[i].Health} attacks {Player2.Table[attacked - 1].Name}, {Player2.Table[attacked - 1].Damage}/{Player2.Table[attacked - 1].Health}!");
                            Player2.Table[attacked - 1].Health -= Player1.Table[i].Damage;
                            Player1.Table[i].Health -= Player2.Table[attacked - 1].Damage;
                            if (Player2.Table[attacked-1].Health <=0)
                            {
                                Console.WriteLine($"Oh no, {Player2.Table[attacked - 1].Name} is dead!");
                                Player2.Table.RemoveAt(attacked - 1);
                            }
                            if (Player1.Table[i].Health <= 0)
                            {
                                Console.WriteLine($"Oh no, {Player1.Table[i].Name} is dead!");
                                Player1.Table.RemoveAt(i);
                            }
                        }
                        
                    //}
                    if(Player2.Hp <= 0)
                    {
                        Console.WriteLine("Player1 won!");
                        break;
                    }
                }
                //Player1: добавляем новое существо в руку и выставляем на стол
                Player1.Hand.Add(Player1.Deck[0]);
                Player1.Deck.RemoveAt(0);

                while (true)
                {
                    Console.WriteLine("\nChoose the minion you want to put on the table:");
                    for (int i = 0; i < Player1.Hand.Count; i++)
                    {
                        Console.WriteLine($"{i + 1} - {Player1.Hand[i].Name}, {Player1.Hand[i].Damage}/{Player1.Hand[i].Health}");
                    }
                    int ind = int.Parse(Console.ReadLine());
                    if (ind > Player1.Hand.Count) Console.WriteLine("I don't understand you.");
                    else
                    {
                        Player1.Table.Add(Player1.Hand[ind - 1]);
                        Console.WriteLine($"You chose {Player1.Hand[ind - 1].Name}, {Player1.Hand[ind - 1].Damage}/{Player1.Hand[ind - 1].Health}. It's on your table. \nPlayer2's turn!");
                        Player1.Hand.RemoveAt(ind - 1);
                        break;
                    }
                }

                //Player2's turn: атака
                if (Player2.Table.Count != 0)
                {
                    Console.WriteLine("Player2 attacks!");
                    Thread.Sleep(700);
                    for (int i = 0; i < Player2.Table.Count; i++)
                    {
                        for (int j = 0; j < Player1.Table.Count; j++)
                        {
                            if (Player2.Table[i].Damage >= Player1.Table[j].Health)
                            {
                                Console.WriteLine($"{Player2.Table[i].Name} attacks your {Player1.Table[j].Name}!");
                                Player1.Table[j].Health -= Player2.Table[i].Damage;
                                Player2.Table[i].Health -= Player1.Table[j].Damage;

                                if (Player2.Table[i].Health <= 0)
                                {
                                    Console.WriteLine($"Oh no, {Player2.Table[i].Name} is dead!");
                                    Player2.Table.Remove(Player2.Table[i]);
                                }
                                if (Player1.Table[j].Health <= 0)
                                {
                                    Console.WriteLine($"Oh no, {Player1.Table[j].Name} is dead!");
                                    Player1.Table.Remove(Player1.Table[j]);
                                }
                            }
                            else
                            {
                                Player1.Hp -= Player2.Table[i].Damage;
                                Console.WriteLine($"{Player2.Table[i].Name} goes to your face! Now you have {Player1.Hp} HP.");
                                break;
                            }
                        }
                        if (Player1.Hp == 0)
                        {
                            Console.WriteLine("Player1 is dead! You won!");
                            break;
                        }
                    }
                }
                else Console.WriteLine("There are no minions on Player2's table.");
                
                //Player2 добавляет на стол новое существо
                Player2.Hand.Add(Player2.Deck[0]);
                Player2.Deck.RemoveAt(0);
                ind2 = rnd.Next(0, Player2.Hand.Count);
                Player2.Table.Add(Player2.Hand[ind2]);
                Thread.Sleep(1000);
                Console.WriteLine($"\nPlayer2 chose {Player2.Hand[ind2].Name}, {Player2.Hand[ind2].Damage}/{Player2.Hand[ind2].Health}. It's on his table. \n\nYour turn!");
                Player2.Hand.RemoveAt(ind2);

            }

        }

    }
}
