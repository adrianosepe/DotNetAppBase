using System;
using System.ComponentModel;
using Grapp.AppBase.Std.Library.Properties;

namespace Grapp.AppBase.Std.Library
{
    public partial class XHelper
    {
        public static partial class DisplayFormat
        {
            public static class Geo
            {
                [Localizable(false)]
                public static string FormatDistance(double distance, UnitOfMeasure.EUnitType currentUnit, bool showCentimeter)
                {
                    if(distance <= 0)
                    {
                        return DbMessages.Geo_FormatDistance_ZeroMeter;
                    }

                    double distanceMetric;

                    if(currentUnit == UnitOfMeasure.EUnitType.Kilometer)
                    {
                        distanceMetric = distance * 1000;
                    }
                    else if(currentUnit == UnitOfMeasure.EUnitType.Degree)
                    {
                        distanceMetric = UnitOfMeasure.ConvertDegreeTo(distance, UnitOfMeasure.EUnitType.Meter);
                    }
                    else
                    {
                        distanceMetric = distance;
                    }

                    var km = (int)Math.Truncate(distanceMetric / 1000);
                    var mt = distanceMetric - 1000 * km;

                    bool hasMeters;
                    if(showCentimeter)
                    {
                        hasMeters = mt > 0;
                    }
                    else
                    {
                        hasMeters = Convert.ToInt32(mt) > 0;
                    }

                    if(hasMeters)
                    {
                        var formatMetter = showCentimeter ? "F2" : "F0";

                        if(km > 0)
                        {
                            return String.Format("{0} Km {1:" + formatMetter + "} metros", km, mt);
                        }

                        return String.Format("{0:" + formatMetter + "} metros", mt);
                    }

                    return $"{km} Km";
                }

                [Localizable(false)]
                public static string FullAddress(string logradouro, string numero, string bairro, string complemento, string cep, string municipio)
                {
                    // Rua Tinguís, 647 - Vila Goes, Apartamento 1 - 86026200 , Londrina - PR
                    return $"{logradouro}, {numero} - {bairro}, {complemento} - {cep}, {municipio}";
                }

                [Localizable(false)]
                public static string SmallAddress(string logradouro, string numero, string bairro, string municipio)
                {
                    // Rua Tinguís, 647 - Vila Goes, Londrina - PR
                    return $"{logradouro}, {numero} - {bairro}, {municipio}";
                }
            }
        }
    }
}