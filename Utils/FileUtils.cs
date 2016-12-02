using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using lab3_registry.IO;

namespace lab3_registry.Utils
{
    public class FileUtils
    {
        public static List<string> getFilenamesFromDir(string path)
        {
            DirectoryInfo d = new DirectoryInfo(path);
            FileInfo[] Files = d.GetFiles("*.txt");
            List<string> filenames = new List<string>();

            foreach (FileInfo file in Files)
            {
                filenames.Add(file.Name.Replace(".txt", ""));
            }

            return filenames;
        }

        public static String getFullPath(List<String> strings)
        {
            StringBuilder sb = new StringBuilder();

            foreach(String str in strings)
            {
                sb.Append(str).Append("\\");
            }

            return sb.ToString();
        }

        public static void RemoveEmptyLines(string filename)
        {
            var lines = System.IO.File.ReadAllLines(filename).Where(arg => !string.IsNullOrWhiteSpace(arg));
            System.IO.File.WriteAllLines(filename, lines);
        }

        public static void RemoveLinesFromFile(string filename, int from)
        {
            string[] lines = Reader.ReadAllLines(filename);
            List<string> linesList = lines.ToList();

            for (int i = from; i < linesList.Count(); i++)
            {
                linesList.RemoveRange(from, linesList.Count() - from);
            }

            Saver.SaveAllLines(filename, linesList.ToArray());
        }

        public static void RemoveLastLine(string filename)
        {
            string[] lines = Reader.ReadAllLines(filename);
            System.IO.File.WriteAllLines(filename, lines.Take(lines.Length - 1).ToArray());
        }

        public static void RemoveLastCharacter(string filename)
        {
            string[] lines = Reader.ReadAllLines(filename);
            List<string> linesList = new List<string>();
            
            foreach (string line in lines)
            {
                linesList.Add(line.Remove(line.Length - 1));
            }

            Saver.SaveAllLines(filename, linesList.ToArray());
        }
    }
}
