﻿using System;
using System.Collections;
using System.Configuration;

namespace Project_Antz_Console
{
    internal class Game
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("From Game: Hello world!");
            
            Server server1 = new Server("S1", 10);
            Player player = new Player("FeuFeve");
            server1.AddPlayer(player);
            
            CommandManager.Init(server1, player);
            
            // Testing
            Player toAttack = server1.Players["Player0"];
            toAttack.Lay("jsn", 1000);
            toAttack.Lay("js", 250);
            toAttack.Lay("tk", 100);

            bool play = true;
            while (play)
            {
                play = CommandManager.ExecuteNextCommand();
            }
        }
    }
}