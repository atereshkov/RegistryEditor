using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3_registry.IO
{
    public class Saver
    {
        public static void SaveAllLines(string path, string[] lines)
        {
            System.IO.File.WriteAllLines(@path, lines);
        }

        public static void SaveText(string path, string text)
        {
            System.IO.File.WriteAllText(@path, text);
        }
    }
}
