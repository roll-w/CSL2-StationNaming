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
using Colossal.Localization;
using Game.Modding;
using Game.SceneFlow;
using Game.Settings;
using Game.UI.Widgets;

namespace StationNaming.Setting;

[FileLocation(Mod.Name)]
[SettingsUITabOrder(SectionGeneral, SectionBuilding)]
[SettingsUISection(SectionGeneral, SectionBuilding)]
[SettingsUIShowGroupName(GroupExperimental, GroupSpawnable, GroupOther)]
[SettingsUIGroupOrder(GroupStable, GroupSpawnable, GroupExperimental, GroupOther)]
public class StationNamingSettings(IMod mod) : ModSetting(mod)
{
    public const string GroupStable = "Stable";

    public const string GroupExperimental = "Experimental";

    public const string GroupOther = "Other";

    public const string GroupSpawnable = "Spawnable";

    public const string SectionGeneral = "General";

    public const string SectionBuilding = "Building";

    [SettingsUISection(SectionGeneral, GroupStable)]
    public bool Enable { get; set; } = true;

    [SettingsUISection(SectionGeneral, GroupStable)]
    [SettingsUIDropdown(typeof(RoadNamingProvider), nameof(RoadNamingProvider.GetFormatOptions))]
    public string IntersectionNamingFormat { get; set; } = "{0} & {1}";

    [SettingsUISection(SectionGeneral, GroupStable)]
    public bool ReverseRoadOrder { get; set; } = false;

    [SettingsUISection(SectionGeneral, GroupStable)]
    [SettingsUISlider(max = 5, min = 1)]
    public int SearchDepth { get; set; } = 2;

    [SettingsUISection(SectionGeneral, GroupStable)]
    public string Prefix { get; set; } = "";

    [SettingsUISection(SectionGeneral, GroupStable)]
    public string Suffix { get; set; } = "";

    [SettingsUISection(SectionGeneral, GroupExperimental)]
    public bool AutoUpdate { get; set; } = true;

    [SettingsUISection(SectionGeneral, GroupExperimental)]
    public bool AutoNaming { get; set; } = true;

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
    [SettingsUIDropdown(typeof(RoadNamingProvider),
        nameof(RoadNamingProvider.GetAddressNameFormatOptions))]
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
    [SettingsUIDropdown(typeof(RoadNamingProvider),
        nameof(RoadNamingProvider.GetNamedAddressNameFormatOptions))]
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
    }

    public string FormatRoadName(string first, string second)
    {
        return ReverseRoadOrder
            ? string.Format(IntersectionNamingFormat, second, first)
            : string.Format(IntersectionNamingFormat, first, second);
    }

    public string FormatCandidateName(string name)
    {
        return $"{Prefix}{name}{Suffix}";
    }

    public NameOptions ToNameOptions()
    {
        return new NameOptions
        {
            BuildingName = BuildingName,
            BuildingNameWithCurrentRoad = BuildingName && BuildingNameWithCurrentRoad,
            SpawnableBuildingName = BuildingName && SpawnableBuildingName,
        };
    }

    public static class RoadNamingProvider
    {
        public static DropdownItem<string>[] GetFormatOptions()
        {
            return
            [
                new DropdownItem<string> { value = "{0} & {1}", displayName = "{0} & {1}" },
                new DropdownItem<string> { value = "{0}{1}", displayName = "{0}{1}" },
                new DropdownItem<string> { value = "{0}-{1}", displayName = "{0}-{1}" },
                new DropdownItem<string> { value = "{0} @{1}", displayName = "{0} @{1}" },
                new DropdownItem<string> { value = "{0} {1}", displayName = "{0} {1}" },
                new DropdownItem<string> { value = "{0}, {1}", displayName = "{0}, {1}" },
                new DropdownItem<string> { value = "{0} ({1})", displayName = "{0} ({1})" },
                new DropdownItem<string> { value = "{0}[{1}]", displayName = "{0}[{1}]" },
                new DropdownItem<string> { value = "{0}_{1}", displayName = "{0}_{1}" },
                new DropdownItem<string> { value = "{0}:{1}", displayName = "{0}:{1}" },
                new DropdownItem<string> { value = "{0}.{1}", displayName = "{0}.{1}" }
            ];
        }


        public static DropdownItem<string>[] GetAddressNameFormatOptions()
        {
            return
            [
                new DropdownItem<string> { value = "{NUMBER} {ROAD}", displayName = "{NUMBER} {ROAD}" },
                new DropdownItem<string> { value = "{ROAD} {NUMBER}", displayName = "{ROAD} {NUMBER}" },
                new DropdownItem<string> { value = "{ROAD}{NUMBER}", displayName = "{ROAD}{NUMBER}" },
                new DropdownItem<string> { value = "{NUMBER}{ROAD}", displayName = "{NUMBER}{ROAD}" },
                new DropdownItem<string> { value = "{ROAD} - {NUMBER}", displayName = "{ROAD} - {NUMBER}" },
            ];
        }

        public static DropdownItem<string>[] GetNamedAddressNameFormatOptions()
        {
            return
            [
                new DropdownItem<string> { value = "{NAME}, {NUMBER} {ROAD}", displayName = "{NAME}, {ROAD} {NUMBER}" },
                new DropdownItem<string> { value = "{NAME}, {ROAD} {NUMBER}", displayName = "{NAME}, {ROAD} {NUMBER}" },
                new DropdownItem<string> { value = "{ROAD} {NUMBER}, {NAME}", displayName = "{ROAD} {NUMBER}, {NAME}" },
                new DropdownItem<string> { value = "{ROAD} {NUMBER} {NAME}", displayName = "{ROAD} {NUMBER} {NAME}" },
                new DropdownItem<string> { value = "{NUMBER} {ROAD} {NAME}", displayName = "{NUMBER} {ROAD} {NAME}" },
                new DropdownItem<string> { value = "{NUMBER} {ROAD}, {NAME}", displayName = "{NUMBER} {ROAD}, {NAME}" }
            ];
        }

        public static DropdownItem<string>[] GetPrefixOptions()
        {
            return
            [
                new DropdownItem<string> { value = "", displayName = "No Prefix" },
                new DropdownItem<string> { value = "Station ", displayName = "Station " },
                new DropdownItem<string> { value = "Stop ", displayName = "Stop " },
            ];
        }

        public static DropdownItem<string>[] GetSuffixOptions()
        {
            return
            [
                new DropdownItem<string> { value = "", displayName = "No Suffix" },
                new DropdownItem<string> { value = " Station", displayName = " Station" },
                new DropdownItem<string> { value = " Stop", displayName = " Stop" },
            ];
        }
    }
}