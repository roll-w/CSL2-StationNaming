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

                CheckAndUpdateFromRefer(entity);
            }
            entities.Dispose();
        }

        private void CheckAndUpdateFromRefer(Entity referEntity)
        {
            var namingAssociations = EntityManager.GetBuffer<NamingAssociation>(referEntity);
            var valid = new List<NamingAssociation>();

            foreach (var naming in namingAssociations)
            {
                var target = naming.Target;
                if (!EntityManager.Exists(target)
                    || !EntityManager.HasComponent<ManualSelectNaming>(target))
                {
                    Mod.GetLogger().Warn($"Target entity {target} associated with {referEntity} does not exist or does not have ManualSelectNaming component. Skipping.");
                    continue;
                }

                if (ProcessTargetUpdate(target))
                {
                    valid.Add(naming);
                }
            }

            namingAssociations.Clear();
            foreach (var naming in valid)
            {
                namingAssociations.Add(naming);
            }
        }

        private bool ProcessTargetUpdate(Entity target)
        {
            var selectNaming =
                EntityManager.GetComponentData<ManualSelectNaming>(target);
            var rawNameCandidate = selectNaming.SelectedName;
            var currentName = _nameSystem.GetRenderedLabelName(target);
            var rawCandidateName = rawNameCandidate.Name.ToString();

            if (!rawNameCandidate.IsValid() ||
                !currentName.Contains(rawCandidateName))
            {
                EntityManager.RemoveComponent<ManualSelectNamingTag>(target);
                return false;
            }

            var updatedName = GetUpdatedName(selectNaming, target);

            rawNameCandidate.Name = updatedName;
            selectNaming.SelectedName = rawNameCandidate;

            EntityManager.SetComponentData(target, selectNaming);

            var targetName = currentName.Replace(rawCandidateName, updatedName);
            _nameSystem.SetCustomName(target, targetName);

            return true;
        }

        private string GetUpdatedName(ManualSelectNaming selectNaming, Entity entity)
        {
            var selectedName = selectNaming.SelectedName;
            var refers = selectedName.Refers;

            return _nameFormatter.FormatRefers(refers, entity);
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