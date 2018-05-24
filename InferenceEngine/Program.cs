using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace InferenceEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            string methodName = args[0];
            string filename = args[1];

            InferenceInput input = Parser.ParseInferenceInput(filename);

            var kb = new KnowledgeBase();

            foreach (var clause in input.knowledgeClauses) {
                kb.Tell(clause);
            }

            kb.Ask(input.queryClause, methodName);

            Console.ReadLine();
        }
    }
}
