// MIT License
// 
// Copyright (c) 2024 RollW
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System.Collections.Generic;

namespace StationNaming.Utils;

public class UserDefinedSequenceProvider(UserDefinedSequenceConfiguration configuration) : ISequenceProvider
{
    private UserDefinedSequenceConfiguration _configuration = configuration;
    // TODO: Implement the UserDefinedSequenceProvider class

    public string GetSequence(int index, int count, ISequenceProvider.SequenceOptions options)
    {
        return GetSequence(index, options);
    }

    public string GetSequence(
        int index,
        ISequenceProvider.SequenceOptions options)
    {
        if (index < 0)
        {
            return "Invalid";
        }

        if (_configuration.CompositeConfig.Valid())
        {
            // TODO:
            var composite = _configuration.CompositeConfig;
            if (composite.Digits.TryGetValue((uint)index, out var digit))
            {
                return digit;
            }

            var digits = new List<string>();
            var current = index;
            var divisor = 1;
            while (current > 0)
            {
                var remainder = current % 10;
                if (remainder > 0)
                {
                    var dg = composite.Digits[(uint)(remainder * divisor)];
                    digits.Add(dg);
                }
                current /= 10;
                divisor *= 10;
            }

            return string.Join("", digits);
        }

        return string.Empty;
    }
}

public struct UserDefinedSequenceConfiguration
{
    public Enumerate EnumerateConfig;
    public Composite CompositeConfig;

    public struct Enumerate
    {
        public Dictionary<uint, string> Sequences;
    }

    public struct Composite
    {
        public Dictionary<uint, string> Digits;
        public string Ten;
        public string Hundred;
        public string Thousand;

        public bool Valid()
        {
            return Digits.Count > 0;
        }
    }
}