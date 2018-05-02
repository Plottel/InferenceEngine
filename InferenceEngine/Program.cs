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
            var input = Parser.ParseInferenceInput("C:/Users/mattn/Desktop/InferenceEngine/InferenceEngine/bin/Debug/test1.txt");

            Console.WriteLine("Knowledge Base:");
            foreach (var clause in input.knowledgeClauses) {
                Console.WriteLine("- " + clause);
            }

            Console.WriteLine("Query:");
            Console.WriteLine(input.queryClause);

            Console.ReadLine();
        }
    }
}
