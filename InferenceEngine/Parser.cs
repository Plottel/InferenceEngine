using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace InferenceEngine
{
    public static class Parser
    {
        public static InferenceInput ParseInferenceInput(string filePath)
        {
            var inferenceInput = new InferenceInput();

            var fileLines = File.ReadAllLines(filePath);
            int tellIndex = 0;
            int askIndex = 0;

            for (int i = 0; i < fileLines.Length; ++i) {
                if (fileLines[i] == "TELL") {
                    tellIndex = i;
                } else if (fileLines[i] == "ASK") {
                    askIndex = i;
                }
            }

            string[] tellLines = new string[askIndex - tellIndex - 1];
            for (int i = 0; i < tellLines.Length; ++i) {
                tellLines[i] = fileLines[tellIndex + 1 + i];
            }

            var clauseStrings = new List<string>();
            foreach (var line in tellLines) {
                var formattedLines = new List<string>();

                foreach (var formattedLine in line.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries)) {
                    formattedLines.Add(formattedLine.Replace(" ", ""));
                }

                clauseStrings.AddRange(formattedLines);
            }

            inferenceInput.knowledgeClauses = clauseStrings;
            inferenceInput.queryClause = fileLines[askIndex + 1];

            return inferenceInput;
        }
    }
}
