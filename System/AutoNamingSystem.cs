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
using Game.UI;
using StationNaming.System.Utils;
using Unity.Collections;
using Unity.Entities;

namespace StationNaming.System
{
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

                _nameSystem.SetCustomName(entity, candidate.Name.ToString());

                EntityManager.RemoveComponent<ToAutoNaming>(entity);
                EntityManager.RemoveComponent<Selected>(entity);

                var entityNaming = new ManualSelectNaming(candidate.DeepCopy());
                var association = new NamingAssociation(entity);

                EntityManager.AddComponentData(entity, entityNaming);
                SystemUtils.AddComponent<ManualSelectNamingTag>(EntityManager, entity);
                AddAssociations(association, candidate.Refers);

                EntityManager.RemoveComponent<NameCandidateTag>(entity);
            }
        }

        private void AddAssociations(
            NamingAssociation association,
            INativeList<NameSourceRefer> refers
        )
        {
            var refersLength = refers.Length;
            if (refersLength == 0)
            {
                return;
            }

            for (var i = 0; i < refersLength; i++)
            {
                var refer = refers[i];

                SystemUtils.GetBuffer<NamingAssociation>(
                    EntityManager,
                    refer.Refer
                ).Add(association);
            }
        }

        private static NameCandidate ChooseCandidate(DynamicBuffer<NameCandidate> candidates)
        {
            return candidates[0];
        }

        protected override void OnCreate()
        {
            base.OnCreate();

            _nameSystem = World.DefaultGameObjectInjectionWorld
                .GetOrCreateSystemManaged<NameSystem>();

            _createdQuery = GetEntityQuery(new EntityQueryDesc
            {
                All = new[]
                {
                    ComponentType.ReadOnly<Selected>(),
                    ComponentType.ReadOnly<ToAutoNaming>()
                },
                None = new[]
                {
                    ComponentType.ReadOnly<Deleted>()
                }
            });

            RequireForUpdate(_createdQuery);
        }
    }
}