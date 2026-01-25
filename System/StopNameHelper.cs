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
using Game.Areas;
using Game.Buildings;
using Game.Common;
using Game.Net;
using Game.Objects;
using Game.Prefabs;
using Game.UI;
using StationNaming.Setting;
using Unity.Entities;

namespace StationNaming.System
{
    public class StopNameHelper
    {
        private NameOptions NameOptions { get; set; } = new();

        private readonly NameFormatter _nameFormatter;
        private readonly NameSystem _nameSystem;
        private EntityManager _entityManager;

        public StopNameHelper(EntityManager entityManager, NameSystem nameSystem, PrefabSystem prefabSystem)
        {
            _entityManager = entityManager;
            _nameSystem = nameSystem;
            _nameFormatter = new NameFormatter(entityManager, nameSystem, prefabSystem);
        }

        public void ApplyTo(NameOptions nameOptions)
        {
            _nameFormatter.Options = nameOptions;
            NameOptions = nameOptions;
        }

        public IEnumerable<NameCandidate> SetCandidatesForStop(Entity stop, int depth = 2)
        {
            var hasAttached = _entityManager.HasComponent<Attached>(stop);
            var hasOwner = _entityManager.HasComponent<Owner>(stop);

            // if the stop has an owner, it could be an inner stop
            if (hasOwner)
            {
                var owner = RetrieveOwner(_entityManager, stop);
                if (_entityManager.HasComponent<Building>(owner))
                {
                    return AddCandidatesIfBuildingStop(stop, owner, depth);
                }
            }

            if (!hasAttached)
            {
                return new List<NameCandidate>();
            }

            var attached = _entityManager.GetComponentData<Attached>(stop);
            if (attached.m_Parent == Entity.Null || attached.m_Parent == default)
            {
                return new List<NameCandidate>();
            }

            return _entityManager.HasComponent<Edge>(attached.m_Parent)
                ? AddCandidatesIfRoadStop(stop, attached, depth)
                : new List<NameCandidate>();
        }

        public IEnumerable<NameCandidate> SetCandidatesForBuilding(
            Entity station, int depth = 2, bool includeSelf = false)
        {
            if (!_entityManager.HasComponent<Building>(station))
            {
                return new List<NameCandidate>();
            }

            var building = _entityManager.GetComponentData<Building>(station);
            if (building.m_RoadEdge == Entity.Null || building.m_RoadEdge == default)
            {
                return new List<NameCandidate>();
            }

            var edge = building.m_RoadEdge;
            return SetCandidatesIfRoad(station, edge, depth, includeSelf);
        }

        private IEnumerable<NameCandidate> AddCandidatesIfBuildingStop(
            Entity stop, Entity owner, int length)
        {
            // get the real owner, not the extension
            if (!_entityManager.HasComponent<Building>(owner))
            {
                return new List<NameCandidate>();
            }

            var candidates = SetCandidatesForBuilding(
                owner, length, includeSelf: false
            );
            var buildingName = _nameSystem.GetRenderedLabelName(owner);
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
            while (true)
            {
                if (!entityManager.HasComponent<Owner>(entity))
                {
                    return entity;
                }

                var owner = entityManager.GetComponentData<Owner>(entity);
                entity = owner.m_Owner;
            }
        }

        private IEnumerable<NameCandidate> AddCandidatesIfRoadStop(Entity stop,
            Attached attached, int depth)
        {
            return SetCandidatesIfRoad(stop, attached.m_Parent, depth);
        }

        private IEnumerable<NameCandidate> SetCandidatesIfRoad(
            Entity target, Entity edge, int depth, bool includeSelf = false)
        {
            HashSet<NameCandidate> nameCandidates = new HashSet<NameCandidate>();

            var collectEdges = EdgeUtils.CollectIntersections(edge, _entityManager, depth);

            var currentRoad = new RoadEdge(Direction.Init, EdgeType.Same, edge);
            var root = EdgeUtils.GetRootEntityForEdge(edge, _entityManager);

            if (NameOptions.IsNameSourceEnabled(NameSource.Intersection))
            {
                AddIntersectionCandidate(edge, root, nameCandidates, depth);
            }

            var roadEdges = collectEdges.ToList();
            if (NameOptions.IsNameSourceEnabled(NameSource.Road))
            {
                nameCandidates.Add(NameCandidate.Of(
                    string.Empty,
                    Direction.Init, EdgeType.Same,
                    edge, NameSource.Road
                ));
                AddOtherRoadCandidate(root, roadEdges, nameCandidates);
            }

            if (NameOptions.BuildingName)
            {
                HashSet<Entity> visitedBuildings = new HashSet<Entity>();
                foreach (var roadEdge in roadEdges)
                {
                    CollectBuildingNames(
                        target,
                        currentRoad, roadEdge,
                        nameCandidates,
                        visitedBuildings, includeSelf
                    );
                }
            }

            SetCandidates(nameCandidates, target);
            return nameCandidates;
        }

        private void AddIntersectionCandidate(
            Entity edge, Entity root,
            ICollection<NameCandidate> nameCandidates, int depth)
        {
            var roadEdges = EdgeUtils.CollectIntersections(
                edge, _entityManager, depth);

            foreach (var roadEdge in roadEdges)
            {
                var refers = new List<NameSourceRefer> { new(root, NameSource.Road) };
                var node = roadEdge.Edge;

                var connectedEdges = _entityManager.GetBuffer<ConnectedEdge>(node);
                foreach (var connectedEdge in connectedEdges)
                {
                    var curEdge = connectedEdge.m_Edge;
                    if (curEdge == edge
                        || !_entityManager.HasComponent<Road>(curEdge))
                    {
                        continue;
                    }

                    var curRoot = EdgeUtils.GetRootEntityForEdge(curEdge, _entityManager);
                    if (curRoot == root)
                    {
                        continue;
                    }

                    var refer = new NameSourceRefer(curRoot, NameSource.Road);
                    if (!refers.Contains(refer))
                    {
                        refers.Add(refer);
                    }
                }

                refers.Add(new NameSourceRefer(node, NameSource.Intersection));

                var nameCandidate = NameCandidate.Of(
                    string.Empty, roadEdge.Direction,
                    roadEdge.EdgeType,
                    refers
                );
                nameCandidates.Add(nameCandidate);
            }
        }

        private void AddOtherRoadCandidate(
            Entity root,
            List<RoadEdge> roadEdges,
            ICollection<NameCandidate> candidates)
        {
            var hasAddStart = false;
            var hasAddEnd = false;
            foreach (var roadEdge in roadEdges)
            {
                if (roadEdge.Direction == Direction.Init
                    || roadEdge.Direction == Direction.Start && hasAddStart
                    || roadEdge.Direction == Direction.End && hasAddEnd)
                {
                    continue;
                }

                var rootEntityForEdge = EdgeUtils.GetRootEntityForEdge(roadEdge.Edge, _entityManager);
                if (root == rootEntityForEdge)
                {
                    continue;
                }

                var nameCandidate = NameCandidate.Of(
                    string.Empty, roadEdge.Direction,
                    roadEdge.EdgeType,
                    roadEdge.Edge, NameSource.Road
                );
                candidates.Add(nameCandidate);
                switch (roadEdge.Direction)
                {
                    case Direction.Start:
                        hasAddStart = true;
                        break;
                    case Direction.End:
                        hasAddEnd = true;
                        break;
                }
            }
        }

        private void CollectBuildingNames(
            Entity self,
            RoadEdge currentRoad,
            RoadEdge roadEdge,
            ICollection<NameCandidate> candidates,
            ISet<Entity> visitedBuildings,
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
                if (!visitedBuildings.Add(connectedBuilding.m_Building))
                {
                    continue;
                }

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

            var refers = new List<NameSourceRefer>
            {
                new(roadEdgeRoot, NameSource.Road),
                new(building, source)
            };

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
                postProcessed.ForEach(it => nameCandidates.Add(it));
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

        private List<NameCandidate> PostProcessNameCandidate(
            NameCandidate candidate,
            Entity target
        )
        {
            if (!NameOptions.EnableDistrictPrefix
                || candidate.Refers.Length == 0
                || candidate.Refers[0].Source == NameSource.Owner)
            {
                return new List<NameCandidate> { GenerateNameCandidate(candidate, target) };
            }

            var districtEntity = GetDistrictEntity(target, _entityManager);
            if (districtEntity == Entity.Null)
            {
                return new List<NameCandidate> { GenerateNameCandidate(candidate, target) };
            }

            var res = new List<NameCandidate>();
            if (NameOptions.SeparateDistrictPrefix)
            {
                res.Add(GenerateNameCandidate(candidate, target));
            }

            var districtRefer = new NameSourceRefer(districtEntity, NameSource.District);
            var refers = candidate.Refers;

            var newRefers = new List<NameSourceRefer> { districtRefer };
            foreach (var r in refers)
            {
                newRefers.Add(r);
            }

            var name = _nameFormatter.FormatRefers(newRefers, target);
            res.Add(NameCandidate.Of(
                name,
                candidate.Direction,
                candidate.EdgeType,
                newRefers
            ));
            return res;
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
            var refers = new List<NameSourceRefer> { districtRefer };
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
                    : candidate.Refers[^1].Source
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
}