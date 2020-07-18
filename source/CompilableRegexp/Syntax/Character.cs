﻿// This is an open source non-commercial project. Dear PVS-Studio, please check it.
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
    }
}
