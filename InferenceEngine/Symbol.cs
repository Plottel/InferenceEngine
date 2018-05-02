using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InferenceEngine
{
    public class Symbol
    {
        public string Name;
        public bool Value;

        public Symbol(string name, bool value)
        {
            Name = name;
            Value = value;
        }
    }
}
