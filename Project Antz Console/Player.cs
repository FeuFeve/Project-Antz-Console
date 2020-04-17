using System;

namespace Project_Antz_Console
{
    public class Player
    {
        internal string Pseudo;
        internal Army Army;

        public Player(string pseudo)
        {
            Pseudo = pseudo;
            Army = new Army();
        }
    }
}