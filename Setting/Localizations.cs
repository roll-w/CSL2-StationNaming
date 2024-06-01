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
                    "The maximum depth to search for name candidates, affecting the generation of name sources for nearby buildings. " +
                    "It is not recommended to set it too large to avoid affecting game performance.")
                .AddTranslation(LocaleCode.ZhHans, "搜索名称候选的最大深度，影响附近建筑名称来源生成。不建议设置过大，以免影响游戏性能。")
                .AddTranslation(LocaleCode.ZhHant, "搜索名稱候選的最大深度，影響附近建築名稱來源生成。不建議設置過大，以免影響遊戲性能。"),
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
                .AddTranslation(LocaleCode.EnUs, "Enable District Name Source")
                .AddTranslation(LocaleCode.ZhHans, "启用地区名称来源")
                .AddTranslation(LocaleCode.ZhHant, "啟用地區名稱來源"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.EnableDistrict]")
                .AddTranslation(LocaleCode.EnUs,
                    "When enabled, allow the district name to be added as a source to the name candidates.")
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
                    "Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.TransportStopAutoNaming]")
                .AddTranslation(LocaleCode.EnUs, "Enable Transport Stop Naming")
                .AddTranslation(LocaleCode.ZhHans, "启用交通站点自动命名")
                .AddTranslation(LocaleCode.ZhHant, "啟用交通站點自動命名"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.TransportStopAutoNaming]")
                .AddTranslation(LocaleCode.EnUs,
                    "When enabled, transport stops will be automatically named when created. ")
                .AddTranslation(LocaleCode.ZhHans, "启用后，自动为新建的交通站点命名。")
                .AddTranslation(LocaleCode.ZhHant, "啟用後，自動為新建的交通站點命名。"),
            new Translation(
                    "Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.TransportStationAutoNaming]")
                .AddTranslation(LocaleCode.EnUs, "Enable Transport Station Naming")
                .AddTranslation(LocaleCode.ZhHans, "启用交通站自动命名")
                .AddTranslation(LocaleCode.ZhHant, "啟用交通站自動命名"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.TransportStationAutoNaming]")
                .AddTranslation(LocaleCode.EnUs,
                    "When enabled, transport stations will be automatically named when created. ")
                .AddTranslation(LocaleCode.ZhHans, "启用后，自动为新建的交通站命名。")
                .AddTranslation(LocaleCode.ZhHant, "啟用後，自動為新建的交通站命名。"),
            new Translation(
                    "Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.TransportDepotAutoNaming]")
                .AddTranslation(LocaleCode.EnUs, "Enable Transport Depot Auto Naming")
                .AddTranslation(LocaleCode.ZhHans, "启用交通车库自动命名")
                .AddTranslation(LocaleCode.ZhHant, "啟用交通車庫自動命名"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.TransportDepotAutoNaming]")
                .AddTranslation(LocaleCode.EnUs,
                    "When enabled, transport depots will be automatically named when created. ")
                .AddTranslation(LocaleCode.ZhHans, "启用后，自动为新建的交通车库命名。")
                .AddTranslation(LocaleCode.ZhHant, "啟用後，自動為新建的交通車庫命名。"),
            new Translation(
                    "Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.SchoolAutoNaming]")
                .AddTranslation(LocaleCode.EnUs, "Enable School Auto Naming")
                .AddTranslation(LocaleCode.ZhHans, "启用学校自动命名")
                .AddTranslation(LocaleCode.ZhHant, "啟用學校自動命名"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.SchoolAutoNaming]")
                .AddTranslation(LocaleCode.EnUs,
                    "When enabled, schools will be automatically named when created. ")
                .AddTranslation(LocaleCode.ZhHans, "启用后，自动为新建的学校命名。")
                .AddTranslation(LocaleCode.ZhHant, "啟用後，自動為新建的學校命名。"),
            new Translation(
                    "Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.FireStationAutoNaming]")
                .AddTranslation(LocaleCode.EnUs, "Enable Fire Station Auto Naming")
                .AddTranslation(LocaleCode.ZhHans, "启用消防站自动命名")
                .AddTranslation(LocaleCode.ZhHant, "啟用消防站自動命名"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.FireStationAutoNaming]")
                .AddTranslation(LocaleCode.EnUs,
                    "When enabled, fire stations will be automatically named when created. ")
                .AddTranslation(LocaleCode.ZhHans, "启用后，自动为新建的消防站命名。")
                .AddTranslation(LocaleCode.ZhHant, "啟用後，自動為新建的消防站命名。"),
            new Translation(
                    "Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.PoliceStationAutoNaming]")
                .AddTranslation(LocaleCode.EnUs, "Enable Police Station Auto Naming")
                .AddTranslation(LocaleCode.ZhHans, "启用警察局自动命名")
                .AddTranslation(LocaleCode.ZhHant, "啟用警察局自動命名"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.PoliceStationAutoNaming]")
                .AddTranslation(LocaleCode.EnUs,
                    "When enabled, police stations will be automatically named when created. ")
                .AddTranslation(LocaleCode.ZhHans, "启用后，自动为新建的警察局命名。")
                .AddTranslation(LocaleCode.ZhHant, "啟用後，自動為新建的警察局命名。"),
            new Translation(
                    "Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.HospitalAutoNaming]")
                .AddTranslation(LocaleCode.EnUs, "Enable Hospital Auto Naming")
                .AddTranslation(LocaleCode.ZhHans, "启用医院自动命名")
                .AddTranslation(LocaleCode.ZhHant, "啟用醫院自動命名"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.HospitalAutoNaming]")
                .AddTranslation(LocaleCode.EnUs,
                    "When enabled, hospitals will be automatically named when created. ")
                .AddTranslation(LocaleCode.ZhHans, "启用后，自动为新建的医院命名。")
                .AddTranslation(LocaleCode.ZhHant, "啟用後，自動為新建的醫院命名。"),
            new Translation(
                    "Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.ParkAutoNaming]")
                .AddTranslation(LocaleCode.EnUs, "Enable Park Auto Naming")
                .AddTranslation(LocaleCode.ZhHans, "启用公园自动命名")
                .AddTranslation(LocaleCode.ZhHant, "啟用公園自動命名"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.ParkAutoNaming]")
                .AddTranslation(LocaleCode.EnUs,
                    "When enabled, parks will be automatically named when created. ")
                .AddTranslation(LocaleCode.ZhHans, "启用后，自动为新建的公园命名。")
                .AddTranslation(LocaleCode.ZhHant, "啟用後，自動為新建的公園命名。"),
            new Translation(
                    "Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.ElectricityAutoNaming]")
                .AddTranslation(LocaleCode.EnUs, "Enable Electricity Auto Naming")
                .AddTranslation(LocaleCode.ZhHans, "启用电力自动命名")
                .AddTranslation(LocaleCode.ZhHant, "啟用電力自動命名"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.ElectricityAutoNaming]")
                .AddTranslation(LocaleCode.EnUs,
                    "When enabled, electricity facilities will be automatically named when created.")
                .AddTranslation(LocaleCode.ZhHans, "启用后，自动为新建的电力设施命名。")
                .AddTranslation(LocaleCode.ZhHant, "啟用後，自動為新建的電力設施命名。"),
            new Translation(
                    "Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.WaterAutoNaming]")
                .AddTranslation(LocaleCode.EnUs, "Enable Water Auto Naming")
                .AddTranslation(LocaleCode.ZhHans, "启用净水自动命名")
                .AddTranslation(LocaleCode.ZhHant, "啟用淨水自動命名"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.WaterAutoNaming]")
                .AddTranslation(LocaleCode.EnUs,
                    "When enabled, water facilities will be automatically named when created.")
                .AddTranslation(LocaleCode.ZhHans, "启用后，自动为新建的净水设施命名。")
                .AddTranslation(LocaleCode.ZhHant, "啟用後，自動為新建的淨水設施命名。"),
            new Translation(
                    "Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.SewageAutoNaming]")
                .AddTranslation(LocaleCode.EnUs, "Enable Sewage Auto Naming")
                .AddTranslation(LocaleCode.ZhHans, "启用污水自动命名")
                .AddTranslation(LocaleCode.ZhHant, "啟用污水自動命名"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.SewageAutoNaming]")
                .AddTranslation(LocaleCode.EnUs,
                    "When enabled, sewage facilities will be automatically named when created.")
                .AddTranslation(LocaleCode.ZhHans, "启用后，自动为新建的污水设施命名。")
                .AddTranslation(LocaleCode.ZhHant, "啟用後，自動為新建的污水設施命名。"),
            new Translation(
                    "Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.GarbageAutoNaming]")
                .AddTranslation(LocaleCode.EnUs, "Enable Garbage Auto Naming")
                .AddTranslation(LocaleCode.ZhHans, "启用垃圾自动命名")
                .AddTranslation(LocaleCode.ZhHant, "啟用垃圾自動命名"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.GarbageAutoNaming]")
                .AddTranslation(LocaleCode.EnUs,
                    "When enabled, garbage facilities will be automatically named when created.")
                .AddTranslation(LocaleCode.ZhHans, "启用后，自动为新建的垃圾设施命名。")
                .AddTranslation(LocaleCode.ZhHant, "啟用後，自動為新建的垃圾設施命名。"),
            new Translation(
                    "Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.DisasterAutoNaming]")
                .AddTranslation(LocaleCode.EnUs, "Enable Disaster Auto Naming")
                .AddTranslation(LocaleCode.ZhHans, "启用灾害自动命名")
                .AddTranslation(LocaleCode.ZhHant, "啟用災害自動命名"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.DisasterAutoNaming]")
                .AddTranslation(LocaleCode.EnUs,
                    "When enabled, disaster facilities will be automatically named when created.")
                .AddTranslation(LocaleCode.ZhHans, "启用后，自动为新建的灾害设施命名。")
                .AddTranslation(LocaleCode.ZhHant, "啟用後，自動為新建的災害設施命名。"),
            new Translation(
                    "Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.DeathcareAutoNaming]")
                .AddTranslation(LocaleCode.EnUs, "Enable Deathcare Auto Naming")
                .AddTranslation(LocaleCode.ZhHans, "启用殡仪自动命名")
                .AddTranslation(LocaleCode.ZhHant, "啟用殯儀自動命名"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.DeathcareAutoNaming]")
                .AddTranslation(LocaleCode.EnUs,
                    "When enabled, deathcare facilities will be automatically named when created.")
                .AddTranslation(LocaleCode.ZhHans, "启用后，自动为新建的殡仪设施命名。")
                .AddTranslation(LocaleCode.ZhHant, "啟用後，自動為新建的殯儀設施命名。"),
            new Translation(
                    "Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.TelecomAutoNaming]")
                .AddTranslation(LocaleCode.EnUs, "Enable Telecom Auto Naming")
                .AddTranslation(LocaleCode.ZhHans, "启用电信自动命名")
                .AddTranslation(LocaleCode.ZhHant, "啟用電信自動命名"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.TelecomAutoNaming]")
                .AddTranslation(LocaleCode.EnUs,
                    "When enabled, telecom facilities will be automatically named when created.")
                .AddTranslation(LocaleCode.ZhHans, "启用后，自动为新建的电信设施命名。")
                .AddTranslation(LocaleCode.ZhHant, "啟用後，自動為新建的電信設施命名。"),

            new Translation(
                    "Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.PostAutoNaming]")
                .AddTranslation(LocaleCode.EnUs, "Enable Post Auto Naming")
                .AddTranslation(LocaleCode.ZhHans, "启用邮政自动命名")
                .AddTranslation(LocaleCode.ZhHant, "啟用郵政自動命名"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.PostAutoNaming]")
                .AddTranslation(LocaleCode.EnUs,
                    "When enabled, post facilities will be automatically named when created.")
                .AddTranslation(LocaleCode.ZhHans, "启用后，自动为新建的邮政设施命名。")
                .AddTranslation(LocaleCode.ZhHant, "啟用後，自動為新建的郵政設施命名。"),
            new Translation(
                    "Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.ParkingAutoNaming]")
                .AddTranslation(LocaleCode.EnUs, "Enable Parking Lot Auto Naming")
                .AddTranslation(LocaleCode.ZhHans, "启用停车场自动命名")
                .AddTranslation(LocaleCode.ZhHant, "啟用停車場自動命名"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.ParkingAutoNaming]")
                .AddTranslation(LocaleCode.EnUs,
                    "When enabled, parking lots will be automatically named when created.")
                .AddTranslation(LocaleCode.ZhHans, "启用后，自动为新建的停車場命名。")
                .AddTranslation(LocaleCode.ZhHant, "啟用後，自動為新建的停車場命名。"),
            new Translation(
                    "Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.CityServiceAutoNaming]")
                .AddTranslation(LocaleCode.EnUs, "Enable Other City Service Auto Naming")
                .AddTranslation(LocaleCode.ZhHans, "启用其他城市服务自动命名")
                .AddTranslation(LocaleCode.ZhHant, "啟用其他城市服務自動命名"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.CityServiceAutoNaming]")
                .AddTranslation(LocaleCode.EnUs,
                    "When enabled, other city service buildings will be automatically named when created.")
                .AddTranslation(LocaleCode.ZhHans, "启用后，自动为新建的其他城市服务建筑命名。")
                .AddTranslation(LocaleCode.ZhHant, "啟用後，自動為新建的其他城市服務建築命名。"),
            new Translation(
                    "Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.RoadFacilityAutoNaming]")
                .AddTranslation(LocaleCode.EnUs, "Enable Road Facility Auto Naming")
                .AddTranslation(LocaleCode.ZhHans, "启用道路设施自动命名")
                .AddTranslation(LocaleCode.ZhHant, "啟用道路設施自動命名"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.RoadFacilityAutoNaming]")
                .AddTranslation(LocaleCode.EnUs,
                    "When enabled, road facilities will be automatically named when created.")
                .AddTranslation(LocaleCode.ZhHans, "启用后，自动为新建的道路设施命名。")
                .AddTranslation(LocaleCode.ZhHant, "啟用後，自動為新建的道路設施命名。"),
            new Translation(
                    "Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.AdminAutoNaming]")
                .AddTranslation(LocaleCode.EnUs, "Enable Administration Auto Naming")
                .AddTranslation(LocaleCode.ZhHans, "启用行政自动命名")
                .AddTranslation(LocaleCode.ZhHant, "啟用行政自動命名"),
            new Translation(
                    "Options.OPTION_DESCRIPTION[StationNaming.StationNaming.Mod.StationNamingSettings.AdminAutoNaming]")
                .AddTranslation(LocaleCode.EnUs,
                    "When enabled, administration buildings will be automatically named when created.")
                .AddTranslation(LocaleCode.ZhHans, "启用后，自动为新建的行政建筑命名。")
                .AddTranslation(LocaleCode.ZhHant, "啟用後，自動為新建的行政建築命名。"),

            new Translation(
                    "Options.OPTION[StationNaming.StationNaming.Mod.StationNamingSettings.DisableCityServiceAutoNaming]")
                .AddTranslation(LocaleCode.EnUs, "Disable All City Service Auto Naming")
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
        ];

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