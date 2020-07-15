﻿#region License

// Copyright(c) 2020 GrappTec
// 
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.

#endregion

using System;
using System.Collections.Generic;
using System.Reflection;
using DotNetAppBase.Std.Library;

// ReSharper disable CheckNamespace
public static class XReflectionExtensions
// ReSharper restore CheckNamespace
{
    public const BindingFlags InstanceBindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
    public const BindingFlags StaticBindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;

    public static IEnumerable<MethodInfo> Get(this Func<BindingFlags, MethodInfo[]> funcGet) => funcGet(BindingFlags.Default);

    public static string GetDescription(this PropertyInfo propertyInfo) => XHelper.Models.GetDescription(propertyInfo);

    public static string GetDisplayName(this PropertyInfo propertyInfo) => XHelper.Models.GetDisplayName(propertyInfo);

    public static Func<BindingFlags, MethodInfo[]> Instance(this Func<BindingFlags, MethodInfo[]> funcGet) => flags => funcGet(flags | BindingFlags.Instance);

    public static Func<BindingFlags, MethodInfo[]> Methods(this Type type) => type.GetMethods;

    public static Func<BindingFlags, MethodInfo[]> Public(this Func<BindingFlags, MethodInfo[]> funcGet) => flags => funcGet(flags | BindingFlags.Public);
}