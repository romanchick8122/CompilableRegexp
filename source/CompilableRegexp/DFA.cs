using CompilableRegexp.Collections;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace CompilableRegexp
{
    internal class DFA
    {
        internal class Node
        {
            internal Node[] Moves;
            internal bool Accepting = false;

            internal Node(int dictSize)
            {
                Moves = new Node[dictSize];
            }
        }

        internal RegexpCharDictionary dict;
        internal Node entry;
        public DFA(NFA automaton)
        {
            dict = automaton.dict;

            var powersets = new Dictionary<HashSet<NFA.Node>, Node>(HashSet<NFA.Node>.CreateSetComparer());
            var reversets = new Dictionary<Node, HashSet<NFA.Node>>();
            HashSet<NFA.Node> GetEpsilonClosure(NFA.Node node)
            {
                var ret = new HashSet<NFA.Node>();
                var unpr = new HashSet<NFA.Node>
                {
                    node
                };
                while (unpr.Any())
                {
                    var curr = unpr.First();
                    unpr.Remove(curr);
                    if (ret.Contains(curr))
                        continue;
                    ret.Add(curr);
                    unpr.UnionWith(curr.Epsilon);
                }
                return ret;
            }

            entry = new Node(automaton.dict.Count);
            reversets[entry] = GetEpsilonClosure(automaton.Entry);
            powersets[reversets[entry]] = entry;

            var bfs = new Queue<Node>();
            bfs.Enqueue(entry);
            while (bfs.Any())
            {
                var curr = bfs.Dequeue();
                var currSet = reversets[curr];
                if (currSet.Any(x => automaton.Terminating == x))
                    curr.Accepting = true;
                for (var i = 0; i < dict.Count; i++)
                {
                    var target = new HashSet<NFA.Node>();
                    currSet.ForEach(x => target.UnionWith(x.Moves[i]));
                    if (powersets.TryGetValue(target, out var targetDfa))
                        curr.Moves[i] = targetDfa;
                    else
                    {
                        curr.Moves[i] = new Node(dict.Count);
                        bfs.Enqueue(curr.Moves[i]);
                        powersets.Add(target, curr.Moves[i]);
                        reversets.Add(curr.Moves[i], target);
                    }
                }
            }
        }

        public bool IsAccepting(string s)
        {
            var curr = entry;
            foreach(char c in s)
            {

                if (!dict.ContainsKey(c))
                    return false;
                curr = curr.Moves[dict[c]];
                if (curr == null)
                    return false;
            }
            return curr.Accepting;
        }
    }
}
