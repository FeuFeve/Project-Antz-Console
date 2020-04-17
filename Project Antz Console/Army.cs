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

        internal void DisplayArmy()
        {
            Console.WriteLine("Army:");
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
        }
    }
}