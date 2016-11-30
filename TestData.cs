using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lab3_registry.Utils;
using lab3_registry.IO;

namespace lab3_registry
{
    public class TestData
    {
        public IList<Folder> Groups = new List<Folder>();
        public IList<string> Filenames = new List<string>();

        public const string PATH_FOLDERS = @"E:\Универ\5SEMESTR\ОС\lab3\lab3_registry\bin\Debug\folders";

        public void Load()
        {
            Filenames = FileUtils.getFilenamesFromDir(PATH_FOLDERS);
            List<Folder> folders = new List<Folder>();

            for (int i = 0; i < Filenames.Count(); i++)
            {
                folders.Add(new Folder() { Key = i, Name = Filenames[i], SubFolders = new List<Folder>(), Files = new List<File>() });
            }

            /*
            folders[0].Files.Add(new File() { Key = 1, Name = "Entry number 1" });
            folders[0].Files.Add(new File() { Key = 2, Name = "Entry number 2" });
            folders[0].Files.Add(new File() { Key = 3, Name = "Entry number 3" });

            folders[1].Files.Add(new File() { Key = 1, Name = "Subkey 1" });
            folders[1].Files.Add(new File() { Key = 2, Name = "Kkeqw 2" });
            folders[1].Files.Add(new File() { Key = 3, Name = "SAxzc 23" });
           
            
            folders[1].SubFolders.Add(folders[3]);
            folders[0].SubFolders.Add(folders[1]);
            folders[0].SubFolders.Add(folders[4]);
            folders[0].SubFolders.Add(folders[2]);
            */

            for (int i = 0; i < folders.Count(); i++)
            {
                Groups.Add(folders[i]);
            }

            getFolder(folders[0].Name);
        }

        /*
        public Folder getFolder(string folderName)
        {
            Folder folder = new Folder();

            string[] lines = Reader.ReadAllLines(PATH_FOLDERS + "\\" + folderName + ".txt");

            for (int i = 0; i < lines.Count(); i++)
            {
                if (lines[i].Contains("parameters-start"))
                {
                    while (!lines[i++].Contains("parameters-end"))
                    {
                        Console.WriteLine(lines[i]);
                    }
                }
            }

            return folder;
        }
        */

        public Folder getFolder(string folderName)
        {
            Folder folder = new Folder();

            string[] lines = Reader.ReadAllLines(PATH_FOLDERS + "\\" + folderName + ".txt");

            Char delimiter = '\\';
            String[] subFolders = null;
            if (lines[0].Contains("\\"))
            {
                subFolders = lines[0].Split(delimiter);
            }

            /*
            for (int i = 0; i < lines.Count(); i++)
            {
                
            }
            */

            for (int j = 0; j < subFolders.Count(); j++)
            {
                Folder subFolder = new Folder() { 
                    Key = j, 
                    Name = subFolders[j], 
                    SubFolders = new List<Folder>(), 
                    Files = new List<File>() };

                Groups[0].SubFolders.Add(subFolder);
            }

            return folder;
        }

        /*
                currentStructure.Add(xE.Value);
                string[] splittedPath = xE.Value.Split('\\');
                if (!tree.Nodes.ContainsKey(splittedPath[0]))
                {
                    tree.Nodes.Add(splittedPath[0], splittedPath[0]);
                }
                if (splittedPath.Length > 1)
                    getTree(tree.Nodes[splittedPath[0]], splittedPath, 1);
         */

    }
}
