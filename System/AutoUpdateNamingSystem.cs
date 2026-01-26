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
using Game.Prefabs;
using Game.UI;
using StationNaming.Setting;
using Unity.Collections;
using Unity.Entities;

namespace StationNaming.System
{
    public partial class AutoUpdateNamingSystem : GameSystemBase
    {
        private NameSystem _nameSystem;
        private PrefabSystem _prefabSystem;
        private EntityQuery _associationQuery;

        private NameFormatter _nameFormatter;

        protected override void OnUpdate()
        {
            var settings = Mod.GetInstance().GetSettings();
            if (!settings.Enable)
            {
                return;
            }

            if (!settings.AutoUpdate)
            {
                return;
            }

            _nameFormatter.Options = settings.ToNameOptions();

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
            var valid = new List<NamingAssociation>();

            foreach (var naming in namingAssociations)
            {
                var target = naming.Target;
                if (!EntityManager.Exists(target)
                    || !EntityManager.HasComponent<ManualSelectNaming>(target))
                {
                    continue;
                }

                var selectNaming =
                    EntityManager.GetComponentData<ManualSelectNaming>(target);
                var rawNameCandidate = selectNaming.SelectedName;
                var currentName = _nameSystem.GetRenderedLabelName(target);
                var rawCandidateName = rawNameCandidate.Name.ToString();
                if (!rawNameCandidate.IsValid() ||
                    !currentName.Contains(rawCandidateName))
                {
                    // it probably means user has changed the name,
                    // we should not update it
                    selectNaming.SelectedName.Release();
                    EntityManager.RemoveComponent<ManualSelectNaming>(target);
                    continue;
                }

                var updatedName = GetUpdatedName(selectNaming, target);

                var copy = rawNameCandidate.DeepCopy();
                copy.Name = updatedName;

                valid.Add(naming);
                EntityManager.AddComponentData(
                    target,
                    new ManualSelectNaming(copy)
                );

                rawNameCandidate.Release();

                var targetName = currentName.Replace(rawCandidateName, updatedName);
                _nameSystem.SetCustomName(target, targetName);
            }

            namingAssociations.Clear();
            foreach (var naming in valid)
            {
                namingAssociations.Add(naming);
            }
        }

        private string GetUpdatedName(ManualSelectNaming selectNaming, Entity entity)
        {
            var selectedName = selectNaming.SelectedName;
            var refers = selectedName.Refers;

            var name = _nameFormatter.FormatRefers(refers, entity);
            return name;
        }

        protected override void OnCreate()
        {
            base.OnCreate();

            _nameSystem = World.DefaultGameObjectInjectionWorld
                .GetExistingSystemManaged<NameSystem>();
            _prefabSystem = World.DefaultGameObjectInjectionWorld
                .GetExistingSystemManaged<PrefabSystem>();
            _nameFormatter = new NameFormatter(EntityManager, _nameSystem, _prefabSystem);

            _associationQuery = GetEntityQuery(new EntityQueryDesc
            {
                All = new[]
                {
                    ComponentType.ReadOnly<NamingAssociation>(),
                    ComponentType.ReadOnly<CustomName>()
                },
                Any = new[]
                {
                    ComponentType.ReadOnly<BatchesUpdated>(),
                    ComponentType.ReadOnly<Updated>()
                },
                None = new[]
                {
                    ComponentType.ReadOnly<Deleted>()
                }
            });

            RequireForUpdate(_associationQuery);
        }
    }
}