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

            // Determine which models are valid.
            foreach (var symbolCombination in GetAllSymbolCombinations(kb)) {
                SetSymbolValues(symbols, symbolCombination);

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

        private void SetSymbolValues(List<Symbol> symbols, List<bool> values)
        {
            for (int i = 0; i < symbols.Count; ++i) {
                symbols[i].Value = values[i];
            }
        }

        private List<List<bool>> GetAllSymbolCombinations(KnowledgeBase kb)
        {
            var result = new List<List<bool>>();

            // Adopted From: 
            // https://stackoverflow.com/questions/39734887/generating-all-possible-true-false-combinations
            int numVars = kb.Symbols.Count;

            for (int i = 0; i < (1 << numVars); ++i) {
                var combination = new List<bool>();

                for (int j = numVars - 1; j >= 0; --j) {
                    combination.Add((i & (1 << j)) != 0);
                }

                result.Add(combination);
            }

            return result;
        }
    }
}
