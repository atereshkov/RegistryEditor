using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using lab3_registry.Utils;
using lab3_registry.IO;

namespace lab3_registry
{
    public class TestData
    {
        public IList<Folder> RootGroups = new List<Folder>();
        public IList<string> Filenames = new List<string>();

        public const string PATH_FOLDERS = @"E:\Универ\5SEMESTR\ОС\lab3\lab3_registry\bin\Debug\folders";
        public const string PATH_PARAMETERS = @"E:\Универ\5SEMESTR\ОС\lab3\lab3_registry\bin\Debug\parameters";

        public void Load()
        {
            Filenames = FileUtils.getFilenamesFromDir(PATH_FOLDERS);
            List<Folder> folders = new List<Folder>();

            List<Parameter> parameters = new List<Parameter>();
            parameters.Add(new Parameter(1, "(Default)", "REG_SZ", "(no value)")); // default parameters for root groups

            for (int i = 0; i < Filenames.Count(); i++) // get all files from dir "folders"
            {
                folders.Add(new Folder() { 
                    Key = i, 
                    Name = Filenames[i], 
                    SubFolders = new List<Folder>(), 
                    Parameters = parameters,
                    Files = new List<File>() });
            }

            for (int i = 0; i < folders.Count(); i++) // add all root folders
            {
                RootGroups.Add(folders[i]);
                getFolder(folders[i].Name, i); // start adding subfolders and subfolders of subfolders and so on...
            }
        }

        public Folder getFolder(string rootGroupName, int groupId)
        {
            Folder folder = new Folder();

            string[] lines = Reader.ReadAllLines(PATH_FOLDERS + "\\" + rootGroupName + ".txt"); // get all lines from one group (HKEY_LOCAL_MACHINE, for example)

            Char delimiter = '\\';
            String[] splittedFolders;

            int j = 0;
            for (int i = 0; i < lines.Count(); i++)
            {
                splittedFolders = lines[i].Split(delimiter); // split it with delimiter "\"

                IList<Parameter> parameters = ParameterUtils.getParametersForFolder(splittedFolders[0], rootGroupName);

                Folder subFolder = new Folder()
                {
                    Key = 0,
                    Name = splittedFolders[0],
                    SubFolders = new List<Folder>(),
                    Parameters = parameters,
                    FullPath = splittedFolders[0], // can write fullpath as name, because it always second after root group folder
                    Files = new List<File>()
                };

                RootGroups[groupId].SubFolders.Add(subFolder); // add first subfolder

                if (!RootGroups[groupId].SubFolders.Contains(subFolder))
                {
                    RootGroups[groupId].SubFolders.Add(subFolder);
                }
                if (splittedFolders.Length > 1)
                {
                    Console.WriteLine("12312312!!!!: " + RootGroups[groupId].SubFolders[j].Name);
                    getFolder(RootGroups[groupId].SubFolders[j], splittedFolders, 1, rootGroupName); // start recursive adding
                }
                j++;
            }
            
            return folder;
        }

        private Folder getFolder(Folder folder, string[] splittedPath, int i, string rootGroupName) // folder = previous path (HC\KOM, HC = folder, KOM = splittedPath[i])
        {
            Console.WriteLine("!!!Current: " + splittedPath[i] + " folder (previous): " + folder.Name);
            IList<Parameter> parameters = ParameterUtils.getParametersForFolder(splittedPath[i], rootGroupName); // getParameters for this folderName

            string fullPath = FolderUtils.getFullPath(splittedPath[i], splittedPath, i);

            Folder splittedPathI = new Folder() { Key = i,
                Name = splittedPath[i], 
                SubFolders = new List<Folder>(),
                Parameters = parameters,
                FullPath = fullPath,
                Files = new List<File>() };

            //Console.WriteLine("FULLPATH FOR " + splittedPath[i] + " : " + fullPath);

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

                return getFolder(folder.SubFolders.ElementAt(foundId), splittedPath, i, rootGroupName);
            }
        }

        public void Save()
        {
            for (int i = 0; i < RootGroups.Count(); i++)
            {
                Console.WriteLine("RootGroup: " + RootGroups[i].Name);

                SaveFolder(RootGroups[i], i); // start saving by root groups
            }
        }

        private Folder SaveFolder(Folder rootFolder, int groupId)
        {
            Folder folder = new Folder();
            string filename = PATH_FOLDERS + "\\" + rootFolder.Name + ".txt";
            string paramsFilename = PATH_PARAMETERS + "\\" + rootFolder.Name + ".txt";

            List<string> existParameters = new List<string>();

            using (StreamWriter streamWriter = new StreamWriter(filename))
            using (StreamWriter paramsWriter = new StreamWriter(paramsFilename))
            {
                for (int i = 0; i < rootFolder.SubFolders.Count(); i++)
                {
                    Console.WriteLine(rootFolder.SubFolders[i].Name);
                    SaveFolder(streamWriter, paramsWriter, rootFolder.SubFolders, existParameters);
                }
                existParameters.Clear();
                streamWriter.Close();
            }

            FileUtils.RemoveEmptyLines(filename);
            FileUtils.RemoveLinesFromFile(filename, rootFolder.SubFolders.Count());
            FileUtils.RemoveLastCharacter(filename);

            return folder;
        }

        public void SaveFolder(StreamWriter writer, StreamWriter paramsWriter, IList<Folder> subFolders, List<string> existParameters)
        {
            List<String> subs = new List<String>();

            foreach (Folder subFolder in subFolders)
            {
                if (subFolders.Count() > 0)
                {
                    Console.WriteLine("Count > 0 " + subFolder.Name);
                    subs.Add(subFolder.Name);
                }
                else
                {
                    //Console.WriteLine("Else: " + subFolder.Name);
                }
                writer.Write(FileUtils.getFullPath(subs));
                //Console.WriteLine("sub = " + FileUtils.getFullPath(subs));
                //Console.WriteLine("FullPath!: " + subFolder.FullPath);

                // write parameters for each folder:

                if (subFolder.Parameters.Count() > 0 && !existParameters.Contains(subFolder.FullPath))
                {
                    paramsWriter.WriteLine(subFolder.FullPath);

                    foreach (Parameter param in subFolder.Parameters)
                    {
                        paramsWriter.WriteLine(ParameterUtils.getStringParameter(param));
                    }

                    paramsWriter.WriteLine("---");
                    existParameters.Add(subFolder.FullPath);
                }
                
                subs.Clear();
                SaveFolder(writer, paramsWriter, subFolder.SubFolders, existParameters);
            }
            writer.WriteLine();
        }


    }
}
