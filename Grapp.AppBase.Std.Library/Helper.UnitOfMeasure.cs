namespace Grapp.AppBase.Std.Library
{
	public partial class XHelper
	{
		public static class UnitOfMeasure
		{
			public const double DegreeOnMeters = 111111.11;
			public const double DegreeOnKilometers = 111.11111;

			public const long Kilobyte = 1024;
			public const long Megabyte = 1024 * Kilobyte;
			public const long Gigabyte = 1024 * Megabyte;
			public const long Terabyte = 1024 * Gigabyte;
			public const long Petabyte = 1024 * Gigabyte;

			public static double ConvertDegreeTo(double rate, EUnitType futureUnit)
			{
				switch(futureUnit)
				{
					case EUnitType.Kilometer:
						return rate * DegreeOnKilometers;

					case EUnitType.Meter:
						return rate * DegreeOnMeters;

					default: return -1;
				}
			}

			public enum EUnitType
			{
				Meter,
				Kilometer,
				Degree
			}
		}
	}
}