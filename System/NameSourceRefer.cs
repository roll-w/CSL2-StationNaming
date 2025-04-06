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
using Game.Net;
using Game.UI;
using Unity.Entities;

namespace StationNaming.System;

public struct NameSourceRefer : IEquatable<NameSourceRefer>,
    ISerializable, IJsonWritable, IJsonReadable
{
    public Entity Refer;
    public NameSource Source;

    public NameSourceRefer(Entity refer, NameSource source)
    {
        Refer = refer;
        Source = source;
    }

    public bool Equals(NameSourceRefer other)
    {
        return Refer.Equals(other.Refer) && Source == other.Source;
    }

    public override bool Equals(object obj)
    {
        return obj is NameSourceRefer other && Equals(other);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            return (Refer.GetHashCode() * 397) ^ (int)Source;
        }
    }

    public void Read(IJsonReader reader)
    {
        reader.ReadMapBegin();
        reader.ReadProperty("refer");
        reader.Read(out Refer);
        reader.ReadProperty("source");
        reader.Read(out string source);
        Source = Enum.TryParse<NameSource>(source, out var nameSource)
            ? nameSource
            : NameSource.None;
        reader.ReadMapEnd();
    }

    public void Write(IJsonWriter writer)
    {
        writer.TypeBegin("NameSourceRefer");
        writer.PropertyName("refer");
        writer.Write(Refer);
        writer.PropertyName("source");
        writer.Write(Source.ToString());
        writer.TypeEnd();
    }

    public override string ToString()
    {
        return $"NameSourceRefer({Refer}, {Source})";
    }

    public static bool operator ==(NameSourceRefer left, NameSourceRefer right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(NameSourceRefer left, NameSourceRefer right)
    {
        return !left.Equals(right);
    }

    public static NameSourceRefer Invalid => new(
        Entity.Null,
        NameSource.None
    );

    public static NameSourceRefer From(Entity entity, NameSource type)
    {
        return new NameSourceRefer(entity, type);
    }

    public void Serialize<TWriter>(TWriter writer) where TWriter : IWriter
    {
        writer.Write(Refer);
        writer.Write(Source.ToString());
    }

    public void Deserialize<TReader>(TReader reader) where TReader : IReader
    {
        reader.Read(out Refer);
        reader.Read(out string source);

        Source = Enum.TryParse<NameSource>(source, out var nameSource)
            ? nameSource
            : NameSource.None;
    }

    public string GetName(
        EntityManager entityManager,
        NameSystem nameSystem)
    {
        if (!entityManager.HasComponent<Aggregated>(Refer))
        {
            return nameSystem.TryGetRealRenderedName(Refer);
        }
        var aggregate = entityManager.GetComponentData<Aggregated>(Refer).m_Aggregate;
        if (aggregate == Entity.Null)
        {
            return string.Empty;
        }
        return nameSystem.GetRenderedLabelName(aggregate);
    }
}