using System;
using System.Collections.Generic;

namespace Project_Antz_Console
{
    internal class Server
    {
        internal string Name;
        internal List<Player> Players = new List<Player>();

        internal Server(string name, int numberOfStartingPlayers)
        {
            Name = name;
            
            for (int i = 0; i < numberOfStartingPlayers; i++)
            {
                AddPlayer(new Player("Player" + i));
            }
        }

        internal void AddPlayer(Player player)
        {
            Players.Add(player);
        }

        internal void DisplayPlayers()
        {
            Console.WriteLine("Players in " + Name + ":");
            foreach (Player player in Players)
            {
                player.DisplayBasics();
            }
        }
    }
}