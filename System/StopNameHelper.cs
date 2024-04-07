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
using System.Linq;
using Game.Buildings;
using Game.Common;
using Game.Net;
using Game.Objects;
using Game.UI;
using StationNaming.Setting;
using Unity.Entities;

namespace StationNaming.System;

public class StopNameHelper(
    EntityManager entityManager,
    NameSystem nameSystem
)
{
    private readonly StationNamingSettings _settings = Mod.GetInstance().GetSettings();

    public IEnumerable<NameCandidate> SetCandidatesForStop(Entity stop, int length = 2)
    {
        var hasAttached = entityManager.HasComponent<Attached>(stop);
        var hasOwner = entityManager.HasComponent<Owner>(stop);

        // if the stop has an owner, it could be a inner stop
        if (hasOwner)
        {
            var owner = entityManager.GetComponentData<Owner>(stop);
            if (entityManager.HasComponent<Building>(owner.m_Owner))
            {
                return AddCandidatesIfBuildingStop(stop, owner, length);
            }
        }

        if (!hasAttached)
        {
            return [];
        }

        var attached = entityManager.GetComponentData<Attached>(stop);
        if (attached.m_Parent == Entity.Null || attached.m_Parent == default)
        {
            return [];
        }

        return entityManager.HasComponent<Edge>(attached.m_Parent)
            ? AddCandidatesIfRoadStop(stop, attached, length)
            : [];
    }

    public IEnumerable<NameCandidate> SetCandidatesForStation(
        Entity station, int length = 2, bool includeSelf = false)
    {
        if (!entityManager.HasComponent<Building>(station))
        {
            return [];
        }

        var building = entityManager.GetComponentData<Building>(station);
        if (building.m_RoadEdge == Entity.Null || building.m_RoadEdge == default)
        {
            return [];
        }

        var edge = building.m_RoadEdge;
        var candidates = SetCandidatesIfRoad(station, edge, length);
        var copy = new HashSet<NameCandidate>(candidates);
        if (!includeSelf)
        {
           copy.RemoveWhere(it => it.Refer == station);
        }
        return copy;
    }

    private IEnumerable<NameCandidate> AddCandidatesIfBuildingStop(
        Entity stop, Owner owner, int length)
    {
        // TODO
        var entity = owner.m_Owner;
        if (!entityManager.HasComponent<Building>(entity))
        {
            return [];
        }

        var candidates = SetCandidatesForStation(
            entity, length, includeSelf: false
        );
        var buildingName = nameSystem.GetRenderedLabelName(entity);
        var nameCandidate = new NameCandidate(
            _settings.FormatCandidateName(buildingName),
            entity, NameSource.Owner,
            Direction.Init, EdgeType.Same
        );
        var copy = new HashSet<NameCandidate>(candidates)
        {
            nameCandidate
        };
        SetCandidates(copy, stop);
        return copy;
    }


    private IEnumerable<NameCandidate> AddCandidatesIfRoadStop(Entity stop,
        Attached attached, int length)
    {
        return SetCandidatesIfRoad(stop, attached.m_Parent, length);
    }

    private IEnumerable<NameCandidate> SetCandidatesIfRoad(
        Entity target, Entity edge, int length)
    {
        HashSet<NameCandidate> nameCandidates = [];

        var collectEdges = EdgeUtils.CollectEdges(
            entityManager, edge, length);

        var aggregated = entityManager.GetComponentData<Aggregated>(edge);
        var currentRoadName = nameSystem.GetRenderedLabelName(
            aggregated.m_Aggregate
        );

        nameCandidates.Add(new NameCandidate(
            _settings.FormatCandidateName(currentRoadName),
            edge,
            NameSource.Road,
            Direction.Init,
            EdgeType.Same
        ));

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

        SetCandidates(nameCandidates, target);
        return nameCandidates;
    }

    private void AddRoadCandidate(
        RoadEdge roadEdge,
        string currentRoadName,
        ICollection<NameCandidate> nameCandidates)
    {
        var roadName = _settings
            .FormatRoadName(currentRoadName, GetEdgeName(roadEdge.Edge));
        var name = _settings.FormatCandidateName(roadName);

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
            var name = _settings.FormatCandidateName(buildingName);

            candidates.Add(new NameCandidate(
                name,
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
        foreach (var nameCandidate in SortBySource(candidates))
        {
            nameCandidates.Add(nameCandidate);
        }
    }

    private static IEnumerable<NameCandidate> SortBySource(
        IEnumerable<NameCandidate> candidates
    )
    {
        return candidates.OrderBy(candidate => candidate.Source);
    }
}