using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3_registry.Utils
{
    public class FolderUtils
    {
        public static string getFullPath(string folderName, string[] splittedPath, int num)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < num + 1; i++)
            {
                if (i == num) // if last
                {
                    sb.Append(splittedPath[i]);
                }
                else
                {
                    sb.Append(splittedPath[i]).Append("\\");
                }
            }

            return sb.ToString();
        }


    }
}
