using System;

namespace Project_Antz_Console
{
    internal class CommandManager
    {
        internal static Server Server;
        internal static Player CurrentPlayer;
        
        internal static void Init(Server server, Player currentPlayer)
        {
            Server = server;
            CurrentPlayer = currentPlayer;
        }
        
        internal static bool ExecuteNextCommand()
        {
            Console.Write("\n> ");

            string command = Console.ReadLine();
            if (command == null)
            {
                Console.WriteLine("- Empty command.");
                return true;
            }
            
            string[] args = command.Split();
            try
            {
                switch (args[0])
                {
                    case "access":
                        switch (args[1])
                        {
                            case "players":                                             // access players
                                Server.DisplayPlayers();
                                break;
                            
                            case "army":                                                // access army
                                CurrentPlayer.Army.DisplayArmy();
                                break;
                            
                            default:
                                DisplayUnknownCommand();
                                break;
                        }
                        break;
                    
                    case "lay":                                                         // lay <jsn> <100>
                        CurrentPlayer.Lay(args[1], Int32.Parse(args[2]));
                        break;
                    
                    case "attack":
                        Player defender = Server.Players[args[1]];
                        FightManager FM = new FightManager(CurrentPlayer, defender, CurrentPlayer.Army, defender.Army);
                        FM.Fight();
                        break;

                    case "quit":                                                        // quit
                        Console.WriteLine("- Exiting the game.");
                        return false;
                    
                    default:
                        DisplayUnknownCommand();
                        break;
                }
            }
            catch (IndexOutOfRangeException)
            {
                DisplayUnknownCommand();
            }

            return true;
        }

        private static void DisplayUnknownCommand()
        {
            Console.WriteLine("- Unknown command.");
        }
    }
}