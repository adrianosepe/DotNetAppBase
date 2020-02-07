﻿using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Grapp.AppBase.Std.Library.Formater;
using JetBrains.Annotations;

namespace Grapp.AppBase.Std.Library
{
    public partial class XHelper
    {
        public static partial class Strings
        {
            public const string Empty = "";
            public const char SpaceChar = ' ';

            public static string Alignment(ETextAlignment alignment, string data, int length, char alignWith = SpaceChar)
            {
                switch(alignment)
                {
                    case ETextAlignment.Left:
                        return Left(data, length, alignWith);

                    case ETextAlignment.Center:
                        return Center(data, length, alignWith);

                    case ETextAlignment.Right:
                        return Right(data, length, alignWith);

                    default:
                        throw new ArgumentOutOfRangeException(nameof(alignment));
                }
            }

            public static bool AllHaveData(params string[] data) => data.All(HasData);

            public static bool HasData(string data) => !String.IsNullOrWhiteSpace(data) && data != Empty;

            [ContractAnnotation("data:null => true")]
            public static bool IsEmptyOrWhiteSpace(string data) => !HasData(data);

            public static bool IsLetterWithDiacritics(char eUnicodeChar)
            {
                var s = eUnicodeChar.ToString().Normalize(NormalizationForm.FormD);

                return s.Length > 1
                       && Char.IsLetter(s[0])
                       && s.Skip(1)
                           .All(c2 => CharUnicodeInfo.GetUnicodeCategory(c2) == UnicodeCategory.NonSpacingMark);
            }

            public static string RemoveDiacritics(string data)
            {
                if(String.IsNullOrWhiteSpace(data))
                {
                    return data;
                }

                data = data.Normalize(NormalizationForm.FormD);
                var chars = data.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).ToArray();

                return new string(chars).Normalize(NormalizationForm.FormC);
            }

            public static string RemoveSpecialCharacters(string input)
            {
                const string pattern = @"[^\w\.@-]";

                return Regex.Replace(input, pattern, Empty);
            }

            public static string Repeat(string value, int count)
            {
                return new StringBuilder().Insert(0, value, count).ToString();
            }

            public static string Truncate(string data, int length)
            {
                if(String.IsNullOrEmpty(data))
                {
                    return data;
                }

                return data.Length <= length ? data : data.Substring(0, length);
            }

            /// <summary>
            ///     Adiciona espaços antes dos caracteres capitais de uma string
            /// </summary>
            /// <param name="input"></param>
            /// <returns></returns>
            public static string UnconcatString(string input) => Regex.Replace(input, "[A-Z]", " $0").Trim();

            private static string Center(string data, int length, char alignWith = SpaceChar)
            {
                data = Truncate(data, length);

                var spaces = length - data.Length;
                var padLeft = spaces / 2 + data.Length;

                return data.PadLeft(padLeft, alignWith).PadRight(length, alignWith);
            }

            private static string Left(string data, int length, char alignWith = SpaceChar) => Truncate(data, length).PadRight(length, alignWith);

            private static string Right(string data, int length, char alignWith = SpaceChar) => Truncate(data, length).PadLeft(length, alignWith);
        }
    }
}