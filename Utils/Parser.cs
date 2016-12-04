using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3_registry.Utils
{
    public class Parser
    {
        public static Parameter parseParameterFromString(string str)
        {
            Parameter parameter = new Parameter();

            if (str != "" || str != null)
            {
                Char delimiter = ',';
                string[] splitted = str.Split(delimiter);

                // remove double quotes:
                for (int i = 0; i < splitted.Length; i++)
                {
                    splitted[i] = splitted[i].Replace("\"", "").Trim();
                }

                parameter = new Parameter(Int32.Parse(splitted[0]), splitted[1], splitted[2], splitted[3]);
            }

            return parameter;
        }
    }
}
