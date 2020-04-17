using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace Project_Antz_Console
{
    public class Unit
    {
        static string[] _recognizedTypes = {"Young Dwarves", "Young Soldiers"};
        
        internal string Type;
        internal int Count;

        internal Unit(string type)
        {
            if (!_recognizedTypes.Contains(type))
            {
                throw new ArgumentException("Unit constructor: '" + type + "' is not a valid unit type.");
            }

            Type = type;
            Count = 0;
        }

        internal Unit(string type, int count) : this(type)
        {
            Count = count;
        }

        internal void Lay(int count)
        {
            bool succeeded = Add(count);
            if (succeeded)
            {
                Console.WriteLine("Layed " + count + " " + Type + " (total: " + Count + ").");
            }
            else
            {
                Console.WriteLine("Couldn't lay " + count + " " + Type + ".");
            }
        }

        internal bool Add(int count)
        {
            Count += count;
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