# Station Naming

This is a mod for `Cities: Skylines 2` that allows you naming your
transport stations and stops automatically.

This mod should be compatible with other auto naming mods, but you should
check the mod settings to avoid conflicts.

## Features

- Automatically name your transport stations and stops.
- Customizable naming rules.
- Generate names from surrounding buildings or roads.

## Settings

Except the showing settings, you could also customize the naming rules in the
mod setting file where is located at 
`AppData/LocalLow/Colossal Order/Cities Skylines II/RollW_StationNaming.coc`.

You could change the naming format for the different types and set your own prefix
and suffix for the station names.

```json lines
RollW_StationNaming
{
  "Prefix": " ", // Set the global prefix here
  "Suffix": " ", // Set the global suffix here
  // You can use the {PREFAB} in the prefix or suffix to insert the prefab name
  "NamingSeparator": " ", // Set the default separator between names 
  "NamedAddressNameFormat": "{NAME}, {NUMBER} {ROAD}", // Set the format for the named address name
  "AddressNameFormat": "{NUMBER} {ROAD}",
  "RoadFormat": {
    "Separator": " ", // Set the separator after the road name
    "Prefix": "", // Set the prefix for the road name only
    "Suffix": "" // Set the suffix for the road name only
  },
  "DistrictFormat": {
    "Separator": " ", // Set the separator after the district name
    "Prefix": "", // Set the prefix for the district name only
    "Suffix": "" // Set the suffix for the district name only
  },
}
```

## Translations

- English
- Simplified Chinese (简体中文)
- Traditional Chinese (繁體中文)

## Planned Features

- Allow setting the naming format for different name sources.
- Allow naming Schools, Hospitals...

## Support

If you have any problems during using this mod, please open an issue or discussion
on GitHub.

## License

```text
MIT License

Copyright (c) 2024 RollW

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
```
