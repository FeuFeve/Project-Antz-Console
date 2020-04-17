using System;
using System.Collections.Generic;

namespace Project_Antz_Console
{
    internal class Army
    {
        internal Dictionary<string, Unit> Units = new Dictionary<string, Unit>();

        internal Army()
        {
            Units["jsn"] = new Unit("Young Dwarves");
            Units["js"] = new Unit("Young Soldiers");
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
                Console.WriteLine("* " + unit.Count + " " + unit.Type);
            }
        }
    }
}