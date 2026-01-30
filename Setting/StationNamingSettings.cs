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
using Colossal.IO.AssetDatabase;
using Game.Modding;
using Game.SceneFlow;
using Game.Settings;
using Game.UI.Widgets;
using StationNaming.System;

namespace StationNaming.Setting
{
    [FileLocation(Mod.Name)]
    [SettingsUITabOrder(
        SectionGeneral, SectionSources, SectionTargets
    )]
    [SettingsUIShowGroupName(
        GroupStops, GroupExperimental, GroupBuilding,
        GroupSpawnable, GroupDistrict, GroupRoad, GroupTransport,
        GroupSchool, GroupFireStation, GroupPoliceStation,
        GroupHospital, GroupPark, GroupElectricity,
        GroupWater, GroupSewage, GroupGarbage,
        GroupDisaster, GroupDeathcare, GroupTelecom,
        GroupPost, GroupParking, GroupCityService,
        GroupOther
    )]
    [SettingsUIGroupOrder(
        GroupStable, GroupStops, GroupBuilding,
        GroupSpawnable, GroupDistrict, GroupRoad, GroupTransport,
        GroupSchool, GroupFireStation, GroupPoliceStation,
        GroupHospital, GroupPark, GroupElectricity,
        GroupWater, GroupSewage, GroupGarbage,
        GroupDisaster, GroupDeathcare, GroupTelecom,
        GroupPost, GroupParking, GroupCityService,
        GroupExperimental, GroupOther
    )]
    public class StationNamingSettings : ModSetting
    {
        public StationNamingSettings(IMod mod) : base(mod)
        {
        }

        public const string GroupStable = "Stable";

        public const string GroupExperimental = "Experimental";

        public const string GroupOther = "Other";

        public const string GroupBuilding = "Building";

        public const string GroupSpawnable = "Spawnable";

        public const string GroupDistrict = "District";

        public const string GroupRoad = "Road";

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

        public bool IsAutoNamingDisabled() => !AutoNaming;

        [SettingsUISection(SectionSources, GroupDistrict)]
        public bool EnableDistrict { get; set; } = true;

        [SettingsUISection(SectionSources, GroupDistrict)]
        [SettingsUIDisableByCondition(typeof(StationNamingSettings), nameof(IsDistrictDisabled))]
        public bool EnableDistrictPrefix { get; set; } = false;

        public bool IsDistrictPrefixDisabled() => !EnableDistrict || !EnableDistrictPrefix;

        /**
         * If true, list candidates with added district prefix separately.
         */
        [SettingsUISection(SectionSources, GroupDistrict)]
        [SettingsUIDisableByCondition(typeof(StationNamingSettings), nameof(IsDistrictPrefixDisabled))]
        public bool DistrictPrefixSeparately { get; set; } = false;

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
        [SettingsUISection(SectionSources, GroupOther)]
        public bool EnableAllSources
        {
            set
            {
                SetSources(true);
                ApplyAndSave();
            }
        }

        [SettingsUIButton]
        [SettingsUIConfirmation]
        [SettingsUISection(SectionSources, GroupOther)]
        public bool DisableAllSources
        {
            set
            {
                SetSources(false);
                ApplyAndSave();
            }
        }

        [SettingsUIButton]
        [SettingsUIConfirmation]
        [SettingsUISection(SectionTargets, GroupOther)]
        public bool EnableCityServiceAutoNaming
        {
            set
            {
                SetCityServiceAutoNaming(true);
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
                SetCityServiceAutoNaming(false);
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

        [SettingsUISection(SectionSources, GroupRoad)]
        public bool RoadSource { get; set; } = true;

        [SettingsUISection(SectionSources, GroupRoad)]
        public bool IntersectionSource  { get; set; } = true;

        [SettingsUISection(SectionSources, GroupTransport)]
        [SettingsUIDisableByCondition(typeof(StationNamingSettings), nameof(IsBuildingNameDisabled))]
        public bool TransportStationSource { get; set; } = true;

        [SettingsUISection(SectionSources, GroupTransport)]
        [SettingsUIDisableByCondition(typeof(StationNamingSettings), nameof(IsBuildingNameDisabled))]
        public bool TransportDepotSource { get; set; } = true;

        [SettingsUISection(SectionSources, GroupSchool)]
        [SettingsUIDisableByCondition(typeof(StationNamingSettings), nameof(IsBuildingNameDisabled))]
        public bool SchoolSource { get; set; } = true;

        [SettingsUISection(SectionSources, GroupFireStation)]
        [SettingsUIDisableByCondition(typeof(StationNamingSettings), nameof(IsBuildingNameDisabled))]
        public bool FireStationSource { get; set; } = true;

        [SettingsUISection(SectionSources, GroupPoliceStation)]
        [SettingsUIDisableByCondition(typeof(StationNamingSettings), nameof(IsBuildingNameDisabled))]
        public bool PoliceStationSource { get; set; } = true;

        [SettingsUISection(SectionSources, GroupHospital)]
        [SettingsUIDisableByCondition(typeof(StationNamingSettings), nameof(IsBuildingNameDisabled))]
        public bool HospitalSource { get; set; } = true;

        [SettingsUISection(SectionSources, GroupPark)]
        [SettingsUIDisableByCondition(typeof(StationNamingSettings), nameof(IsBuildingNameDisabled))]
        public bool ParkSource { get; set; } = true;

        [SettingsUISection(SectionSources, GroupElectricity)]
        [SettingsUIDisableByCondition(typeof(StationNamingSettings), nameof(IsBuildingNameDisabled))]
        public bool ElectricitySource { get; set; } = true;

        [SettingsUISection(SectionSources, GroupWater)]
        [SettingsUIDisableByCondition(typeof(StationNamingSettings), nameof(IsBuildingNameDisabled))]
        public bool WaterSource { get; set; } = true;

        [SettingsUISection(SectionSources, GroupSewage)]
        [SettingsUIDisableByCondition(typeof(StationNamingSettings), nameof(IsBuildingNameDisabled))]
        public bool SewageSource { get; set; } = true;

        [SettingsUISection(SectionSources, GroupGarbage)]
        [SettingsUIDisableByCondition(typeof(StationNamingSettings), nameof(IsBuildingNameDisabled))]
        public bool GarbageSource { get; set; } = true;

        [SettingsUISection(SectionSources, GroupDisaster)]
        [SettingsUIDisableByCondition(typeof(StationNamingSettings), nameof(IsBuildingNameDisabled))]
        public bool DisasterSource { get; set; } = true;

        [SettingsUISection(SectionSources, GroupDeathcare)]
        [SettingsUIDisableByCondition(typeof(StationNamingSettings), nameof(IsBuildingNameDisabled))]
        public bool DeathcareSource { get; set; } = true;

        [SettingsUISection(SectionSources, GroupTelecom)]
        [SettingsUIDisableByCondition(typeof(StationNamingSettings), nameof(IsBuildingNameDisabled))]
        public bool TelecomSource { get; set; } = true;

        [SettingsUISection(SectionSources, GroupPost)]
        [SettingsUIDisableByCondition(typeof(StationNamingSettings), nameof(IsBuildingNameDisabled))]
        public bool PostSource { get; set; } = true;

        [SettingsUISection(SectionSources, GroupParking)]
        [SettingsUIDisableByCondition(typeof(StationNamingSettings), nameof(IsBuildingNameDisabled))]
        public bool ParkingSource { get; set; } = true;

        [SettingsUISection(SectionSources, GroupCityService)]
        [SettingsUIDisableByCondition(typeof(StationNamingSettings), nameof(IsBuildingNameDisabled))]
        public bool RoadFacilitySource { get; set; } = true;

        [SettingsUISection(SectionSources, GroupCityService)]
        [SettingsUIDisableByCondition(typeof(StationNamingSettings), nameof(IsBuildingNameDisabled))]
        public bool AdminSource { get; set; } = true;

        private NameSource _stopNameSourcePriority1 = NameSource.Owner;
        private NameSource _stopNameSourcePriority2 = NameSource.Intersection;
        private NameSource _stopNameSourcePriority3 = NameSource.Road;

        private static bool IsAllowedDefaultSource(NameSource source) =>
            source == NameSource.None
            || source == NameSource.Owner
            || source == NameSource.Road
            || source == NameSource.Intersection
            || source == NameSource.District;

        public bool IsStopDefaultSourceDisabled() => !AutoNaming || !TransportStopAutoNaming;

        [SettingsUISection(SectionTargets, GroupTransport)]
        [SettingsUIDisableByCondition(typeof(StationNamingSettings), nameof(IsStopDefaultSourceDisabled))]
        [SettingsUIDropdown(typeof(DefaultAutoNamingSourceProvider), nameof(DefaultAutoNamingSourceProvider.GetOptions))]
        [SettingsUIAdvanced]
        public int StopNameSourcePriority1
        {
            get => (int)_stopNameSourcePriority1;
            set
            {
                var source = (NameSource)value;
                _stopNameSourcePriority1 = IsAllowedDefaultSource(source) ? source : NameSource.None;
            }
        }

        [SettingsUISection(SectionTargets, GroupTransport)]
        [SettingsUIDisableByCondition(typeof(StationNamingSettings), nameof(IsStopDefaultSourceDisabled))]
        [SettingsUIDropdown(typeof(DefaultAutoNamingSourceProvider), nameof(DefaultAutoNamingSourceProvider.GetOptions))]
        [SettingsUIAdvanced]
        public int StopNameSourcePriority2
        {
            get => (int)_stopNameSourcePriority2;
            set
            {
                var source = (NameSource)value;
                _stopNameSourcePriority2 = IsAllowedDefaultSource(source) ? source : NameSource.None;
            }
        }

        [SettingsUISection(SectionTargets, GroupTransport)]
        [SettingsUIDisableByCondition(typeof(StationNamingSettings), nameof(IsStopDefaultSourceDisabled))]
        [SettingsUIDropdown(typeof(DefaultAutoNamingSourceProvider), nameof(DefaultAutoNamingSourceProvider.GetOptions))]
        [SettingsUIAdvanced]
        public int StopNameSourcePriority3
        {
            get => (int)_stopNameSourcePriority3;
            set
            {
                var source = (NameSource)value;
                _stopNameSourcePriority3 = IsAllowedDefaultSource(source) ? source : NameSource.None;
            }
        }

        public NameSource[] GetStopNameSourcePriorities()
        {
            var list = new List<NameSource>();
            if (_stopNameSourcePriority1 != NameSource.None) list.Add(_stopNameSourcePriority1);
            if (_stopNameSourcePriority2 != NameSource.None) list.Add(_stopNameSourcePriority2);
            if (_stopNameSourcePriority3 != NameSource.None) list.Add(_stopNameSourcePriority3);
            return list.ToArray();
        }

        [SettingsUISection(SectionTargets, GroupTransport)]
        [SettingsUIDisableByCondition(typeof(StationNamingSettings), nameof(IsAutoNamingDisabled))]
        public bool TransportStopAutoNaming { get; set; } = true;

        /// <summary>
        /// Should apply prefix and suffix to stops.
        /// </summary>
        [SettingsUISection(SectionTargets, GroupTransport)]
        [SettingsUIAdvanced]
        public bool ApplyXfixToStops { get; set; } = false;

        public bool IsNotApplyXfixToStops() => !ApplyXfixToStops;

        [SettingsUITextInput]
        [SettingsUISection(SectionTargets, GroupTransport)]
        [SettingsUIDisableByCondition(typeof(StationNamingSettings), nameof(IsNotApplyXfixToStops))]
        [SettingsUIAdvanced]
        public string StopPrefix { get; set; } = "";

        [SettingsUITextInput]
        [SettingsUISection(SectionTargets, GroupTransport)]
        [SettingsUIDisableByCondition(typeof(StationNamingSettings), nameof(IsNotApplyXfixToStops))]
        [SettingsUIAdvanced]
        public string StopSuffix { get; set; } = "";

        [SettingsUISection(SectionTargets, GroupTransport)]
        [SettingsUIDisableByCondition(typeof(StationNamingSettings), nameof(IsAutoNamingDisabled))]
        public bool TransportStationAutoNaming { get; set; } = true;

        /// <summary>
        /// Should apply prefix and suffix to stations.
        /// </summary>
        [SettingsUISection(SectionTargets, GroupTransport)]
        [SettingsUIAdvanced]
        public bool ApplyXfixToTransportStation { get; set; } = false;

        public bool IsNotApplyXfixToTransportStation() => !ApplyXfixToTransportStation;

        [SettingsUITextInput]
        [SettingsUISection(SectionTargets, GroupTransport)]
        [SettingsUIDisableByCondition(typeof(StationNamingSettings), nameof(IsNotApplyXfixToTransportStation))]
        [SettingsUIAdvanced]
        public string TransportStationPrefix { get; set; } = "";

        [SettingsUITextInput]
        [SettingsUISection(SectionTargets, GroupTransport)]
        [SettingsUIDisableByCondition(typeof(StationNamingSettings), nameof(IsNotApplyXfixToTransportStation))]
        [SettingsUIAdvanced]
        public string TransportStationSuffix { get; set; } = "";

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



            AddressNameFormat = "{NUMBER} {ROAD}";
            NamedAddressNameFormat = "{NAME}, {NUMBER} {ROAD}";
            OverrideVanillaAddress = true;

            EnableDistrict = true;
            EnableDistrictPrefix = false;
            DistrictPrefixSeparately = false;

            RoadFormat = NameFormat.Invalid;
            DistrictFormat = NameFormat.Invalid;

            RoadSource = true;
            IntersectionSource = true;
            TransportStationSource = true;
            TransportDepotSource = true;
            SchoolSource = true;
            FireStationSource = true;
            PoliceStationSource = true;
            HospitalSource = true;
            ParkSource = true;
            ElectricitySource = true;
            WaterSource = true;
            SewageSource = true;
            GarbageSource = true;
            DisasterSource = true;
            DeathcareSource = true;
            TelecomSource = true;
            PostSource = true;
            ParkingSource = true;
            RoadFacilitySource = true;
            AdminSource = true;

            TransportStopAutoNaming = true;
            ApplyXfixToStops = false;
            StopPrefix = "";
            StopSuffix = "";

            TransportStationAutoNaming = true;
            ApplyXfixToTransportStation = false;
            TransportStationPrefix = "";
            TransportStationSuffix = "";

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

            StopNameSourcePriority1 = (int)NameSource.Owner;
            StopNameSourcePriority2 = (int)NameSource.Intersection;
            StopNameSourcePriority3 = (int)NameSource.Road;
        }

        public void SetSources(bool enable = false)
        {
            BuildingName = enable;
            EnableDistrict = enable;
            RoadSource = enable;
            IntersectionSource = enable;
            TransportStationSource = enable;
            TransportDepotSource = enable;
            SchoolSource = enable;
            FireStationSource = enable;
            PoliceStationSource = enable;
            HospitalSource = enable;
            ParkSource = enable;
            ElectricitySource = enable;
            WaterSource = enable;
            SewageSource = enable;
            GarbageSource = enable;
            DisasterSource = enable;
            DeathcareSource = enable;
            TelecomSource = enable;
            PostSource = enable;
            ParkingSource = enable;
            RoadFacilitySource = enable;
            AdminSource = enable;
        }

        public void SetCityServiceAutoNaming(bool enable = false)
        {
            TransportStationAutoNaming = enable;
            TransportDepotAutoNaming = enable;
            SchoolAutoNaming = enable;
            FireStationAutoNaming = enable;
            PoliceStationAutoNaming = enable;
            HospitalAutoNaming = enable;
            ParkAutoNaming = enable;
            ElectricityAutoNaming = enable;
            WaterAutoNaming = enable;
            SewageAutoNaming = enable;
            GarbageAutoNaming = enable;
            DisasterAutoNaming = enable;
            DeathcareAutoNaming = enable;
            TelecomAutoNaming = enable;
            PostAutoNaming = enable;
            ParkingAutoNaming = enable;
            RoadFacilityAutoNaming = enable;
            AdminAutoNaming = enable;
        }

        private List<NameSource> GetEnabledSources()
        {
            List<NameSource> sources = new List<NameSource> { NameSource.Owner };
            if (BuildingName)
            {
                sources.AddRange(new[]
                {
                    NameSource.Building,
                    NameSource.SignatureBuilding,
                    NameSource.CityService
                });
            }
            if (RoadSource)
            {
                sources.Add(NameSource.Road);
            }
            if (IntersectionSource)
            {
                sources.Add(NameSource.Intersection);
            }
            if (SpawnableBuildingName)
            {
                sources.Add(NameSource.SpawnableBuilding);
            }
            if (EnableDistrict)
            {
                sources.Add(NameSource.District);
            }
            if (TransportStationSource)
            {
                sources.Add(NameSource.TransportStation);
            }
            if (TransportDepotSource)
            {
                sources.Add(NameSource.TransportDepot);
            }
            if (SchoolSource)
            {
                sources.Add(NameSource.School);
            }
            if (FireStationSource)
            {
                sources.Add(NameSource.FireStation);
            }
            if (PoliceStationSource)
            {
                sources.Add(NameSource.PoliceStation);
            }
            if (HospitalSource)
            {
                sources.Add(NameSource.Hospital);
            }
            if (ParkSource)
            {
                sources.Add(NameSource.Park);
            }
            if (ElectricitySource)
            {
                sources.Add(NameSource.Electricity);
            }
            if (WaterSource)
            {
                sources.Add(NameSource.Water);
            }
            if (SewageSource)
            {
                sources.Add(NameSource.Sewage);
            }
            if (GarbageSource)
            {
                sources.Add(NameSource.Garbage);
            }
            if (DisasterSource)
            {
                sources.Add(NameSource.Disaster);
            }
            if (DeathcareSource)
            {
                sources.Add(NameSource.Deathcare);
            }
            if (TelecomSource)
            {
                sources.Add(NameSource.Telecom);
            }
            if (PostSource)
            {
                sources.Add(NameSource.Post);
            }
            if (ParkingSource)
            {
                sources.Add(NameSource.Parking);
            }
            if (RoadFacilitySource)
            {
                sources.Add(NameSource.RoadFacility);
            }
            if (AdminSource)
            {
                sources.Add(NameSource.Admin);
            }
            return sources;
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
                SeparateDistrictPrefix = EnableDistrict && EnableDistrictPrefix && DistrictPrefixSeparately,
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
                },
                EnabledSources = GetEnabledSources()
            };

            if (ApplyXfixToStops)
            {
                options.TargetFormats[TargetType.Stop] = new NameFormat
                {
                    Prefix = StopPrefix,
                    Suffix = StopSuffix
                };
            }

            if (ApplyXfixToTransportStation)
            {
                options.TargetFormats[TargetType.Station] = new NameFormat
                {
                    Prefix = TransportStationPrefix,
                    Suffix = TransportStationSuffix
                };
            }

            return options;
        }

        public static class RoadNamingProvider
        {
            public static DropdownItem<NameFormat>[] GetNameFormatOptions()
            {
                return new DropdownItem<NameFormat>[]
                {
                    new() { value = NameFormat.Invalid, displayName = "None" },
                    new() { value = new NameFormat { Separator = " " }, displayName = "Space" },
                    new() { value = new NameFormat { Separator = " - " }, displayName = "Hyphen" },
                };
            }
        }

        public static class DefaultAutoNamingSourceProvider
        {
            public static DropdownItem<int>[] GetOptions()
            {
                return new DropdownItem<int>[]
                {
                    new() { value = (int)NameSource.None, displayName = "StationNaming.NameSource[None]" },
                    new() { value = (int)NameSource.Owner, displayName = "StationNaming.NameSource[Owner]" },
                    new() { value = (int)NameSource.Road, displayName = "StationNaming.NameSource[Road]" },
                    new() { value = (int)NameSource.Intersection, displayName = "StationNaming.NameSource[Intersection]" },
                    new() { value = (int)NameSource.District, displayName = "StationNaming.NameSource[District]" },
                };
            }
        }
    }
}