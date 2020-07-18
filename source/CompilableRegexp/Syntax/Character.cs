// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace CompilableRegexp.Syntax
{
    internal class Character : SyntaxElement
    {
        internal char Char;

        public Character(char @char)
        {
            Char = @char;
        }

        internal override NFA.NFANode ToNFANode(NFA.NFANode entry)
        {
            var terminator = new NFA.NFANode();
            entry.AddMove(Char, terminator);
            if (Next == null)
                return terminator;
            return Next.ToNFANode(terminator);
        }
    }
}
