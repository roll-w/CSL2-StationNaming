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
using Game.UI.Widgets;
using StationNaming.System;

namespace StationNaming.Setting;

[FileLocation(Mod.Name)]
[SettingsUITabOrder(SectionGeneral, SectionBuilding, SectionOther)]
//[SettingsUISection(SectionGeneral, SectionBuilding, SectionOther)]
[SettingsUIShowGroupName(
    GroupStops,
    GroupExperimental, GroupSpawnable,
    GroupDistrict, GroupOther
)]
[SettingsUIGroupOrder(
    GroupStable, GroupStops,
    GroupSpawnable, GroupDistrict,
    GroupExperimental, GroupOther
)]
public class StationNamingSettings(IMod mod) : ModSetting(mod)
{
    public const string GroupStable = "Stable";

    public const string GroupExperimental = "Experimental";

    public const string GroupOther = "Other";

    public const string GroupSpawnable = "Spawnable";

    public const string GroupDistrict = "District";

    public const string GroupStops = "Stops";

    public const string SectionGeneral = "General";

    public const string SectionBuilding = "Building";

    public const string SectionOther = "OtherSource";

    [SettingsUISection(SectionGeneral, GroupStable)]
    public bool Enable { get; set; } = true;

    [SettingsUISection(SectionGeneral, GroupStable)]
    [SettingsUIDropdown(typeof(RoadNamingProvider), nameof(RoadNamingProvider.GetFormatOptions))]
    public string IntersectionNamingFormat { get; set; } = "{0} & {1}";

    [SettingsUISection(SectionGeneral, GroupStable)]
    [SettingsUITextInput]
    public string NamingSeparator { get; set; } = " & ";

    [SettingsUISection(SectionOther, GroupStable)]
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

    [SettingsUISection(SectionGeneral, GroupStops)]
    [SettingsUIMultilineText]
    public string StopsDescription { get; set; } = string.Empty;

    /// <summary>
    /// Should apply prefix and suffix to stops.
    /// </summary>
    [SettingsUISection(SectionGeneral, GroupStops)]
    public bool ApplyXfixToStops { get; set; } = false;

    public bool IsNotApplyXfixToStops() => !ApplyXfixToStops;

    [SettingsUITextInput]
    [SettingsUISection(SectionGeneral, GroupStops)]
    [SettingsUIDisableByCondition(typeof(StationNamingSettings), nameof(IsNotApplyXfixToStops))]
    public string StopPrefix { get; set; } = "";

    [SettingsUITextInput]
    [SettingsUISection(SectionGeneral, GroupStops)]
    [SettingsUIDisableByCondition(typeof(StationNamingSettings), nameof(IsNotApplyXfixToStops))]
    public string StopSuffix { get; set; } = "";

    [SettingsUISection(SectionOther, GroupDistrict)]
    public bool EnableDistrict { get; set; } = false;

    [SettingsUISection(SectionOther, GroupDistrict)]
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

    [SettingsUISection(SectionBuilding, GroupStable)]
    public bool BuildingName { get; set; } = true;

    public bool IsBuildingNameDisabled() => !BuildingName;

    [SettingsUISection(SectionBuilding, GroupStable)]
    [SettingsUIDisableByCondition(typeof(StationNamingSettings), nameof(IsBuildingNameDisabled))]
    public bool BuildingNameWithCurrentRoad { get; set; } = true;

    [SettingsUISection(SectionBuilding, GroupSpawnable)]
    [SettingsUIDisableByCondition(typeof(StationNamingSettings), nameof(IsBuildingNameDisabled))]
    public bool SpawnableBuildingName { get; set; } = false;

    private string _addressNameFormat = "{NUMBER} {ROAD}";

    [SettingsUISection(SectionBuilding, GroupSpawnable)]
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

    [SettingsUISection(SectionBuilding, GroupSpawnable)]
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

    [SettingsUISection(SectionBuilding, GroupSpawnable)]
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


    public override void SetDefaults()
    {
        Enable = true;
        IntersectionNamingFormat = "{0} & {1}";
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

        EnableDistrict = false;

        RoadFormat = NameFormat.Invalid;
        DistrictFormat = NameFormat.Invalid;
    }

    public NameOptions ToNameOptions()
    {
        var options = new NameOptions
        {
            BuildingName = BuildingName,
            BuildingNameWithCurrentRoad = BuildingName && BuildingNameWithCurrentRoad,
            SpawnableBuildingName = BuildingName && SpawnableBuildingName,
            EnableDistrict = EnableDistrict,
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
        public static DropdownItem<string>[] GetFormatOptions()
        {
            return
            [
                new DropdownItem<string> { value = "{0} & {1}", displayName = "{0} & {1}" },
                new DropdownItem<string> { value = "{0}{1}", displayName = "{0}{1}" },
                new DropdownItem<string> { value = "{0} @{1}", displayName = "{0} @{1}" },
                new DropdownItem<string> { value = "{0} {1}", displayName = "{0} {1}" },
                new DropdownItem<string> { value = "{0}, {1}", displayName = "{0}, {1}" },
                new DropdownItem<string> { value = "{0}_{1}", displayName = "{0}_{1}" },
                new DropdownItem<string> { value = "{0}:{1}", displayName = "{0}:{1}" },
                new DropdownItem<string> { value = "{0}.{1}", displayName = "{0}.{1}" }
            ];
        }

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