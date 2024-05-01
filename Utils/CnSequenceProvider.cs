// MIT License
// 
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

namespace StationNaming.Utils;

public class CnSequenceProvider: ISequenceProvider
{
    public string GetSequence(int index, int count, ISequenceProvider.SequenceOptions options)
    {
        return GetSequence(index, options);
    }

    public string GetSequence(int index, ISequenceProvider.SequenceOptions options)
    {
        if (index < 0)
        {
            return "Invalid";
        }

        if (index == 0)
        {
            return "零";
        }

        if (index < 10)
        {
            return GetSingleDigit(index);
        }

        if (index < 100)
        {
            return GetTens(index);
        }

        return GetSingleDigit(index % 10) + "百" + GetTens(index % 100);
    }

    private static string GetSingleDigit(int index)
    {
        return index switch
        {
            1 => "一",
            2 => "二",
            3 => "三",
            4 => "四",
            5 => "五",
            6 => "六",
            7 => "七",
            8 => "八",
            9 => "九",
            _ => "Invalid"
        };
    }

    private static string GetTens(int index)
    {
        return index switch
        {
            10 => "十",
            20 => "二十",
            30 => "三十",
            40 => "四十",
            50 => "五十",
            60 => "六十",
            70 => "七十",
            80 => "八十",
            90 => "九十",
            _ => GetSingleDigit(index / 10) + "十" + GetSingleDigit(index % 10)
        };
    }
}