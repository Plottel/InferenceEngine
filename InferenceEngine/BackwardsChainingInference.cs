using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InferenceEngine
{
    public class BackwardsChainingInference : InferenceMethod
    {
        private KnowledgeBase _kb;
        private List<string> _path;

        private string PathString
        {
            get
            {
                var result = "";

                foreach (var s in _path) {
                    result += s + " ";
                }

                return result;
            }
        }

        public string Ask(Symbol query, KnowledgeBase kb)
        {
            _kb = kb;
            _path = new List<string>();

            if (kb.SymbolIsKnown(query)) {
                return "YES: " + query.Name;
            }

            if (BackwardChain(query)) {
                return "YES: " + PathString;
            }

            return "NO";
        }

        private bool BackwardChain(Symbol s)
        {
            _path.Insert(0, s.Name);

            if (_kb.SymbolIsKnown(s)) {
                return true;
            }

            foreach (var symbol in GetSymbolsImplying(s)) {
                if (!BackwardChain(symbol)) {
                    return false;
                }
            }

            return true;
        }       

        private List<Symbol> GetSymbolsImplying(Symbol s)
        {
            var result = new List<Symbol>();

            foreach (var sentence in _kb.Knowledge) {
                if (sentence.Implication.Name == s.Name) {
                    result.AddRange(sentence.LeftSide);
                }
            }

            return result;
        }
    }
}
