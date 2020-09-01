using System;
using System.Collections.Generic;
using System.Text;

namespace CompilableRegexp
{
    public class Regex
    {
        private DFA automaton;
        public Regex(string expr)
        {
            var (syntax, dict) = Syntax.SyntaxElement.Parse(expr);
            var NFAEntry = new NFA.Node(dict.Count);
            var nfa = new NFA(NFAEntry, syntax.ToNFANode(NFAEntry, dict), dict);
            automaton = new DFA(nfa);
        }
        public bool IsMatch(string input) => automaton.IsAccepting(input);
    }
}
