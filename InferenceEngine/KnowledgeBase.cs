using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InferenceEngine
{
    public class KnowledgeBase
    {
        private List<Clause> _knowledgeClauses = 
            new List<Clause>();

        public void Tell(string clauseString)
        {
            _knowledgeClauses.Add(new Clause(clauseString));
        }

        public void Ask(string clauseString)
        {
            
        }

        public void Print()
        {
            Console.WriteLine("-- KNOWLEDGE BASE --");
            Console.WriteLine("");
            Console.WriteLine("Knowledge Clauses");
        }
    }
}
