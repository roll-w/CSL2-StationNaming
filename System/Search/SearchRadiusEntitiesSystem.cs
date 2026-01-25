// MIT License
//
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

#define BURST

using System.Runtime.CompilerServices;
using Colossal.Collections;
using Game;
using Game.Common;
using Game.Objects;
using Game.Prefabs;
using Game.Tools;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;

namespace StationNaming.System.Search
{
    public partial class SearchRadiusEntitiesSystem : GameSystemBase
    {
        private SearchSystem _objectSearchSystem;
        private ComponentLookup<Transform> _transformLookup;
        private ComponentLookup<PrefabRef> _prefabRefLookup;
        private ComponentLookup<ObjectGeometryData> _objectGeometryDataLookup;
        private EntityQuery _selectedEntityQuery;

        protected override void OnUpdate()
        {
            _prefabRefLookup.Update(this);
            _objectGeometryDataLookup.Update(this);
            _transformLookup.Update(this);
            var settings = Mod.GetInstance().GetSettings();
            if (!settings.Enable)
            {
                return;
            }

            var entities = _selectedEntityQuery.ToEntityArray(Allocator.Temp);
            var searchRadius = settings.SearchRadius;
            var searchTree = _objectSearchSystem.GetStaticSearchTree(true, out var treeHandle);

            foreach (var entity in entities)
            {
                var hitEntities = new NativeList<Entity>(PredictArraySize(searchRadius), Allocator.TempJob);
                var job = new FindEntitiesInRadiusJob
                {
                    Center = entity,
                    Radius = searchRadius,
                    SearchTree = searchTree,
                    PrefabRefLookup = _prefabRefLookup,
                    ObjectGeometryDataLookup = _objectGeometryDataLookup,
                    TransformLookup = _transformLookup,
                    Results = hitEntities
                };

                var jobHandle = job.Schedule(treeHandle);
                jobHandle.Complete();

                Mod.GetLogger().Debug($"Click on the location to find {hitEntities.Length} entities");
                foreach (var hit in hitEntities)
                {
                    // TODO: handle entities
                }

                hitEntities.Dispose();
                _objectSearchSystem.AddStaticSearchTreeReader(jobHandle);
            }
        }

        protected override void OnCreate()
        {
            Mod.GetLogger().Info("SearchRadiusEntitiesSystem created.");
            _objectSearchSystem = World.GetOrCreateSystemManaged<SearchSystem>();
            _prefabRefLookup = GetComponentLookup<PrefabRef>();
            _objectGeometryDataLookup = GetComponentLookup<ObjectGeometryData>();
            _transformLookup = GetComponentLookup<Transform>();
            _selectedEntityQuery = GetEntityQuery(new EntityQueryDesc
            {
                Any = new[]
                {
                    ComponentType.ReadOnly<Selected>()
                },
                None = new[]
                {
                    ComponentType.ReadOnly<Deleted>(),
                    ComponentType.ReadOnly<Temp>()
                }
            });
        }

        private static int PredictArraySize(float radius)
        {
            var estimate = (int)(math.PI * radius * radius / 100f);
            return math.clamp(estimate, 16, 1024);
        }

#if BURST
        [BurstCompile]
#endif
        private struct FindEntitiesInRadiusJob : IJob
        {
            [ReadOnly] public Entity Center;
            [ReadOnly] public ComponentLookup<PrefabRef> PrefabRefLookup;
            [ReadOnly] public ComponentLookup<ObjectGeometryData> ObjectGeometryDataLookup;
            [ReadOnly] public ComponentLookup<Transform> TransformLookup;
            [ReadOnly] public float Radius;
            [ReadOnly] public NativeQuadTree<Entity, QuadTreeBoundsXZ> SearchTree;

            public NativeList<Entity> Results;

            public void Execute()
            {
                var transform = TransformLookup[Center];
                var prefab = PrefabRefLookup[Center].m_Prefab;
                var objectGeometryData = ObjectGeometryDataLookup[prefab];

                var selfMin = objectGeometryData.m_Bounds.min + transform.m_Position;
                var selfMax = objectGeometryData.m_Bounds.max + transform.m_Position;

                var iterator = new RadiusIterator
                {
                    SelfMin = selfMin,
                    SelfMax = selfMax,
                    ThresholdSq = Radius * Radius,
                    Results = Results
                };

                SearchTree.Iterate(ref iterator);
            }
        }

        private struct RadiusIterator : INativeQuadTreeIterator<Entity, QuadTreeBoundsXZ>
        {
            public float3 SelfMin;
            public float3 SelfMax;
            public float ThresholdSq;
            public NativeList<Entity> Results;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool Intersect(QuadTreeBoundsXZ bounds)
            {
                var otherMin = bounds.m_Bounds.min;
                var otherMax = bounds.m_Bounds.max;
                var dx = math.max(0, math.max(SelfMin.x - otherMax.x, otherMin.x - SelfMax.x));
                var dz = math.max(0, math.max(SelfMin.z - otherMax.z, otherMin.z - SelfMax.z));

                var distSq = dx * dx + dz * dz;
                return distSq <= ThresholdSq;
            }

            public void Iterate(QuadTreeBoundsXZ bounds, Entity entity)
            {
                if (Intersect(bounds))
                {
                    Results.Add(entity);
                }
            }
        }
    }
}