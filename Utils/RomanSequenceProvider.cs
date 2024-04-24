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

/// <summary>
/// Provides Roman numerals for an index.
///
/// Example: 1 -> I, 2 -> II, 3 -> III
///
/// </summary>
public class RomanSequenceProvider : ISequenceProvider
{
    public string GetSequence(int index, int count, ISequenceProvider.SequenceOptions options)
    {
        return GetSequence(index, options);
    }

    public string GetSequence(int index, ISequenceProvider.SequenceOptions options)
    {
        return index switch
        {
            < 0 => "Invalid",
            0 => "N",
            < 10 => GetSingleDigit(index),
            < 100 => GetTens(index) + GetSingleDigit(index % 10),
            _ => GetHundreds(index) + GetTens(index % 100) +
                 GetSingleDigit(index % 10)
        };
    }

    private static string GetSingleDigit(int index)
    {
        return index switch
        {
            1 => "I",
            2 => "II",
            3 => "III",
            4 => "IV",
            5 => "V",
            6 => "VI",
            7 => "VII",
            8 => "VIII",
            9 => "IX",
            _ => "Invalid"
        };
    }

    private static string GetTens(int index)
    {
        return index switch
        {
            10 => "X",
            20 => "XX",
            30 => "XXX",
            40 => "XL",
            50 => "L",
            60 => "LX",
            70 => "LXX",
            80 => "LXXX",
            90 => "XC",
            _ => "Invalid"
        };
    }

    private static string GetHundreds(int index)
    {
        return index switch
        {
            100 => "C",
            200 => "CC",
            300 => "CCC",
            400 => "CD",
            500 => "D",
            600 => "DC",
            700 => "DCC",
            800 => "DCCC",
            900 => "CM",
            _ => "Invalid"
        };
    }
}