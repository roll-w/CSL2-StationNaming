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

using Colossal.IO.AssetDatabase;
using Game.Modding;
using Game.SceneFlow;
using Game.Settings;
using Game.UI.Localization;
using Game.UI.Widgets;
using StationNaming.System;

namespace StationNaming.Setting;

[FileLocation(Mod.Name)]
[SettingsUITabOrder(
    SectionGeneral, SectionSources, SectionTargets
)]
[SettingsUIShowGroupName(
    GroupStops, GroupExperimental, GroupBuilding,
    GroupSpawnable, GroupDistrict, GroupTransport,
    GroupSchool, GroupFireStation, GroupPoliceStation,
    GroupHospital, GroupPark, GroupElectricity,
    GroupWater, GroupSewage, GroupGarbage,
    GroupDisaster, GroupDeathcare, GroupTelecom,
    GroupPost, GroupParking, GroupCityService,
    GroupOther
)]
[SettingsUIGroupOrder(
    GroupStable, GroupStops, GroupBuilding,
    GroupSpawnable, GroupDistrict, GroupTransport,
    GroupSchool, GroupFireStation, GroupPoliceStation,
    GroupHospital, GroupPark, GroupElectricity,
    GroupWater, GroupSewage, GroupGarbage,
    GroupDisaster, GroupDeathcare, GroupTelecom,
    GroupPost, GroupParking, GroupCityService,
    GroupExperimental, GroupOther
)]
public class StationNamingSettings(IMod mod) : ModSetting(mod)
{
    public const string GroupStable = "Stable";

    public const string GroupExperimental = "Experimental";

    public const string GroupOther = "Other";

    public const string GroupBuilding = "Building";

    public const string GroupSpawnable = "Spawnable";

    public const string GroupDistrict = "District";

    public const string GroupStops = "Stops";

    public const string GroupSchool = "School";

    public const string GroupTransport = "Transport";

    public const string GroupFireStation = "FireStation";

    public const string GroupPoliceStation = "PoliceStation";

    public const string GroupHospital = "Hospital";

    public const string GroupPark = "Park";

    public const string GroupElectricity = "Electricity";

    public const string GroupWater = "Water";

    public const string GroupSewage = "Sewage";

    public const string GroupGarbage = "Garbage";

    public const string GroupDisaster = "Disaster";

    public const string GroupDeathcare = "Deathcare";

    public const string GroupTelecom = "Telecom";

    public const string GroupPost = "Post";

    public const string GroupParking = "Parking";

    public const string GroupCityService = "CityService";

    public const string SectionGeneral = "General";

    public const string SectionSources = "Sources";

    public const string SectionTargets = "Targets";

    [SettingsUISection(SectionGeneral, GroupStable)]
    public bool Enable { get; set; } = true;

    [SettingsUISection(SectionGeneral, GroupStable)]
    [SettingsUITextInput]
    public string NamingSeparator { get; set; } = " & ";

    [SettingsUISection(SectionSources, GroupStable)]
    [SettingsUIDropdown(typeof(RoadNamingProvider), nameof(RoadNamingProvider.GetNameFormatOptions))]
    public NameFormat RoadFormat { get; set; } = NameFormat.Invalid;

    [SettingsUISection(SectionGeneral, GroupStable)]
    public bool ReverseRoadOrder { get; set; } = false;

    [SettingsUISection(SectionGeneral, GroupStable)]
    [SettingsUISlider(max = 5, min = 1)]
    public int SearchDepth { get; set; } = 2;

    [SettingsUISection(SectionGeneral, GroupStable)]
    [SettingsUITextInput]
    public string Prefix { get; set; } = "";

    [SettingsUISection(SectionGeneral, GroupStable)]
    [SettingsUITextInput]
    public string Suffix { get; set; } = "";

    [SettingsUISection(SectionGeneral, GroupStable)]
    public bool AutoUpdate { get; set; } = true;

    [SettingsUISection(SectionGeneral, GroupStable)]
    public bool AutoNaming { get; set; } = true;

    /// <summary>
    /// Should apply prefix and suffix to stops.
    /// </summary>
    [SettingsUISection(SectionTargets, GroupTransport)]
    public bool ApplyXfixToStops { get; set; } = false;

    public bool IsNotApplyXfixToStops() => !ApplyXfixToStops;

    [SettingsUITextInput]
    [SettingsUISection(SectionTargets, GroupTransport)]
    [SettingsUIDisableByCondition(typeof(StationNamingSettings), nameof(IsNotApplyXfixToStops))]
    public string StopPrefix { get; set; } = "";

    [SettingsUITextInput]
    [SettingsUISection(SectionTargets, GroupTransport)]
    [SettingsUIDisableByCondition(typeof(StationNamingSettings), nameof(IsNotApplyXfixToStops))]
    public string StopSuffix { get; set; } = "";

    [SettingsUISection(SectionSources, GroupDistrict)]
    public bool EnableDistrict { get; set; } = true;

    [SettingsUISection(SectionSources, GroupDistrict)]
    [SettingsUIDisableByCondition(typeof(StationNamingSettings), nameof(IsDistrictDisabled))]
    public bool EnableDistrictPrefix { get; set; } = false;

    [SettingsUISection(SectionSources, GroupDistrict)]
    [SettingsUIDropdown(typeof(RoadNamingProvider), nameof(RoadNamingProvider.GetNameFormatOptions))]
    [SettingsUIDisableByCondition(typeof(StationNamingSettings), nameof(IsDistrictDisabled))]
    public NameFormat DistrictFormat { get; set; } = NameFormat.Invalid;

    public bool IsDistrictDisabled() => !EnableDistrict;

    [SettingsUIButton]
    [SettingsUIConfirmation]
    [SettingsUISection(SectionGeneral, GroupOther)]
    public bool ResetSettings
    {
        set
        {
            SetDefaults();
            ApplyAndSave();
        }
    }

    [SettingsUIButton]
    [SettingsUIConfirmation]
    [SettingsUISection(SectionTargets, GroupOther)]
    public bool DisableCityServiceAutoNaming
    {
        set
        {
            SetDisableCityServiceAutoNaming();
            ApplyAndSave();
        }
    }

    [SettingsUISection(SectionSources, GroupBuilding)]
    public bool BuildingName { get; set; } = true;

    public bool IsBuildingNameDisabled() => !BuildingName;

    [SettingsUISection(SectionSources, GroupBuilding)]
    [SettingsUIDisableByCondition(typeof(StationNamingSettings), nameof(IsBuildingNameDisabled))]
    public bool BuildingNameWithCurrentRoad { get; set; } = true;

    [SettingsUISection(SectionSources, GroupSpawnable)]
    [SettingsUIDisableByCondition(typeof(StationNamingSettings), nameof(IsBuildingNameDisabled))]
    public bool SpawnableBuildingName { get; set; } = false;

    private string _addressNameFormat = "{NUMBER} {ROAD}";

    [SettingsUISection(SectionSources, GroupSpawnable)]
    [SettingsUITextInput]
    public string AddressNameFormat
    {
        get => _addressNameFormat;
        set
        {
            _addressNameFormat = value;
            ApplyAddressFormats();
        }
    }

    private string _namedAddressNameFormat = "{NAME}, {NUMBER} {ROAD}";

    [SettingsUISection(SectionSources, GroupSpawnable)]
    [SettingsUITextInput]
    public string NamedAddressNameFormat
    {
        get => _namedAddressNameFormat;
        set
        {
            _namedAddressNameFormat = value;
            ApplyAddressFormats();
        }
    }

    private bool _overrideVanillaAddress = true;

    [SettingsUISection(SectionSources, GroupSpawnable)]
    public bool OverrideVanillaAddress
    {
        get => _overrideVanillaAddress;
        set
        {
            _overrideVanillaAddress = value;
            if (!value)
            {
                ApplyAddressFormats(true);
            }
            else
            {
                ApplyAddressFormats();
            }
        }
    }

    public void ApplyAddressFormats(bool resetVanilla = false)
    {
        var localizationManager = GameManager.instance.localizationManager;
        if (resetVanilla)
        {
            localizationManager.activeDictionary.Add(
                GameLocalizationKeys.AddressNameFormat,
                "{NUMBER} {ROAD}", true
            );
            localizationManager.activeDictionary.Add(
                GameLocalizationKeys.NamedAddressNameFormat,
                "{NAME}, {NUMBER} {ROAD}", true
            );
            return;
        }

        if (!OverrideVanillaAddress)
        {
            return;
        }

        localizationManager.activeDictionary.Add(
            GameLocalizationKeys.AddressNameFormat,
            AddressNameFormat, true
        );
        localizationManager.activeDictionary.Add(
            GameLocalizationKeys.NamedAddressNameFormat,
            NamedAddressNameFormat, true
        );
    }

    public bool IsAutoNamingDisabled() => !AutoNaming;

    [SettingsUISection(SectionTargets, GroupTransport)]
    [SettingsUIDisableByCondition(typeof(StationNamingSettings), nameof(IsAutoNamingDisabled))]
    public bool TransportStopAutoNaming { get; set; } = true;

    [SettingsUISection(SectionTargets, GroupTransport)]
    [SettingsUIDisableByCondition(typeof(StationNamingSettings), nameof(IsAutoNamingDisabled))]
    public bool TransportStationAutoNaming { get; set; } = true;

    [SettingsUISection(SectionTargets, GroupTransport)]
    [SettingsUIDisableByCondition(typeof(StationNamingSettings), nameof(IsAutoNamingDisabled))]
    public bool TransportDepotAutoNaming { get; set; } = true;

    [SettingsUISection(SectionTargets, GroupSchool)]
    [SettingsUIDisableByCondition(typeof(StationNamingSettings), nameof(IsAutoNamingDisabled))]
    public bool SchoolAutoNaming { get; set; } = true;

    [SettingsUISection(SectionTargets, GroupFireStation)]
    [SettingsUIDisableByCondition(typeof(StationNamingSettings), nameof(IsAutoNamingDisabled))]
    public bool FireStationAutoNaming { get; set; } = true;

    [SettingsUISection(SectionTargets, GroupPoliceStation)]
    [SettingsUIDisableByCondition(typeof(StationNamingSettings), nameof(IsAutoNamingDisabled))]
    public bool PoliceStationAutoNaming { get; set; } = true;

    [SettingsUISection(SectionTargets, GroupHospital)]
    [SettingsUIDisableByCondition(typeof(StationNamingSettings), nameof(IsAutoNamingDisabled))]
    public bool HospitalAutoNaming { get; set; } = true;

    [SettingsUISection(SectionTargets, GroupPark)]
    [SettingsUIDisableByCondition(typeof(StationNamingSettings), nameof(IsAutoNamingDisabled))]
    public bool ParkAutoNaming { get; set; } = true;

    [SettingsUISection(SectionTargets, GroupElectricity)]
    [SettingsUIDisableByCondition(typeof(StationNamingSettings), nameof(IsAutoNamingDisabled))]
    public bool ElectricityAutoNaming { get; set; } = true;

    [SettingsUISection(SectionTargets, GroupWater)]
    [SettingsUIDisableByCondition(typeof(StationNamingSettings), nameof(IsAutoNamingDisabled))]
    public bool WaterAutoNaming { get; set; } = true;

    [SettingsUISection(SectionTargets, GroupSewage)]
    [SettingsUIDisableByCondition(typeof(StationNamingSettings), nameof(IsAutoNamingDisabled))]
    public bool SewageAutoNaming { get; set; } = true;

    [SettingsUISection(SectionTargets, GroupGarbage)]
    [SettingsUIDisableByCondition(typeof(StationNamingSettings), nameof(IsAutoNamingDisabled))]
    public bool GarbageAutoNaming { get; set; } = true;

    [SettingsUISection(SectionTargets, GroupDisaster)]
    [SettingsUIDisableByCondition(typeof(StationNamingSettings), nameof(IsAutoNamingDisabled))]
    public bool DisasterAutoNaming { get; set; } = true;

    [SettingsUISection(SectionTargets, GroupDeathcare)]
    [SettingsUIDisableByCondition(typeof(StationNamingSettings), nameof(IsAutoNamingDisabled))]
    public bool DeathcareAutoNaming { get; set; } = true;

    [SettingsUISection(SectionTargets, GroupTelecom)]
    [SettingsUIDisableByCondition(typeof(StationNamingSettings), nameof(IsAutoNamingDisabled))]
    public bool TelecomAutoNaming { get; set; } = true;

    [SettingsUISection(SectionTargets, GroupPost)]
    [SettingsUIDisableByCondition(typeof(StationNamingSettings), nameof(IsAutoNamingDisabled))]
    public bool PostAutoNaming { get; set; } = true;

    [SettingsUISection(SectionTargets, GroupParking)]
    [SettingsUIDisableByCondition(typeof(StationNamingSettings), nameof(IsAutoNamingDisabled))]
    public bool ParkingAutoNaming { get; set; } = true;

    [SettingsUISection(SectionTargets, GroupCityService)]
    [SettingsUIDisableByCondition(typeof(StationNamingSettings), nameof(IsAutoNamingDisabled))]
    public bool RoadFacilityAutoNaming { get; set; } = true;

    [SettingsUISection(SectionTargets, GroupCityService)]
    [SettingsUIDisableByCondition(typeof(StationNamingSettings), nameof(IsAutoNamingDisabled))]
    public bool AdminAutoNaming { get; set; } = true;

    public override void SetDefaults()
    {
        Enable = true;
        NamingSeparator = " & ";

        ReverseRoadOrder = false;
        SearchDepth = 2;
        Prefix = "";
        Suffix = "";
        AutoUpdate = true;
        AutoNaming = true;

        BuildingName = true;
        BuildingNameWithCurrentRoad = true;
        SpawnableBuildingName = false;

        ApplyXfixToStops = false;
        StopPrefix = "";
        StopSuffix = "";

        AddressNameFormat = "{NUMBER} {ROAD}";
        NamedAddressNameFormat = "{NAME}, {NUMBER} {ROAD}";
        OverrideVanillaAddress = true;

        EnableDistrict = true;
        EnableDistrictPrefix = false;

        RoadFormat = NameFormat.Invalid;
        DistrictFormat = NameFormat.Invalid;

        TransportStopAutoNaming = true;
        TransportStationAutoNaming = true;
        TransportDepotAutoNaming = true;
        SchoolAutoNaming = true;
        FireStationAutoNaming = true;
        PoliceStationAutoNaming = true;
        HospitalAutoNaming = true;
        ParkAutoNaming = true;
        ElectricityAutoNaming = true;
        WaterAutoNaming = true;
        SewageAutoNaming = true;
        GarbageAutoNaming = true;
        DisasterAutoNaming = true;
        DeathcareAutoNaming = true;
        TelecomAutoNaming = true;
        PostAutoNaming = true;
        ParkingAutoNaming = true;
        RoadFacilityAutoNaming = true;
        AdminAutoNaming = true;
    }

    public void SetDisableCityServiceAutoNaming()
    {
        TransportStationAutoNaming = false;
        TransportDepotAutoNaming = false;
        SchoolAutoNaming = false;
        FireStationAutoNaming = false;
        PoliceStationAutoNaming = false;
        HospitalAutoNaming = false;
        ParkAutoNaming = false;
        ElectricityAutoNaming = false;
        WaterAutoNaming = false;
        SewageAutoNaming = false;
        GarbageAutoNaming = false;
        DisasterAutoNaming = false;
        DeathcareAutoNaming = false;
        TelecomAutoNaming = false;
        PostAutoNaming = false;
        ParkingAutoNaming = false;
        RoadFacilityAutoNaming = false;
        AdminAutoNaming = false;
    }

    public NameOptions ToNameOptions()
    {
        var options = new NameOptions
        {
            BuildingName = BuildingName,
            BuildingNameWithCurrentRoad = BuildingName && BuildingNameWithCurrentRoad,
            SpawnableBuildingName = BuildingName && SpawnableBuildingName,
            EnableDistrict = EnableDistrict,
            EnableDistrictPrefix = EnableDistrict && EnableDistrictPrefix,
            SourceFormats =
            {
                DefaultFormat = new NameFormat
                {
                    Separator = NamingSeparator
                },
                [NameSource.District] = DistrictFormat,
                [NameSource.Road] = RoadFormat
            },

            TargetFormats =
            {
                DefaultFormat = new NameFormat
                {
                    Suffix = Suffix,
                    Prefix = Prefix,
                    Separator = ""
                }
            }
        };

        if (ApplyXfixToStops)
        {
            options.TargetFormats[TargetType.Stop] = new NameFormat
            {
                Prefix = StopPrefix,
                Suffix = StopSuffix
            };
        }

        return options;
    }

    public static class RoadNamingProvider
    {
        public static DropdownItem<NameFormat>[] GetNameFormatOptions()
        {
            return
            [
                new DropdownItem<NameFormat> { value = NameFormat.Invalid, displayName = "None" },
                new DropdownItem<NameFormat> { value = new NameFormat { Separator = " " }, displayName = "Space" },
                new DropdownItem<NameFormat> { value = new NameFormat { Separator = " - " }, displayName = "Hyphen" },
            ];
        }
    }
}