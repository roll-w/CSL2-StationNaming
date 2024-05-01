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
using System.Reflection;
using Colossal.Localization;
using Game.Buildings;
using Game.Prefabs;
using Game.SceneFlow;
using Game.UI;
using StationNaming.Setting;
using Unity.Entities;
using DeathcareFacility = Game.Buildings.DeathcareFacility;
using DisasterFacility = Game.Buildings.DisasterFacility;
using FireStation = Game.Buildings.FireStation;
using GarbageFacility = Game.Buildings.GarbageFacility;
using Hospital = Game.Buildings.Hospital;
using Park = Game.Buildings.Park;
using ParkingFacility = Game.Buildings.ParkingFacility;
using PoliceStation = Game.Buildings.PoliceStation;
using PostFacility = Game.Buildings.PostFacility;
using School = Game.Buildings.School;
using SewageOutlet = Game.Buildings.SewageOutlet;
using TelecomFacility = Game.Buildings.TelecomFacility;
using TransportDepot = Game.Buildings.TransportDepot;
using TransportStation = Game.Buildings.TransportStation;
using TransportStop = Game.Routes.TransportStop;
using WaterPumpingStation = Game.Buildings.WaterPumpingStation;

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
        { typeof(ElectricityProducer), NameSource.Electricity },
        { typeof(SewageOutlet), NameSource.Sewage },
        { typeof(WaterPumpingStation), NameSource.Water },
        { typeof(GarbageFacility), NameSource.CityService },
        { typeof(DisasterFacility), NameSource.CityService },
        { typeof(DeathcareFacility), NameSource.CityService },
        { typeof(TelecomFacility), NameSource.CityService },
        { typeof(PostFacility), NameSource.CityService },
        { typeof(ParkingFacility), NameSource.CityService },
        { typeof(Building), NameSource.Building }
    };

    private static readonly Dictionary<Type, NameSource> PrefabToNameSource = new()
    {
        { typeof(SpawnableBuildingData), NameSource.SpawnableBuilding }
    };

    public static NameSource TryGetBuildingSource(Entity target, EntityManager entityManager)
    {
        foreach (var pair in ComponentToNameSource)
        {
            if (!entityManager.HasComponent(target, pair.Key))
            {
                continue;
            }

            if (pair.Value != NameSource.Building)
            {
                return pair.Value;
            }

            var source = TryGetBuildingSourceByPrefab(target, entityManager);
            if (source != NameSource.None && source != NameSource.Unknown)
            {
                return source;
            }

            return pair.Value;
        }

        return NameSource.Unknown;
    }

    private static NameSource TryGetBuildingSourceByPrefab(Entity target, EntityManager entityManager)
    {
        if (!entityManager.HasComponent<PrefabRef>(target))
        {
            return NameSource.None;
        }

        var prefab = entityManager.GetComponentData<PrefabRef>(target)
            .m_Prefab;
        if (prefab == Entity.Null)
        {
            return NameSource.None;
        }

        foreach (var pair in PrefabToNameSource)
        {
            if (entityManager.HasComponent(prefab, pair.Key))
            {
                return pair.Value;
            }
        }

        return NameSource.None;
    }

    public static TargetType GetTargetType(Entity target, EntityManager entityManager)
    {
        if (entityManager.HasComponent<TransportStop>(target))
        {
            return TargetType.Stop;
        }

        // TODO: add more target types
        return TargetType.None;
    }

    private static readonly FieldInfo NameTypeField = typeof(NameSystem.Name)
        .GetField("m_NameType",
            BindingFlags.NonPublic | BindingFlags.Instance);

    private static readonly FieldInfo NameIDField = typeof(NameSystem.Name)
        .GetField("m_NameID",
            BindingFlags.NonPublic | BindingFlags.Instance);

    private static readonly FieldInfo NameArgsField = typeof(NameSystem.Name)
        .GetField("m_NameArgs",
            BindingFlags.NonPublic | BindingFlags.Instance);

    public static string TryGetRealRenderedName(
        this NameSystem nameSystem,
        Entity entity)
    {
        try
        {
            var nameByReflection = TryGetNameByReflection(nameSystem, entity);
            if (!string.IsNullOrEmpty(nameByReflection))
            {
                return nameByReflection;
            }
        }
        catch (Exception e)
        {
            Mod.GetLogger().Warn(e);
        }

        return nameSystem.GetRenderedLabelName(entity);
    }

    private static string TryGetNameByReflection(NameSystem nameSystem, Entity entity)
    {
        var name = nameSystem.GetName(entity);

        if (NameTypeField == null || NameIDField == null || NameArgsField == null)
        {
            return nameSystem.GetRenderedLabelName(entity);
        }

        var nameType = (NameSystem.NameType)NameTypeField.GetValue(name);
        var nameID = (string)NameIDField.GetValue(name);
        var nameArgs = (string[])NameArgsField.GetValue(name);

        var localizationManager = GameManager.instance.localizationManager;

        switch (nameType)
        {
            case NameSystem.NameType.Custom:
                return nameID;
            case NameSystem.NameType.Localized:
            {
                if (localizationManager
                    .activeDictionary
                    .TryGetValue(nameID, out var localized))
                {
                    return localized;
                }

                break;
            }
            case NameSystem.NameType.Formatted:
            {
                return FormatName(nameID, nameArgs, localizationManager);
            }
        }

        return string.Empty;
    }

    private static string GetFormatOf(string nameKey)
    {
        var settings = Mod.GetInstance().GetSettings();
        return nameKey switch
        {
            GameLocalizationKeys.AddressNameFormat => settings.AddressNameFormat,
            GameLocalizationKeys.NamedAddressNameFormat => settings.NamedAddressNameFormat,
            _ => nameKey
        };
    }

    private const string KeyNumber = "NUMBER";

    /**
     * Formats a localized name with arguments.
     */
    private static string FormatName(
        string nameID,
        string[] nameArgs,
        LocalizationManager localizationManager)
    {
        if (!localizationManager.activeDictionary
                .TryGetValue(nameID, out var localized))
        {
            return string.Empty;
        }

        if (nameArgs.Length % 2 != 0)
        {
            return localized;
        }

        localized = GetFormatOf(localized);

        // e.g.: nameID="{NAME}, {NUMBER}"
        // nameArgs=["NAME", "ExampleName", "NUMBER", "1"]
        // => "ExampleName, 1"
        for (var i = 0; i < nameArgs.Length; i += 2)
        {
            if (i + 1 >= nameArgs.Length)
            {
                break;
            }

            var key = nameArgs[i];
            var value = GetNameArgLocalized(
                localizationManager, key, nameArgs[i + 1]
            );

            localized = localized.Replace($"{{{key}}}", value);
        }

        return localized;
    }

    private static string GetNameArgLocalized(
        LocalizationManager localizationManager,
        string key,
        string value)
    {
        if (key == KeyNumber)
        {
            return value;
        }

        return localizationManager.GetLocalized(value);
    }

    public static string GetLocalized(
        this LocalizationManager localizationManager,
        string key)
    {
        return localizationManager.activeDictionary
            .TryGetValue(key, out var localized)
            ? localized
            : key;
    }
}