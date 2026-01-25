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
using Unity.Entities;

namespace StationNaming.System
{
    public static class EdgeUtils
    {
        public static IEnumerable<RoadEdge> CollectIntersections(
            Entity edge,
            EntityManager entityManager,
            int depth)
        {
            if (depth <= 0)
            {
                return new List<RoadEdge>();
            }

            var edgeComponent = entityManager.GetComponentData<Edge>(edge);
            var root = GetRootEntityForEdge(edge, entityManager);
            var result = new List<RoadEdge>();
            var seen = new HashSet<RoadEdge>();

            SearchForIntersections(edgeComponent.m_Start, root, entityManager, depth, Direction.Start, result, seen);
            SearchForIntersections(edgeComponent.m_End, root, entityManager, depth, Direction.End, result, seen);

            return result;
        }

        private static void SearchForIntersections(
            Entity edge, Entity root,
            EntityManager entityManager,
            int depth, Direction direction,
            List<RoadEdge> intersections,
            HashSet<RoadEdge> seen)
        {
            if (depth < 0 || edge == Entity.Null)
            {
                return;
            }

            var queue = new List<(Entity entity, int left, Entity cameFrom, bool isEdge)>();
            var queueIndex = 0;
            var visitedNodes = new HashSet<Entity>();
            var visitedEdges = new HashSet<Entity>();
            var rootCache = new Dictionary<Entity, Entity>();

            // start from the given entity (usually a node)
            queue.Add(new ValueTuple<Entity, int, Entity, bool>(edge, depth, Entity.Null,
                entityManager.HasComponent<Edge>(edge)));
            visitedNodes.Add(edge);

            while (queueIndex < queue.Count)
            {
                var (cur, left, cameFrom, isEdge) = queue[queueIndex++];
                if (left < 0 || cur == Entity.Null)
                {
                    continue;
                }

                if (isEdge)
                {
                    if (!entityManager.HasComponent<Edge>(cur))
                    {
                        continue;
                    }

                    if (!rootCache.TryGetValue(cur, out var curRoot))
                    {
                        curRoot = GetRootEntityForEdge(cur, entityManager);
                        rootCache[cur] = curRoot;
                    }

                    if (curRoot != root)
                    {
                        continue;
                    }

                    var eComp = entityManager.GetComponentData<Edge>(cur);
                    Entity nextNode;
                    if (cameFrom != Entity.Null)
                    {
                        nextNode = eComp.m_Start == cameFrom ? eComp.m_End : eComp.m_Start;
                    }
                    else
                    {
                        nextNode = direction == Direction.Start ? eComp.m_End : eComp.m_Start;
                    }

                    if (nextNode == Entity.Null)
                    {
                        continue;
                    }

                    if (visitedNodes.Add(nextNode))
                    {
                        queue.Add(new ValueTuple<Entity, int, Entity, bool>(nextNode, left - 1, cur, false));
                    }

                    continue;
                }

                // cur is a node: inspect connected edges
                if (!entityManager.HasBuffer<ConnectedEdge>(cur))
                {
                    continue;
                }

                var connectedEdges = entityManager.GetBuffer<ConnectedEdge>(cur);
                var anyRoad = false;
                var allSelf = true;
                var sameEdges = new List<Entity>();

                foreach (var connectedEdge in connectedEdges)
                {
                    var ce = connectedEdge.m_Edge;
                    if (!entityManager.HasComponent<Road>(ce))
                    {
                        continue;
                    }

                    anyRoad = true;

                    if (!rootCache.TryGetValue(ce, out var ceRoot))
                    {
                        ceRoot = GetRootEntityForEdge(ce, entityManager);
                        rootCache[ce] = ceRoot;
                    }

                    if (ceRoot != root)
                    {
                        allSelf = false;
                    }
                    else
                    {
                        if (ce != cameFrom && !sameEdges.Contains(ce))
                        {
                            sameEdges.Add(ce);
                        }
                    }
                }

                if (anyRoad && !allSelf)
                {
                    var roadEdge = new RoadEdge(direction, EdgeType.Same, cur, left);
                    if (seen.Add(roadEdge))
                    {
                        intersections.Add(roadEdge);
                    }

                    continue;
                }

                if (left <= 0)
                {
                    continue;
                }

                foreach (var se in sameEdges)
                {
                    if (visitedEdges.Add(se))
                    {
                        queue.Add(new ValueTuple<Entity, int, Entity, bool>(se, left - 1, cur, true));
                    }
                }
            }
        }

        public static Entity GetRootEntityForEdge(Entity entity, EntityManager entityManager)
        {
            if (!entityManager.HasComponent<Aggregated>(entity))
            {
                return entity;
            }

            return entityManager.GetComponentData<Aggregated>(entity).m_Aggregate;
        }
    }

    public readonly struct RoadEdge : IEquatable<RoadEdge>
    {
        public readonly Direction Direction;
        [Obsolete] public readonly EdgeType EdgeType;
        public readonly Entity Edge;
        public readonly int Depth;

        public RoadEdge(Direction direction, EdgeType edgeType, Entity edge, int depth = 1)
        {
            Direction = direction;
            EdgeType = edgeType;
            Edge = edge;
            Depth = depth;
        }

        public bool Equals(RoadEdge other)
        {
            return Direction == other.Direction && Edge.Equals(other.Edge) &&
                   EdgeType == other.EdgeType;
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
                       ^ ((int)EdgeType * 397);
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
}