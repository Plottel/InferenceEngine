using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InferenceEngine
{
    // Rename to sentence
    public class Sentence
    {
        private string _string;
        private Dictionary<string, Symbol> _leftSide;
        private Symbol _implication;
        private bool _isKnown = false;

        public bool IsKnown {get => _isKnown;}

        public List<Symbol> Symbols
        {
            get
            {
                var result = new List<Symbol> { _implication };

                if (!IsKnown) {
                    result.AddRange(_leftSide.Values);
                }

                return result;
            }
        }

        public Sentence(string clauseString)
        {
            _string = clauseString;
            _leftSide = new Dictionary<string, Symbol>();

            // Horn Clause or Known?
            if (_string.Contains("=>"))
            {
                // Horn Clause
                string[] bothSides = _string.Split(new string[] {"=>"}, StringSplitOptions.RemoveEmptyEntries);

                // Single symbol left side or do conjunctions exist?
                if (bothSides[0].Contains("&"))
                {
                    // Conjunctions
                    string[] symbolNames = bothSides[0].Split(new string[] {"&"}, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var symbolName in symbolNames)
                        _leftSide.Add(symbolName, new Symbol(symbolName, true));                    

                }
                else
                {
                    // Single symbol left side
                    string symbolName = bothSides[0];
                    _leftSide.Add(symbolName, new Symbol(symbolName, true));
                }

                // Add implication
                string implicationSymbolName = bothSides[1];
                _implication = new Symbol(implicationSymbolName, true);
                _isKnown = false; 
            }
            else
            {
                // Known clause
                _implication = new Symbol(_string, true);
                _isKnown = true;
            }
        }

        public bool IsTrueFor(List<Symbol> symbols)
        {
            // Set symbol values
            foreach (var symbol in symbols)
            {
                if (_implication.Name == symbol.Name)
                    _implication.Value = symbol.Value;
                else if (_leftSide.ContainsKey(symbol.Name))
                    _leftSide[symbol.Name].Value = symbol.Value;
            }

            // Evaluate logic
            if (IsKnown)
                return _implication.Value; // Is known is single symbol statement.
            else
            {
                // All left side true?
                bool leftSideValue = true;
                foreach (var symbol in _leftSide.Values)
                {
                    if (symbol.Value == false)
                    {
                        leftSideValue = false;
                        break;
                    }
                }

                // Evaluate using implication law
                //      L    R    Result
                //      T    T      T
                //      T    F      F
                //      F    T      T
                //      F    F      T

                bool left = leftSideValue;
                bool right = _implication.Value;

                if (!left)
                    return true;
                else
                    return right;   
            }
        }

        public void Print()
        {
            if (IsKnown)
                Console.WriteLine(_implication.Name + " is known");
            else 
            {
                string print = "";

                foreach (var symbol in _leftSide) {
                    print += symbol.Key + "&";
                }

                print = print.Remove(print.Length - 1); // Remove extra "&"

                print += "=>" + _implication.Name;

                Console.WriteLine(print);
            }
        }
    }
}
