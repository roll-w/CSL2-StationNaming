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

using System;
using Colossal.Serialization.Entities;
using Unity.Collections;
using Unity.Entities;

namespace StationNaming.System;

[InternalBufferCapacity(0)]
public struct NameCandidate(string name, NameSource source): IBufferElementData,
    IEmptySerializable, IEquatable<NameCandidate>
{
    public FixedString512Bytes Name = new(name);
    public NameSource Source = source;

    public bool Equals(NameCandidate other)
    {
        return Name.Equals(other.Name) && Source == other.Source;
    }

    public override bool Equals(object obj)
    {
        return obj is NameCandidate other && Equals(other);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            return (Name.GetHashCode() * 397) ^ (int)Source;
        }
    }

    public override string ToString()
    {
        return $"Candidate['{Name}'({Source})]";
    }
}