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

using Game;
using StationNaming.System.Utils;
using Unity.Collections;
using Unity.Entities;

namespace StationNaming.System.Cleanup
{
    /// <summary>
    /// System to clean up unused buffers and components.
    /// </summary>
    public partial class CleanupSystem : GameSystemBase
    {
        private EntityQuery _selectedQuery;
        private EntityQuery _nameCandidatesQuery;
        private EntityQuery _manualSelectNamingQuery;

        protected override void OnUpdate()
        {
            var selectedEntities = _selectedQuery.ToEntityArray(Allocator.Temp);
            foreach (var entity in selectedEntities)
            {
                EntityManager.RemoveComponent<NameCandidateTag>(entity);
            }

            var nameCandidateEntities = _nameCandidatesQuery.ToEntityArray(Allocator.Temp);
            foreach (var entity in nameCandidateEntities)
            {
                SystemUtils.TryCleanRemoveBuffer<NameCandidate>(EntityManager, entity);
            }

            var manualSelectNamingEntities = _manualSelectNamingQuery.ToEntityArray(Allocator.Temp);
            foreach (var entity in manualSelectNamingEntities)
            {
                SystemUtils.TryCleanRemoveComponent<ManualSelectNaming>(EntityManager, entity);
            }
        }

        protected override void OnCreate()
        {
            base.OnCreate();
            Mod.GetLogger().Info("CleanupSystem created.");

            // If an entity was not selected, it shouldn't have the NameCandidateTag
            _selectedQuery = GetEntityQuery(new EntityQueryDesc
            {
                All = new[]
                {
                    ComponentType.ReadOnly<NameCandidateTag>()
                },
                None = new[]
                {
                    ComponentType.ReadOnly<Selected>()
                }
            });

            _nameCandidatesQuery = GetEntityQuery(new EntityQueryDesc
            {
                All = new[]
                {
                    ComponentType.ReadOnly<NameCandidate>()
                },
                None = new[]
                {
                    ComponentType.ReadOnly<NameCandidateTag>()
                }
            });

            _manualSelectNamingQuery = GetEntityQuery(new EntityQueryDesc
            {
                All = new[]
                {
                    ComponentType.ReadOnly<ManualSelectNaming>()
                },
                None = new[]
                {
                    ComponentType.ReadOnly<ManualSelectNamingTag>()
                }
            });

            RequireAnyForUpdate(_selectedQuery, _nameCandidatesQuery, _manualSelectNamingQuery);
        }
    }
}