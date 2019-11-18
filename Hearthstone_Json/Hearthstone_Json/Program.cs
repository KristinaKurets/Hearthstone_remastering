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

            //TRY GAME
            Player.AddMinion(Player);
            Ai.AddRandomMinion(Ai);

            //цикл:
            while (Player.Hp > 0 && Ai.Hp > 0)
            {
                //Player: Выбираем, кого атаковать
                Thread.Sleep(1000);
                Console.WriteLine("\nIt's time to attack");
                for (int i = 0; i < Player.Table.Count; i++)
                {
                    Console.WriteLine($"You have to choose a target to attack with {Player.Table[i].Name}, {Player.Table[i].Damage}/{Player.Table[i].Health}:");
                    for (int s = 0; s < Ai.Table.Count; s++)
                    {
                        Console.WriteLine($"{s + 1} - {Ai.Table[s].Name}, {Ai.Table[s].Damage}/{Ai.Table[s].Health}");
                    }
                    Console.WriteLine($"{Ai.Table.Count + 1} - go to face. Now Ai has {Ai.Hp} HP");
                    int attacked = int.Parse(Console.ReadLine());
                    //for (int j = 0; j < Ai.Table.Count; j++)
                    //{
                       
                        if (attacked == Ai.Table.Count + 1)
                        {
                            Ai.Hp -= Player.Table[i].Damage;
                            Console.WriteLine($"You decided go to enemy face. Now Ai has {Ai.Hp} HP");
                            if (Ai.Hp<=0)
                            {
                                Console.WriteLine("You won!");
                                break;
                            }
                           
                            
                        }
                        else if (attacked <= Ai.Table.Count)
                        {
                            Console.WriteLine($"{Player.Table[i].Name}, {Player.Table[i].Damage}/{Player.Table[i].Health} attacks {Ai.Table[attacked - 1].Name}, {Ai.Table[attacked - 1].Damage}/{Ai.Table[attacked - 1].Health}!");
                            Ai.Table[attacked - 1].Health -= Player.Table[i].Damage;
                            Player.Table[i].Health -= Ai.Table[attacked - 1].Damage;
                            if (Ai.Table[attacked-1].Health <=0)
                            {
                                Console.WriteLine($"Oh no, {Ai.Table[attacked - 1].Name} is dead!");
                                Ai.Table.RemoveAt(attacked - 1);
                            }
                            if (Player.Table[i].Health <= 0)
                            {
                                Console.WriteLine($"Oh no, {Player.Table[i].Name} is dead!");
                                Player.Table.RemoveAt(i);
                            }
                        }
                        
                    //}
                    if(Ai.Hp <= 0)
                    {
                        Console.WriteLine("Player won!");
                        break;
                    }
                }
                //Player: добавляем новое существо в руку и выставляем на стол
                Player.AddMinion(Player);

                //Ai's turn: атака
                if (Ai.Table.Count != 0)
                {
                    Console.WriteLine("Ai attacks!");
                    Thread.Sleep(700);
                    for (int i = 0; i < Ai.Table.Count; i++)
                    {
                        for (int j = 0; j < Player.Table.Count; j++)
                        {
                            if (Ai.Table[i].Damage >= Player.Table[j].Health)
                            {
                                Console.WriteLine($"{Ai.Table[i].Name} attacks your {Player.Table[j].Name}!");
                                Player.Table[j].Health -= Ai.Table[i].Damage;
                                Ai.Table[i].Health -= Player.Table[j].Damage;

                                if (Ai.Table[i].Health <= 0)
                                {
                                    Console.WriteLine($"Oh no, {Ai.Table[i].Name} is dead!");
                                    Ai.Table.Remove(Ai.Table[i]);
                                }
                                if (Player.Table[j].Health <= 0)
                                {
                                    Console.WriteLine($"Oh no, {Player.Table[j].Name} is dead!");
                                    Player.Table.Remove(Player.Table[j]);
                                }
                            }
                            else
                            {
                                Player.Hp -= Ai.Table[i].Damage;
                                Console.WriteLine($"{Ai.Table[i].Name} goes to your face! Now you have {Player.Hp} HP.");
                                break;
                            }
                        }
                        if (Player.Hp == 0)
                        {
                            Console.WriteLine("Player is dead! You won!");
                            break;
                        }
                    }
                }
                else Console.WriteLine("There are no minions on Ai's table.");


                Ai.AddRandomMinion(Ai);

            }

        }

    }
}
