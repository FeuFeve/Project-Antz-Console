using System;
using System.Collections.Generic;

namespace Project_Antz_Console
{
    public class Unit
    {
        internal static Dictionary<string, string> RecognizedTypes = new Dictionary<string, string>();
        internal static Dictionary<string, Dictionary<string, double>> UnitsStats = new Dictionary<string, Dictionary<string, double>>();
        
        internal string Type;
        internal int Count;
        internal Dictionary<string, double> BaseStats = new Dictionary<string, double>();
        internal Dictionary<string, double> Stats = new Dictionary<string, double>();

        internal static void Init()
        {
            dynamic array = JsonReader.ReadFile(JsonFile.unitsFile);
            foreach (var unit in array)
            {
                string commandName = unit.command_name;
                string name = unit.name;
                
                RecognizedTypes[commandName] = name;
                
                Dictionary<string, double> unitDetails = new Dictionary<string, double>();

                unitDetails["life"] = unit.life;
                unitDetails["attack"] = unit.attack;
                unitDetails["defense"] = unit.defense;
                
                UnitsStats[name] = unitDetails;
            }
        }

        internal Unit(string type)
        {
            Dictionary<string, double> unitBaseStats = UnitsStats[RecognizedTypes[type]];
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
            RecalculateStats();
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
                RecalculateStats();
                return true;
            }
        }

        internal void RecalculateStats()
        {
            Stats["life"] = Count * BaseStats["life"];
            Stats["attack"] = Count * BaseStats["attack"];
            Stats["defense"] = Count * BaseStats["defense"];
        }

        internal int GetCountFromLife(double totalLife)
        {
            double lifePerUnit = BaseStats["life"];
            int count = (int)Math.Ceiling(totalLife / lifePerUnit);
            return count;
        }
    }
}