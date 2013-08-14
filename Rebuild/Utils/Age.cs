using System;
using Rebuild.Extensions;
using System.Text;

namespace Rebuild.Utils
{
    public struct Age : IEquatable<Age>
    {
        public Age(int years)
            : this()
        {
            Years = years;
        }

        public Age(int years, int months)
            : this()
        {
            Years = years;
            Months = months;
        }

        public Age(int years, int months, int days)
            : this()
        {
            Years = years;
            Months = months;
            Days = days;
        }

        public Age(int years, int months, int days, TimeSpan time)
            : this()
        {
            Years = years;
            Months = months;
            Days = days;
            Time = time;
        }

        public bool Equals(Age other)
        {
            return other.Days == Days
                && other.Months == Months
                && other.Years == Years
                && other.Time == Time;
        }

        public override bool Equals(object obj)
        {
            return obj is Age && Equals((Age)obj);
        }

        public override int GetHashCode()
        {
            return Days
                .CombineHash(Months)
                .CombineHash(Years)
                .CombineHash(Time);
        }

        public override string ToString()
        {
            return string.Format("{0} Year(s), {1} Month(s), {2} Day(s), {3}", Years, Months, Days, Time);
        }

        public Age TruncateTime()
        {
            return new Age(Years, Months, Days);
        }

        public Age YearsMonthsOnly()
        {
            return new Age(Years, Months);
        }

        public Age YearsOnly()
        {
            return new Age(Years);
        }

        public int Days { get; private set; }

        public int Months { get; private set; }

        public TimeSpan Time { get; private set; }

        public int Years { get; private set; }
    }
}
