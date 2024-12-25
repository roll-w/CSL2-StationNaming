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
using Game.Routes;
using Unity.Entities;

namespace StationNaming.System;

public static class EdgeUtils
{
    public static IEnumerable<RoadEdge> CollectIntersections(
        Entity edge,
        EntityManager entityManager,
        int depth)
    {
        if (depth <= 0)
        {
            return [];
        }

        var edgeComponent = entityManager.GetComponentData<Edge>(edge);
        var root = GetRootEntityForEdge(edge, entityManager);
        List<RoadEdge> result = [];

        SearchForIntersections(edgeComponent.m_Start, root, entityManager, depth, Direction.Start, result);
        SearchForIntersections(edgeComponent.m_End, root, entityManager, depth, Direction.End, result);

        return result;
    }

    private static void SearchForIntersections(
        Entity edge, Entity root,
        EntityManager entityManager,
        int depth, Direction direction,
        List<RoadEdge> intersections)
    {
        if (depth <= 0 || edge == Entity.Null)
        {
            return;
        }

        if (entityManager.HasComponent<Edge>(edge))
        {
            var edgeRoot = GetRootEntityForEdge(edge, entityManager);
            if (edgeRoot != root)
            {
                return;
            }

            var edgeComponent = entityManager.GetComponentData<Edge>(edge);
            var nextEdge = direction switch
            {
                Direction.End => edgeComponent.m_End,
                Direction.Start => edgeComponent.m_Start,
                _ => Entity.Null
            };
            SearchForIntersections(nextEdge, root, entityManager, depth - 1, direction, intersections);
            return;
        }

        if (!entityManager.HasBuffer<ConnectedEdge>(edge))
        {
            return;
        }

        var anyRoad = false;
        var allSelf = true;
        var connectedEdges = entityManager.GetBuffer<ConnectedEdge>(edge);

        foreach (var connectedEdge in connectedEdges)
        {
            if (!entityManager.HasComponent<Road>(connectedEdge.m_Edge))
            {
                continue;
            }
            if (allSelf)
            {
                var edgeRoot = GetRootEntityForEdge(connectedEdge.m_Edge, entityManager);
                if (edgeRoot != root)
                {
                    anyRoad = true;
                    allSelf = false;
                }
            }
            SearchForIntersections(connectedEdge.m_Edge, root, entityManager, depth - 1, direction, intersections);
        }

        if (!anyRoad)
        {
            return;
        }
        RoadEdge roadEdge = new(direction, EdgeType.Same, edge, depth);
        intersections.Add(roadEdge);
    }

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
        List<RoadEdge> edges =
        [
            new(Direction.Init, EdgeType.Same, edgeEntity)
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
        List<RoadEdge> edges)
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
        List<RoadEdge> edges)
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

            if (!entityManager.HasComponent<Road>(connectedEdge.m_Edge))
            {
                continue;
            }

            var edgeAggregated = GetRootEntityForEdge(connectedEdge.m_Edge, entityManager);
            if (edgeAggregated != aggregate)
            {
                edges.Add(new RoadEdge(direction, EdgeType.Other, connectedEdge.m_Edge, depth));
                continue;
            }

            var newEdge = new RoadEdge(direction, EdgeType.Same, connectedEdge.m_Edge, depth);
            if (edges.Contains(newEdge))
            {
                continue;
            }
            edges.Add(newEdge);
            CollectEdges(
                entityManager, connectedEdge.m_Edge, depth - 1,
                aggregate, direction, edges
            );
        }
    }
}

public readonly struct RoadEdge(
    Direction direction,
    EdgeType edgeType,
    Entity edge,
    int depth = 1
) : IEquatable<RoadEdge>
{
    public readonly Direction Direction = direction;
    public readonly EdgeType EdgeType = edgeType;
    public readonly Entity Edge = edge;
    public readonly int Depth = depth;

    public bool Equals(RoadEdge other)
    {
        return Direction == other.Direction && Edge.Equals(other.Edge) &&
               EdgeType == other.EdgeType && Depth == other.Depth;
    }

    public override bool Equals(object obj)
    {
        return obj is RoadEdge other && Equals(other);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            return ((int)Direction * 397)
                   ^ Edge.GetHashCode()
                   ^ ((int)EdgeType * 397)
                   ^ Depth;
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