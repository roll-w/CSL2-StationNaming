﻿// Copyright (c) 2024 RollW
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
using Game.Areas;
using Game.Buildings;
using Game.Common;
using Game.Net;
using Game.Objects;
using Game.Prefabs;
using Game.UI;
using StationNaming.Setting;
using Unity.Entities;

namespace StationNaming.System;

public class StopNameHelper(
    EntityManager entityManager,
    NameSystem nameSystem,
    PrefabSystem prefabSystem
)
{
    private NameOptions NameOptions { get; set; } = new();

    private readonly NameFormatter _nameFormatter = new(entityManager, nameSystem, prefabSystem);
    private EntityManager _entityManager = entityManager;

    public void ApplyTo(NameOptions nameOptions)
    {
        _nameFormatter.Options = nameOptions;
        NameOptions = nameOptions;
    }

    public IEnumerable<NameCandidate> SetCandidatesForStop(Entity stop, int length = 2)
    {
        var hasAttached = _entityManager.HasComponent<Attached>(stop);
        var hasOwner = _entityManager.HasComponent<Owner>(stop);

        // if the stop has an owner, it could be an inner stop
        if (hasOwner)
        {
            var owner = RetrieveOwner(_entityManager, stop);
            if (_entityManager.HasComponent<Building>(owner))
            {
                return AddCandidatesIfBuildingStop(stop, owner, length);
            }
        }

        if (!hasAttached)
        {
            return [];
        }

        var attached = _entityManager.GetComponentData<Attached>(stop);
        if (attached.m_Parent == Entity.Null || attached.m_Parent == default)
        {
            return [];
        }

        return _entityManager.HasComponent<Edge>(attached.m_Parent)
            ? AddCandidatesIfRoadStop(stop, attached, length)
            : [];
    }

    public IEnumerable<NameCandidate> SetCandidatesForStation(
        Entity station, int length = 2, bool includeSelf = false)
    {
        if (!_entityManager.HasComponent<Building>(station))
        {
            return [];
        }

        var building = _entityManager.GetComponentData<Building>(station);
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
        if (!_entityManager.HasComponent<Building>(owner))
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
            _entityManager, edge, length);

        var root = EdgeUtils.GetRootEntityForEdge(edge, _entityManager);
        var currentRoad = new RoadEdge(Direction.Init, EdgeType.Same, edge);
        var roadRefer = new NameSourceRefer(root, NameSource.Road);

        nameCandidates.Add(NameCandidate.Of(
            string.Empty,
            Direction.Init, EdgeType.Same,
            roadRefer
        ));

        var roadEdges = collectEdges.ToList();

        AddRoadCandidate(Direction.End, roadEdges, nameCandidates, currentRoad);
        AddRoadCandidate(Direction.Start, roadEdges, nameCandidates, currentRoad);

        foreach (var roadEdge in roadEdges)
        {
            if (NameOptions.BuildingName)
            {
                CollectBuildingNames(
                    target,
                    currentRoad, roadEdge,
                    nameCandidates, includeSelf
                );
            }
        }

        SetCandidates(nameCandidates, target);
        return nameCandidates;
    }

    private void AddRoadCandidate(
        Direction direction,
        IEnumerable<RoadEdge> roadEdges,
        ICollection<NameCandidate> nameCandidates,
        RoadEdge currentRoad
    )
    {
        var edges = roadEdges
            .Where(e => e.Direction == direction
                        && e.EdgeType == EdgeType.Other)
            .OrderBy(it => -it.Depth)
            .ToList();
        if (edges.Count == 0)
        {
            return;
        }

        var first = edges.FirstOrDefault();

        AddRoadCandidate(first, currentRoad.Edge, nameCandidates);
    }

    private void AddRoadCandidate(
        RoadEdge roadEdge,
        Entity currentRoadRoot,
        ICollection<NameCandidate> nameCandidates)
    {
        var root = EdgeUtils.GetRootEntityForEdge(roadEdge.Edge, _entityManager);

        List<NameSourceRefer> refers =
        [
            new(currentRoadRoot, NameSource.Road),
            new(root, NameSource.Road)
        ];

        nameCandidates.Add(NameCandidate.Of(
            string.Empty,
            roadEdge.Direction,
            roadEdge.EdgeType,
            refers
        ));

        nameCandidates.Add(NameCandidate.Of(
            string.Empty,
            roadEdge.Direction,
            roadEdge.EdgeType,
            root, NameSource.Intersection
        ));
    }

    private void CollectBuildingNames(
        Entity self,
        RoadEdge currentRoad,
        RoadEdge roadEdge,
        ICollection<NameCandidate> candidates,
        bool includeSelf)
    {
        if (!_entityManager.HasBuffer<ConnectedBuilding>(roadEdge.Edge))
        {
            return;
        }

        var connectedBuildings =
            _entityManager.GetBuffer<ConnectedBuilding>(roadEdge.Edge);
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
        var building = connectedBuilding.m_Building;
        if (building == self && !includeSelf)
        {
            return;
        }

        var source = NameUtils.TryGetBuildingSource(
            building,
            _entityManager
        );

        if (!NameOptions.IsNameSourceEnabled(source))
        {
            return;
        }

        var buildingRefer = new NameSourceRefer(building, source);

        candidates.Add(NameCandidate.Of(
            string.Empty, roadEdge.Direction, roadEdge.EdgeType,
            buildingRefer
        ));

        if (!NameOptions.BuildingNameWithCurrentRoad)
        {
            return;
        }

        var roadEdgeRoot = EdgeUtils.GetRootEntityForEdge(
            currentRoad.Edge, _entityManager
        );

        List<NameSourceRefer> refers =
        [
            new(roadEdgeRoot, NameSource.Road),
            new(building, source)
        ];

        candidates.Add(NameCandidate.Of(
            string.Empty, roadEdge.Direction, roadEdge.EdgeType,
            refers
        ));
    }

    private void SetCandidates(
        IEnumerable<NameCandidate> candidates,
        Entity target
    )
    {
        var nameCandidates = _entityManager.AddBuffer<NameCandidate>(target);
        nameCandidates.Clear();
        foreach (var nameCandidate in SortBySource(candidates))
        {
            var postProcessed = PostProcessNameCandidate(nameCandidate, target);
            nameCandidates.Add(postProcessed);
        }

        if (!NameOptions.EnableDistrict)
        {
            return;
        }

        if (GenerateDistrictNameCandidate(target, out var districtCandidate))
        {
            nameCandidates.Add(districtCandidate);
        }
    }

    private NameCandidate PostProcessNameCandidate(
        NameCandidate candidate,
        Entity target
    )
    {
        if (!NameOptions.EnableDistrictPrefix || candidate.Refers.Length == 0)
        {
            return GenerateNameCandidate(candidate, target);
        }

        var first = candidate.Refers[0];

        if (first.Source == NameSource.Owner)
        {
            return GenerateNameCandidate(candidate, target);
        }

        var districtEntity = GetDistrictEntity(target, _entityManager);
        if (districtEntity == Entity.Null)
        {
            return GenerateNameCandidate(candidate, target);
        }

        var districtRefer = new NameSourceRefer(districtEntity, NameSource.District);
        var refers = candidate.Refers;

        var newRefers = new List<NameSourceRefer>(refers.Length + 1);
        foreach (var r in refers)
        {
            newRefers.Add(r);
        }

        newRefers.Insert(0, districtRefer);

        var name = _nameFormatter.FormatRefers(newRefers, target);

        return NameCandidate.Of(
            name,
            candidate.Direction,
            candidate.EdgeType,
            newRefers
        );
    }

    private bool GenerateDistrictNameCandidate(
        Entity target,
        out NameCandidate candidate
    )
    {
        var districtEntity = GetDistrictEntity(target, _entityManager);
        if (districtEntity == Entity.Null)
        {
            candidate = default;
            return false;
        }

        var districtRefer = new NameSourceRefer(districtEntity, NameSource.District);
        List<NameSourceRefer> refers = [districtRefer];
        var name = _nameFormatter.FormatRefers(refers, target);

        candidate = NameCandidate.Of(
            name,
            Direction.Init,
            EdgeType.Same,
            districtRefer
        );
        return true;
    }

    private NameCandidate GenerateNameCandidate(
        NameCandidate raw,
        Entity target
    )
    {
        if (!raw.Name.IsEmpty)
        {
            return raw;
        }

        var name = _nameFormatter.FormatRefers(
            raw.Refers, target
        );

        return new NameCandidate(
            name,
            raw.Refers,
            raw.Direction,
            raw.EdgeType
        );
    }

    private static IEnumerable<NameCandidate> SortBySource(
        IEnumerable<NameCandidate> candidates
    )
    {
        return candidates.OrderBy(candidate =>
            candidate.Refers.Length == 0
                ? NameSource.None
                : candidate.Refers[candidate.Refers.Length - 1].Source
        );
    }

    private static Entity GetDistrictEntity(
        Entity entity,
        EntityManager entityManager
    )
    {
        if (entityManager.HasComponent<CurrentDistrict>(entity))
        {
            return entityManager.GetComponentData<CurrentDistrict>(entity).m_District;
        }

        if (entityManager.HasComponent<Owner>(entity))
        {
            return GetDistrictEntity(
                RetrieveOwner(entityManager, entity),
                entityManager
            );
        }

        if (!entityManager.HasComponent<Attached>(entity))
        {
            return Entity.Null;
        }

        var attached = entityManager.GetComponentData<Attached>(entity);
        var attachedParent = attached.m_Parent;

        if (!entityManager.HasComponent<BorderDistrict>(attachedParent))
        {
            return Entity.Null;
        }

        var borderDistrict = entityManager.GetComponentData<BorderDistrict>(attachedParent);
        return borderDistrict.m_Left != Entity.Null
            ? borderDistrict.m_Left
            : borderDistrict.m_Right;
    }
}