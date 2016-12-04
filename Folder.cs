using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3_registry
{
    public class Folder
    {
        public int Key { get; set; }
        public string Name { get; set; }

        public string FullPath { get; set; }

        public IList<Parameter> Parameters { get; set; }

        public IList<Folder> SubFolders { get; set; }
        public IList<File> Files { get; set; }

        public IList<object> Items
        {
            get
            {
                IList<object> childNodes = new List<object>();
                foreach (var group in this.SubFolders)
                    childNodes.Add(group);
                foreach (var entry in this.Files)
                    childNodes.Add(entry);

                return childNodes;
            }
        }
    }
}
