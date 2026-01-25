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
using Game.Buildings;
using Game.Common;
using Game.Routes;
using Unity.Collections;
using Unity.Entities;

namespace StationNaming.System
{
    /// <summary>
    /// The system that automatically tags the newly created buildings.
    ///
    /// When a building is created, this system will check if the building
    /// could be named automatically.
    /// If so, it will add the "ToAutoNaming" tag to the building.
    /// </summary>
    public partial class AutoTaggingSystem : GameSystemBase
    {
        private EntityQuery _createdQuery;

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
                if (!CouldNaming(entity))
                {
                    continue;
                }

                EntityManager.AddComponent<ToAutoNaming>(entity);
                EntityManager.AddComponent<Selected>(entity);
            }
        }

        private bool CouldNaming(Entity entity)
        {
            var settings = Mod.GetInstance().GetSettings();
            if (EntityManager.HasComponent<TransportStop>(entity))
            {
                return settings.TransportStopAutoNaming;
            }

            // Get the newly created building's type.
            // (Although we still use "NameSource" here, it's not a good idea, but it works.)
            var nameSource = NameUtils.TryGetBuildingSource(entity, EntityManager);
            if (!nameSource.CouldNaming())
            {
                return false;
            }

            return CheckSetting(nameSource);
        }

        private static bool CheckSetting(NameSource nameSource)
        {
            var settings = Mod.GetInstance().GetSettings();
            return nameSource switch
            {
                NameSource.TransportStation => settings.TransportStationAutoNaming,
                NameSource.TransportDepot => settings.TransportDepotAutoNaming,
                NameSource.FireStation => settings.FireStationAutoNaming,
                NameSource.PoliceStation => settings.PoliceStationAutoNaming,
                NameSource.School => settings.SchoolAutoNaming,
                NameSource.Hospital => settings.HospitalAutoNaming,
                NameSource.Park => settings.ParkAutoNaming,
                NameSource.Electricity => settings.ElectricityAutoNaming,
                NameSource.Water => settings.WaterAutoNaming,
                NameSource.Sewage => settings.SewageAutoNaming,
                NameSource.Garbage => settings.GarbageAutoNaming,
                NameSource.Deathcare => settings.DeathcareAutoNaming,
                NameSource.Telecom => settings.TelecomAutoNaming,
                NameSource.Parking => settings.ParkingAutoNaming,
                NameSource.Admin => settings.AdminAutoNaming,
                NameSource.RoadFacility => settings.RoadFacilityAutoNaming,
                _ => false
            };
        }

        protected override void OnCreate()
        {
            base.OnCreate();

            _createdQuery = GetEntityQuery(new EntityQueryDesc
            {
                All = new[]
                {
                    ComponentType.ReadOnly<Created>()
                },
                Any = new[]
                {
                    ComponentType.ReadOnly<TransportStop>(),
                    ComponentType.ReadOnly<TransportStation>(),
                    ComponentType.ReadOnly<Building>()
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