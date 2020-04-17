using System;
using System.Collections.Generic;

namespace Project_Antz_Console
{
    public class Unit
    {
        internal static Dictionary<string, string> RecognizedTypes = new Dictionary<string, string>();
        internal static Dictionary<string, Dictionary<string, int>> UnitsStats = new Dictionary<string, Dictionary<string, int>>();
        
        internal string Type;
        internal int Count;
        internal Dictionary<string, int> BaseStats = new Dictionary<string, int>();
        internal Dictionary<string, int> Stats = new Dictionary<string, int>();

        internal static void Init()
        {
            dynamic array = JsonReader.ReadFile(JsonFile.unitsFile);
            foreach (var unit in array)
            {
                string commandName = unit.command_name;
                string name = unit.name;
                
                RecognizedTypes[commandName] = name;
                
                Dictionary<string, int> unitDetails = new Dictionary<string, int>();

                unitDetails["life"] = unit.life;
                unitDetails["attack"] = unit.attack;
                unitDetails["defense"] = unit.defense;
                
                UnitsStats[name] = unitDetails;
            }
        }

        internal Unit(string type)
        {
            Dictionary<string, int> unitBaseStats = UnitsStats[RecognizedTypes[type]];
            if (unitBaseStats == null)
            {
                throw new ArgumentException("Unit constructor: '" + type + "' is not a valid unit type.");
            }

            Type = RecognizedTypes[type];
            Count = 0;
            BaseStats = unitBaseStats;
            
            Stats["life"] = 0;
            Stats["attack"] = 0;
            Stats["defense"] = 0;
        }

        internal Unit(string type, int count) : this(type)
        {
            Add(count);
        }

        internal void Lay(int count)
        {
            bool succeeded = Add(count);
            if (succeeded)
            {
                Console.WriteLine($"Layed {count} {Type} (total: {Count}).");
            }
            else
            {
                Console.WriteLine($"Couldn't lay {count} {Type}.");
            }
        }

        internal bool Add(int count)
        {
            Count += count;
            
            Stats["life"] += count * BaseStats["life"];
            Stats["attack"] += count * BaseStats["attack"];
            Stats["defense"] += count * BaseStats["defense"];
            
            return true;
        }

        internal bool Remove(int count)
        {
            if (Count < count)
            {
                return false;
            }
            else
            {
                Count -= count;
                return true;
            }
        }
    }
}