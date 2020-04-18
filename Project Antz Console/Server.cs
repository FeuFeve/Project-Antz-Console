using System;
using System.Collections.Generic;
using System.Linq;

namespace Project_Antz_Console
{
    internal class Server
    {
        internal string Name;
        
        internal Dictionary<string, Player> Players = new Dictionary<string, Player>();
        internal Player HumanPlayer;

        internal Server(string name, int numberOfAI)
        {
            Name = name;
            
            for (int i = 0; i < numberOfAI; i++)
            {
                AddPlayer(new Player("Player" + i));
            }
        }

        internal Server(string name, int numberOfAI, Player humanPlayer)
            : this(name, numberOfAI)
        {
            HumanPlayer = humanPlayer;
        }

        internal void AddPlayer(Player player)
        {
            if (Players.ContainsKey(player.Pseudo))
            {
                Console.WriteLine($"A player named \"{player.Pseudo}\" already exists.");
            }
            else
            {
                Players[player.Pseudo] = player;
            }
        }

        internal void DisplayPlayers()
        {
            Console.WriteLine("Players in " + Name + ":");
            foreach (KeyValuePair<string, Player> kvp in Players)
            {
                Player player = kvp.Value;
                player.DisplayBasics();
            }
        }
    }
}