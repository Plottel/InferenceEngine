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
            //var fileLines = File.ReadAllLines(args[0]);
            InferenceInput input = Parser.ParseInferenceInput("C:/Users/mattn/Desktop/InferenceEngine/InferenceEngine/bin/Debug/test1.txt");

            var kb = new KnowledgeBase();

            foreach (var clause in input.knowledgeClauses) {
                kb.Tell(clause);
            }

            foreach (var symbol in kb.Symbols.Values) {
                kb.Ask(symbol.Name, "FC");
            }

            Console.ReadLine();
        }
    }
}
