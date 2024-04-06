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
using Colossal.UI.Binding;
using Unity.Collections;
using Unity.Entities;

namespace StationNaming.System;

[InternalBufferCapacity(0)]
public struct NameCandidate : IBufferElementData,
    IEmptySerializable, IEquatable<NameCandidate>, IJsonWritable, IJsonReadable
{
    public FixedString512Bytes Name;
    public Entity Refer;
    public NameSource Source;
    public Direction Direction;
    public EdgeType EdgeType;

    public NameCandidate(
        string name, Entity refer, NameSource source,
        Direction direction, EdgeType edgeType)
    {
        Name = name;
        Refer = refer;
        Source = source;
        Direction = direction;
        EdgeType = edgeType;
    }

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
            var hashCode = Name.GetHashCode();
            hashCode = (hashCode * 397) ^ (int)Source;
            return hashCode;
        }
    }

    public void Read(IJsonReader reader)
    {
        reader.ReadMapBegin();
        reader.ReadProperty("name");
        reader.Read(out string name);
        Name = name;

        reader.ReadProperty("refer");
        reader.Read(out Refer);

        reader.ReadProperty("source");
        reader.Read(out string source);
        Source = Enum.TryParse<NameSource>(source, out var nameSource)
            ? nameSource
            : NameSource.Unknown;

        reader.ReadProperty("direction");
        reader.Read(out string direction);
        Direction = Enum.TryParse<Direction>(direction, out var dir)
            ? dir
            : Direction.Init;

        reader.ReadProperty("edgeType");
        reader.Read(out string edgeType);
        EdgeType = Enum.TryParse<EdgeType>(edgeType, out var edge)
            ? edge
            : EdgeType.Same;

        reader.ReadMapEnd();
    }

    public void Write(IJsonWriter writer)
    {
        writer.TypeBegin("NameCandidate");
        writer.PropertyName("name");
        writer.Write(Name.ToString());
        writer.PropertyName("source");
        writer.Write(Source.ToString());
        writer.PropertyName("direction");
        writer.Write(Direction.ToString());
        writer.PropertyName("edgeType");
        writer.Write(EdgeType.ToString());
        writer.PropertyName("refer");
        writer.Write(Refer);
        writer.TypeEnd();
    }

    public override string ToString()
    {
        return $"Candidate['{Name}'({Source}-{Direction})]";
    }

    public static implicit operator ManagedNameCandidate(NameCandidate candidate)
    {
        return new ManagedNameCandidate(candidate);
    }
}

public struct ManagedNameCandidate(
    string name,
    Entity refer,
    NameSource source,
    Direction direction,
    EdgeType edgeType)
    : IEquatable<ManagedNameCandidate>, IJsonWritable, IJsonReadable
{
    public string Name = name;
    public Entity Refer = refer;
    public NameSource Source = source;
    public Direction Direction = direction;
    public EdgeType EdgeType = edgeType;

    public ManagedNameCandidate(NameCandidate candidate) : this(
        candidate.Name.ToString(),
        candidate.Refer,
        candidate.Source,
        candidate.Direction,
        candidate.EdgeType)
    {
    }

    public bool Equals(ManagedNameCandidate other)
    {
        return Name.Equals(other.Name) && Source == other.Source;
    }

    public override bool Equals(object obj)
    {
        return obj is ManagedNameCandidate other && Equals(other);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            var hashCode = Name.GetHashCode();
            hashCode = (hashCode * 397) ^ (int)Source;
            return hashCode;
        }
    }

    public override string ToString()
    {
        return $"Candidate['{Name}'({Source}-{Direction})]";
    }

    public void Read(IJsonReader reader)
    {
        reader.ReadMapBegin();
        reader.ReadProperty("name");
        reader.Read(out Name);

        reader.ReadProperty("refer");
        reader.Read(out Refer);

        reader.ReadProperty("source");
        reader.Read(out string source);
        Source = Enum.TryParse<NameSource>(source, out var nameSource)
            ? nameSource
            : NameSource.Unknown;

        reader.ReadProperty("direction");
        reader.Read(out string direction);
        Direction = Enum.TryParse<Direction>(direction, out var dir)
            ? dir
            : Direction.Init;

        reader.ReadProperty("edgeType");
        reader.Read(out string edgeType);
        EdgeType = Enum.TryParse<EdgeType>(edgeType, out var edge)
            ? edge
            : EdgeType.Same;

        reader.ReadMapEnd();
    }

    public void Write(IJsonWriter writer)
    {
        writer.TypeBegin("NameCandidate");
        writer.PropertyName("name");
        writer.Write(Name);
        writer.PropertyName("source");
        writer.Write(Source.ToString());
        writer.PropertyName("direction");
        writer.Write(Direction.ToString());
        writer.PropertyName("edgeType");
        writer.Write(EdgeType.ToString());
        writer.PropertyName("refer");
        writer.Write(Refer);
        writer.TypeEnd();
    }
}