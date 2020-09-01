// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
using CompilableRegexp.Collections;
using System.Collections.Generic;

namespace CompilableRegexp
{
    internal class NFA
    {
        internal class Node
        {
            internal readonly HashSet<Node>[] Moves;
            internal HashSet<Node> Epsilon = new HashSet<Node>();
            internal Node(int dictSize)
            {
                Moves = new HashSet<Node>[dictSize];
                for (var i = 0; i < dictSize; i++)
                    Moves[i] = new HashSet<Node>();
            }
        }
        internal RegexpCharDictionary dict;
        internal Node Entry;
        internal Node Terminating;

        public NFA(Node entry, Node terminating, RegexpCharDictionary dict)
        {
            Entry = entry;
            Terminating = terminating;
            this.dict = dict;
        }
    }
}
