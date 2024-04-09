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
using Unity.Entities;

namespace StationNaming.System;

public struct NamingAssociation(Entity target) : IBufferElementData,
    IQueryTypeParameter, IEmptySerializable, IEquatable<NamingAssociation>,
    ISerializable
{
    public Entity Target = target;

    public bool Equals(NamingAssociation other)
    {
        return Target == other.Target;
    }

    public override bool Equals(object obj)
    {
        return obj is NamingAssociation other && Equals(other);
    }

    public override int GetHashCode()
    {
        return Target.GetHashCode();
    }

    public static bool operator ==(NamingAssociation left, NamingAssociation right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(NamingAssociation left, NamingAssociation right)
    {
        return !left.Equals(right);
    }

    public void Serialize<TWriter>(TWriter writer) where TWriter : IWriter
    {
        writer.Write(Target);
    }

    public void Deserialize<TReader>(TReader reader) where TReader : IReader
    {
        reader.Read(out Target);
    }
}