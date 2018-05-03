using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InferenceEngine
{
    public class KnowledgeBase
    {
        private List<Sentence> _knowledgeClauses = 
            new List<Sentence>();

        public void Tell(string clauseString)
        {
            _knowledgeClauses.Add(new Sentence(clauseString));
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
