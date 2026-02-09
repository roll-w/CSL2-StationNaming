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
using Unity.Collections;
using Unity.Entities;

namespace StationNaming.System.Cleanup
{
    /// <summary>
    /// System to perform migration setup before cleanup operations.
    ///
    /// The system creates a global singleton entity with CleanupSetup component
    /// to mark that migration is complete. CleanupSystem will only run after
    /// this setup is done.
    /// </summary>
    public partial class CleanupSetupSystem : GameSystemBase
    {
        private EntityQuery _nameCandidatesForMigrationQuery;
        private EntityQuery _manualSelectNamingForMigrationQuery;
        private EntityQuery _cleanupSetupEntityQuery;

        protected override void OnUpdate()
        {
            if (_cleanupSetupEntityQuery.CalculateEntityCount() > 0)
            {
                // Migration already done, disable this system
                Enabled = false;
                return;
            }

            PerformMigration();
            CreateCleanupSetupMarker();

            // Disable this system after migration is complete
            Enabled = false;

            Mod.GetLogger().Info("CleanupSetupSystem: Migration completed successfully.");
        }

        private void PerformMigration()
        {
            MigrateNameCandidates();
            MigrateManualSelectNamings();
        }

        private void MigrateNameCandidates()
        {
            var entities = _nameCandidatesForMigrationQuery.ToEntityArray(Allocator.Temp);
            var count = 0;

            foreach (var entity in entities)
            {
                EntityManager.AddComponent<NameCandidateTag>(entity);
                count++;
            }

            if (count > 0)
            {
                Mod.GetLogger().Info($"CleanupSetupSystem: Migrated {count} NameCandidate entities.");
            }

            entities.Dispose();
        }

        private void MigrateManualSelectNamings()
        {
            var entities = _manualSelectNamingForMigrationQuery.ToEntityArray(Allocator.Temp);
            var count = 0;

            foreach (var entity in entities)
            {
                EntityManager.AddComponent<ManualSelectNamingTag>(entity);
                count++;
            }

            if (count > 0)
            {
                Mod.GetLogger().Info($"CleanupSetupSystem: Migrated {count} ManualSelectNaming entities.");
            }

            entities.Dispose();
        }

        /// <summary>
        /// Creates a global singleton entity with CleanupSetup component
        /// to mark that migration is complete.
        /// </summary>
        private void CreateCleanupSetupMarker()
        {
            var setupEntity = EntityManager.CreateEntity();
            EntityManager.AddComponent<CleanupSetup>(setupEntity);
            EntityManager.SetName(setupEntity, "StationNaming_CleanupSetup");
        }

        protected override void OnCreate()
        {
            base.OnCreate();
            Mod.GetLogger().Info("CleanupSetupSystem created.");
            Enabled = true;

            _nameCandidatesForMigrationQuery = GetEntityQuery(new EntityQueryDesc
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

            _manualSelectNamingForMigrationQuery = GetEntityQuery(new EntityQueryDesc
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

            _cleanupSetupEntityQuery = GetEntityQuery(new EntityQueryDesc
            {
                All = new[]
                {
                    ComponentType.ReadOnly<CleanupSetup>()
                }
            });
        }
    }
}