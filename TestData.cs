using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lab3_registry.Utils;

namespace lab3_registry
{
    public class TestData
    {
        public IList<Folder> Groups = new List<Folder>();

        public const string PATH_FOLDERS = @"E:\Универ\5SEMESTR\ОС\lab3\lab3_registry\bin\Debug\folders";

        public void Load()
        {
            List<string> filenames = FileUtils.getFilenamesFromDir(PATH_FOLDERS);
            List<Folder> folders = new List<Folder>();

            for (int i = 0; i < filenames.Count(); i++)
            {
                folders.Add(new Folder() { Key = i, Name = filenames[i], SubFolders = new List<Folder>(), Files = new List<File>() });
            }

            folders[0].Files.Add(new File() { Key = 1, Name = "Entry number 1" });
            folders[0].Files.Add(new File() { Key = 2, Name = "Entry number 2" });
            folders[0].Files.Add(new File() { Key = 3, Name = "Entry number 3" });

            folders[1].Files.Add(new File() { Key = 1, Name = "Subkey 1" });
            folders[1].Files.Add(new File() { Key = 2, Name = "Kkeqw 2" });
            folders[1].Files.Add(new File() { Key = 3, Name = "SAxzc 23" });

            folders[1].SubFolders.Add(folders[3]);
            folders[0].SubFolders.Add(folders[1]);
            //grp4.SubFolders.Add(grp1);
            //grp2.SubFolders.Add(grp4);

            for (int i = 0; i < folders.Count(); i++)
            {
                Groups.Add(folders[i]);
            }
        }
    }
}
