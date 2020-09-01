using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;

namespace CompilableRegexp
{
    [Pure]
    internal static class ThrowHelper
    {
        internal static void ThrowKeyNotFoundException()
        {
            throw new KeyNotFoundException();
        }
    }
}
