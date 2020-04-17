using System;

namespace Project_Antz_Console
{
    internal class Player
    {
        internal string Pseudo;
        internal Army Army;

        internal Player(string pseudo)
        {
            Pseudo = pseudo;
            Army = new Army();
        }

        internal void Lay(string type, int count)
        {
            Army.Lay(type, count);
        }

        internal void DisplayBasics()
        {
            Console.WriteLine(Pseudo);
        }
    }
}