using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InferenceEngine
{
    public class KnowledgeBase
    {
        private List<Sentence> _knowledge = new List<Sentence>();
        private Dictionary<string, Symbol> _symbols = new Dictionary<string, Symbol>();
        private InferenceMethod _method;

        public List<Sentence> Knowledge { get => _knowledge; }
        public Dictionary<string, Symbol> Symbols { get => _symbols; }

        public void Tell(string clauseString)
        {
            var newSentence = new Sentence(clauseString);
            
            foreach (var symbol in newSentence.Symbols) 
            {
                if (!_symbols.ContainsKey(symbol.Name)) {
                    _symbols[symbol.Name] = symbol;
                }
            }

            _knowledge.Add(newSentence);
        }

        public void Ask(string clauseString, string methodName)
        {
            if (methodName == "TT")
                _method = new TruthTableInference();
            else if (methodName == "FC")
                _method = new ForwardsChainingInference();
            else if (methodName == "BC")
                _method = new BackwardsChainingInference();

            Console.WriteLine(_method.Ask(new Symbol(clauseString, true), this));
        }

        public bool SymbolIsKnown(Symbol s)
        {
            foreach (var sentence in Knowledge) {
                if (sentence.IsKnown) {
                    if (sentence.Implication.Name == s.Name) {
                        return true;
                    }
                }
            }

            return false;
        }

        public void Print()
        {
            Console.WriteLine("-- KNOWLEDGE BASE --");
            Console.WriteLine("");
            Console.WriteLine("Knowledge Sentences");

            foreach (var sentence in _knowledge) {
                sentence.Print();
            }

            Console.WriteLine("All Symbols");

            string symbols = "";

            foreach (var symbol in _symbols.Keys)
                symbols += symbol + ";";

            Console.WriteLine(symbols);
        }
    }
}
