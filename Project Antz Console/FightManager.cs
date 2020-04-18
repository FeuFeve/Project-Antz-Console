using System;
using System.Collections.Generic;

namespace Project_Antz_Console
{
    internal class FightManager
    {
        internal Player Attacker;
        internal Army AttackerArmy;
        
        internal Player Defender;
        internal Army DefenderArmy;

        internal FightManager(Player attacker, Player defender, Army attackerArmy, Army defenderArmy)
        {
            Attacker = attacker;
            AttackerArmy = attackerArmy;

            Defender = defender;
            DefenderArmy = defenderArmy;
        }

        internal bool Fight()
        {
            Console.WriteLine($"{Attacker.Pseudo} is attacking {Defender.Pseudo}");
            Console.WriteLine($"Attacking troops: {AttackerArmy.ToStringFightingTroops()}");
            Console.WriteLine($"Defending troops: {DefenderArmy.ToStringFightingTroops()}");
            
            Army aArmy = AttackerArmy;
            double aLife, aAttack;

            Army dArmy = DefenderArmy;
            double dLife, dDefense;

            bool oneArmyIsDead = false;
            while (!oneArmyIsDead)
            {
                Dictionary<string, double> aArmyStats = aArmy.CalculateArmyStats();
                aLife = aArmyStats["life"];
                aAttack = aArmyStats["attack"];
                
                Dictionary<string, double> dArmyStats = dArmy.CalculateArmyStats();
                dLife = dArmyStats["life"];
                dDefense = dArmyStats["defense"];
                
                // Attacker turn
                oneArmyIsDead = true;
            }
            
            return false;
        }
    }
}