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
using Game.Buildings;
using Game.Net;
using Game.Objects;
using Game.UI;
using Unity.Entities;

namespace StationNaming.System;

public class StopNameHelper(
    EntityManager entityManager,
    NameSystem nameSystem
)
{
    public IEnumerable<NameCandidate> SetCandidatesForStop(Entity stop)
    {
        if (!entityManager.HasComponent<Attached>(stop))
        {
            return [];
        }

        var attached = entityManager.GetComponentData<Attached>(stop);
        if (attached.m_Parent == Entity.Null || attached.m_Parent == default)
        {
            return [];
        }

        return entityManager.HasComponent<Edge>(attached.m_Parent)
            ? AddCandidatesIfRoadStop(stop)
            : AddCandidatesIfBuildingStop(stop);
    }


    private IEnumerable<NameCandidate> AddCandidatesIfBuildingStop(Entity stop)
    {
        var attached = entityManager.GetComponentData<Attached>(stop);
        // TODO: Check if the attached entity is a building
        return [];
    }


    private IEnumerable<NameCandidate> AddCandidatesIfRoadStop(Entity stop)
    {
        var attached = entityManager.GetComponentData<Attached>(stop);
        if (attached.m_Parent == Entity.Null ||
            !entityManager.HasComponent<Edge>(attached.m_Parent))
        {
            return [];
        }

        HashSet<NameCandidate> nameCandidates = [];

        var collectEdges = EdgeUtils.CollectEdges(
            entityManager, attached.m_Parent, 1);

        var aggregated = entityManager.GetComponentData<Aggregated>(attached.m_Parent);
        var currentRoadName = nameSystem.GetRenderedLabelName(
            aggregated.m_Aggregate
        );

        bool hasStart = false, hasEnd = false;
        foreach (var roadEdge in collectEdges)
        {
            CollectBuildingNames(roadEdge, nameCandidates);
            if (roadEdge.EdgeType != EdgeType.Other)
            {
                continue;
            }

            switch (roadEdge.Direction)
            {
                case Direction.Start when !hasStart:
                    hasStart = true;
                    AddRoadCandidate(roadEdge, currentRoadName, nameCandidates);
                    break;
                case Direction.End when !hasEnd:
                    hasEnd = true;
                    AddRoadCandidate(roadEdge, currentRoadName, nameCandidates);
                    break;
                case Direction.Init:
                default:
                    continue;
            }
        }

        SetCandidates(nameCandidates, stop);
        return nameCandidates;
    }


    private void AddRoadCandidate(
        RoadEdge roadEdge,
        string currentRoadName,
        ICollection<NameCandidate> nameCandidates)
    {
        var name = Mod.GetInstance().GetSettings()
            .FormatRoadName(currentRoadName, GetEdgeName(roadEdge.Edge));

        nameCandidates.Add(new NameCandidate(
            name,
            roadEdge.Edge,
            NameSource.Road,
            roadEdge.Direction,
            roadEdge.EdgeType
        ));
    }

    private string GetEdgeName(Entity edge)
    {
        if (!entityManager.HasComponent<Aggregated>(edge))
        {
            return string.Empty;
        }

        var aggregated = entityManager.GetComponentData<Aggregated>(edge);
        return nameSystem.GetRenderedLabelName(aggregated.m_Aggregate);
    }

    private void CollectBuildingNames(
        RoadEdge roadEdge,
        ICollection<NameCandidate> candidates)
    {
        if (!entityManager.HasBuffer<ConnectedBuilding>(roadEdge.Edge))
        {
            return;
        }

        var connectedBuildings =
            entityManager.GetBuffer<ConnectedBuilding>(roadEdge.Edge);
        foreach (var connectedBuilding in connectedBuildings)
        {
            var source = NameUtils.TryGetBuildingSource(
                connectedBuilding.m_Building,
                entityManager
            );
            var buildingName = nameSystem.GetRenderedLabelName(
                connectedBuilding.m_Building
            );

            candidates.Add(new NameCandidate(
                buildingName,
                connectedBuilding.m_Building,
                source,
                roadEdge.Direction, roadEdge.EdgeType
            ));
        }
    }

    private void SetCandidates(
        IEnumerable<NameCandidate> candidates,
        Entity target
    )
    {
        var nameCandidates = entityManager.AddBuffer<NameCandidate>(target);
        nameCandidates.Clear();
        foreach (var nameCandidate in candidates)
        {
            nameCandidates.Add(nameCandidate);
        }
    }
}