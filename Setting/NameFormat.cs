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

namespace StationNaming.Setting;

public struct NameFormat
{
    public const string Prefab = "[Prefab]";

    public string Separator { get; set; } = " ";
    public string Prefix { get; set; } = "";
    public string Suffix { get; set; } = "";

    public NameFormat()
    {
    }

    public NameFormat(string separator, string prefix, string suffix)
    {
        Separator = separator;
        Prefix = prefix;
        Suffix = suffix;
    }

    public bool IsAnyPrefab()
    {
        return Prefix == Prefab || Suffix == Prefab;
    }

    private string GetPrefix(string prefab)
    {
        return Prefix == Prefab ? prefab : Prefix;
    }

    private string GetSuffix(string prefab)
    {
        return Suffix == Prefab ? prefab : Suffix;
    }

    public string Format(
        string name,
        string prefabName = "",
        bool hasNext = true)
    {
        return GetPrefix(prefabName) + name +
               GetSuffix(prefabName) +
               (hasNext ? Separator : "");
    }
}