using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InferenceEngine
{
    public class Clause
    {
        private string _string;

        public Clause(string clauseString)
        {
            _string = clauseString;

            // a&b&c => d
        }

        public bool IsTrueFor(string clauseString)
        {
            return true;
        }
    }
}
