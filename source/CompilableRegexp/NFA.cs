// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
using System.Collections.Generic;

namespace CompilableRegexp
{
    internal class NFA
    {
        internal class NFANode
        {
            internal Dictionary<char, NFANode> Moves = new Dictionary<char, NFANode>();
            internal NFANode Any;
            internal List<NFANode> Epsilon = new List<NFANode>();
        }
        internal NFANode Entry;
        internal NFANode Terminating;

        public NFA(NFANode entry, NFANode terminating)
        {
            Entry = entry;
            Terminating = terminating;
        }
    }
}
