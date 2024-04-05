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
public class StationNamingSettings(IMod mod) : ModSetting(mod)
{
    public bool Enable { get; set; } = true;

    [SettingsUIDropdown(typeof(RoadNamingProvider), nameof(RoadNamingProvider.GetOptions))]
    public string RoadNamingFormat { get; set; } = "{0}{1}";

    public override void SetDefaults()
    {
        RoadNamingFormat = "{0}{1}";
    }

    public string FormatRoadName(string first, string second, bool reverse = false)
    {
        return reverse ? string.Format(RoadNamingFormat, second, first) :
            string.Format(RoadNamingFormat, first, second);
    }


    public static class RoadNamingProvider
    {
        public static DropdownItem<string>[] GetOptions()
        {
            return
            [
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
    }
}