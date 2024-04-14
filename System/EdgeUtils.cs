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
using Game.Net;
using Game.UI;
using Unity.Entities;

namespace StationNaming.System;

public static class EdgeUtils
{
    public static Entity GetRootEntityForEdge(Entity entity, EntityManager entityManager)
    {
        if (entityManager.HasComponent<Aggregated>(entity))
        {
            return entityManager.GetComponentData<Aggregated>(entity).m_Aggregate;
        }

        return entity;
    }

    public static IEnumerable<RoadEdge> CollectEdges(
        EntityManager entityManager,
        Entity edgeEntity, int depth)
    {
        HashSet<RoadEdge> edges =
        [
            new RoadEdge(Direction.Init, EdgeType.Same, edgeEntity)
        ];
        var aggregated = entityManager.GetComponentData<Aggregated>(edgeEntity);
        CollectEdges(
            entityManager, edgeEntity, depth,
            aggregated.m_Aggregate,
            Direction.Init,
            edges
        );
        return edges;
    }

    private static void CollectEdges(
        EntityManager entityManager,
        Entity edgeEntity, int depth,
        Entity aggregate, // to identify whether the edge belongs to the same road
        Direction direction,
        ISet<RoadEdge> edges)
    {
        if (depth <= 0)
        {
            return;
        }

        var aggregated = entityManager.GetComponentData<Aggregated>(edgeEntity);
        if (aggregated.m_Aggregate != aggregate)
        {
            return;
        }

        var edge = entityManager.GetComponentData<Edge>(edgeEntity);

        if (direction is Direction.Start or Direction.Init)
        {
            GetNextEdge(entityManager, edgeEntity, depth, aggregate,
                edge.m_Start, Direction.Start, edges);
        }

        if (direction is Direction.End or Direction.Init)
        {
            GetNextEdge(entityManager, edgeEntity, depth, aggregate,
                edge.m_End, Direction.End, edges);
        }
    }

    private static void GetNextEdge(
        EntityManager entityManager,
        Entity edgeEntity,
        int depth,
        Entity aggregate,
        Entity node, Direction direction,
        ISet<RoadEdge> edges)
    {
        if (!entityManager.HasBuffer<ConnectedEdge>(node))
        {
            return;
        }

        var connectedEdges = entityManager.GetBuffer<ConnectedEdge>(node);
        foreach (var connectedEdge in connectedEdges)
        {
            if (connectedEdge.m_Edge == edgeEntity)
            {
                continue;
            }

            var edgeAggregated =
                entityManager.GetComponentData<Aggregated>(connectedEdge.m_Edge);
            if (edgeAggregated.m_Aggregate != aggregate)
            {
                edges.Add(new RoadEdge(direction, EdgeType.Other, connectedEdge.m_Edge));
                continue;
            }

            if (edges.Add(new RoadEdge(direction, EdgeType.Same, connectedEdge.m_Edge)))
            {
                CollectEdges(
                    entityManager, connectedEdge.m_Edge, depth - 1,
                    aggregate, direction, edges
                );
            }
        }
    }
}

public readonly struct RoadEdge(
    Direction direction,
    EdgeType edgeType,
    Entity edge
) : IEquatable<RoadEdge>
{
    public readonly Direction Direction = direction;
    public readonly EdgeType EdgeType = edgeType;
    public readonly Entity Edge = edge;

    public string GetRoadName(
        EntityManager entityManager,
        NameSystem nameSystem)
    {
        if (!entityManager.HasComponent<Aggregated>(Edge))
        {
            return string.Empty;
        }

        var aggregate = entityManager.GetComponentData<Aggregated>(Edge).m_Aggregate;
        return nameSystem.GetRenderedLabelName(aggregate);
    }

    public bool Equals(RoadEdge other)
    {
        return Direction == other.Direction && Edge.Equals(other.Edge) && EdgeType == other.EdgeType;
    }

    public override bool Equals(object obj)
    {
        return obj is RoadEdge other && Equals(other);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            return ((int)Direction * 397) ^ Edge.GetHashCode() ^ ((int)EdgeType * 397);
        }
    }
}

public enum Direction : uint
{
    Init,
    Start,
    End
}

public enum EdgeType : uint
{
    Same,
    Other
}