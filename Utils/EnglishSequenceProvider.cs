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
/// Provides English numerals for an index.
///
/// Example: 1 -> first, 2 -> second, 3 -> third
///
/// </summary>
public class EnglishSequenceProvider : ISequenceProvider
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
            0 => GetZeroth(options.Capitalize),
            < 10 => GetSingleDigit(index, options.Capitalize),
            < 20 => GetTeen(index, options.Capitalize),
            < 100 => GetTens(index, options.Capitalize),
            _ => GetNumber(index % 10, options.Capitalize) + " hundred " + GetTens(index % 100, options.Capitalize)
        };
    }

    private static string GetZeroth(CapitalizeType capitalizeType)
    {
        return capitalizeType switch
        {
            CapitalizeType.First => "Zeroth",
            CapitalizeType.All => "ZEROTH",
            _ => "zeroth"
        };
    }

    private static string GetNumber(int digit, CapitalizeType capitalizeType)
    {
        var number = digit switch
        {
            0 => "zero",
            1 => "one",
            2 => "two",
            3 => "three",
            4 => "four",
            5 => "five",
            6 => "six",
            7 => "seven",
            8 => "eight",
            9 => "nine",
            _ => "invalid"
        };

        return capitalizeType switch
        {
            CapitalizeType.First => char.ToUpper(number[0]) + number.Substring(1),
            CapitalizeType.All => number.ToUpper(),
            _ => number
        };
    }

    private static string GetSingleDigit(int index, CapitalizeType capitalizeType)
    {
        var digit = index switch
        {
            1 => "first",
            2 => "second",
            3 => "third",
            4 => "fourth",
            5 => "fifth",
            6 => "sixth",
            7 => "seventh",
            8 => "eighth",
            9 => "ninth",
            _ => "invalid"
        };
        return capitalizeType switch
        {
            CapitalizeType.First => char.ToUpper(digit[0]) + digit.Substring(1),
            CapitalizeType.All => digit.ToUpper(),
            _ => digit
        };
    }

    private static string GetTeen(int index, CapitalizeType capitalizeType)
    {
        var teen = index switch
        {
            10 => "tenth",
            11 => "eleventh",
            12 => "twelfth",
            13 => "thirteenth",
            14 => "fourteenth",
            15 => "fifteenth",
            16 => "sixteenth",
            17 => "seventeenth",
            18 => "eighteenth",
            19 => "nineteenth",
            _ => "invalid"
        };
        return capitalizeType switch
        {
            CapitalizeType.First => char.ToUpper(teen[0]) + teen.Substring(1),
            CapitalizeType.All => teen.ToUpper(),
            _ => teen
        };
    }

    private static string GetTens(int index, CapitalizeType capitalizeType)
    {
        var tens = (index / 10) switch
        {
            2 => "twenty",
            3 => "thirty",
            4 => "forty",
            5 => "fifty",
            6 => "sixty",
            7 => "seventy",
            8 => "eighty",
            9 => "ninety",
            _ => "invalid"
        };
        var digit = GetSingleDigit(index % 10, CapitalizeType.None);
        return capitalizeType switch
        {
            CapitalizeType.First => char.ToUpper(tens[0]) + tens.Substring(1) + "-" + digit,
            CapitalizeType.All => tens.ToUpper() + "-" + digit.ToUpper(),
            _ => tens + "-" + digit
        };
    }
}