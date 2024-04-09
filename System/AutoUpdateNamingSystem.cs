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
using Game.Buildings;
using Game.Common;
using Game.Net;
using Game.Objects;
using Game.UI;
using Unity.Collections;
using Unity.Entities;

namespace StationNaming.System;

public partial class AutoUpdateNamingSystem : GameSystemBase
{
    private NameSystem _nameSystem;
    private EntityQuery _associationQuery;

    protected override void OnUpdate()
    {
        if (!Mod.GetInstance().GetSettings().Enable)
        {
            return;
        }

        if (!Mod.GetInstance().GetSettings().AutoUpdate)
        {
            return;
        }

        var entities = _associationQuery.ToEntityArray(Allocator.Temp);

        foreach (var entity in entities)
        {
            if (!EntityManager.HasBuffer<NamingAssociation>(entity))
            {
                continue;
            }

            CheckAndUpdate(entity);
        }
    }

    private void CheckAndUpdate(Entity entity)
    {
        var namingAssociations = EntityManager.GetBuffer<NamingAssociation>(entity);
        List<NamingAssociation> valid = [];

        foreach (var naming in namingAssociations)
        {
            var target = naming.Target;
            if (!EntityManager.HasComponent<ManualSelectNaming>(target))
            {
                continue;
            }

            var selectNaming =
                EntityManager.GetComponentData<ManualSelectNaming>(target);
            var name = _nameSystem.GetRenderedLabelName(target);
            var nameCandidate = selectNaming.SelectedName;
            if (name != nameCandidate.Name)
            {
                // it probably means the name has been changed by user,
                // we should not update it
                EntityManager.RemoveComponent<ManualSelectNaming>(target);
                continue;
            }

            var updatedName = GetUpdatedName(entity, target, selectNaming);

            var copy = nameCandidate;
            copy.Name = updatedName;

            valid.Add(naming);
            EntityManager.AddComponentData(
                target,
                new ManualSelectNaming(copy)
            );
            _nameSystem.SetCustomName(target, updatedName);
        }

        namingAssociations.Clear();
        foreach (var naming in valid)
        {
            namingAssociations.Add(naming);
        }
    }

    private string GetUpdatedName(
        Entity entity,
        Entity self,
        ManualSelectNaming selectNaming)
    {
        var selectedName = selectNaming.SelectedName;
        switch (selectedName.Source)
        {
            case NameSource.Owner:
                return _nameSystem.GetRenderedLabelName(entity);
            case NameSource.Road:
            {
                var roadName = _nameSystem.GetRenderedLabelName(entity);
                return Mod.GetInstance().GetSettings()
                    .FormatCandidateName(roadName);
            }
            case NameSource.Intersection:
            {
                var currentRoadName = GetRoadNameByAttached(self);
                var roadName = _nameSystem.GetRenderedLabelName(entity);

                var formatRoadName = Mod.GetInstance().GetSettings()
                    .FormatRoadName(currentRoadName, roadName);

                return Mod.GetInstance().GetSettings().FormatCandidateName(
                    formatRoadName
                );
            }
        }

        var entityName = _nameSystem.GetRenderedLabelName(entity);
        return Mod.GetInstance().GetSettings().FormatCandidateName(entityName);
    }

    private string GetRoadNameByAttached(Entity entity)
    {
        if (EntityManager.HasComponent<Building>(entity))
        {
            var building = EntityManager.GetComponentData<Building>(entity);
            return GetRoadName(building.m_RoadEdge);
        }

        if (!EntityManager.HasComponent<Attached>(entity))
        {
            return "";
        }

        var attached = EntityManager.GetComponentData<Attached>(entity).m_Parent;
        return GetRoadName(attached);
    }

    private string GetRoadName(Entity entity)
    {
        if (!EntityManager.HasComponent<Aggregated>(entity))
        {
            return "";
        }

        var aggregate = EntityManager.GetComponentData<Aggregated>(entity)
            .m_Aggregate;
        return _nameSystem.GetRenderedLabelName(aggregate);
    }

    protected override void OnCreate()
    {
        base.OnCreate();

        _nameSystem = World.DefaultGameObjectInjectionWorld
            .GetExistingSystemManaged<NameSystem>();

        _associationQuery = GetEntityQuery(new EntityQueryDesc
        {
            All =
            [
                ComponentType.ReadOnly<NamingAssociation>(),
                ComponentType.ReadOnly<CustomName>()
            ],
            Any =
            [
                ComponentType.ReadOnly<BatchesUpdated>(),
                ComponentType.ReadOnly<Updated>()
            ],
            None =
            [
                ComponentType.ReadOnly<Deleted>()
            ]
        });

        RequireForUpdate(_associationQuery);
    }
}