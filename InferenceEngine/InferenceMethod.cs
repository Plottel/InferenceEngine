using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InferenceEngine
{
    public interface InferenceMethod
    {
        string Ask(Symbol query, KnowledgeBase kb);
    }
}
