using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Text;

namespace TimePeriodNamespace
{
    public struct TimePeriod :IEquatable<TimePeriod>, IComparable<TimePeriod>
    {
        public readonly long Interval { get; } // in seconds

        public TimePeriod(long seconds) 
        {
            if (!IsValidLong(seconds)) throw new ArgumentException("Value is not valid byte values.");
            Interval = seconds; 
        }

        public TimePeriod(int hours, int minutes, int seconds) 
        {
            if (!IsValidInt(hours) || !IsValidInt(minutes) || !IsValidInt(seconds)) throw new ArgumentException("One or more values are not valid byte values.");
            Interval = hours * 3600 + minutes * 60 + seconds; 
        }

        public TimePeriod(int hours, int minutes) : this(hours, minutes, 0) { }

        public TimePeriod(Time first, Time second) 
        {
            if (!IsValidTime(first) || !IsValidTime(second)) throw new ArgumentException("One or more values are not valid byte values.");

            int seconds =0, minutes=0, hours=0;
            Time temp = first;

            for(int i = temp.Seconds; (i%60) != second.Seconds; i++)
            {
                seconds++;
            }
            temp += seconds;

            for (int i = temp.Minutes; (i % 60) != second.Minutes; i++)
            {
                minutes++;
            }

            temp += 60 * minutes;

            for (int i = temp.Hours; (i % 24) != second.Hours; i++)
            {
                hours++;
            }

            Interval = seconds + 60*minutes + 3600*hours;   
        }

        public bool Equals(TimePeriod other)
        {
            if(Interval == other.Interval) return true;
            else return false;
        }

        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(TimePeriod left, TimePeriod right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(TimePeriod left, TimePeriod right)
        {
            return !left.Equals(right);
        }


        public override string ToString()
        {
            int h, m, s;

            h = (int)Math.Floor((double)(Interval / 3600));
            m = (int)Math.Floor((double)((Interval % 3600) / 60));
            s = (int)((Interval % 3600) % 60);

            return $"{h}:{m}:{s}";
        }

        public int CompareTo(TimePeriod other)
        {
            if (Interval > other.Interval) return 1;
            else if (Interval == other.Interval) return 0;
            else return -1;
        }

        public static bool operator >(TimePeriod left, TimePeriod right)
        {
            if(left.CompareTo(right) == 1) return true;
            else return false;
        }
        public static bool operator >=(TimePeriod left, TimePeriod right)
        {
            if (left.CompareTo(right) == 1 || left.CompareTo(right) == 0) return true;
            else return false;
        }

        public static bool operator <(TimePeriod left, TimePeriod right)
        {
            if (left.CompareTo(right) == -1) return true;
            else return false;
        }
        public static bool operator <=(TimePeriod left, TimePeriod right)
        {
            if (left.CompareTo(right) == -1 || left.CompareTo(right) == 0) return true;
            else return false;
        }

        public static TimePeriod Plus(TimePeriod left, TimePeriod right)
        {
            return new TimePeriod(left.Interval + right.Interval);
        }

        public static TimePeriod Minus(TimePeriod left, TimePeriod right)
        {
            return new TimePeriod(left.Interval - right.Interval);
        }

        public static TimePeriod operator +(TimePeriod left, TimePeriod right) => Plus(left, right);

        public static TimePeriod operator -(TimePeriod left, TimePeriod right) => Minus(left, right);


        private static bool IsValidLong(long value)
        {
            return typeof(long).IsAssignableFrom(value.GetType());
        }

        private static bool IsValidInt(int value)
        {
            return typeof(int).IsAssignableFrom(value.GetType());
        }

        private static bool IsValidTime(Time value)
        {
            return typeof(Time).IsAssignableFrom(value.GetType());
        }
    }
}
