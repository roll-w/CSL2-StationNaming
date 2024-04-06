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
using Colossal.UI.Binding;
using Game.UI;
using Unity.Entities;

namespace StationNaming.System;

public partial class UIBindingSystem : UISystemBase
{
    private Entity _selectedEntity;
    private NameSystem _nameSystem;

    protected override void OnCreate()
    {
        base.OnCreate();
        _nameSystem = World.DefaultGameObjectInjectionWorld
            .GetOrCreateSystemManaged<NameSystem>();

        AddBinding(new TriggerBinding<Entity>(
            Mod.Name,
            nameof(SetSelectedEntity),
            SetSelectedEntity
        ));

        AddBinding(new TriggerBinding<Entity, NameCandidate>(
            Mod.Name,
            nameof(SetCandidateFor),
            SetCandidateFor,
            null, new ValueReader<NameCandidate>()
        ));

        AddBinding(new CallBinding<Entity, List<ManagedNameCandidate>>(
            Mod.Name, "GetCandidates",
            GetNameCandidates
        ));

        Mod.GetLogger().Info("UI binding system initialized.");
    }

    private void SetSelectedEntity(Entity entity)
    {
        if (_selectedEntity != Entity.Null &&
            EntityManager.HasComponent<Selected>(_selectedEntity))
        {
            EntityManager.RemoveComponent<Selected>(_selectedEntity);
        }
        EntityManager.AddComponent<Selected>(entity);
        _selectedEntity = entity;
    }


    private List<ManagedNameCandidate> GetNameCandidates(Entity entity)
    {
        SetSelectedEntity(entity);

        if (entity == Entity.Null)
        {
            // Mod.GetLogger().Info($"[GetNameCandidates] entity is NULL");
            return [];
        }

        if (!EntityManager.HasBuffer<NameCandidate>(entity))
        {
            // Mod.GetLogger().Info($"[GetNameCandidates] Get candidates for {entity}, but it has no buffer.");
            return [];
        }

        var buffer = EntityManager.GetBuffer<NameCandidate>(entity);

        // Mod.GetLogger().Info($"[GetNameCandidates] Get candidates for {entity} with {buffer.Length} candidates.");

        List<ManagedNameCandidate> result = [];
        foreach (var nameCandidate in buffer)
        {
            result.Add(nameCandidate);
        }

        return result;
    }


    private void SetCandidateFor(Entity entity, NameCandidate candidate)
    {
        SetSelectedEntity(entity);

        if (entity == Entity.Null)
        {
            return;
        }

        var naming = new ManualSelectNaming(candidate);
        if (!EntityManager.HasBuffer<NameCandidate>(entity))
        {
            // we dont want to set the name if the buffer is not present
            return;
        }

        EntityManager.AddComponentData(entity, naming);
        _nameSystem.SetCustomName(entity, candidate.Name.ToString());
    }
}