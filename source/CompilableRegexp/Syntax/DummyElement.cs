// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using CompilableRegexp.Collections;

namespace CompilableRegexp.Syntax
{
    internal class DummyElement : SyntaxElement
    {
        internal override NFA.Node ToNFANode(NFA.Node entry, RegexpCharDictionary dict)
        {
            if (Next == null)
                return entry;
            return Next.ToNFANode(entry, dict);
        }
    }
}
