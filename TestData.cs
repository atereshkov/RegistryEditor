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
        public IList<Folder> RootGroups = new List<Folder>();
        public IList<string> Filenames = new List<string>();

        public const string PATH_FOLDERS = @"E:\Универ\5SEMESTR\ОС\lab3\lab3_registry\bin\Debug\folders";

        public void Load()
        {
            Filenames = FileUtils.getFilenamesFromDir(PATH_FOLDERS);
            List<Folder> folders = new List<Folder>();

            for (int i = 0; i < Filenames.Count(); i++) // get all files from dir "folders"
            {
                folders.Add(new Folder() { Key = i, Name = Filenames[i], SubFolders = new List<Folder>(), Files = new List<File>() });
            }

            for (int i = 0; i < folders.Count(); i++) // add all root folders
            {
                RootGroups.Add(folders[i]);
                getFolder(folders[i].Name, i); // start adding subfolders and subfolders of subfolders and so on...
            }
        }

        public Folder getFolder(string folderName, int groupId)
        {
            Folder folder = new Folder();

            string[] lines = Reader.ReadAllLines(PATH_FOLDERS + "\\" + folderName + ".txt");

            Char delimiter = '\\';
            String[] splittedFolders;

            int j = 0;
            for (int i = 0; i < lines.Count(); i++)
            {
                splittedFolders = lines[i].Split(delimiter);

                Folder subFolder = new Folder()
                {
                    Key = 0,
                    Name = splittedFolders[0],
                    SubFolders = new List<Folder>(),
                    Files = new List<File>()
                };

                RootGroups[groupId].SubFolders.Add(subFolder);

                if (!RootGroups[groupId].SubFolders.Contains(subFolder))
                {
                    RootGroups[groupId].SubFolders.Add(subFolder);
                }
                if (splittedFolders.Length > 1)
                {
                    getFolder(RootGroups[groupId].SubFolders[j], splittedFolders, 1);
                }
                j++;
            }
            
            return folder;
        }

        private Folder getFolder(Folder folder, string[] splittedPath, int i)
        {
            Folder splittedPathI = new Folder() { Key = i, Name = splittedPath[i], SubFolders = new List<Folder>(), Files = new List<File>() };
            if (!folder.SubFolders.Contains(splittedPathI))
            {
                folder.SubFolders.Add(splittedPathI);
            }

            if (folder.Name == splittedPathI.Name)
            {
                return folder;
            }

            if (i == splittedPath.Length - 1)
            {
                return folder;
            }
            else
            {
                i++;
                Folder folderToFind = new Folder() { Key = i, Name = splittedPath[i - 1], SubFolders = new List<Folder>(), Files = new List<File>() };
                int foundId = 0;

                for (int j = 0; j < folder.SubFolders.Count(); j++) // find subfolder in exists subfolders
                {
                    if (folder.SubFolders[j].Name == folderToFind.Name)
                    {
                        foundId = j;
                        break;
                    }
                }

                return getFolder(folder.SubFolders.ElementAt(foundId), splittedPath, i);
            }
        }

        public void Save()
        {
            for (int i = 0; i < RootGroups.Count(); i++)
            {
                Console.WriteLine("RootGroup: " + RootGroups[i].Name);

                SaveFolder(RootGroups[i], i);
            }
        }

        private Folder SaveFolder(Folder rootFolder, int groupId)
        {
            Folder folder = new Folder();
            Saver.SaveText(PATH_FOLDERS + "\\save\\" + rootFolder.Name + ".txt", rootFolder.Name);

            for (int i = 0; i < rootFolder.SubFolders.Count(); i++)
                Console.WriteLine(rootFolder.SubFolders[i].Name);

            return folder;
        }

    }
}
