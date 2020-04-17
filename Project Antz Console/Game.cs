using System;

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

            bool play = true;
            while (play)
            {
                play = CommandManager.ExecuteNextCommand();
            }
        }
    }
}