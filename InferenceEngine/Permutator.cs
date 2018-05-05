using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InferenceEngine
{
    public class Permutator
    {
        private List<Symbol> _symbols;

        public Permutator(List<Symbol> symbols)
        {
            _symbols = symbols;

            // First permutation = everything false;
            foreach (var symbol in symbols)
                symbol.Value = false;
        }

        public List<Symbol> GetNextPermutation()
        {
            return new List<Symbol>();
        }   
    }
}
