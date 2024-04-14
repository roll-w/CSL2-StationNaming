# Station Naming

This is a mod for `Cities: Skylines 2` that allows you naming your
transport stations and stops automatically.

This mod should be compatible with other auto naming mods, but you should
check the mod settings to avoid conflicts.

## Features

- Automatically name your transport stations and stops.
- Customizable naming rules.
- Could generate names from the surrounding buildings or roads.

## Settings

Except the showing settings, you could also customize the naming rules in the
mod setting file where is located at 
`AppData/LocalLow/Colossal Order/Cities Skylines II/RollW_StationNaming.coc`.

You could change the naming format for the intersection and set your own prefix
and suffix for the station names.

```json lines
RollW_StationNaming
{
    "IntersectionNamingFormat": "{0} & {1}", // The naming format for intersection
    "Prefix": " ", // Set your own prefix here
    "Suffix": " ", // Set your own suffix here
}
```

## Translations

- English
- Simplified Chinese (简体中文)
- Traditional Chinese (繁體中文)

## Planned Features

- Supports district name as prefix/suffix.
- Allow setting the naming format for different name sources.
- Supports naming Schools, Hospitals...

## Support

If you have any problems during using this mod, please open an issue or discussion
on Github.

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
