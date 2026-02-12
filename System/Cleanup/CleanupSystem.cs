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
        private EntityQuery _cleanupSetupQuery;

        protected override void OnUpdate()
        {
            if (_cleanupSetupQuery.CalculateEntityCount() == 0)
            {
                // Setup not yet complete, skip cleanup
                return;
            }

            var selectedEntities = _selectedQuery.ToEntityArray(Allocator.Temp);
            foreach (var entity in selectedEntities)
            {
                EntityManager.RemoveComponent<NameCandidateTag>(entity);
            }

            selectedEntities.Dispose();

            var nameCandidateEntities = _nameCandidatesQuery.ToEntityArray(Allocator.Temp);
            foreach (var entity in nameCandidateEntities)
            {
                SystemUtils.TryCleanRemoveBuffer<NameCandidate>(EntityManager, entity);
            }

            nameCandidateEntities.Dispose();

            var manualSelectNamingEntities = _manualSelectNamingQuery.ToEntityArray(Allocator.Temp);
            foreach (var entity in manualSelectNamingEntities)
            {
                if (!EntityManager.HasComponent<ManualSelectNaming>(entity))
                {
                    continue;
                }

                var manual = EntityManager.GetComponentData<ManualSelectNaming>(entity);

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
                    for (var i = buffer.Length - 1; i >= 0; i--)
                    {
                        if (buffer[i].Target == entity)
                        {
                            buffer.RemoveAt(i);
                        }
                    }
                }

                SystemUtils.TryCleanRemoveComponent<ManualSelectNaming>(EntityManager, entity);
            }

            manualSelectNamingEntities.Dispose();
        }

        protected override void OnCreate()
        {
            base.OnCreate();
            Mod.GetLogger().Info("CleanupSystem created.");

            // Query to check if CleanupSetupSystem has completed migration
            _cleanupSetupQuery = GetEntityQuery(new EntityQueryDesc
            {
                All = new[]
                {
                    ComponentType.ReadOnly<CleanupSetup>()
                }
            });

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