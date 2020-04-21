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
            string[] args = GetNextCommand();
            if (args == null)
            {
                return true;
            }
            
            try
            {
                switch (args[0])
                {
                    case "create":                                                       // create <server name>
                        ServerManager.CreateNewServer(args[1]);
                        break;
                    
                    case "delete":                                                       // delete <server name>
                        ServerManager.DeleteServer(args[1]);
                        break;
                    
                    case "list":
                        switch (args[1])
                        {
                            case "servers":                                              // list servers
                                ServerManager.ListAllServers();
                                break;
                            
                            case "commands":                                             // list commands
                                ListCommands();
                                break;
                            
                            default:
                                DisplayUnknownCommand();
                                break;
                        }
                        break;
                    
                    case "access":
                        switch (args[1])
                        {
                            case "players":                                              // access players
                                Server.DisplayPlayers();
                                break;

                            case "army":                                                 // access army
                                CurrentPlayer.Army.DisplayArmy();
                                break;

                            default:
                                DisplayUnknownCommand();
                                break;
                        }

                        break;

                    case "lay":                                                          // lay <jsn> <100>
                        CurrentPlayer.Lay(args[1], Int32.Parse(args[2]));
                        break;

                    case "attack":                                                       // attack <player name>
                        try
                        {
                            CurrentPlayer.Attack(Server.Players[args[1]]);
                        }
                        catch (System.Collections.Generic.KeyNotFoundException)
                        {
                            Console.WriteLine($"Player {args[1]} not found");
                        }
                        break;

                    case "quit":                                                         // quit
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
            catch (FormatException)
            {
                DisplayUnknownCommand();
            }

            return true;
        }

        internal static string[] GetNextCommand()
        {
            Console.Write("\n> ");

            string command = Console.ReadLine();
            if (command == null)
            {
                Console.WriteLine("- Empty command.");
                return null;
            }
            
            string[] args = command.Split();
            return args;
        }

        private static void DisplayUnknownCommand()
        {
            Console.WriteLine("- Unknown command.");
        }

        private static void ListCommands()
        {
            Console.WriteLine("Available commands are:");
            Console.WriteLine("* create <server name>");
            Console.WriteLine("* delete <server name>");
            Console.WriteLine("* list servers");
            Console.WriteLine("* list commands");
            Console.WriteLine("* access players");
            Console.WriteLine("* access army");
            Console.WriteLine("* lay <jsn> <100>");
            Console.WriteLine("* attack <player name>");
            Console.WriteLine("* quit");
        }
    }
}