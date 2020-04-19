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

        internal void Attack(Player defender)
        {
            Army.CalculateArmyStats();
            if ((int)Army.Stats["count"] == 0)
            {
                Console.WriteLine("You don't have any troops to attack.");
                return;
            }
            
            FightManager FM = new FightManager(this, defender, this.Army, defender.Army);
            FM.Fight();
        }

        internal void DisplayBasics()
        {
            Console.WriteLine(Pseudo);
        }
    }
}