using MoreLinq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CompilableRegexp.Collections
{
    internal class RegexpCharDictionary : IReadOnlyDictionary<char, ushort>
    {
        internal class Builder
        {
            private readonly bool[] needsChangeAfter = new bool[65536];
            private readonly bool[] used = new bool[65536];
            private readonly int[] ranges = new int[65536];

            internal RegexpCharDictionary Build()
            {
                var ret = new RegexpCharDictionary();
                var currRanges = 0;
                for (int i = 0; i <= ushort.MaxValue; i++)
                {
                    currRanges += ranges[i];
                    if (used[i] || currRanges > 0)
                    {
                        ret.mapping[i] = ret.count;
                        if (needsChangeAfter[i])
                            ret.count++;
                    }
                    else
                    {
                        ret.mapping[i] = ushort.MaxValue;
                    }
                }
                return ret;
            }
            internal void Use(char c)
            {
                needsChangeAfter[c] = true;
                if (c != 0)
                    needsChangeAfter[c - 1] = true;
                used[c] = true;
            }
            internal void Use(char from, char to)
            {
                if (from > 0)
                    needsChangeAfter[from - 1] = true;
                needsChangeAfter[to] = true;
                ranges[from]++;
                if (to < ushort.MaxValue)
                    ranges[to + 1]--;
            }
        }

        private readonly ushort[] mapping = new ushort[65536];
        private ushort count;

        public ushort this[char key]
        {
            get
            {
                if (mapping[key] == ushort.MaxValue)
                    ThrowHelper.ThrowKeyNotFoundException();
                return mapping[key];
            }
        }

        public IEnumerable<char> Keys => Enumerable.Range(0, ushort.MaxValue).Where(x => mapping[x] != ushort.MaxValue).Select(x => (char)x);

        public IEnumerable<ushort> Values => mapping.Window(2).Where(x => x[0] != ushort.MaxValue && x[0] == x[1]).Select(x => x[0]);

        public int Count => count;

        public bool ContainsKey(char key) => mapping[key] != ushort.MaxValue;

        public IEnumerator<KeyValuePair<char, ushort>> GetEnumerator()
        {
            ushort curr = 0;
            do
            {
                if (mapping[curr] != ushort.MaxValue)
                    yield return new KeyValuePair<char, ushort>((char)curr, mapping[curr]);
                curr++;
            } while (curr != 0);
        }

        public bool TryGetValue(char key, out ushort value)
        {
            value = mapping[key];
            if (mapping[key] == ushort.MaxValue)
                return false;
            else
                return true;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
