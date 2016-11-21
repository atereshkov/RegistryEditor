using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3_registry
{
    public class Parameter
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }

        public Parameter(int Id, string Name, string Type, string Value)
        {
            this.Id = Id;
            this.Name = Name;
            this.Type = Type;
            this.Value = Value;
        }
        
        public Parameter() { }
    }
}
