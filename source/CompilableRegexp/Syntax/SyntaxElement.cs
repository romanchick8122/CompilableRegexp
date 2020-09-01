// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using CompilableRegexp.Collections;

namespace CompilableRegexp.Syntax
{
    internal abstract class SyntaxElement
    {
        internal SyntaxElement Next;
        internal SyntaxElement Prev;
        internal SyntaxElement Parent;
        internal abstract NFA.Node ToNFANode(NFA.Node entry, RegexpCharDictionary dict);
        internal static (SyntaxElement syntax, RegexpCharDictionary charsDictionary) Parse(string regExp)
        {
            var charsDictionaryBuilder = new RegexpCharDictionary.Builder();
            SyntaxElement ret = new DummyElement(), curr = ret;
            for (int i = 0; i < regExp.Length; i++)
            {
                //TODO: Character Escapes
                //TODO: Character Classes
                //TODO: Anchors
                //TODO: Grouping Constructs
                //TODO: Quantifiers
                //TODO: Backreference Constructs
                //TODO: Alternation Constructs
                //TODO: Miscellaneous Constructs
                charsDictionaryBuilder.Use(regExp[i]);
                curr.Next = new Character(regExp[i])
                {
                    Prev = curr,
                    Parent = curr.Parent
                };
                curr = curr.Next;
            }
            return (ret, charsDictionaryBuilder.Build());
        }
    }
}
