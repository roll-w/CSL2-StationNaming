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
using System.Collections.Generic;
using System.Linq;
using Colossal.Serialization.Entities;
using Colossal.UI.Binding;
using Unity.Collections;
using Unity.Entities;

namespace StationNaming.System;

[InternalBufferCapacity(0)]
public struct NameCandidate : IBufferElementData,
    ISerializable, IEquatable<NameCandidate>, IJsonWritable, IJsonReadable
{
    public FixedString512Bytes Name;
    public FixedList128Bytes<NameSourceRefer> Refers;
    public Direction Direction;
    public EdgeType EdgeType;

    public NameCandidate(
        string name, FixedList128Bytes<NameSourceRefer> refers,
        Direction direction, EdgeType edgeType)
    {
        switch (refers.Length)
        {
            case 0:
                throw new ArgumentException("NameCandidate must have at least 1 refer");
            case > 2:
                // currently we only support 2 refers max
                throw new ArgumentException("NameCandidate can only have 2 refers");
        }

        Name = name;
        Refers = refers;
        Direction = direction;
        EdgeType = edgeType;
    }

    public bool Equals(NameCandidate other)
    {
        if (!Name.Equals(other.Name))
        {
            return false;
        }

        if (Refers.Length != other.Refers.Length)
        {
            return false;
        }

        for (var i = 0; i < Refers.Length; i++)
        {
            if (Refers[i].Source != other.Refers[i].Source)
            {
                return false;
            }
        }

        return true;
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
            foreach (var refer in Refers)
            {
                hashCode = (hashCode * 397) ^ refer.Source.GetHashCode();
            }

            return hashCode;
        }
    }

    public List<NameSourceRefer> RefersToList()
    {
        List<NameSourceRefer> refers = [];

        foreach (var refer in Refers)
        {
            refers.Add(refer);
        }

        return refers;
    }

    public void Read(IJsonReader reader)
    {
        reader.ReadMapBegin();
        reader.ReadProperty("name");
        reader.Read(out string name);
        Name = name;

        var size = reader.ReadArrayBegin();
        Refers = [];
        for (uint i = 0; i < size; i++)
        {
            reader.ReadArrayElement(i);
            reader.ReadMapBegin();
            reader.ReadProperty("refer");
            reader.Read(out Entity refer);
            reader.ReadProperty("source");
            reader.Read(out string source);
            var nameSource = Enum.TryParse<NameSource>(source, out var src)
                ? src
                : NameSource.None;
            Refers.Add(new NameSourceRefer(refer, nameSource));
        }

        reader.ReadArrayEnd();
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
        writer.PropertyName("refers");
        writer.ArrayBegin(Refers.Length);
        foreach (var refer in Refers)
        {
            writer.Write(refer);
        }

        writer.PropertyName("direction");
        writer.Write(Direction.ToString());
        writer.PropertyName("edgeType");
        writer.Write(EdgeType.ToString());
        writer.ArrayEnd();
        writer.TypeEnd();
    }

    public override string ToString()
    {
        return $"Candidate['{Name}'({Refers}-{Direction})]";
    }

    public static implicit operator ManagedNameCandidate(NameCandidate candidate)
    {
        return new ManagedNameCandidate(candidate);
    }

    public void Serialize<TWriter>(TWriter writer) where TWriter : IWriter
    {
        writer.Write(Name.ToString());
        writer.Write(Refers.ToNativeArray(Allocator.Temp));
        writer.Write(Direction.ToString());
        writer.Write(EdgeType.ToString());
    }

    public void Deserialize<TReader>(TReader reader) where TReader : IReader
    {
        reader.Read(out string name);
        Name = name;

        reader.Read(out string direction);
        Direction = Enum.TryParse<Direction>(direction, out var dir)
            ? dir
            : Direction.Init;

        reader.Read(out string edgeType);
        EdgeType = Enum.TryParse<EdgeType>(edgeType, out var edge)
            ? edge
            : EdgeType.Same;
    }

    public static NameCandidate Of(
        string name, Direction direction, EdgeType edgeType,
        Entity refer, NameSource source)
    {
        FixedList128Bytes<NameSourceRefer> refers =
        [
            new NameSourceRefer(refer, source)
        ];

        return new NameCandidate(
            name, refers, direction, edgeType
        );
    }


    public static NameCandidate Of(
        string name,
        Direction direction, EdgeType edgeType,
        Entity refer1, NameSource source1,
        Entity refer2, NameSource source2
    )
    {
        FixedList128Bytes<NameSourceRefer> refers =
        [
            new NameSourceRefer(refer1, source1),
            new NameSourceRefer(refer2, source2)
        ];

        return new NameCandidate(
            name, refers, direction, edgeType
        );
    }

    public static NameCandidate Of(
        string name, Direction direction, EdgeType edgeType,
        params KeyValuePair<Entity, NameSource>[] refer)
    {
        var refers = new FixedList128Bytes<NameSourceRefer>();
        foreach (var pair in refer)
        {
            refers.Add(new NameSourceRefer(pair.Key, pair.Value));
        }

        return new NameCandidate(
            name, refers, direction, edgeType
        );
    }
}

public struct ManagedNameCandidate(
    string name,
    List<NameSourceRefer> refers,
    Direction direction,
    EdgeType edgeType
)
    : IEquatable<ManagedNameCandidate>, IJsonWritable, IJsonReadable
{
    public string Name = name;
    public List<NameSourceRefer> Refers = refers;
    public Direction Direction = direction;
    public EdgeType EdgeType = edgeType;

    public ManagedNameCandidate(NameCandidate candidate) : this(
        candidate.Name.ToString(),
        candidate.RefersToList(),
        candidate.Direction,
        candidate.EdgeType
    )
    {
    }

    public bool Equals(ManagedNameCandidate other)
    {
        return Name.Equals(other.Name) && Refers.SequenceEqual(other.Refers);
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
            hashCode = (hashCode * 397) ^ Refers.GetHashCode();
            return hashCode;
        }
    }

    public override string ToString()
    {
        return $"Candidate['{Name}'({Refers}-{Direction})]";
    }

    public void Read(IJsonReader reader)
    {
        reader.ReadMapBegin();
        reader.ReadProperty("name");
        reader.Read(out string name);
        Name = name;

        reader.ReadProperty("refers");
        var size = reader.ReadArrayBegin();
        Refers = [];

        for (uint i = 0; i < size; i++)
        {
            reader.ReadArrayElement(i);
            reader.ReadMapBegin();
            reader.ReadProperty("refer");
            reader.Read(out Entity refer);
            reader.ReadProperty("source");
            reader.Read(out string source);
            var nameSource = Enum.TryParse<NameSource>(source, out var src)
                ? src
                : NameSource.None;
            Refers.Add(new NameSourceRefer(refer, nameSource));
            reader.ReadMapEnd();
        }
        reader.ReadArrayEnd();
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
        writer.PropertyName("refers");
        writer.ArrayBegin((uint)Refers.Count);
        foreach (var refer in Refers)
        {
            writer.Write(refer);
        }
        writer.ArrayEnd();
        writer.PropertyName("direction");
        writer.Write(Direction.ToString());
        writer.PropertyName("edgeType");
        writer.Write(EdgeType.ToString());
        writer.TypeEnd();
    }

    public NameCandidate ToUnmanaged()
    {
        FixedList128Bytes<NameSourceRefer> refers = [];
        foreach (var refer in Refers)
        {
            refers.Add(refer);
        }
        return new NameCandidate(
            Name, refers, Direction, EdgeType
        );
    }
}