using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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
    }
}
