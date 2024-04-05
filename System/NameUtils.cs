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

using System;
using System.Collections.Generic;
using Game.Buildings;
using Unity.Entities;

namespace StationNaming.System;

public static class NameUtils
{
    private static readonly Dictionary<Type, NameSource> ComponentToNameSource = new()
    {
        { typeof(Signature), NameSource.SignatureBuilding },
        { typeof(TransportDepot), NameSource.TransportDepot },
        { typeof(Park), NameSource.Park },
        { typeof(School), NameSource.School },
        { typeof(FireStation), NameSource.FireStation },
        { typeof(PoliceStation), NameSource.PoliceStation },
        { typeof(Hospital), NameSource.Hospital },
        { typeof(TransportStation), NameSource.TransportStation },
        { typeof(GarbageFacility), NameSource.CityService },
        { typeof(DisasterFacility), NameSource.CityService },
        { typeof(DeathcareFacility), NameSource.CityService },
        { typeof(TelecomFacility), NameSource.CityService },
        { typeof(Building), NameSource.Building }
    };

    public static NameSource TryGetBuildingSource(Entity target, EntityManager entityManager)
    {
        foreach (var pair in ComponentToNameSource)
        {
            if (entityManager.HasComponent(target, pair.Key))
            {
                return pair.Value;
            }
        }

        return NameSource.Unknown;
    }
}