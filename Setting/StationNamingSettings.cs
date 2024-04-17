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

    public override void SetDefaults()
    {
        Enable = true;
        IntersectionNamingFormat = "{0}{1}";
        ReverseRoadOrder = false;
        SearchDepth = 2;
        Prefix = "";
        Suffix = "";
        AutoUpdate = true;
        AutoNaming = true;
        BuildingName = true;
        BuildingNameWithCurrentRoad = true;
        SpawnableBuildingName = false;
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