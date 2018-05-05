using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InferenceEngine
{
    public class ForwardsChainingInference : InferenceMethod
    {
        private KnowledgeBase _kb;

        public string Ask(Symbol query, KnowledgeBase kb)
        {
            _kb = kb;
            // count = Dictionary<string, int>
            // Number of premises for each sentence
            var count = new Dictionary<Sentence, int>();

            foreach (var sentence in kb.Knowledge) {
                count[sentence] = sentence.LeftSide.Count;
            }

            // inferred = Dictionary<string, bool>
            // Whether each symbol has been inferred yet
            var inferred = new Dictionary<string, bool>();

            foreach (var symbol in kb.Symbols) {
                inferred[symbol.Key] = false;
            }

            // agenda = List<Symbol>
            // The inferred symbols. Begins with "known" symbols
            var agenda = new Stack<Symbol>();

            foreach (var symbol in kb.Symbols) {
                if (kb.SymbolIsKnown(symbol.Value)) {
                    agenda.Push(symbol.Value);
                }
            }

            string path = "";

            foreach (var symbol in agenda) {
                path += symbol.Name + " ";
            }

            while (agenda.Count > 0) {
                var premise = agenda.Pop();

                if (!inferred[premise.Name]) {
                    inferred[premise.Name] = true;

                    foreach (var sentence in GetSentencesContaining(premise)) {
                        --count[sentence];

                        if (count[sentence] == 0) {
                            if (sentence.Implication.Name == query.Name) {
                                return "YES: " + path + query.Name;
                            }

                            agenda.Push(sentence.Implication);

                            path += sentence.Implication.Name + " ";
                        }
                    }
                }
            }

            return query.Name + ": NO";

        }

        private List<Sentence> GetSentencesContaining(Symbol symbol)
        {
            var result = new List<Sentence>();

            foreach (var sentence in _kb.Knowledge) {
                if (sentence.LeftSide.Find(s => s.Name == symbol.Name) != null) {
                    result.Add(sentence);
                } else if (sentence.IsKnown && sentence.Implication.Name == symbol.Name) {
                    result.Add(sentence);
                }
            }

            return result;
        }
    }
}
