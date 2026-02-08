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
using Game;
using Game.Rendering;
using Game.Routes;
using Game.UI;
using Game.UI.InGame;
using StationNaming.System.Utils;
using Unity.Entities;

namespace StationNaming.System
{
    public partial class UIBindingSystem : UISystemBase
    {
        private Entity _selectedEntity;
        private NameSystem _nameSystem;
        private SelectedInfoUISystem _selectedInfoUISystem;
        private CameraUpdateSystem _cameraUpdateSystem;

        private ValueBinding<Entity> _selectedEntityBinding;

        public override GameMode gameMode => GameMode.Game;

        protected override void OnCreate()
        {
            base.OnCreate();
            _nameSystem = World.DefaultGameObjectInjectionWorld
                .GetOrCreateSystemManaged<NameSystem>();

            _selectedInfoUISystem = World.DefaultGameObjectInjectionWorld
                .GetOrCreateSystemManaged<SelectedInfoUISystem>();
            _cameraUpdateSystem = World.DefaultGameObjectInjectionWorld
                .GetOrCreateSystemManaged<CameraUpdateSystem>();

            AddBinding(new TriggerBinding<Entity>(
                Mod.Name,
                "setSelectedEntity",
                SetSelectedEntity
            ));

            AddBinding(
                _selectedEntityBinding = new ValueBinding<Entity>(
                    Mod.Name,
                    "selectedEntity",
                    Entity.Null
                )
            );

            AddUpdateBinding(new GetterValueBinding<bool>(
                Mod.Name, "isShowCandidates",
                IsShowCandidates
            ));

            AddBinding(new CallBinding<Entity, ManagedNameCandidate, bool>(
                Mod.Name,
                "setCandidateFor",
                SetCandidateFor
            ));

            AddBinding(new CallBinding<Entity, List<ManagedNameCandidate>>(
                Mod.Name, "getCandidates",
                GetNameCandidates
            ));

            AddBinding(new TriggerBinding<Entity>(
                Mod.Name,
                "navigateToEntity",
                NavigateToEntity
            ));

            AddBinding(new TriggerBinding<ManagedNameCandidate>(
                Mod.Name,
                "navigateToCandidate",
                NavigateToCandidate
            ));

            AddBinding(new CallBinding<Entity, ManagedNameCandidate>(
                Mod.Name,
                "getManualSelected",
                GetManualSelected
            ));

            AddBinding(new TriggerBinding<Entity>(
                Mod.Name,
                "removeManualSelected",
                RemoveManualSelected
            ));

            Mod.GetLogger().Info("UI binding system initialized.");
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();

            // delegate from SelectedInfoUISystem
            _selectedEntityBinding.Update(_selectedInfoUISystem.selectedEntity);
        }

        internal void SetSelectedEntity(Entity entity)
        {
            if (_selectedEntity == entity && entity != Entity.Null && entity != default)
            {
                EntityManager.AddComponent<Selected>(entity);
                return;
            }

            if (_selectedEntity != Entity.Null &&
                EntityManager.HasComponent<Selected>(_selectedEntity))
            {
                EntityManager.RemoveComponent<Selected>(_selectedEntity);
                EntityManager.RemoveComponent<NameCandidateTag>(_selectedEntity);
            }

            if (entity != Entity.Null || entity != default)
            {
                EntityManager.AddComponent<Selected>(entity);
            }

            _selectedEntity = entity;
        }

        internal List<ManagedNameCandidate> GetNameCandidates(Entity entity)
        {
            SetSelectedEntity(entity);

            if (entity == Entity.Null)
            {
                return new List<ManagedNameCandidate>();
            }

            if (!EntityManager.HasBuffer<NameCandidate>(entity))
            {
                return new List<ManagedNameCandidate>();
            }

            var buffer = EntityManager.GetBuffer<NameCandidate>(entity);

            var result = new List<ManagedNameCandidate>();
            foreach (var nameCandidate in buffer)
            {
                result.Add(nameCandidate);
            }

            return result;
        }

        internal bool IsShowCandidates()
        {
            var entity = _selectedInfoUISystem.selectedEntity;
            SetSelectedEntity(entity);

            if (entity == Entity.Null)
            {
                return false;
            }

            if (EntityManager.HasComponent<TransportStop>(entity))
            {
                return true;
            }

            var buildingSource = NameUtils.TryGetBuildingSource(entity, EntityManager);

            return buildingSource.CouldNaming();
        }

        internal bool SetCandidateFor(Entity entity, ManagedNameCandidate candidate)
        {
            SetSelectedEntity(entity);

            if (entity == Entity.Null)
            {
                return false;
            }

            if (!EntityManager.HasBuffer<NameCandidate>(entity))
            {
                // we dont want to set the name if the buffer is not present
                return false;
            }

            var naming = new ManualSelectNaming(candidate);

            foreach (var refer in candidate.Refers)
            {
                CheckAndAddCurrent(
                    refer.Refer,
                    entity
                );
            }

            EntityManager.AddComponentData(entity, naming);
            SystemUtils.AddComponent<ManualSelectNamingTag>(EntityManager, entity);
            _nameSystem.SetCustomName(entity, candidate.Name);

            return true;
        }

        internal void NavigateToEntity(Entity entity)
        {
            if (entity == Entity.Null
                || _cameraUpdateSystem.orbitCameraController == null
                || !EntityManager.Exists(entity))
            {
                return;
            }

            _cameraUpdateSystem.orbitCameraController.followedEntity = entity;
            _cameraUpdateSystem.orbitCameraController.TryMatchPosition(_cameraUpdateSystem.activeCameraController);
            _cameraUpdateSystem.activeCameraController = _cameraUpdateSystem.orbitCameraController;
        }

        internal void NavigateToCandidate(ManagedNameCandidate candidate)
        {
            var refers = candidate.Refers;
            if (refers.Count == 1)
            {
                NavigateToEntity(refers[0].Refer);
                return;
            }

            var last = refers[^1];
            NavigateToEntity(last.Refer);
        }

        /// <summary>
        /// Add the target to the candidate list of the parent entity.
        /// </summary>
        public void AddToCandidates(Entity parent, Entity target)
        {
            // TODO:
        }

        private void CheckAndAddCurrent(Entity refer, Entity entity)
        {
            if (refer == Entity.Null)
            {
                return;
            }

            var buffer = SystemUtils.GetBuffer<NamingAssociation>(EntityManager, refer);
            if (buffer.Length == 0)
            {
                buffer.Add(new NamingAssociation(entity));
                return;
            }

            foreach (var naming in buffer)
            {
                if (naming.Target == entity)
                {
                    return;
                }
            }

            buffer.Add(new NamingAssociation(entity));
        }

        internal ManagedNameCandidate GetManualSelected(Entity entity)
        {
            SetSelectedEntity(entity);

            if (entity == Entity.Null || !EntityManager.HasComponent<ManualSelectNaming>(entity))
            {
                return new ManagedNameCandidate("", new List<NameSourceRefer>(), Direction.Init, EdgeType.Same);
            }

            var manual = EntityManager.GetComponentData<ManualSelectNaming>(entity);
            return manual.SelectedName;
        }

        internal void RemoveManualSelected(Entity entity)
        {
            if (entity == Entity.Null)
            {
                return;
            }

            if (!EntityManager.HasComponent<ManualSelectNaming>(entity))
            {
                return;
            }

            var manual = EntityManager.GetComponentData<ManualSelectNaming>(entity);

            // remove associations stored on refer entities
            foreach (var refer in manual.SelectedName.Refers)
            {
                if (refer.Refer == Entity.Null)
                {
                    continue;
                }

                if (!EntityManager.HasBuffer<NamingAssociation>(refer.Refer))
                {
                    continue;
                }

                var buffer = EntityManager.GetBuffer<NamingAssociation>(refer.Refer);
                for (int i = buffer.Length - 1; i >= 0; i--)
                {
                    if (buffer[i].Target == entity)
                    {
                        buffer.RemoveAt(i);
                    }
                }
            }

            EntityManager.RemoveComponent<ManualSelectNaming>(entity);
        }
    }
}