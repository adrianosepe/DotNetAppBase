﻿using System;
using System.IO;
using System.Linq;

// ReSharper disable CheckNamespace
public static class XStreamExtensions
// ReSharper restore CheckNamespace
{
	public static bool IsEquals(this MemoryStream ms1, MemoryStream ms2)
	{
		if(ms1.Length != ms2.Length)
		{
			return false;
		}

		ms1.Position = 0;
		ms2.Position = 0;

		var msArray1 = ms1.ToArray();
		var msArray2 = ms2.ToArray();

		return msArray1.SequenceEqual(msArray2);
	}
}