// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using CompilableRegexp.Collections;

namespace CompilableRegexp.Syntax
{
    internal class Character : SyntaxElement
    {
        internal char Char;

        public Character(char @char)
        {
            Char = @char;
        }

        internal override NFA.Node ToNFANode(NFA.Node entry, RegexpCharDictionary dict)
        {
            var terminator = new NFA.Node(dict.Count);
            entry.Moves[dict[Char]].Add(terminator);
            if (Next == null)
                return terminator;
            return Next.ToNFANode(terminator, dict);
        }
    }
}
