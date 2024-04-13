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
        //
        // var entityManager = EntityManager;
        // var buffer = new EntityCommandBuffer(Allocator.TempJob);
        //
        // Entities.WithName("StationNaming_AutoNamingSystem")
        //     .WithAll<Selected, ToAutoNaming>()
        //     .WithNone<Deleted>()
        //     .WithStructuralChanges()
        //     .ForEach((Entity entity) =>
        //     {
        //         if (!entityManager.HasBuffer<NameCandidate>(entity))
        //         {
        //             return;
        //         }
        //         var candidates = entityManager
        //             .GetBuffer<NameCandidate>(entity);
        //         var candidate = ChooseCandidate(candidates);
        //         if (entityManager.HasComponent<Owner>(entity))
        //         {
        //             var owner = StopNameHelper.RetrieveOwner(entityManager, entity);
        //             candidate.Refers[0] = new NameSourceRefer(owner, NameSource.Owner);
        //         }
        //
        //         buffer.AddComponent(entity, new ToSetName
        //         {
        //             Name = candidate.Name
        //         });
        //
        //         buffer.RemoveComponent<ToAutoNaming>(entity);
        //         buffer.RemoveComponent<Selected>(entity);
        //
        //         var entityNaming = new ManualSelectNaming(candidate);
        //         var association = new NamingAssociation(entity);
        //
        //         buffer.AddComponent(entity, entityNaming);
        //         foreach (var refer in candidate.Refers)
        //         {
        //             buffer.AppendToBuffer(refer.Refer, association);
        //         }
        //
        //     }).Schedule();


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
                candidate.Refers[0] = new NameSourceRefer(owner, NameSource.Owner);
            }

            _nameSystem.SetCustomName(entity, candidate.Name.ToString());

            EntityManager.RemoveComponent<ToAutoNaming>(entity);
            EntityManager.RemoveComponent<Selected>(entity);

            var entityNaming = new ManualSelectNaming(candidate);
            var association = new NamingAssociation(entity);

            EntityManager.AddComponentData(entity, entityNaming);
            AddAssociations(association, candidate.Refers);
        }
    }

    private void AddAssociations(
        NamingAssociation association,
        IEnumerable<NameSourceRefer> refers)
    {
        foreach (var refer in refers)
        {
            GetBuffer<NamingAssociation>(refer.Refer).Add(association);
        }
    }

    private static NameCandidate ChooseCandidate(DynamicBuffer<NameCandidate> candidates)
    {
        NameCandidate ownerCandidate = default;
        NameCandidate roadCandidate = default;

        foreach (var candidate in candidates)
        {
            var size = candidate.Refers.Length;
            if (size > 1)
            {
                continue;
            }

            var refer = candidate.Refers[0];
            switch (refer.Source)
            {
                case NameSource.Owner:
                    ownerCandidate = candidate;
                    break;
                case NameSource.Road:
                    roadCandidate = candidate;
                    break;
            }
        }

        if (ownerCandidate.Refers[0].Source == NameSource.Owner)
        {
            return ownerCandidate;
        }

        return roadCandidate.Refers[0].Source == NameSource.Road
            ? roadCandidate
            : candidates[0];
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