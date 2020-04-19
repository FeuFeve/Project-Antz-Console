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

        internal void Fight()
        {
            Console.WriteLine($"{Attacker.Pseudo} is attacking {Defender.Pseudo}\n");
            
            string attackingTroops = AttackerArmy.ToStringFightingTroops();
            string defendingTroops = DefenderArmy.ToStringFightingTroops();

            if (defendingTroops.Equals("None."))
            {
                Console.WriteLine("Troops were nowhere to be found on the defender's side.");
                Console.WriteLine("That's an opportunity for the attacker, who wins by default!");
                return;
            }
            
            Console.WriteLine($"Attacking troops: {attackingTroops}");
            Console.WriteLine($"Defending troops: {DefenderArmy.ToStringFightingTroops()}\n");
            
            Army aArmy = AttackerArmy;
            double aLife, aAttack;
            double aUnitRemainingLife = 0;

            Army dArmy = DefenderArmy;
            double dLife, dDefense;
            double dUnitRemainingLife = 0;

            bool attackerIsAlive = true;
            bool defenderIsAlive = true;
            while (attackerIsAlive && defenderIsAlive)
            {
                Dictionary<string, double> aArmyStats = aArmy.CalculateArmyStats(aUnitRemainingLife);
                if (aArmyStats["count"] == 0)
                {
                    attackerIsAlive = false;
                }
                aLife = aArmyStats["life"];
                aAttack = aArmyStats["attack"];
                double aKilledUnits = 0;
                
                Dictionary<string, double> dArmyStats = dArmy.CalculateArmyStats(dUnitRemainingLife);
                if (dArmyStats["count"] == 0)
                {
                    defenderIsAlive = false;
                }
                dLife = dArmyStats["life"];
                dDefense = dArmyStats["defense"];
                double dKilledUnits = 0;
                
                // Check if any of the attacker or defender army is dead
                if (!attackerIsAlive && !defenderIsAlive)
                {
                    Console.WriteLine("\nAttacker's and defender's armies are both dead... No winner this time.");
                    break;
                }
                if (!attackerIsAlive)
                {
                    Console.WriteLine("\nThe defenses were too strong.");
                    Console.WriteLine("The defender has bravely repealed the attacker.");
                    break;
                }

                if (!defenderIsAlive)
                {
                    Console.WriteLine("\nThe defenses failed to hold out against the attacker.");
                    Console.WriteLine("The attacker triumphs victoriously over the defender.");
                    break;
                }
                
                // Attacker turn
                Console.Write($"The attacker inflicts {aAttack} damages");
                foreach (KeyValuePair<string, Unit> kvp in dArmy.Units)
                {
                    Unit dCurrentUnit = kvp.Value;
                    double dCurrentUnitLife = dCurrentUnit.Stats["life"];

                    dUnitRemainingLife = dCurrentUnitLife - aAttack;
                    if (dUnitRemainingLife < 0) // If the attacker has more attack left than the current unit type has life
                    {
                        aKilledUnits += dCurrentUnit.Count;
                        dCurrentUnit.Count = 0;
                        aAttack -= dCurrentUnitLife;
                    }
                    else
                    {
                        dCurrentUnitLife -= aAttack;
                        int dFrom = dCurrentUnit.Count;
                        int dTo = dCurrentUnit.GetCountFromLife(dCurrentUnitLife);
                        aKilledUnits += dFrom - dTo;
                        dCurrentUnit.Count = dTo;
                        break;
                    }
                }
                Console.WriteLine($" and kill {aKilledUnits} units.");
                
                // Defender turn
                Console.Write($"The defender inflicts {dDefense} damages");
                foreach (KeyValuePair<string, Unit> kvp in aArmy.Units)
                {
                    Unit aCurrentUnit = kvp.Value;
                    double aCurrentUnitLife = aCurrentUnit.Stats["life"];
                    
                    aUnitRemainingLife = aCurrentUnitLife - dDefense;
                    if (aUnitRemainingLife < 0) // If the attacker has more attack left than the current unit type has life
                    {
                        dKilledUnits += aCurrentUnit.Count;
                        aCurrentUnit.Count = 0;
                        dDefense -= aCurrentUnitLife;
                    }
                    else
                    {
                        aCurrentUnitLife -= dDefense;
                        int aFrom = aCurrentUnit.Count;
                        int aTo = aCurrentUnit.GetCountFromLife(aCurrentUnitLife);
                        dKilledUnits += aFrom - aTo;
                        aCurrentUnit.Count = aTo;
                        break;
                    }
                }
                Console.WriteLine($" and kill {dKilledUnits} units.");
            }
        }
    }
}