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
            Console.Write("> ");

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
                            case "players":
                                Server.DisplayPlayers();
                                break;
                            
                            case "army":
                                CurrentPlayer.Army.DisplayArmy();
                                break;
                            
                            default:
                                DisplayUnknownCommand();
                                break;
                        }
                        break;
                    
                    case "lay":
                        CurrentPlayer.Lay(args[1], Int32.Parse(args[2]));
                        break;

                    case "quit":
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