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
        private static readonly Translation[] Translations = {
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
                    "The maximum depth to search for name candidates, affecting the generation of name sources for nearby buildings. " +
                    "The search depth is determined by the road nodes. The actual search range on roads with dense nodes will be smaller. " +
                    "It is not recommended to set it too large to avoid affecting game performance.")
                .AddTranslation(LocaleCode.ZhHans,
                    "搜索名称候选的最大深度，影响附近建筑名称来源生成。搜索深度根据道路节点确定，在节点过于密集的道路上实际的搜索范围会变小。不建议设置过大，以免影响游戏性能。")
                .AddTranslation(LocaleCode.ZhHant,
                    "搜索名稱候選的最大深度，影響附近建築名稱來源生成。搜索深度根據道路節點確定，在節點過於密集的道路上實際的搜索範圍會變小。不建議設置過大，以免影響遊戲性能。"),
            new Translation("Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.SearchRadius]")
                .AddTranslation(LocaleCode.EnUs, "Search Radius")
                .AddTranslation(LocaleCode.ZhHans, "搜索半径")
                .AddTranslation(LocaleCode.ZhHant, "搜索半徑"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.SearchRadius]")
                .AddTranslation(LocaleCode.EnUs,
                    "The radius to search for name candidates, affecting the generation of name sources for nearby buildings. " +
                    "It is recommended to set a moderate value to balance performance and naming effect.")
                .AddTranslation(LocaleCode.ZhHans,
                    "搜索名称候选的半径，影响附近建筑名称来源生成。建议设置为适中的数值，以平衡性能和命名效果。")
                .AddTranslation(LocaleCode.ZhHant,
                    "搜索名稱候選的半徑，影響附近建築名稱來源生成。建議設置為適中的數值，以平衡性能和命名效果。"),
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
                .AddTranslation(LocaleCode.EnUs, "The prefix to be added to the station name. Enter {ASSET} to replace with the target asset name, such as \"Train Station\".")
                .AddTranslation(LocaleCode.ZhHans, "要添加到站点名称的前缀。输入 {ASSET} 以替换为目标资产名称，如\"火车站\"等。")
                .AddTranslation(LocaleCode.ZhHant, "要添加到站點名稱的前綴。輸入 {ASSET} 以替換為目標資產名稱，如\"火車站\"等。"),
            new Translation("Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.Suffix]")
                .AddTranslation(LocaleCode.EnUs, "Suffix")
                .AddTranslation(LocaleCode.ZhHans, "后缀")
                .AddTranslation(LocaleCode.ZhHant, "後綴"),
            new Translation("Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.Suffix]")
                .AddTranslation(LocaleCode.EnUs,
                    "The suffix to be added to the station name. Enter {ASSET} to replace with the target asset name, such as \"Train Station\".")
                .AddTranslation(LocaleCode.ZhHans, "要添加到站点名称的后缀。输入 {ASSET} 以替换为目标资产名称，如\"火车站\"等。")
                .AddTranslation(LocaleCode.ZhHant, "要添加到站點名稱的後綴。輸入 {ASSET} 以替換為目標資產名稱，如\"火車站\"等。"),

            new Translation("Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.ApplyXfixToStops]")
                .AddTranslation(LocaleCode.EnUs, "Apply Prefix/Suffix to Stops")
                .AddTranslation(LocaleCode.ZhHans, "应用前后缀到站牌")
                .AddTranslation(LocaleCode.ZhHant, "應用前後綴到站牌"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.ApplyXfixToStops]")
                .AddTranslation(LocaleCode.EnUs,
                    "Whether to apply the prefix and suffix to the name on the stop, enable this option to set the prefix and suffix for the stops separately.")
                .AddTranslation(LocaleCode.ZhHans, "是否将前缀和后缀应用到站牌的名称中，选中此选项可单独设置对站牌的前后缀。")
                .AddTranslation(LocaleCode.ZhHant, "是否將前綴和後綴應用到站牌的名稱中，選中此選項可單獨設置對站牌的前後綴。"),

            new Translation("Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.StopPrefix]")
                .AddTranslation(LocaleCode.EnUs, "Stop Prefix")
                .AddTranslation(LocaleCode.ZhHans, "站牌前缀")
                .AddTranslation(LocaleCode.ZhHant, "站牌前綴"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.StopPrefix]")
                .AddTranslation(LocaleCode.EnUs, "The prefix to be added to the name on the stop.")
                .AddTranslation(LocaleCode.ZhHans, "要添加到站牌上的名称的前缀。")
                .AddTranslation(LocaleCode.ZhHant, "要添加到站牌上的名稱的前綴。"),
            new Translation("Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.StopSuffix]")
                .AddTranslation(LocaleCode.EnUs, "Stop Suffix")
                .AddTranslation(LocaleCode.ZhHans, "站牌后缀")
                .AddTranslation(LocaleCode.ZhHant, "站牌後綴"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.StopSuffix]")
                .AddTranslation(LocaleCode.EnUs, "The suffix to be added to the name on the stop.")
                .AddTranslation(LocaleCode.ZhHans, "要添加到站牌上的名称的后缀。")
                .AddTranslation(LocaleCode.ZhHant, "要添加到站牌上的名稱的後綴。"),

            new Translation("Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.ApplyXfixToTransportStation]")
                .AddTranslation(LocaleCode.EnUs, "Apply Prefix/Suffix to Stations")
                .AddTranslation(LocaleCode.ZhHans, "应用前后缀到站点")
                .AddTranslation(LocaleCode.ZhHant, "應用前後綴到站點"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.ApplyXfixToTransportStation]")
                .AddTranslation(LocaleCode.EnUs,
                    "Whether to apply the prefix and suffix to the station name on the stop, enable this option to set the prefix and suffix for the transport stations separately.")
                .AddTranslation(LocaleCode.ZhHans, "是否将前缀和后缀应用到站点名称中，选中此选项可单独设置对站点的前后缀。")
                .AddTranslation(LocaleCode.ZhHant, "是否將前綴和後綴應用到站點名稱中，選中此選項可單獨設置對站點的前後綴。"),

            new Translation("Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.TransportStationPrefix]")
                .AddTranslation(LocaleCode.EnUs, "Transport Station Prefix")
                .AddTranslation(LocaleCode.ZhHans, "站点前缀")
                .AddTranslation(LocaleCode.ZhHant, "站點前綴"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.TransportStationPrefix]")
                .AddTranslation(LocaleCode.EnUs, "The prefix to be added to the name on the transport station.")
                .AddTranslation(LocaleCode.ZhHans, "要添加到站点上的名称的前缀。")
                .AddTranslation(LocaleCode.ZhHant, "要添加到站點上的名稱的前綴。"),
            new Translation("Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.TransportStationSuffix]")
                .AddTranslation(LocaleCode.EnUs, "Transport Station Suffix")
                .AddTranslation(LocaleCode.ZhHans, "站点后缀")
                .AddTranslation(LocaleCode.ZhHant, "站點後綴"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.TransportStationSuffix]")
                .AddTranslation(LocaleCode.EnUs, "The suffix to be added to the name on the transport station.")
                .AddTranslation(LocaleCode.ZhHans, "要添加到站点上的名称的后缀。")
                .AddTranslation(LocaleCode.ZhHant, "要添加到站點上的名稱的後綴。"),

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
                    "When enabled, stations, stops, and buildings are automatically named when they are created, " +
                    "based on other setting options. Named according to their owner, or their current road.")
                .AddTranslation(LocaleCode.ZhHans, "启用后，根据不同的设置，自动为新建的站点、建筑命名。按照所有者、当前道路的顺序命名。")
                .AddTranslation(LocaleCode.ZhHant, "啟用後，根據不同的設置，自動為新建的站點、建築命名。按照所有者、當前道路的順序命名。"),
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
                .AddTranslation(LocaleCode.ZhHans, "启用后，允许从周围自长建筑中生成名称候选。[可能造成资源消耗增加，请按需开启。]")
                .AddTranslation(LocaleCode.ZhHant, "啟用後，允許從周圍自長建築中生成名稱候選。[可能造成資源消耗增加，請按需開啟。]"),
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
                .AddTranslation(LocaleCode.EnUs, "District Name Source")
                .AddTranslation(LocaleCode.ZhHans, "地区名称来源")
                .AddTranslation(LocaleCode.ZhHant, "地區名稱來源"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.EnableDistrict]")
                .AddTranslation(LocaleCode.EnUs,
                    "When enabled, allow the district name to be added as a source to the name candidates. ")
                .AddTranslation(LocaleCode.ZhHans, "启用后，允许将区域名称作为来源添加到名称候选列表中。")
                .AddTranslation(LocaleCode.ZhHant, "啟用後，允許將區域名稱作為來源添加到名稱候選列表中。"),
            new Translation(
                    "Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.EnableDistrictPrefix]")
                .AddTranslation(LocaleCode.EnUs, "Add District Name Prefix")
                .AddTranslation(LocaleCode.ZhHans, "添加地区名称前缀")
                .AddTranslation(LocaleCode.ZhHant, "添加地區名稱前綴"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.EnableDistrictPrefix]")
                .AddTranslation(LocaleCode.EnUs,
                    "When enabled, add the district name as a prefix/suffix for each name candidate (controlled by the reverse order option).")
                .AddTranslation(LocaleCode.ZhHans, "启用后，为每个名称候选都添加区域名称作为前缀/后缀（受到反转顺序控制）。")
                .AddTranslation(LocaleCode.ZhHant, "啟用後，為每個名稱候選都添加區域名稱作為前綴/後綴（受到反轉順序控制）。"),
            new Translation(
                    "Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.DistrictPrefixSeparately]")
                .AddTranslation(LocaleCode.EnUs, "List Separately With District Prefix")
                .AddTranslation(LocaleCode.ZhHans, "单独列出地区前缀项")
                .AddTranslation(LocaleCode.ZhHant, "單獨列出地區前綴項"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.DistrictPrefixSeparately]")
                .AddTranslation(LocaleCode.EnUs,
                    "When enabled, the names with the district prefix added will be listed separately without overriding the original name candidates.")
                .AddTranslation(LocaleCode.ZhHans, "启用后，将添加了地区前缀的名称候选单独列出，而不覆盖原有候选名称。")
                .AddTranslation(LocaleCode.ZhHant, "啟用後，將添加了地區前綴的名稱候選單獨列出，而不覆蓋原有候選名稱。"),

            new Translation("Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.RoadSource]")
                .AddTranslation(LocaleCode.EnUs, "Road Source")
                .AddTranslation(LocaleCode.ZhHans, "道路来源")
                .AddTranslation(LocaleCode.ZhHant, "道路來源"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.RoadSource]")
                .AddTranslation(LocaleCode.EnUs,
                    "When enabled, allow generate name candidates from surrounding roads. ")
                .AddTranslation(LocaleCode.ZhHans, "启用后，允许从道路中生成名称候选。")
                .AddTranslation(LocaleCode.ZhHant, "啟用後，允許從道路中生成名稱候選。"),
            new Translation("Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.IntersectionSource]")
                .AddTranslation(LocaleCode.EnUs, "Intersection Source")
                .AddTranslation(LocaleCode.ZhHans, "交叉口来源")
                .AddTranslation(LocaleCode.ZhHant, "交叉口來源"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.IntersectionSource]")
                .AddTranslation(LocaleCode.EnUs,
                    "When enabled, allow generate name candidates from intersections.")
                .AddTranslation(LocaleCode.ZhHans, "启用后，允许从交叉口中生成名称候选。")
                .AddTranslation(LocaleCode.ZhHant, "啟用後，允許從交叉口中生成名稱候選。"),
            new Translation(
                    "Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.TransportStationSource]")
                .AddTranslation(LocaleCode.EnUs, "Transport Station Source")
                .AddTranslation(LocaleCode.ZhHans, "交通站来源")
                .AddTranslation(LocaleCode.ZhHant, "交通站來源"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.TransportStationSource]")
                .AddTranslation(LocaleCode.EnUs,
                    "When enabled, allow generate name candidates from surrounding transport stations. ")
                .AddTranslation(LocaleCode.ZhHans, "启用后，允许从交通站中生成名称候选。")
                .AddTranslation(LocaleCode.ZhHant, "啟用後，允許從交通站中生成名稱候選。"),

            new Translation(
                    "Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.TransportDepotSource]")
                .AddTranslation(LocaleCode.EnUs, "Transport Depot Source")
                .AddTranslation(LocaleCode.ZhHans, "交通车库来源")
                .AddTranslation(LocaleCode.ZhHant, "交通車庫來源"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.TransportDepotSource]")
                .AddTranslation(LocaleCode.EnUs,
                    "When enabled, allow generate name candidates from surrounding transport depots. ")
                .AddTranslation(LocaleCode.ZhHans, "启用后，允许从交通车库中生成名称候选。")
                .AddTranslation(LocaleCode.ZhHant, "啟用後，允許從交通車庫中生成名稱候選。"),
            new Translation("Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.SchoolSource]")
                .AddTranslation(LocaleCode.EnUs, "School Source")
                .AddTranslation(LocaleCode.ZhHans, "学校来源")
                .AddTranslation(LocaleCode.ZhHant, "學校來源"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.SchoolSource]")
                .AddTranslation(LocaleCode.EnUs,
                    "When enabled, allow generate name candidates from surrounding schools. ")
                .AddTranslation(LocaleCode.ZhHans, "启用后，允许从学校中生成名称候选。")
                .AddTranslation(LocaleCode.ZhHant, "啟用後，允許從學校中生成名稱候選。"),
            new Translation("Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.FireStationSource]")
                .AddTranslation(LocaleCode.EnUs, "Fire Station Source")
                .AddTranslation(LocaleCode.ZhHans, "消防站来源")
                .AddTranslation(LocaleCode.ZhHant, "消防站來源"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.FireStationSource]")
                .AddTranslation(LocaleCode.EnUs,
                    "When enabled, allow generate name candidates from surrounding fire stations. ")
                .AddTranslation(LocaleCode.ZhHans, "启用后，允许从消防站中生成名称候选。")
                .AddTranslation(LocaleCode.ZhHant, "啟用後，允許從消防站中生成名稱候選。"),
            new Translation("Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.PoliceStationSource]")
                .AddTranslation(LocaleCode.EnUs, "Police Station Source")
                .AddTranslation(LocaleCode.ZhHans, "警察局来源")
                .AddTranslation(LocaleCode.ZhHant, "警察局來源"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.PoliceStationSource]")
                .AddTranslation(LocaleCode.EnUs,
                    "When enabled, allow generate name candidates from surrounding police stations. ")
                .AddTranslation(LocaleCode.ZhHans, "启用后，允许从警察局中生成名称候选。")
                .AddTranslation(LocaleCode.ZhHant, "啟用後，允許從警察局中生成名稱候選。"),
            new Translation("Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.HospitalSource]")
                .AddTranslation(LocaleCode.EnUs, "Hospital Source")
                .AddTranslation(LocaleCode.ZhHans, "医院来源")
                .AddTranslation(LocaleCode.ZhHant, "醫院來源"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.HospitalSource]")
                .AddTranslation(LocaleCode.EnUs,
                    "When enabled, allow generate name candidates from surrounding hospitals. ")
                .AddTranslation(LocaleCode.ZhHans, "启用后，允许从医院中生成名称候选。")
                .AddTranslation(LocaleCode.ZhHant, "啟用後，允許從醫院中生成名稱候選。"),
            new Translation("Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.ParkSource]")
                .AddTranslation(LocaleCode.EnUs, "Park Source")
                .AddTranslation(LocaleCode.ZhHans, "公园来源")
                .AddTranslation(LocaleCode.ZhHant, "公園來源"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.ParkSource]")
                .AddTranslation(LocaleCode.EnUs,
                    "When enabled, allow generate name candidates from surrounding parks. ")
                .AddTranslation(LocaleCode.ZhHans, "启用后，允许从公园中生成名称候选。")
                .AddTranslation(LocaleCode.ZhHant, "啟用後，允許從公園中生成名稱候選。"),
            new Translation("Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.ElectricitySource]")
                .AddTranslation(LocaleCode.EnUs, "Electricity Facility Source")
                .AddTranslation(LocaleCode.ZhHans, "电力设施来源")
                .AddTranslation(LocaleCode.ZhHant, "電力設施來源"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.ElectricitySource]")
                .AddTranslation(LocaleCode.EnUs,
                    "When enabled, allow generate name candidates from surrounding electricity facilities. ")
                .AddTranslation(LocaleCode.ZhHans, "启用后，允许从电力设施中生成名称候选。")
                .AddTranslation(LocaleCode.ZhHant, "啟用後，允許從電力設施中生成名稱候選。"),
            // Water
            new Translation("Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.WaterSource]")
                .AddTranslation(LocaleCode.EnUs, "Water Facility Source")
                .AddTranslation(LocaleCode.ZhHans, "水务设施来源")
                .AddTranslation(LocaleCode.ZhHant, "水務設施來源"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.WaterSource]")
                .AddTranslation(LocaleCode.EnUs,
                    "When enabled, allow generate name candidates from surrounding water facilities. ")
                .AddTranslation(LocaleCode.ZhHans, "启用后，允许从水务设施中生成名称候选。")
                .AddTranslation(LocaleCode.ZhHant, "啟用後，允許從水務設施中生成名稱候選。"),
            // Sewage
            new Translation("Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.SewageSource]")
                .AddTranslation(LocaleCode.EnUs, "Sewage Facility Source")
                .AddTranslation(LocaleCode.ZhHans, "污水设施来源")
                .AddTranslation(LocaleCode.ZhHant, "污水設施來源"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.SewageSource]")
                .AddTranslation(LocaleCode.EnUs,
                    "When enabled, allow generate name candidates from surrounding sewage facilities. ")
                .AddTranslation(LocaleCode.ZhHans, "启用后，允许从污水设施中生成名称候选。")
                .AddTranslation(LocaleCode.ZhHant, "啟用後，允許從污水設施中生成名稱候選。"),
            new Translation("Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.GarbageSource]")
                .AddTranslation(LocaleCode.EnUs, "Garbage Facility Source")
                .AddTranslation(LocaleCode.ZhHans, "垃圾设施来源")
                .AddTranslation(LocaleCode.ZhHant, "垃圾設施來源"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.GarbageSource]")
                .AddTranslation(LocaleCode.EnUs,
                    "When enabled, allow generate name candidates from surrounding garbage facilities. ")
                .AddTranslation(LocaleCode.ZhHans, "启用后，允许从垃圾设施中生成名称候选。")
                .AddTranslation(LocaleCode.ZhHant, "啟用後，允許從垃圾設施中生成名稱候選。"),
            new Translation("Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.DisasterSource]")
                .AddTranslation(LocaleCode.EnUs, "Disaster Facility Source")
                .AddTranslation(LocaleCode.ZhHans, "灾害设施来源")
                .AddTranslation(LocaleCode.ZhHant, "災害設施來源"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.DisasterSource]")
                .AddTranslation(LocaleCode.EnUs,
                    "When enabled, allow generate name candidates from surrounding disaster facilities. ")
                .AddTranslation(LocaleCode.ZhHans, "启用后，允许从灾害设施中生成名称候选。")
                .AddTranslation(LocaleCode.ZhHant, "啟用後，允許從災害設施中生成名稱候選。"),
            new Translation("Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.DeathcareSource]")
                .AddTranslation(LocaleCode.EnUs, "Deathcare Facility Source")
                .AddTranslation(LocaleCode.ZhHans, "殡仪设施来源")
                .AddTranslation(LocaleCode.ZhHant, "殯儀設施來源"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.DeathcareSource]")
                .AddTranslation(LocaleCode.EnUs,
                    "When enabled, allow generate name candidates from surrounding deathcare facilities. ")
                .AddTranslation(LocaleCode.ZhHans, "启用后，允许从殡仪设施中生成名称候选。")
                .AddTranslation(LocaleCode.ZhHant, "啟用後，允許從殯儀設施中生成名稱候選。"),
            new Translation("Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.TelecomSource]")
                .AddTranslation(LocaleCode.EnUs, "Telecom Facility Source")
                .AddTranslation(LocaleCode.ZhHans, "电信设施来源")
                .AddTranslation(LocaleCode.ZhHant, "電信設施來源"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.TelecomSource]")
                .AddTranslation(LocaleCode.EnUs,
                    "When enabled, allow generate name candidates from surrounding telecom facilities. ")
                .AddTranslation(LocaleCode.ZhHans, "启用后，允许从电信设施中生成名称候选。")
                .AddTranslation(LocaleCode.ZhHant, "啟用後，允許從電信設施中生成名稱候選。"),
            new Translation("Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.PostSource]")
                .AddTranslation(LocaleCode.EnUs, "Post Facility Source")
                .AddTranslation(LocaleCode.ZhHans, "邮政设施来源")
                .AddTranslation(LocaleCode.ZhHant, "郵政設施來源"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.PostSource]")
                .AddTranslation(LocaleCode.EnUs,
                    "When enabled, allow generate name candidates from surrounding post facilities. ")
                .AddTranslation(LocaleCode.ZhHans, "启用后，允许从邮政设施中生成名称候选。")
                .AddTranslation(LocaleCode.ZhHant, "啟用後，允許從郵政設施中生成名稱候選。"),
            new Translation("Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.ParkingSource]")
                .AddTranslation(LocaleCode.EnUs, "Parking Facility Source")
                .AddTranslation(LocaleCode.ZhHans, "停车设施来源")
                .AddTranslation(LocaleCode.ZhHant, "停車設施來源"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.ParkingSource]")
                .AddTranslation(LocaleCode.EnUs,
                    "When enabled, allow generate name candidates from surrounding parking facilities. ")
                .AddTranslation(LocaleCode.ZhHans, "启用后，允许从停车设施中生成名称候选。")
                .AddTranslation(LocaleCode.ZhHant, "啟用後，允許從停車設施中生成名稱候選。"),
            new Translation("Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.RoadFacilitySource]")
                .AddTranslation(LocaleCode.EnUs, "Road Facility Source")
                .AddTranslation(LocaleCode.ZhHans, "道路设施来源")
                .AddTranslation(LocaleCode.ZhHant, "道路設施來源"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.RoadFacilitySource]")
                .AddTranslation(LocaleCode.EnUs,
                    "When enabled, allow generate name candidates from surrounding road facilities. ")
                .AddTranslation(LocaleCode.ZhHans, "启用后，允许从道路设施中生成名称候选。")
                .AddTranslation(LocaleCode.ZhHant, "啟用後，允許從道路設施中生成名稱候選。"),
            new Translation("Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.AdminSource]")
                .AddTranslation(LocaleCode.EnUs, "Administrative Facility Source")
                .AddTranslation(LocaleCode.ZhHans, "行政设施来源")
                .AddTranslation(LocaleCode.ZhHant, "行政設施來源"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.AdminSource]")
                .AddTranslation(LocaleCode.EnUs,
                    "When enabled, allow generate name candidates from surrounding administrative facilities. ")
                .AddTranslation(LocaleCode.ZhHans, "启用后，允许从行政设施中生成名称候选。")
                .AddTranslation(LocaleCode.ZhHant, "啟用後，允許從行政設施中生成名稱候選。"),

            new Translation("Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.EnableAllSources]")
                .AddTranslation(LocaleCode.EnUs, "Enable All Sources")
                .AddTranslation(LocaleCode.ZhHans, "启用所有来源")
                .AddTranslation(LocaleCode.ZhHant, "啟用所有來源"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.EnableAllSources]")
                .AddTranslation(LocaleCode.EnUs, "Enable all naming sources.")
                .AddTranslation(LocaleCode.ZhHans, "启用所有名称来源")
                .AddTranslation(LocaleCode.ZhHant, "啟用所有名稱來源"),
            new Translation("Options.WARNING[StationNaming.StationNaming.Mod.StationNamingSettings.EnableAllSources]")
                .AddTranslation(LocaleCode.EnUs, "Are you sure you want to enable all naming sources?")
                .AddTranslation(LocaleCode.ZhHans, "你确定要启用所有名称来源吗？")
                .AddTranslation(LocaleCode.ZhHant, "你確定要啟用所有名稱來源嗎？"),
            new Translation("Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.DisableAllSources]")
                .AddTranslation(LocaleCode.EnUs, "Disable All Sources")
                .AddTranslation(LocaleCode.ZhHans, "禁用所有来源")
                .AddTranslation(LocaleCode.ZhHant, "啟用所有來源"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.DisableAllSources]")
                .AddTranslation(LocaleCode.EnUs, "Disable all naming sources.")
                .AddTranslation(LocaleCode.ZhHans, "禁用所有名称来源")
                .AddTranslation(LocaleCode.ZhHant, "禁用所有名稱來源"),
            new Translation("Options.WARNING[StationNaming.StationNaming.Mod.StationNamingSettings.DisableAllSources]")
                .AddTranslation(LocaleCode.EnUs, "Are you sure you want to enable all naming sources?")
                .AddTranslation(LocaleCode.ZhHans, "你确定要启用所有名称来源吗？")
                .AddTranslation(LocaleCode.ZhHant, "你確定要禁用所有名稱來源嗎？"),

            new Translation(
                    "Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.TransportStopAutoNaming]")
                .AddTranslation(LocaleCode.EnUs, "Auto Naming Transport Stop")
                .AddTranslation(LocaleCode.ZhHans, "交通站点自动命名")
                .AddTranslation(LocaleCode.ZhHant, "交通站點自動命名"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.TransportStopAutoNaming]")
                .AddTranslation(LocaleCode.EnUs,
                    "When enabled, transport stops will be automatically named when created. ")
                .AddTranslation(LocaleCode.ZhHans, "启用后，自动为新建的交通站点命名。")
                .AddTranslation(LocaleCode.ZhHant, "啟用後，自動為新建的交通站點命名。"),
            new Translation(
                    "Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.TransportStationAutoNaming]")
                .AddTranslation(LocaleCode.EnUs, "Auto Naming Transport Station")
                .AddTranslation(LocaleCode.ZhHans, "交通站自动命名")
                .AddTranslation(LocaleCode.ZhHant, "交通站自動命名"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.TransportStationAutoNaming]")
                .AddTranslation(LocaleCode.EnUs,
                    "When enabled, transport stations will be automatically named when created. ")
                .AddTranslation(LocaleCode.ZhHans, "启用后，自动为新建的交通站命名。")
                .AddTranslation(LocaleCode.ZhHant, "啟用後，自動為新建的交通站命名。"),
            new Translation(
                    "Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.TransportDepotAutoNaming]")
                .AddTranslation(LocaleCode.EnUs, "Auto Naming Transport Depot")
                .AddTranslation(LocaleCode.ZhHans, "交通车库自动命名")
                .AddTranslation(LocaleCode.ZhHant, "交通車庫自動命名"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.TransportDepotAutoNaming]")
                .AddTranslation(LocaleCode.EnUs,
                    "When enabled, transport depots will be automatically named when created. ")
                .AddTranslation(LocaleCode.ZhHans, "启用后，自动为新建的交通车库命名。")
                .AddTranslation(LocaleCode.ZhHant, "啟用後，自動為新建的交通車庫命名。"),
            new Translation(
                    "Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.SchoolAutoNaming]")
                .AddTranslation(LocaleCode.EnUs, "Auto Naming School")
                .AddTranslation(LocaleCode.ZhHans, "学校自动命名")
                .AddTranslation(LocaleCode.ZhHant, "學校自動命名"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.SchoolAutoNaming]")
                .AddTranslation(LocaleCode.EnUs,
                    "When enabled, schools will be automatically named when created. ")
                .AddTranslation(LocaleCode.ZhHans, "启用后，自动为新建的学校命名。")
                .AddTranslation(LocaleCode.ZhHant, "啟用後，自動為新建的學校命名。"),
            new Translation(
                    "Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.FireStationAutoNaming]")
                .AddTranslation(LocaleCode.EnUs, "Auto Naming Fire Station")
                .AddTranslation(LocaleCode.ZhHans, "消防站自动命名")
                .AddTranslation(LocaleCode.ZhHant, "消防站自動命名"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.FireStationAutoNaming]")
                .AddTranslation(LocaleCode.EnUs,
                    "When enabled, fire stations will be automatically named when created. ")
                .AddTranslation(LocaleCode.ZhHans, "启用后，自动为新建的消防站命名。")
                .AddTranslation(LocaleCode.ZhHant, "啟用後，自動為新建的消防站命名。"),
            new Translation(
                    "Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.PoliceStationAutoNaming]")
                .AddTranslation(LocaleCode.EnUs, "Auto Naming Police Station")
                .AddTranslation(LocaleCode.ZhHans, "警察局自动命名")
                .AddTranslation(LocaleCode.ZhHant, "警察局自動命名"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.PoliceStationAutoNaming]")
                .AddTranslation(LocaleCode.EnUs,
                    "When enabled, police stations will be automatically named when created. ")
                .AddTranslation(LocaleCode.ZhHans, "启用后，自动为新建的警察局命名。")
                .AddTranslation(LocaleCode.ZhHant, "啟用後，自動為新建的警察局命名。"),
            new Translation(
                    "Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.HospitalAutoNaming]")
                .AddTranslation(LocaleCode.EnUs, "Auto Naming Hospital")
                .AddTranslation(LocaleCode.ZhHans, "医院自动命名")
                .AddTranslation(LocaleCode.ZhHant, "醫院自動命名"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.HospitalAutoNaming]")
                .AddTranslation(LocaleCode.EnUs,
                    "When enabled, hospitals will be automatically named when created. ")
                .AddTranslation(LocaleCode.ZhHans, "启用后，自动为新建的医院命名。")
                .AddTranslation(LocaleCode.ZhHant, "啟用後，自動為新建的醫院命名。"),
            new Translation(
                    "Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.ParkAutoNaming]")
                .AddTranslation(LocaleCode.EnUs, "Auto Naming Park")
                .AddTranslation(LocaleCode.ZhHans, "公园自动命名")
                .AddTranslation(LocaleCode.ZhHant, "公園自動命名"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.ParkAutoNaming]")
                .AddTranslation(LocaleCode.EnUs,
                    "When enabled, parks will be automatically named when created. ")
                .AddTranslation(LocaleCode.ZhHans, "启用后，自动为新建的公园命名。")
                .AddTranslation(LocaleCode.ZhHant, "啟用後，自動為新建的公園命名。"),
            new Translation(
                    "Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.ElectricityAutoNaming]")
                .AddTranslation(LocaleCode.EnUs, "Auto Naming Electricity Facility")
                .AddTranslation(LocaleCode.ZhHans, "电力设施自动命名")
                .AddTranslation(LocaleCode.ZhHant, "電力設施自動命名"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.ElectricityAutoNaming]")
                .AddTranslation(LocaleCode.EnUs,
                    "When enabled, electricity facilities will be automatically named when created.")
                .AddTranslation(LocaleCode.ZhHans, "启用后，自动为新建的电力设施命名。")
                .AddTranslation(LocaleCode.ZhHant, "啟用後，自動為新建的電力設施命名。"),
            new Translation(
                    "Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.WaterAutoNaming]")
                .AddTranslation(LocaleCode.EnUs, "Auto Naming Water Facility")
                .AddTranslation(LocaleCode.ZhHans, "净水设施自动命名")
                .AddTranslation(LocaleCode.ZhHant, "淨水設施自動命名"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.WaterAutoNaming]")
                .AddTranslation(LocaleCode.EnUs,
                    "When enabled, water facilities will be automatically named when created.")
                .AddTranslation(LocaleCode.ZhHans, "启用后，自动为新建的净水设施命名。")
                .AddTranslation(LocaleCode.ZhHant, "啟用後，自動為新建的淨水設施命名。"),
            new Translation(
                    "Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.SewageAutoNaming]")
                .AddTranslation(LocaleCode.EnUs, "Auto Naming Sewage Facility")
                .AddTranslation(LocaleCode.ZhHans, "污水设施自动命名")
                .AddTranslation(LocaleCode.ZhHant, "污水設施自動命名"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.SewageAutoNaming]")
                .AddTranslation(LocaleCode.EnUs,
                    "When enabled, sewage facilities will be automatically named when created.")
                .AddTranslation(LocaleCode.ZhHans, "启用后，自动为新建的污水设施命名。")
                .AddTranslation(LocaleCode.ZhHant, "啟用後，自動為新建的污水設施命名。"),
            new Translation(
                    "Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.GarbageAutoNaming]")
                .AddTranslation(LocaleCode.EnUs, "Auto Naming Garbage Facility")
                .AddTranslation(LocaleCode.ZhHans, "垃圾设施自动命名")
                .AddTranslation(LocaleCode.ZhHant, "垃圾設施自動命名"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.GarbageAutoNaming]")
                .AddTranslation(LocaleCode.EnUs,
                    "When enabled, garbage facilities will be automatically named when created.")
                .AddTranslation(LocaleCode.ZhHans, "启用后，自动为新建的垃圾设施命名。")
                .AddTranslation(LocaleCode.ZhHant, "啟用後，自動為新建的垃圾設施命名。"),
            new Translation(
                    "Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.DisasterAutoNaming]")
                .AddTranslation(LocaleCode.EnUs, "Auto Naming Disaster Facility")
                .AddTranslation(LocaleCode.ZhHans, "灾害设施自动命名")
                .AddTranslation(LocaleCode.ZhHant, "災害設施自動命名"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.DisasterAutoNaming]")
                .AddTranslation(LocaleCode.EnUs,
                    "When enabled, disaster facilities will be automatically named when created.")
                .AddTranslation(LocaleCode.ZhHans, "启用后，自动为新建的灾害设施命名。")
                .AddTranslation(LocaleCode.ZhHant, "啟用後，自動為新建的災害設施命名。"),
            new Translation(
                    "Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.DeathcareAutoNaming]")
                .AddTranslation(LocaleCode.EnUs, "Auto Naming Deathcare Facility")
                .AddTranslation(LocaleCode.ZhHans, "殡仪设施自动命名")
                .AddTranslation(LocaleCode.ZhHant, "殯儀設施自動命名"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.DeathcareAutoNaming]")
                .AddTranslation(LocaleCode.EnUs,
                    "When enabled, deathcare facilities will be automatically named when created.")
                .AddTranslation(LocaleCode.ZhHans, "启用后，自动为新建的殡仪设施命名。")
                .AddTranslation(LocaleCode.ZhHant, "啟用後，自動為新建的殯儀設施命名。"),
            new Translation(
                    "Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.TelecomAutoNaming]")
                .AddTranslation(LocaleCode.EnUs, "Auto Naming Telecom Facility")
                .AddTranslation(LocaleCode.ZhHans, "电信设施自动命名")
                .AddTranslation(LocaleCode.ZhHant, "電信設施自動命名"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.TelecomAutoNaming]")
                .AddTranslation(LocaleCode.EnUs,
                    "When enabled, telecom facilities will be automatically named when created.")
                .AddTranslation(LocaleCode.ZhHans, "启用后，自动为新建的电信设施命名。")
                .AddTranslation(LocaleCode.ZhHant, "啟用後，自動為新建的電信設施命名。"),

            new Translation(
                    "Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.PostAutoNaming]")
                .AddTranslation(LocaleCode.EnUs, "Auto Naming Post Facility")
                .AddTranslation(LocaleCode.ZhHans, "邮政设施自动命名")
                .AddTranslation(LocaleCode.ZhHant, "郵政設施自動命名"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.PostAutoNaming]")
                .AddTranslation(LocaleCode.EnUs,
                    "When enabled, post facilities will be automatically named when created.")
                .AddTranslation(LocaleCode.ZhHans, "启用后，自动为新建的邮政设施命名。")
                .AddTranslation(LocaleCode.ZhHant, "啟用後，自動為新建的郵政設施命名。"),
            new Translation(
                    "Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.ParkingAutoNaming]")
                .AddTranslation(LocaleCode.EnUs, "Auto Naming Parking Lot")
                .AddTranslation(LocaleCode.ZhHans, "停车场自动命名")
                .AddTranslation(LocaleCode.ZhHant, "停車場自動命名"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.ParkingAutoNaming]")
                .AddTranslation(LocaleCode.EnUs,
                    "When enabled, parking lots will be automatically named when created.")
                .AddTranslation(LocaleCode.ZhHans, "启用后，自动为新建的停車場命名。")
                .AddTranslation(LocaleCode.ZhHant, "啟用後，自動為新建的停車場命名。"),
            new Translation(
                    "Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.CityServiceAutoNaming]")
                .AddTranslation(LocaleCode.EnUs, "Auto Naming Other City Service Buildings")
                .AddTranslation(LocaleCode.ZhHans, "其他城市服务建筑自动命名")
                .AddTranslation(LocaleCode.ZhHant, "其他城市服務建築自動命名"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.CityServiceAutoNaming]")
                .AddTranslation(LocaleCode.EnUs,
                    "When enabled, other city service buildings will be automatically named when created.")
                .AddTranslation(LocaleCode.ZhHans, "启用后，自动为新建的其他城市服务建筑命名。")
                .AddTranslation(LocaleCode.ZhHant, "啟用後，自動為新建的其他城市服務建築命名。"),
            new Translation(
                    "Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.RoadFacilityAutoNaming]")
                .AddTranslation(LocaleCode.EnUs, "Auto Naming Road Facility")
                .AddTranslation(LocaleCode.ZhHans, "道路设施自动命名")
                .AddTranslation(LocaleCode.ZhHant, "道路設施自動命名"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.RoadFacilityAutoNaming]")
                .AddTranslation(LocaleCode.EnUs,
                    "When enabled, road facilities will be automatically named when created.")
                .AddTranslation(LocaleCode.ZhHans, "启用后，自动为新建的道路设施命名。")
                .AddTranslation(LocaleCode.ZhHant, "啟用後，自動為新建的道路設施命名。"),
            new Translation(
                    "Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.AdminAutoNaming]")
                .AddTranslation(LocaleCode.EnUs, "Auto Naming Administration Building")
                .AddTranslation(LocaleCode.ZhHans, "行政建筑自动命名")
                .AddTranslation(LocaleCode.ZhHant, "行政建築自動命名"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.AdminAutoNaming]")
                .AddTranslation(LocaleCode.EnUs,
                    "When enabled, administrative buildings will be automatically named when created.")
                .AddTranslation(LocaleCode.ZhHans, "启用后，自动为新建的行政建筑命名。")
                .AddTranslation(LocaleCode.ZhHant, "啟用後，自動為新建的行政建築命名。"),

            new Translation(
                    "Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.EnableCityServiceAutoNaming]")
                .AddTranslation(LocaleCode.EnUs, "Enable Auto Naming All City Services")
                .AddTranslation(LocaleCode.ZhHans, "启用所有城市服务自动命名")
                .AddTranslation(LocaleCode.ZhHant, "啟用所有城市服務自動命名"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.EnableCityServiceAutoNaming]")
                .AddTranslation(LocaleCode.EnUs, "Enable all city service buildings auto naming.")
                .AddTranslation(LocaleCode.ZhHans, "启用所有城市服务建筑自动命名。")
                .AddTranslation(LocaleCode.ZhHant, "啟用所有城市服務建築自動命名。"),
            new Translation(
                    "Options.WARNING[StationNaming.StationNaming.Mod.StationNamingSettings.EnableCityServiceAutoNaming]")
                .AddTranslation(LocaleCode.EnUs,
                    "Are you sure you want to enable all city service buildings auto naming?")
                .AddTranslation(LocaleCode.ZhHans, "你确定要启用所有城市服务建筑自动命名吗？")
                .AddTranslation(LocaleCode.ZhHant, "你確定要啟用所有城市服務建築自動命名嗎？"),

            new Translation(
                    "Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.DisableCityServiceAutoNaming]")
                .AddTranslation(LocaleCode.EnUs, "Disable Auto Naming All City Services")
                .AddTranslation(LocaleCode.ZhHans, "禁用所有城市服务自动命名")
                .AddTranslation(LocaleCode.ZhHant, "禁用所有城市服務自動命名"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.DisableCityServiceAutoNaming]")
                .AddTranslation(LocaleCode.EnUs, "Disable all city service buildings auto naming.")
                .AddTranslation(LocaleCode.ZhHans, "禁用所有城市服务建筑自动命名。")
                .AddTranslation(LocaleCode.ZhHant, "禁用所有城市服務建築自動命名。"),
            new Translation(
                    "Options.WARNING[StationNaming.StationNaming.Mod.StationNamingSettings.DisableCityServiceAutoNaming]")
                .AddTranslation(LocaleCode.EnUs,
                    "Are you sure you want to disable all city service buildings auto naming?")
                .AddTranslation(LocaleCode.ZhHans, "你确定要禁用所有城市服务建筑自动命名吗？")
                .AddTranslation(LocaleCode.ZhHant, "你確定要禁用所有城市服務建築自動命名嗎？"),

            new Translation("Options.GROUP[StationNaming.StationNaming.Mod.Experimental]")
                .AddTranslation(LocaleCode.EnUs, "Experimental")
                .AddTranslation(LocaleCode.ZhHans, "实验性")
                .AddTranslation(LocaleCode.ZhHant, "實驗性"),
            new Translation("Options.GROUP[StationNaming.StationNaming.Mod.Stops]")
                .AddTranslation(LocaleCode.EnUs, "Stops")
                .AddTranslation(LocaleCode.ZhHans, "站牌")
                .AddTranslation(LocaleCode.ZhHant, "站牌"),
            new Translation("Options.GROUP[StationNaming.StationNaming.Mod.Road]")
                .AddTranslation(LocaleCode.EnUs, "Road")
                .AddTranslation(LocaleCode.ZhHans, "道路")
                .AddTranslation(LocaleCode.ZhHant, "道路"),
            new Translation("Options.GROUP[StationNaming.StationNaming.Mod.Transport]")
                .AddTranslation(LocaleCode.EnUs, "Transport")
                .AddTranslation(LocaleCode.ZhHans, "交通")
                .AddTranslation(LocaleCode.ZhHant, "交通"),
            new Translation("Options.GROUP[StationNaming.StationNaming.Mod.District]")
                .AddTranslation(LocaleCode.EnUs, "District")
                .AddTranslation(LocaleCode.ZhHans, "区域")
                .AddTranslation(LocaleCode.ZhHant, "區域"),
            new Translation("Options.GROUP[StationNaming.StationNaming.Mod.Building]")
                .AddTranslation(LocaleCode.EnUs, "Building")
                .AddTranslation(LocaleCode.ZhHans, "建筑")
                .AddTranslation(LocaleCode.ZhHant, "建築"),
            new Translation("Options.GROUP[StationNaming.StationNaming.Mod.Spawnable]")
                .AddTranslation(LocaleCode.EnUs, "Spawnable Building")
                .AddTranslation(LocaleCode.ZhHans, "自长建筑")
                .AddTranslation(LocaleCode.ZhHant, "自長建築"),
            new Translation("Options.GROUP[StationNaming.StationNaming.Mod.School]")
                .AddTranslation(LocaleCode.EnUs, "School")
                .AddTranslation(LocaleCode.ZhHans, "学校")
                .AddTranslation(LocaleCode.ZhHant, "學校"),
            new Translation("Options.GROUP[StationNaming.StationNaming.Mod.FireStation]")
                .AddTranslation(LocaleCode.EnUs, "Fire Station")
                .AddTranslation(LocaleCode.ZhHans, "消防站")
                .AddTranslation(LocaleCode.ZhHant, "消防站"),
            new Translation("Options.GROUP[StationNaming.StationNaming.Mod.PoliceStation]")
                .AddTranslation(LocaleCode.EnUs, "Police Station")
                .AddTranslation(LocaleCode.ZhHans, "警察局")
                .AddTranslation(LocaleCode.ZhHant, "警察局"),
            new Translation("Options.GROUP[StationNaming.StationNaming.Mod.Hospital]")
                .AddTranslation(LocaleCode.EnUs, "Hospital")
                .AddTranslation(LocaleCode.ZhHans, "医院")
                .AddTranslation(LocaleCode.ZhHant, "醫院"),
            new Translation("Options.GROUP[StationNaming.StationNaming.Mod.Park]")
                .AddTranslation(LocaleCode.EnUs, "Park")
                .AddTranslation(LocaleCode.ZhHans, "公园")
                .AddTranslation(LocaleCode.ZhHant, "公園"),
            new Translation("Options.GROUP[StationNaming.StationNaming.Mod.Electricity]")
                .AddTranslation(LocaleCode.EnUs, "Electricity")
                .AddTranslation(LocaleCode.ZhHans, "电力")
                .AddTranslation(LocaleCode.ZhHant, "電力"),
            new Translation("Options.GROUP[StationNaming.StationNaming.Mod.Water]")
                .AddTranslation(LocaleCode.EnUs, "Water")
                .AddTranslation(LocaleCode.ZhHans, "净水")
                .AddTranslation(LocaleCode.ZhHant, "淨水"),
            new Translation("Options.GROUP[StationNaming.StationNaming.Mod.Sewage]")
                .AddTranslation(LocaleCode.EnUs, "Sewage")
                .AddTranslation(LocaleCode.ZhHans, "污水")
                .AddTranslation(LocaleCode.ZhHant, "污水"),
            new Translation("Options.GROUP[StationNaming.StationNaming.Mod.Garbage]")
                .AddTranslation(LocaleCode.EnUs, "Garbage")
                .AddTranslation(LocaleCode.ZhHans, "垃圾")
                .AddTranslation(LocaleCode.ZhHant, "垃圾"),
            new Translation("Options.GROUP[StationNaming.StationNaming.Mod.Disaster]")
                .AddTranslation(LocaleCode.EnUs, "Disaster")
                .AddTranslation(LocaleCode.ZhHans, "灾害")
                .AddTranslation(LocaleCode.ZhHant, "災害"),
            new Translation("Options.GROUP[StationNaming.StationNaming.Mod.Deathcare]")
                .AddTranslation(LocaleCode.EnUs, "Deathcare")
                .AddTranslation(LocaleCode.ZhHans, "殡仪")
                .AddTranslation(LocaleCode.ZhHant, "殯儀"),
            new Translation("Options.GROUP[StationNaming.StationNaming.Mod.Telecom]")
                .AddTranslation(LocaleCode.EnUs, "Telecom")
                .AddTranslation(LocaleCode.ZhHans, "电信")
                .AddTranslation(LocaleCode.ZhHant, "電信"),
            new Translation("Options.GROUP[StationNaming.StationNaming.Mod.Post]")
                .AddTranslation(LocaleCode.EnUs, "Post")
                .AddTranslation(LocaleCode.ZhHans, "邮政")
                .AddTranslation(LocaleCode.ZhHant, "郵政"),
            new Translation("Options.GROUP[StationNaming.StationNaming.Mod.Parking]")
                .AddTranslation(LocaleCode.EnUs, "Parking Lot")
                .AddTranslation(LocaleCode.ZhHans, "停车场")
                .AddTranslation(LocaleCode.ZhHant, "停車場"),
            new Translation("Options.GROUP[StationNaming.StationNaming.Mod.CityService]")
                .AddTranslation(LocaleCode.EnUs, "City Service")
                .AddTranslation(LocaleCode.ZhHans, "城市服务")
                .AddTranslation(LocaleCode.ZhHant, "城市服務"),
            new Translation("Options.GROUP[StationNaming.StationNaming.Mod.Other]")
                .AddTranslation(LocaleCode.EnUs, "Other")
                .AddTranslation(LocaleCode.ZhHans, "其他")
                .AddTranslation(LocaleCode.ZhHant, "其他"),

            new Translation("Options.TAB[StationNaming.StationNaming.Mod.General]")
                .AddTranslation(LocaleCode.EnUs, "General")
                .AddTranslation(LocaleCode.ZhHans, "常规")
                .AddTranslation(LocaleCode.ZhHant, "常規"),
            new Translation("Options.TAB[StationNaming.StationNaming.Mod.Targets]")
                .AddTranslation(LocaleCode.EnUs, "Targets")
                .AddTranslation(LocaleCode.ZhHans, "目标")
                .AddTranslation(LocaleCode.ZhHant, "目標"),
            new Translation("Options.TAB[StationNaming.StationNaming.Mod.Sources]")
                .AddTranslation(LocaleCode.EnUs, "Sources")
                .AddTranslation(LocaleCode.ZhHans, "来源")
                .AddTranslation(LocaleCode.ZhHant, "來源"),

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
                .AddTranslation(LocaleCode.EnUs, "Electricity")
                .AddTranslation(LocaleCode.ZhHans, "电力")
                .AddTranslation(LocaleCode.ZhHant, "電力"),
            new Translation("StationNaming.NameSource[Water]")
                .AddTranslation(LocaleCode.EnUs, "Water")
                .AddTranslation(LocaleCode.ZhHans, "净水")
                .AddTranslation(LocaleCode.ZhHant, "淨水"),
            new Translation("StationNaming.NameSource[Sewage]")
                .AddTranslation(LocaleCode.EnUs, "Sewage")
                .AddTranslation(LocaleCode.ZhHans, "污水")
                .AddTranslation(LocaleCode.ZhHant, "污水"),
            new Translation("StationNaming.NameSource[Admin]")
                .AddTranslation(LocaleCode.EnUs, "Administration")
                .AddTranslation(LocaleCode.ZhHans, "行政")
                .AddTranslation(LocaleCode.ZhHant, "行政"),
            new Translation("StationNaming.NameSource[RoadFacility]")
                .AddTranslation(LocaleCode.EnUs, "Road Service")
                .AddTranslation(LocaleCode.ZhHans, "道路服务")
                .AddTranslation(LocaleCode.ZhHant, "道路服務"),
            new Translation("StationNaming.NameSource[Garbage]")
                .AddTranslation(LocaleCode.EnUs, "Garbage")
                .AddTranslation(LocaleCode.ZhHans, "垃圾")
                .AddTranslation(LocaleCode.ZhHant, "垃圾"),
            new Translation("StationNaming.NameSource[Disaster]")
                .AddTranslation(LocaleCode.EnUs, "Disaster")
                .AddTranslation(LocaleCode.ZhHans, "灾害")
                .AddTranslation(LocaleCode.ZhHant, "災害"),
            new Translation("StationNaming.NameSource[Deathcare]")
                .AddTranslation(LocaleCode.EnUs, "Deathcare")
                .AddTranslation(LocaleCode.ZhHans, "殡仪")
                .AddTranslation(LocaleCode.ZhHant, "殯儀"),
            new Translation("StationNaming.NameSource[Telecom]")
                .AddTranslation(LocaleCode.EnUs, "Telecom")
                .AddTranslation(LocaleCode.ZhHans, "电信")
                .AddTranslation(LocaleCode.ZhHant, "電信"),
            new Translation("StationNaming.NameSource[Post]")
                .AddTranslation(LocaleCode.EnUs, "Post")
                .AddTranslation(LocaleCode.ZhHans, "邮政")
                .AddTranslation(LocaleCode.ZhHant, "郵政"),
            new Translation("StationNaming.NameSource[Parking]")
                .AddTranslation(LocaleCode.EnUs, "Parking Lot")
                .AddTranslation(LocaleCode.ZhHans, "停车场")
                .AddTranslation(LocaleCode.ZhHant, "停車場"),
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
        };

        public static void LoadTranslations(StationNamingSettings settings)
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
