using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InferenceEngine
{
    public class TruthTableInference : InferenceMethod
    {
        public string Ask(Symbol query, KnowledgeBase kb)
        {
            var validModels = new List<List<Symbol>>();

            var symbols = kb.Symbols.Values.ToList();

            var p = new Permutator(symbols);

            // Get all valid models.
            while (p.GetNextPermutation() != null) {
                if (IsValidModel(kb, symbols))
                    validModels.Add(symbols);
            }

            // Query is true if the query symbol is true in ALL valid models.
            foreach (var validSymbols in validModels) {
                var querySymbol = validSymbols.Find(s => s.Name == query.Name);

                if (querySymbol.Value == false)
                    return "NO";
            }

            // Query was true in all valid models, result is true.
            return "YES: " + validModels.Count;
        }

        private bool IsValidModel(KnowledgeBase kb, List<Symbol> symbols)
        {
            foreach (var sentence in kb.Knowledge) {
                if (!sentence.IsTrueFor(symbols)) {
                    return false;
                }
            }

            return true;
        }
    }
}
