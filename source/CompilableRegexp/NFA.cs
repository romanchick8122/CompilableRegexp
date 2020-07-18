// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
using System.Collections.Generic;

namespace CompilableRegexp
{
    internal class NFA
    {
        internal class NFANode
        {
            internal IDictionary<char, List<NFANode>> Moves = new Dictionary<char, List<NFANode>>();
            internal List<NFANode> Epsilon = new List<NFANode>();
            internal void AddMove(char c, NFANode target)
            {
                if (Moves.ContainsKey(c))
                    Moves[c].Add(target);
                else
                    Moves.Add(c, new List<NFANode>()
                    {
                        target
                    });
            }
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
