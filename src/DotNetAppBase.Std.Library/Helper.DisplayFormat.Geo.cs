#region License

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
using System.ComponentModel;
using DotNetAppBase.Std.Library.Properties;

namespace DotNetAppBase.Std.Library
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
                    if (distance <= 0)
                    {
                        return DbMessages.Geo_FormatDistance_ZeroMeter;
                    }

                    double distanceMetric;

                    if (currentUnit == UnitOfMeasure.EUnitType.Kilometer)
                    {
                        distanceMetric = distance * 1000;
                    }
                    else if (currentUnit == UnitOfMeasure.EUnitType.Degree)
                    {
                        distanceMetric = UnitOfMeasure.ConvertDegreeTo(distance, UnitOfMeasure.EUnitType.Meter);
                    }
                    else
                    {
                        distanceMetric = distance;
                    }

                    var km = (int) Math.Truncate(distanceMetric / 1000);
                    var mt = distanceMetric - 1000 * km;

                    bool hasMeters;
                    if (showCentimeter)
                    {
                        hasMeters = mt > 0;
                    }
                    else
                    {
                        hasMeters = Convert.ToInt32(mt) > 0;
                    }

                    if (hasMeters)
                    {
                        var formatMetter = showCentimeter ? "F2" : "F0";

                        if (km > 0)
                        {
                            return string.Format("{0} Km {1:" + formatMetter + "} metros", km, mt);
                        }

                        return string.Format("{0:" + formatMetter + "} metros", mt);
                    }

                    return $"{km} Km";
                }

                [Localizable(false)]
                // Rua Tinguís, 647 - Vila Goes, Apartamento 1 - 86026200 , Londrina - PR
                public static string FullAddress(string logradouro, string numero, string bairro, string complemento, string cep, string municipio) => $"{logradouro}, {numero} - {bairro}, {complemento} - {cep}, {municipio}";

                [Localizable(false)]
                // Rua Tinguís, 647 - Vila Goes, Londrina - PR
                public static string SmallAddress(string logradouro, string numero, string bairro, string municipio) => $"{logradouro}, {numero} - {bairro}, {municipio}";
            }
        }
    }
}