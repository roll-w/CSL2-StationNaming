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

using Colossal.Localization;
using Game.SceneFlow;

namespace StationNaming.Setting
{
    public static class Localizations
    {
        // TODO: generate keys using ModSettings
        private static readonly Translation[] Translations =
        [
            new Translation("Options.SECTION[StationNaming.StationNaming.Mod]")
                .AddTranslation(LocaleCode.EnUs, "Station Naming")
                .AddTranslation(LocaleCode.ZhHans, "站点命名")
                .AddTranslation(LocaleCode.ZhHant, "站點命名"),
            new Translation("Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.Enable]")
                .AddTranslation(LocaleCode.EnUs, "Enable Station Naming")
                .AddTranslation(LocaleCode.ZhHans, "启用站点命名")
                .AddTranslation(LocaleCode.ZhHant, "啟用站點命名"),
            new Translation("Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.Enable]")
                .AddTranslation(LocaleCode.EnUs,
                    "Enable or disable station naming. When disabled, existing names will not be updated, " +
                    "and no name candidates will be generated.")
                .AddTranslation(LocaleCode.ZhHans, "在此启用或禁用站点命名。禁用时不会更新现有命名，且不会生成名称候选。")
                .AddTranslation(LocaleCode.ZhHant, "在此啟用或禁用站點命名。禁用時不會更新現有命名，且不會生成名稱候選。"),
            new Translation("Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.SearchDepth]")
                .AddTranslation(LocaleCode.EnUs, "Search Depth")
                .AddTranslation(LocaleCode.ZhHans, "搜索深度")
                .AddTranslation(LocaleCode.ZhHant, "搜索深度"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.SearchDepth]")
                .AddTranslation(LocaleCode.EnUs,
                    "The maximum depth to search for name candidates, setting too high is not recommended as it may affect game performance.")
                .AddTranslation(LocaleCode.ZhHans, "搜索名称候选的最大深度，不建议设置过大，以免影响游戏性能。")
                .AddTranslation(LocaleCode.ZhHant, "搜索名稱候選的最大深度，不建議設置過大，以免影響遊戲性能。"),
            new Translation(
                    "Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.IntersectionNamingFormat]")
                .AddTranslation(LocaleCode.EnUs, "Intersection Naming Format")
                .AddTranslation(LocaleCode.ZhHans, "交叉口命名格式")
                .AddTranslation(LocaleCode.ZhHant, "交叉口命名格式"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.IntersectionNamingFormat]")
                .AddTranslation(LocaleCode.EnUs,
                    "The format used when generating the name of the intersection source. " +
                    "[Please use the Name Separator option instead, this setting will be removed in the next version]")
                .AddTranslation(LocaleCode.ZhHans,
                    "在生成交叉口来源的名称时使用的格式。[请使用名称分隔符选项代替，下个版本将移除此设置]")
                .AddTranslation(LocaleCode.ZhHant,
                    "在生成交叉口來源的名稱時使用的格式。[請使用名稱分隔符選項代替，下個版本將移除此設置]"),
            new Translation("Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.ReverseRoadOrder]")
                .AddTranslation(LocaleCode.EnUs, "Reverse Name Order")
                .AddTranslation(LocaleCode.ZhHans, "反转名称顺序")
                .AddTranslation(LocaleCode.ZhHant, "反轉名稱順序"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.ReverseRoadOrder]")
                .AddTranslation(LocaleCode.EnUs,
                    "Reverse the order of the name parts, reverse the order of the different parts of the name. The default order is District->Road->Building.")
                .AddTranslation(LocaleCode.ZhHans, "反转名称顺序，将名称的不同部分的顺序进行反转。默认顺序为 地区->道路->建筑。")
                .AddTranslation(LocaleCode.ZhHant, "反轉名稱順序，將名稱的不同部分的順序進行反轉。默認順序為 地區->道路->建築。"),
            new Translation("Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.Prefix]")
                .AddTranslation(LocaleCode.EnUs, "Prefix")
                .AddTranslation(LocaleCode.ZhHans, "前缀")
                .AddTranslation(LocaleCode.ZhHant, "前綴"),
            new Translation("Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.Prefix]")
                .AddTranslation(LocaleCode.EnUs, "The prefix to be added to the station name.")
                .AddTranslation(LocaleCode.ZhHans, "要添加到站点名称的前缀。")
                .AddTranslation(LocaleCode.ZhHant, "要添加到站點名稱的前綴。"),
            new Translation("Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.Suffix]")
                .AddTranslation(LocaleCode.EnUs, "Suffix")
                .AddTranslation(LocaleCode.ZhHans, "后缀")
                .AddTranslation(LocaleCode.ZhHant, "後綴"),
            new Translation("Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.Suffix]")
                .AddTranslation(LocaleCode.EnUs,
                    "The suffix to be added to the station name. Enter {PREFAB} to replace with the target asset name, such as \"Train Station\".")
                .AddTranslation(LocaleCode.ZhHans, "要添加到站点名称的后缀。输入 {PREFAB} 以替换为目标资产名称，如\"火车站\"等。")
                .AddTranslation(LocaleCode.ZhHant, "要添加到站點名稱的後綴。輸入 {PREFAB} 以替換為目標資產名稱，如\"火車站\"等。"),

            new Translation("Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.ApplyXfixToStops]")
                .AddTranslation(LocaleCode.EnUs, "Apply Prefix/Suffix to Stops")
                .AddTranslation(LocaleCode.ZhHans, "应用前后缀到站牌")
                .AddTranslation(LocaleCode.ZhHant, "應用前後綴到站牌"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.ApplyXfixToStops]")
                .AddTranslation(LocaleCode.EnUs,
                    "Whether to apply the prefix and suffix to the station name on the stop, enable this option to set the prefix and suffix for the stops separately.")
                .AddTranslation(LocaleCode.ZhHans, "是否将前缀和后缀应用到站牌的站点名称中，选中此选项可单独设置对站牌的前后缀。")
                .AddTranslation(LocaleCode.ZhHant, "是否將前綴和後綴應用到站牌的站點名稱中，選中此選項可單獨設置對站牌的前後綴。"),

            new Translation("Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.StopPrefix]")
                .AddTranslation(LocaleCode.EnUs, "Stop Prefix")
                .AddTranslation(LocaleCode.ZhHans, "站牌前缀")
                .AddTranslation(LocaleCode.ZhHant, "站牌前綴"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.StopPrefix]")
                .AddTranslation(LocaleCode.EnUs, "The prefix to be added to the station name on the stop.")
                .AddTranslation(LocaleCode.ZhHans, "要添加到站牌上的站点名称的前缀。")
                .AddTranslation(LocaleCode.ZhHant, "要添加到站牌上的站點名稱的前綴。"),
            new Translation("Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.StopSuffix]")
                .AddTranslation(LocaleCode.EnUs, "Stop Suffix")
                .AddTranslation(LocaleCode.ZhHans, "站牌后缀")
                .AddTranslation(LocaleCode.ZhHant, "站牌後綴"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.StopSuffix]")
                .AddTranslation(LocaleCode.EnUs, "The suffix to be added to the station name on the stop.")
                .AddTranslation(LocaleCode.ZhHans, "要添加到站牌上的站点名称的后缀。")
                .AddTranslation(LocaleCode.ZhHant, "要添加到站牌上的站點名稱的後綴。"),

            new Translation("Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.NamingSeparator]")
                .AddTranslation(LocaleCode.EnUs, "Default Name Separator")
                .AddTranslation(LocaleCode.ZhHans, "默认名称间分隔符")
                .AddTranslation(LocaleCode.ZhHant, "默認名稱間分隔符"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.NamingSeparator]")
                .AddTranslation(LocaleCode.EnUs, "Set the separator between the names under the default.")
                .AddTranslation(LocaleCode.ZhHans, "设置默认下的名称之间的分隔符。")
                .AddTranslation(LocaleCode.ZhHant, "設置默認下的名稱之間的分隔符。"),
            new Translation("Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.RoadFormat]")
                .AddTranslation(LocaleCode.EnUs, "Road Format"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.RoadFormat]")
                .AddTranslation(LocaleCode.EnUs, "Road format."),
            new Translation("Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.AutoUpdate]")
                .AddTranslation(LocaleCode.EnUs, "Auto Update Name")
                .AddTranslation(LocaleCode.ZhHans, "自动更新站名")
                .AddTranslation(LocaleCode.ZhHant, "自動更新站名"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.AutoUpdate]")
                .AddTranslation(LocaleCode.EnUs,
                    "When enabled, the station name will be automatically updated based on the selected name source.")
                .AddTranslation(LocaleCode.ZhHans, "启用后，将根据所选的命名来源自动更新站点名称。")
                .AddTranslation(LocaleCode.ZhHant, "啟用後，將根據所選的命名來源自動更新站點名稱。"),
            new Translation("Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.AutoNaming]")
                .AddTranslation(LocaleCode.EnUs, "Auto Naming")
                .AddTranslation(LocaleCode.ZhHans, "自动命名")
                .AddTranslation(LocaleCode.ZhHant, "自動命名"),
            new Translation("Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.ResetSettings]")
                .AddTranslation(LocaleCode.EnUs, "Reset Settings")
                .AddTranslation(LocaleCode.ZhHans, "重置设置")
                .AddTranslation(LocaleCode.ZhHant, "重置設置"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.ResetSettings]")
                .AddTranslation(LocaleCode.EnUs, "Reset all settings to default values.")
                .AddTranslation(LocaleCode.ZhHans, "重置所有设置为默认值。")
                .AddTranslation(LocaleCode.ZhHant, "重置所有設置為默認值。"),
            new Translation("Options.WARNING[StationNaming.StationNaming.Mod.StationNamingSettings.ResetSettings]")
                .AddTranslation(LocaleCode.EnUs, "Are you sure you want to reset all settings?")
                .AddTranslation(LocaleCode.ZhHans, "你确定要重置所有设置吗？")
                .AddTranslation(LocaleCode.ZhHant, "你確定要重置所有設置嗎？"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.AutoNaming]")
                .AddTranslation(LocaleCode.EnUs,
                    "When enabled, the station will be automatically named when created. " +
                    "Named according to the order of the owner and the current road.")
                .AddTranslation(LocaleCode.ZhHans, "启用后，自动为新建的站点命名。按照所有者、当前道路的顺序命名。")
                .AddTranslation(LocaleCode.ZhHant, "啟用後，自動為新建的站點命名。按照所有者、當前道路的順序命名。"),
            new Translation("Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.BuildingName]")
                .AddTranslation(LocaleCode.EnUs, "Enable Building Name Source")
                .AddTranslation(LocaleCode.ZhHans, "启用建筑命名来源")
                .AddTranslation(LocaleCode.ZhHant, "啟用建築命名來源"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.BuildingName]")
                .AddTranslation(LocaleCode.EnUs,
                    "When enabled, allow generate name candidates from surrounding buildings. ")
                .AddTranslation(LocaleCode.ZhHans, "启用后，允许从周围建筑中生成名称候选。")
                .AddTranslation(LocaleCode.ZhHant, "啟用後，允許從周圍建築中生成名稱候選。"),

            new Translation(
                    "Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.BuildingNameWithCurrentRoad]")
                .AddTranslation(LocaleCode.EnUs, "Building Name With Current Road")
                .AddTranslation(LocaleCode.ZhHans, "建筑命名包含当前道路")
                .AddTranslation(LocaleCode.ZhHant, "建築命名包含當前道路"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.BuildingNameWithCurrentRoad]")
                .AddTranslation(LocaleCode.EnUs,
                    "When enabled, allow generate name candidates from surrounding buildings with current road name, " +
                    "the format will be the same as the intersection naming format. ")
                .AddTranslation(LocaleCode.ZhHans, "启用后，允许从周围建筑中生成包含当前道路的名称候选，格式将与交叉口命名格式相同。")
                .AddTranslation(LocaleCode.ZhHant, "啟用後，允許從周圍建築中生成包含當前道路的名稱候選，格式將與交叉口命名格式相同。"),

            new Translation(
                    "Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.SpawnableBuildingName]")
                .AddTranslation(LocaleCode.EnUs, "Enable Spawnable Building Name Source")
                .AddTranslation(LocaleCode.ZhHans, "启用自长建筑命名来源")
                .AddTranslation(LocaleCode.ZhHant, "啟用自長建築命名來源"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.SpawnableBuildingName]")
                .AddTranslation(LocaleCode.EnUs,
                    "When enabled, allow generate name candidates from surrounding spawnable buildings. " +
                    "[May cause increased resource consumption, please enable as needed. ]")
                .AddTranslation(LocaleCode.ZhHans, "启用后，允许从周围自长建筑中生成名称候选。[但可能造成资源消耗增加，请按需开启。]")
                .AddTranslation(LocaleCode.ZhHant, "啟用後，允許從周圍自長建築中生成名稱候選。[但可能造成資源消耗增加，請按需開啟。]"),
            new Translation(
                    "Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.AddressNameFormat]")
                .AddTranslation(LocaleCode.EnUs, "Address Format")
                .AddTranslation(LocaleCode.ZhHans, "地址格式")
                .AddTranslation(LocaleCode.ZhHant, "地址格式"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.AddressNameFormat]")
                .AddTranslation(LocaleCode.EnUs,
                    "Set the in-game address format, {ROAD} represents the current road, {NUMBER} represents the number. " +
                    "The game defaults to \"{NUMBER} {ROAD}\".")
                .AddTranslation(LocaleCode.ZhHans, "设置游戏内地址格式，{ROAD} 表示当前道路，{NUMBER} 表示编号。" +
                                                   "游戏默认为 \"{NUMBER} {ROAD}\"。")
                .AddTranslation(LocaleCode.ZhHant, "設置遊戲內地址格式，{ROAD} 表示當前道路，{NUMBER} 表示編號。" +
                                                   "遊戲默認為 \"{NUMBER} {ROAD}\"。"),

            new Translation(
                    "Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.NamedAddressNameFormat]")
                .AddTranslation(LocaleCode.EnUs, "Named Address Format")
                .AddTranslation(LocaleCode.ZhHans, "名称地址格式")
                .AddTranslation(LocaleCode.ZhHant, "名稱地址格式"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.NamedAddressNameFormat]")
                .AddTranslation(LocaleCode.EnUs, "Set the in-game address format with name (usually company name), " +
                                                 "{ROAD} represents the current road, {NUMBER} represents the number, {NAME} represents the name. " +
                                                 "The game defaults to \"{NAME}, {NUMBER} {ROAD}\".")
                .AddTranslation(LocaleCode.ZhHans, "设置游戏内带有名称（通常是企业名）的地址格式，" +
                                                   "{ROAD} 表示当前道路，{NUMBER} 表示编号，{NAME} 表示名称。" +
                                                   "游戏默认为 \"{NAME}, {NUMBER} {ROAD}\"。")
                .AddTranslation(LocaleCode.ZhHant, "設置遊戲內帶有名稱（通常是企業名）的地址格式，" +
                                                   "{ROAD} 表示當前道路，{NUMBER} 表示編號，{NAME} 表示名稱。" +
                                                   "遊戲默認為 \"{NAME}, {NUMBER} {ROAD}\"。"),

            new Translation(
                    "Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.OverrideVanillaAddress]")
                .AddTranslation(LocaleCode.EnUs, "Override Vanilla Address Format")
                .AddTranslation(LocaleCode.ZhHans, "覆盖原版地址格式")
                .AddTranslation(LocaleCode.ZhHant, "覆蓋原版地址格式"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.OverrideVanillaAddress]")
                .AddTranslation(LocaleCode.EnUs,
                    "When enabled, the above two formats will be applied to the vanilla address at the same time.")
                .AddTranslation(LocaleCode.ZhHans, "启用后，将同时应用上述两项格式覆盖到原版地址中。")
                .AddTranslation(LocaleCode.ZhHant, "啟用後，將同時應用上述兩項格式覆蓋到原版地址中。"),

            new Translation(
                    "Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.EnableDistrict]")
                .AddTranslation(LocaleCode.EnUs, "Enable District Name Source")
                .AddTranslation(LocaleCode.ZhHans, "启用地区名称来源")
                .AddTranslation(LocaleCode.ZhHant, "啟用地區名稱來源"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.EnableDistrict]")
                .AddTranslation(LocaleCode.EnUs,
                    "When enabled, add the district name as a prefix/suffix for each name candidate (controlled by the reverse order option).")
                .AddTranslation(LocaleCode.ZhHans, "启用后，为每个名称候选都添加地区名称作为前缀/后缀（受到反转顺序控制）。")
                .AddTranslation(LocaleCode.ZhHant, "啟用後，為每個名稱候選都添加地區名稱作為前綴/後綴（受到反轉順序控制）。"),

            new Translation("Options.GROUP[StationNaming.StationNaming.Mod.Experimental]")
                .AddTranslation(LocaleCode.EnUs, "Experimental")
                .AddTranslation(LocaleCode.ZhHans, "实验性")
                .AddTranslation(LocaleCode.ZhHant, "實驗性"),
            new Translation("Options.GROUP[StationNaming.StationNaming.Mod.Stops]")
                .AddTranslation(LocaleCode.EnUs, "Stops")
                .AddTranslation(LocaleCode.ZhHans, "站牌")
                .AddTranslation(LocaleCode.ZhHant, "站牌"),
            new Translation("Options.GROUP[StationNaming.StationNaming.Mod.District]")
                .AddTranslation(LocaleCode.EnUs, "District")
                .AddTranslation(LocaleCode.ZhHans, "区域")
                .AddTranslation(LocaleCode.ZhHant, "區域"),
            new Translation("Options.GROUP[StationNaming.StationNaming.Mod.Spawnable]")
                .AddTranslation(LocaleCode.EnUs, "Spawnable Building")
                .AddTranslation(LocaleCode.ZhHans, "自长建筑")
                .AddTranslation(LocaleCode.ZhHant, "自長建築"),
            new Translation("Options.GROUP[StationNaming.StationNaming.Mod.Other]")
                .AddTranslation(LocaleCode.EnUs, "Other")
                .AddTranslation(LocaleCode.ZhHans, "其他")
                .AddTranslation(LocaleCode.ZhHant, "其他"),
            new Translation("Options.TAB[StationNaming.StationNaming.Mod.General]")
                .AddTranslation(LocaleCode.EnUs, "General")
                .AddTranslation(LocaleCode.ZhHans, "常规")
                .AddTranslation(LocaleCode.ZhHant, "常規"),
            new Translation("Options.TAB[StationNaming.StationNaming.Mod.Building]")
                .AddTranslation(LocaleCode.EnUs, "Building")
                .AddTranslation(LocaleCode.ZhHans, "建筑")
                .AddTranslation(LocaleCode.ZhHant, "建築"),
            new Translation("Options.TAB[StationNaming.StationNaming.Mod.OtherSource]")
                .AddTranslation(LocaleCode.EnUs, "Other")
                .AddTranslation(LocaleCode.ZhHans, "其他")
                .AddTranslation(LocaleCode.ZhHant, "其他"),

            new Translation(
                    "Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.StopsDescription]")
                .AddTranslation(LocaleCode.EnUs, ""),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.StopsDescription]")
                .AddTranslation(LocaleCode.EnUs,
                    "This is the options specifically set for the stops, " +
                    "which may be replaced by more detailed settings in the future.")
                .AddTranslation(LocaleCode.ZhHans,
                    "这是为站牌单独设置的选项，之后可能会被更详细的设置取代。")
                .AddTranslation(LocaleCode.ZhHant,
                    "這是為站牌單獨設置的選項，之後可能會被更詳細的設置取代。"),
            new Translation("StationNaming.NameCandidates")
                .AddTranslation(LocaleCode.EnUs, "Name Candidates")
                .AddTranslation(LocaleCode.ZhHans, "名称候选")
                .AddTranslation(LocaleCode.ZhHant, "名稱候選"),
            new Translation("StationNaming.NameCandidates[Tooltip]")
                .AddTranslation(LocaleCode.EnUs,
                    "Here are all the name candidates, you can select them to replace the existing name.")
                .AddTranslation(LocaleCode.ZhHans, "这里是所有的名称候选，您可以选择它们来替换现有名称。")
                .AddTranslation(LocaleCode.ZhHant, "這裡是所有的名稱候選，您可以選擇它們來替換現有名稱。"),
            new Translation("StationNaming.Candidates")
                .AddTranslation(LocaleCode.EnUs, "Candidates")
                .AddTranslation(LocaleCode.ZhHans, "候选列表")
                .AddTranslation(LocaleCode.ZhHant, "候選列表"),
            new Translation("StationNaming.SpawnableCandidates")
                .AddTranslation(LocaleCode.EnUs, "Spawnable Building Candidates")
                .AddTranslation(LocaleCode.ZhHans, "自长建筑来源候选列表")
                .AddTranslation(LocaleCode.ZhHant, "自長建築來源候選列表"),

            new Translation("StationNaming.NameSource[Building]")
                .AddTranslation(LocaleCode.EnUs, "Building")
                .AddTranslation(LocaleCode.ZhHans, "一般建筑")
                .AddTranslation(LocaleCode.ZhHant, "一般建築"),
            new Translation("StationNaming.NameSource[Road]")
                .AddTranslation(LocaleCode.EnUs, "Road")
                .AddTranslation(LocaleCode.ZhHans, "道路")
                .AddTranslation(LocaleCode.ZhHant, "道路"),
            new Translation("StationNaming.NameSource[District]")
                .AddTranslation(LocaleCode.EnUs, "District")
                .AddTranslation(LocaleCode.ZhHans, "区域")
                .AddTranslation(LocaleCode.ZhHant, "區域"),
            new Translation("StationNaming.NameSource[Intersection]")
                .AddTranslation(LocaleCode.EnUs, "Intersection")
                .AddTranslation(LocaleCode.ZhHans, "交叉口")
                .AddTranslation(LocaleCode.ZhHant, "交叉口"),
            new Translation("StationNaming.NameSource[Owner]")
                .AddTranslation(LocaleCode.EnUs, "Owner")
                .AddTranslation(LocaleCode.ZhHans, "所有者")
                .AddTranslation(LocaleCode.ZhHant, "所有者"),
            new Translation("StationNaming.NameSource[TransportStation]")
                .AddTranslation(LocaleCode.EnUs, "Transport Station")
                .AddTranslation(LocaleCode.ZhHans, "交通站点")
                .AddTranslation(LocaleCode.ZhHant, "交通站點"),
            new Translation("StationNaming.NameSource[TransportDepot]")
                .AddTranslation(LocaleCode.EnUs, "Transport Depot")
                .AddTranslation(LocaleCode.ZhHans, "交通车库")
                .AddTranslation(LocaleCode.ZhHant, "交通車庫"),
            new Translation("StationNaming.NameSource[SpawnableBuilding]")
                .AddTranslation(LocaleCode.EnUs, "Spawnable Building")
                .AddTranslation(LocaleCode.ZhHans, "自长建筑")
                .AddTranslation(LocaleCode.ZhHant, "自長建築"),
            new Translation("StationNaming.NameSource[SignatureBuilding]")
                .AddTranslation(LocaleCode.EnUs, "Signature Building")
                .AddTranslation(LocaleCode.ZhHans, "标志性建筑")
                .AddTranslation(LocaleCode.ZhHant, "標誌性建築"),
            new Translation("StationNaming.NameSource[School]")
                .AddTranslation(LocaleCode.EnUs, "School")
                .AddTranslation(LocaleCode.ZhHans, "学校")
                .AddTranslation(LocaleCode.ZhHant, "學校"),
            new Translation("StationNaming.NameSource[FireStation]")
                .AddTranslation(LocaleCode.EnUs, "Fire Station")
                .AddTranslation(LocaleCode.ZhHans, "消防站")
                .AddTranslation(LocaleCode.ZhHant, "消防站"),
            new Translation("StationNaming.NameSource[PoliceStation]")
                .AddTranslation(LocaleCode.EnUs, "Police Station")
                .AddTranslation(LocaleCode.ZhHans, "警察局")
                .AddTranslation(LocaleCode.ZhHant, "警察局"),
            new Translation("StationNaming.NameSource[Hospital]")
                .AddTranslation(LocaleCode.EnUs, "Hospital")
                .AddTranslation(LocaleCode.ZhHans, "医院")
                .AddTranslation(LocaleCode.ZhHant, "醫院"),
            new Translation("StationNaming.NameSource[Park]")
                .AddTranslation(LocaleCode.EnUs, "Park")
                .AddTranslation(LocaleCode.ZhHans, "公园")
                .AddTranslation(LocaleCode.ZhHant, "公園"),
            new Translation("StationNaming.NameSource[Electricity]")
                .AddTranslation(LocaleCode.EnUs, "Electricity Service")
                .AddTranslation(LocaleCode.ZhHans, "电力服务")
                .AddTranslation(LocaleCode.ZhHant, "電力服務"),
            new Translation("StationNaming.NameSource[Water]")
                .AddTranslation(LocaleCode.EnUs, "Water Service")
                .AddTranslation(LocaleCode.ZhHans, "净水服务")
                .AddTranslation(LocaleCode.ZhHant, "淨水服務"),
            new Translation("StationNaming.NameSource[Sewage]")
                .AddTranslation(LocaleCode.EnUs, "Sewage Service")
                .AddTranslation(LocaleCode.ZhHans, "污水服务")
                .AddTranslation(LocaleCode.ZhHant, "污水服務"),
            new Translation("StationNaming.NameSource[CityService]")
                .AddTranslation(LocaleCode.EnUs, "City Service")
                .AddTranslation(LocaleCode.ZhHans, "城市服务")
                .AddTranslation(LocaleCode.ZhHant, "城市服務"),
            new Translation("StationNaming.NameSource[Unknown]")
                .AddTranslation(LocaleCode.EnUs, "Unknown")
                .AddTranslation(LocaleCode.ZhHans, "未知")
                .AddTranslation(LocaleCode.ZhHant, "未知"),
            new Translation("StationNaming.NameSource[None]")
                .AddTranslation(LocaleCode.EnUs, "None")
                .AddTranslation(LocaleCode.ZhHans, "None")
                .AddTranslation(LocaleCode.ZhHant, "None"),
        ];

        public static void LoadTranslations()
        {
            foreach (var supportedLocale in GameManager.instance
                         .localizationManager.GetSupportedLocales())
            {
                var locale = FromString(supportedLocale);
                var dictionary = Translation.ToDictionary(Translations, locale);
                GameManager.instance.localizationManager.AddSource(
                    supportedLocale, new MemorySource(dictionary)
                );
            }
        }

        private static LocaleCode FromString(string code)
        {
            return code switch
            {
                "en-US" => LocaleCode.EnUs,
                "zh-HANS" => LocaleCode.ZhHans,
                "zh-HANT" => LocaleCode.ZhHant,
                _ => LocaleCode.EnUs
            };
        }
    }
}