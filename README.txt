All source code and commit history available at https://github.com/Plottel/InferenceEngine

The .exe can be found at InferenceEngine/bin/Debug

The .exe needs to be run from the command line, with 2 parameters passed:
1. The name of the inference method.
2. The name of the file - this file must be in the same folder as the .exe

Inference Method names are:
- TT (Truth Table)
- FC (Forward Chaining)
- BC (Backward Chaining)

Possible use cases:

InferenceEngine.exe TT test1.txt
InferenceEngine.exe FC test2.txt
InferenceEngine.exe BC test3.txt