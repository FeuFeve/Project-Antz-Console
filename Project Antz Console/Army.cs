using System;
using System.Collections.Generic;

namespace Project_Antz_Console
{
    internal class Army
    {
        internal Dictionary<string, Unit> Units = new Dictionary<string, Unit>();

        internal Army()
        {
            Unit.Init();

            foreach (KeyValuePair<string, string> entry in Unit.RecognizedTypes)
            {
                Units[entry.Key] = new Unit(entry.Key);
            }
        }

        internal void Lay(string type, int count)
        {
            try
            {
                Unit unit = Units[type];
                unit.Lay(count);
            }
            catch (KeyNotFoundException)
            {
                Console.WriteLine("Unrecognized type of unit to lay: '" + type + "'.");
            }
        }

        internal Dictionary<string, double> CalculateArmyStats()
        {
            double life = 0;
            double attack = 0;
            double defense = 0;

            foreach (KeyValuePair<string, Unit> kvp in Units)
            {
                Unit unit = kvp.Value;
                life += unit.Stats["life"];
                attack += unit.Stats["attack"];
                defense += unit.Stats["defense"];
            }

            Dictionary<string, double> ArmyStats = new Dictionary<string, double>();
            ArmyStats["life"] = life;
            ArmyStats["attack"] = attack;
            ArmyStats["defense"] = defense;

            return ArmyStats;
        }

        internal void DisplayArmy()
        {
            Console.WriteLine("### Army: ###");
            foreach (KeyValuePair<string, Unit> kvp in Units)
            {
                Unit unit = kvp.Value;
                string line = "* " + unit.Count
                            + " " + unit.Type
                            + " (life: " + unit.Stats["life"]
                            + ", attack: " + unit.Stats["attack"]
                            + ", defense: " + unit.Stats["defense"] + ")";
                Console.WriteLine(line);
            }
            
            Console.WriteLine("### Full stats: ###");
            
            Dictionary<string, double> ArmyStats = CalculateArmyStats();
            Console.WriteLine($"* Life: {ArmyStats["life"]}");
            Console.WriteLine($"* Attack: {ArmyStats["attack"]}");
            Console.WriteLine($"* Defense: {ArmyStats["defense"]}");
        }

        internal string ToStringFightingTroops()
        {
            string fightingTroops = "";
            foreach (KeyValuePair<string, Unit> kvp in Units)
            {
                if (!String.IsNullOrEmpty(fightingTroops))
                {
                    fightingTroops += ", ";
                }
                
                Unit unit = kvp.Value;
                fightingTroops += unit.Count + " " + unit.Type;
            }

            return fightingTroops + ".";
        }
    }
}