using System;
using System.Linq;
using System.Runtime.InteropServices;
using Grapp.AppBase.Std.Exceptions.Contract;

namespace Grapp.AppBase.Std.Library.ComponentModel.Model.Primitives
{
    [StructLayout(LayoutKind.Sequential)]
    public struct XTimeSpan : IEquatable<XTimeSpan>, IComparable<TimeSpan>, IComparable<XTimeSpan>
    {
        public const int DaysPerYear = 365;

        public int Months { get; set; }

        public int Years { get; set; }

        public TimeSpan TimeSpan { get; set; }

        public long Ticks => ((TimeSpan)this).Ticks;

        public int Days => ((TimeSpan)this).Days;

        public int Hours => ((TimeSpan)this).Hours;

        public int Milliseconds => ((TimeSpan)this).Milliseconds;

        public int Minutes => ((TimeSpan)this).Minutes;

        public int Seconds => ((TimeSpan)this).Seconds;

        public double TotalDays => ((TimeSpan)this).TotalDays;

        public double TotalHours => ((TimeSpan)this).TotalHours;

        public double TotalMilliseconds => ((TimeSpan)this).TotalMilliseconds;

        public double TotalMinutes => ((TimeSpan)this).TotalMinutes;

        public double TotalSeconds => ((TimeSpan)this).TotalSeconds;

        public XTimeSpan Add(XTimeSpan number)
        {
            return AddInternal(this, number);
        }

        public XTimeSpan Subtract(XTimeSpan timeSpan)
        {
            return SubtractInternal(this, timeSpan);
        }

        public static XTimeSpan operator +(XTimeSpan left, XTimeSpan right)
        {
            return AddInternal(left, right);
        }

        public static XTimeSpan operator +(XTimeSpan left, TimeSpan right)
        {
            return AddInternal(left, right);
        }

        public static XTimeSpan operator +(TimeSpan left, XTimeSpan right)
        {
            return AddInternal(left, right);
        }

        public static XTimeSpan operator -(XTimeSpan value)
        {
            return value.Negate();
        }

        public static XTimeSpan operator -(XTimeSpan left, XTimeSpan right)
        {
            return SubtractInternal(left, right);
        }

        public static XTimeSpan operator -(TimeSpan left, XTimeSpan right)
        {
            return SubtractInternal(left, right);
        }

        public static XTimeSpan operator -(XTimeSpan left, TimeSpan right)
        {
            return SubtractInternal(left, right);
        }

        public static bool operator ==(XTimeSpan left, XTimeSpan right)
        {
            return left.Years == right.Years && left.Months == right.Months && left.TimeSpan == right.TimeSpan;
        }

        public static bool operator ==(TimeSpan left, XTimeSpan right)
        {
            return (XTimeSpan)left == right;
        }

        public static bool operator ==(XTimeSpan left, TimeSpan right)
        {
            return left == (XTimeSpan)right;
        }

        public static bool operator !=(XTimeSpan left, XTimeSpan right)
        {
            return !(left == right);
        }

        public static bool operator !=(TimeSpan left, XTimeSpan right)
        {
            return !(left == right);
        }

        public static bool operator !=(XTimeSpan left, TimeSpan right)
        {
            return !(left == right);
        }

        public static bool operator <(XTimeSpan left, XTimeSpan right)
        {
            return (TimeSpan)left < (TimeSpan)right;
        }

        public static bool operator <(XTimeSpan left, TimeSpan right)
        {
            return (TimeSpan)left < right;
        }

        public static bool operator <(TimeSpan left, XTimeSpan right)
        {
            return left < (TimeSpan)right;
        }

        public static bool operator <=(XTimeSpan left, XTimeSpan right)
        {
            return (TimeSpan)left <= (TimeSpan)right;
        }

        public static bool operator <=(XTimeSpan left, TimeSpan right)
        {
            return (TimeSpan)left <= right;
        }

        public static bool operator <=(TimeSpan left, XTimeSpan right)
        {
            return left <= (TimeSpan)right;
        }

        public static bool operator >(XTimeSpan left, XTimeSpan right)
        {
            return (TimeSpan)left > (TimeSpan)right;
        }

        public static bool operator >(XTimeSpan left, TimeSpan right)
        {
            return (TimeSpan)left > right;
        }

        public static bool operator >(TimeSpan left, XTimeSpan right)
        {
            return left > (TimeSpan)right;
        }

        public static bool operator >=(XTimeSpan left, XTimeSpan right)
        {
            return (TimeSpan)left >= (TimeSpan)right;
        }

        public static bool operator >=(XTimeSpan left, TimeSpan right)
        {
            return (TimeSpan)left >= right;
        }

        public static bool operator >=(TimeSpan left, XTimeSpan right)
        {
            return left >= (TimeSpan)right;
        }

        public static implicit operator TimeSpan(XTimeSpan timeSpan)
        {
            var daysFromYears = DaysPerYear * timeSpan.Years;
            var daysFromMonths = 30 * timeSpan.Months;
            var days = daysFromMonths + daysFromYears;
            return new TimeSpan(days, 0, 0, 0) + timeSpan.TimeSpan;
        }

        public static implicit operator XTimeSpan(TimeSpan timeSpan)
        {
            return new XTimeSpan {TimeSpan = timeSpan};
        }

        public object Clone()
        {
            return new XTimeSpan
                {
                    TimeSpan = TimeSpan,
                    Months = Months,
                    Years = Years
                };
        }

        bool IEquatable<XTimeSpan>.Equals(XTimeSpan other)
        {
            return Equals(other);
        }

        public override string ToString()
        {
            return ((TimeSpan)this).ToString();
        }

        public override bool Equals(object obj)
        {
            if(obj == null)
            {
                return false;
            }
            var type = obj.GetType();
            if(type == typeof(XTimeSpan))
            {
                return this == (XTimeSpan)obj;
            }
            if(type == typeof(TimeSpan))
            {
                return this == (TimeSpan)obj;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Months.GetHashCode() ^ Years.GetHashCode() ^ TimeSpan.GetHashCode();
        }

        private static XTimeSpan AddInternal(XTimeSpan left, XTimeSpan right)
        {
            return new XTimeSpan
                {
                    Years = left.Years + right.Years,
                    Months = left.Months + right.Months,
                    TimeSpan = left.TimeSpan + right.TimeSpan
                };
        }

        private static XTimeSpan SubtractInternal(XTimeSpan left, XTimeSpan right)
        {
            return new XTimeSpan
                {
                    Years = left.Years - right.Years,
                    Months = left.Months - right.Months,
                    TimeSpan = left.TimeSpan - right.TimeSpan
                };
        }

        public int CompareTo(TimeSpan other)
        {
            return ((TimeSpan)this).CompareTo(other);
        }

        public int CompareTo(object value)
        {
            if(value is TimeSpan span)
            {
                return ((TimeSpan)this).CompareTo(span);
            }

            throw XArgumentException.Create(nameof(value), "Value must be a TimeSpan");
        }

        public int CompareTo(XTimeSpan value)
        {
            return ((TimeSpan)this).CompareTo(value);
        }

        public TimeSpan Negate()
        {
            return new XTimeSpan
                {
                    TimeSpan = -TimeSpan,
                    Months = -Months,
                    Years = -Years
                };
        }
    }
}