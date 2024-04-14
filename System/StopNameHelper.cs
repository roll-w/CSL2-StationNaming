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
using Unity.Collections;
using Unity.Entities;

namespace StationNaming.System;

public class StopNameHelper(
    EntityManager entityManager,
    NameSystem nameSystem
)
{
    public bool BuildingName { get; set; } = true;
    public bool BuildingNameWithCurrentRoad { get; set; } = true;

    private readonly StationNamingSettings _settings = Mod.GetInstance().GetSettings();

    public void ApplyTo(NameOptions nameOptions)
    {
        BuildingName = nameOptions.BuildingName;
        BuildingNameWithCurrentRoad = nameOptions.BuildingNameWithCurrentRoad;
    }

    public IEnumerable<NameCandidate> SetCandidatesForStop(Entity stop, int length = 2)
    {
        var hasAttached = entityManager.HasComponent<Attached>(stop);
        var hasOwner = entityManager.HasComponent<Owner>(stop);

        // if the stop has an owner, it could be a inner stop
        if (hasOwner)
        {
            var owner = RetrieveOwner(entityManager, stop);
            if (entityManager.HasComponent<Building>(owner))
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
        return SetCandidatesIfRoad(station, edge, length, includeSelf);
    }

    private IEnumerable<NameCandidate> AddCandidatesIfBuildingStop(
        Entity stop, Entity owner, int length)
    {
        // get the real owner, not the extension
        if (!entityManager.HasComponent<Building>(owner))
        {
            return [];
        }

        var candidates = SetCandidatesForStation(
            owner, length, includeSelf: false
        );
        var buildingName = nameSystem.GetRenderedLabelName(owner);
        var copy = new HashSet<NameCandidate>(candidates)
        {
            NameCandidate.Of(
                buildingName, Direction.Init, EdgeType.Same,
                owner, NameSource.Owner
            )
        };
        SetCandidates(copy, stop);
        return copy;
    }

    public static Entity RetrieveOwner(EntityManager entityManager, Entity entity)
    {
        if (!entityManager.HasComponent<Owner>(entity))
        {
            return entity;
        }

        var owner = entityManager.GetComponentData<Owner>(entity);
        return RetrieveOwner(entityManager, owner.m_Owner);
    }

    private IEnumerable<NameCandidate> AddCandidatesIfRoadStop(Entity stop,
        Attached attached, int length)
    {
        return SetCandidatesIfRoad(stop, attached.m_Parent, length);
    }

    private IEnumerable<NameCandidate> SetCandidatesIfRoad(
        Entity target, Entity edge, int length, bool includeSelf = false)
    {
        HashSet<NameCandidate> nameCandidates = [];

        var collectEdges = EdgeUtils.CollectEdges(
            entityManager, edge, length);

        var root = EdgeUtils.GetRootEntityForEdge(edge, entityManager);
        var currentRoad = new RoadEdge(Direction.Init, EdgeType.Same, edge);

        var currentRoadName = currentRoad.GetRoadName(entityManager, nameSystem);

        nameCandidates.Add(NameCandidate.Of(
            _settings.FormatCandidateName(currentRoadName),
            Direction.Init, EdgeType.Same,
            root,
            NameSource.Road)
        );

        bool hasStart = false, hasEnd = false;
        foreach (var roadEdge in collectEdges)
        {
            if (BuildingName)
            {
                CollectBuildingNames(
                    target,
                    currentRoad, roadEdge,
                    nameCandidates, includeSelf
                );
            }

            if (roadEdge.EdgeType != EdgeType.Other)
            {
                continue;
            }

            switch (roadEdge.Direction)
            {
                case Direction.Start when !hasStart:
                    hasStart = true;
                    AddRoadCandidate(roadEdge, currentRoadName, root, nameCandidates);
                    break;
                case Direction.End when !hasEnd:
                    hasEnd = true;
                    AddRoadCandidate(roadEdge, currentRoadName, root, nameCandidates);
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
        Entity currentRoadRoot,
        ICollection<NameCandidate> nameCandidates)
    {
        var roadName = _settings.FormatRoadName(
            currentRoadName,
            roadEdge.GetRoadName(entityManager, nameSystem)
        );
        var name = _settings.FormatCandidateName(roadName);
        var root = EdgeUtils.GetRootEntityForEdge(roadEdge.Edge, entityManager);

        nameCandidates.Add(NameCandidate.Of(
            name,
            roadEdge.Direction,
            roadEdge.EdgeType,
            currentRoadRoot, NameSource.Road,
            root, NameSource.Road
        ));
    }

    private void CollectBuildingNames(
        Entity self,
        RoadEdge currentRoad,
        RoadEdge roadEdge,
        ICollection<NameCandidate> candidates,
        bool includeSelf)
    {
        if (!entityManager.HasBuffer<ConnectedBuilding>(roadEdge.Edge))
        {
            return;
        }

        var connectedBuildings =
            entityManager.GetBuffer<ConnectedBuilding>(roadEdge.Edge);
        foreach (var connectedBuilding in connectedBuildings)
        {
            GenerateBuildingSourceCandidates(
                self, currentRoad,
                roadEdge, candidates,
                includeSelf, connectedBuilding
            );
        }
    }

    private void GenerateBuildingSourceCandidates(
        Entity self,
        RoadEdge currentRoad,
        RoadEdge roadEdge,
        ICollection<NameCandidate> candidates,
        bool includeSelf,
        ConnectedBuilding connectedBuilding)
    {
        if (connectedBuilding.m_Building == self && !includeSelf)
        {
            return;
        }

        var source = NameUtils.TryGetBuildingSource(
            connectedBuilding.m_Building,
            entityManager
        );
        var buildingName = nameSystem.GetRenderedLabelName(
            connectedBuilding.m_Building
        );
        var name = _settings.FormatCandidateName(buildingName);

        candidates.Add(NameCandidate.Of(
            name, roadEdge.Direction, roadEdge.EdgeType,
            connectedBuilding.m_Building,
            source
        ));

        if (!BuildingNameWithCurrentRoad)
        {
            return;
        }

        var roadName = currentRoad.GetRoadName(entityManager, nameSystem);
        name = _settings.FormatRoadName(roadName, buildingName);

        var roadEdgeRoot = EdgeUtils.GetRootEntityForEdge(roadEdge.Edge, entityManager);

        candidates.Add(
            NameCandidate.Of(
                name, roadEdge.Direction, roadEdge.EdgeType,
                roadEdgeRoot, NameSource.Road,
                connectedBuilding.m_Building, source
            )
        );
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
        return candidates.OrderBy(candidate => candidate.Refers[0].Source);
    }

    public static string FormatRefers(
        IList<NameSourceRefer> refers,
        EntityManager entityManager,
        NameSystem nameSystem
    )
    {
        var count = refers.Count;
        var settings = Mod.GetInstance().GetSettings();
        switch (count)
        {
            case 0:
                return "";
            case 1:
            {
                var nameSourceRefer = refers[0];
                if (nameSourceRefer.Source == NameSource.Owner)
                {
                    return nameSourceRefer.GetName(entityManager, nameSystem);
                }

                return settings.FormatCandidateName(
                    nameSourceRefer.GetName(entityManager, nameSystem)
                );
            }
            case 2:
            {
                var first = refers[0];
                var second = refers[1];
                var formatRoadName = settings.FormatRoadName(
                    first.GetName(entityManager, nameSystem),
                    second.GetName(entityManager, nameSystem)
                );
                return settings.FormatCandidateName(formatRoadName);
            }
        }

        return "Unsupported";
    }


    public static string FormatRefers(
        INativeList<NameSourceRefer> refers,
        EntityManager entityManager,
        NameSystem nameSystem
    )
    {
        List<NameSourceRefer> copy = [];
        for (var i = 0; i < refers.Length; i++)
        {
            copy.Add(refers[i]);
        }

        return FormatRefers(copy, entityManager, nameSystem);
    }
}