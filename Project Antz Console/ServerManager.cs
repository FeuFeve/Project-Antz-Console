using System;
using System.IO;

namespace Project_Antz_Console
{
    internal class ServerManager
    {
        static private string ServersPath = "../../Data/Servers/";
        static private string BaseDirName = "Base";


        static internal void CreateNewServer(string name)
        {
            // Paths
            string baseDirPath = ServersPath + BaseDirName;
            string newServerPath = ServersPath + name;
            
            // If a server of the same name already exists, cancel the creation
            if (Directory.Exists(newServerPath))
            {
                Console.WriteLine("A server with the same name already exists.");
                Console.WriteLine($"Consider using another server name, or deleting it first using 'delete {name}'.");
                return;
            }
            
            // Directories
            DirectoryInfo baseDir = new DirectoryInfo(baseDirPath);
            DirectoryInfo newServerDir = new DirectoryInfo(newServerPath);
            
            CopyAll(baseDir, newServerDir);
            
            Console.WriteLine($"Successfully created server \"{name}\".");
        }

        static private void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            Directory.CreateDirectory(target.FullName);
 
            // Copy each file into the new directory.
            foreach (FileInfo fi in source.GetFiles())
            {
                fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
            }
 
            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir = target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }

        static internal void DeleteServer(string name)
        {
            // Path
            string pathOfServerToDelete = ServersPath + name;
            
            // If the server do not exists, cancel the deletion
            if (!Directory.Exists(pathOfServerToDelete))
            {
                Console.WriteLine($"Error: no known server named \"{name}\".");
                return;
            }
            
            // Confirmation asking
            Console.WriteLine($"Are you sure you want to delete \"{name}\"?");
            Console.WriteLine("(y/n)");

            string[] args = CommandManager.GetNextCommand();
            if (args == null || args[0] != "y")
            {
                Console.WriteLine("Aborting the deletion.");
                return;
            }

            // Directories
            DirectoryInfo dirOfServerToDelete = new DirectoryInfo(pathOfServerToDelete);
            dirOfServerToDelete.Delete(true);
            
            Console.WriteLine($"Successfully deleted server \"{name}\".");
        }

        static internal void ListAllServers()
        {
            Console.WriteLine("List of the servers:");
            
            string[] servers = Directory.GetDirectories(ServersPath);
            foreach (string server in servers)
            {
                string serverName = new DirectoryInfo(server).Name;
                if (!serverName.Equals(BaseDirName))
                {
                    Console.WriteLine($"* {serverName}");
                }
            }
        }
    }
}