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

using Game;
using Game.Common;
using Game.Net;
using Game.UI;
using Unity.Collections;
using Unity.Entities;

namespace StationNaming.System;

public partial class AutoNamingSystem : GameSystemBase
{
    private EntityQuery _createdQuery;
    private NameSystem _nameSystem;

    protected override void OnUpdate()
    {
        if (!Mod.GetInstance().GetSettings().Enable)
        {
            return;
        }

        if (!Mod.GetInstance().GetSettings().AutoNaming)
        {
            return;
        }

        var entities = _createdQuery.ToEntityArray(Allocator.Temp);

        foreach (var entity in entities)
        {
            if (!EntityManager.HasBuffer<NameCandidate>(entity))
            {
                continue;
            }

            var candidates = EntityManager
                .GetBuffer<NameCandidate>(entity);
            var candidate = ChooseCandidate(candidates);

            if (EntityManager.HasComponent<Owner>(entity))
            {
                var owner = StopNameHelper.RetrieveOwner(EntityManager, entity);
                candidate.Refer = owner;
                candidate.Source = NameSource.Owner;
            }

            _nameSystem.SetCustomName(entity, candidate.Name.ToString());

            EntityManager.RemoveComponent<ToAutoNaming>(entity);
            EntityManager.RemoveComponent<Selected>(entity);

            var entityNaming = new ManualSelectNaming(candidate);
            var association = new NamingAssociation(entity);

            EntityManager.AddComponentData(entity, entityNaming);
            GetBuffer<NamingAssociation>(GetRootEntity(candidate.Refer))
                .Add(association);
        }
    }

    private static NameCandidate ChooseCandidate(DynamicBuffer<NameCandidate> candidates)
    {
        NameCandidate ownerCandidate = default;
        NameCandidate roadCandidate = default;

        foreach (var candidate in candidates)
        {
            switch (candidate.Source)
            {
                case NameSource.Owner:
                    ownerCandidate = candidate;
                    break;
                case NameSource.Road:
                    roadCandidate = candidate;
                    break;
            }
        }

        if (ownerCandidate.Source != default)
        {
            return ownerCandidate;
        }

        return roadCandidate.Source != default
            ? roadCandidate
            : candidates[0];
    }

    private Entity GetRootEntity(Entity entity)
    {
        if (EntityManager.HasComponent<Aggregated>(entity))
        {
            return EntityManager.GetComponentData<Aggregated>(entity).m_Aggregate;
        }

        return entity;
    }

    protected override void OnCreate()
    {
        base.OnCreate();

        _nameSystem = World.DefaultGameObjectInjectionWorld
            .GetOrCreateSystemManaged<NameSystem>();

        _createdQuery = GetEntityQuery(new EntityQueryDesc
        {
            All =
            [
                ComponentType.ReadOnly<Selected>(),
                ComponentType.ReadOnly<ToAutoNaming>()
            ],
            None =
            [
                ComponentType.ReadOnly<Deleted>()
            ]
        });

        RequireForUpdate(_createdQuery);
    }

    private DynamicBuffer<T> GetBuffer<T>(Entity entity) where T : unmanaged, IBufferElementData
    {
        return EntityManager.HasBuffer<T>(entity)
            ? EntityManager.GetBuffer<T>(entity)
            : EntityManager.AddBuffer<T>(entity);
    }
}