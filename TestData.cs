using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3_registry
{
    public class TestData
    {
        public IList<Folder> Groups = new List<Folder>();

        public void Load()
        {
            Folder grp1 = new Folder() { Key = 1, Name = "Group 1", SubFolders = new List<Folder>(), Files = new List<File>() };
            Folder grp2 = new Folder() { Key = 2, Name = "Group 2", SubFolders = new List<Folder>(), Files = new List<File>() };
            Folder grp3 = new Folder() { Key = 3, Name = "Group 3", SubFolders = new List<Folder>(), Files = new List<File>() };
            Folder grp4 = new Folder() { Key = 4, Name = "Group 4", SubFolders = new List<Folder>(), Files = new List<File>() };

            grp1.Files.Add(new File() { Key = 1, Name = "Entry number 1" });
            grp1.Files.Add(new File() { Key = 2, Name = "Entry number 2" });
            grp1.Files.Add(new File() { Key = 3, Name = "Entry number 3" });

            grp2.Files.Add(new File() { Key = 4, Name = "Entry number 4" });
            grp2.Files.Add(new File() { Key = 5, Name = "Entry number 5" });
            grp2.Files.Add(new File() { Key = 6, Name = "Entry number 6" });

            grp3.Files.Add(new File() { Key = 7, Name = "Entry number 7" });
            grp3.Files.Add(new File() { Key = 8, Name = "Entry number 8" });
            grp3.Files.Add(new File() { Key = 9, Name = "Entry number 9" });

            grp4.Files.Add(new File() { Key = 10, Name = "Entry number 10" });
            grp4.Files.Add(new File() { Key = 11, Name = "Entry number 11" });
            grp4.Files.Add(new File() { Key = 12, Name = "Entry number 12" });

            grp4.SubFolders.Add(grp1);
            grp2.SubFolders.Add(grp4);

            Groups.Add(grp1);
            Groups.Add(grp2);
            Groups.Add(grp3);
        }
    }
}
