using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation.Provider;

namespace TimePeriodNamespace
{
    public readonly struct Time : IEquatable<Time>, IComparable<Time>
    {
        public readonly byte Hours { get; }
        public readonly byte Minutes { get; }
        public readonly byte Seconds { get; }

        public Time(byte h, byte m, byte s)
        {
            if (!IsValidByte(h) || !IsValidByte(m) || !IsValidByte(s))
            {
                throw new ArgumentException("One or more values are not valid byte values.");
            }

            Hours = (byte)(h % 24);
            Minutes = (byte)(m % 60);
            Seconds = (byte)(s % 60);
        }

        public Time(byte h, byte m): this(h,m,0) { }

        public Time(byte h) : this(h, 0, 0) { }

        public Time(string s)
        {
            string[] data = s.Split(':');

            if (data.Length != 3) { throw new ArgumentOutOfRangeException("Wrong input format. Correct format \"hh:mm:ss\""); }
            byte hh,mm,ss;

            if (byte.TryParse(data[0], out hh))
            {
                if(hh > 23)
                {
                    Hours = (byte)(hh % 24);
                }
                else
                {
                    Hours = hh;
                }
            }
            else
            {
                Hours = 0;
            }

            if (byte.TryParse(data[1], out mm))
            {
                if (mm > 59)
                {
                    Minutes = (byte)(mm % 60);
                }
                else
                {
                    Minutes = mm;
                }
            }
            else
            {
                Minutes = 0;
            }

            if (byte.TryParse(data[2], out ss))
            {
                if(ss > 59)
                {
                    Seconds = (byte)(ss % 60); ;
                }
                else
                {
                    Seconds = ss;
                }
            }
            else
            {
                Seconds = 0;
            }
        }

        public override string ToString() => $"{Hours}:{Minutes}:{Seconds}";

        public bool Equals(Time other)
        {
            if(Hours == other.Hours && Minutes == other.Minutes && Seconds == other.Seconds) return true;
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

        public int CompareTo([AllowNull] Time other)
        {

            if (Hours > other.Hours) return 1;
            else
            {
                if(Hours == other.Hours)
                {
                    if(Minutes > other.Minutes) return 1;
                    else
                    {
                        if(Minutes == other.Minutes)
                        {
                            if(Seconds > other.Seconds) return 1;
                            else
                            {
                                if(Seconds == other.Seconds)
                                {
                                    return 0;
                                }
                                else
                                {
                                    return -1;
                                }
                            }
                        }
                        else
                        {
                            return -1;
                        }
                    }
                }
                else
                {
                    return -1;
                }
            }

            throw new NotImplementedException();
        }

        public static bool operator ==(Time left, Time right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Time left, Time right)
        {
            return !left.Equals(right);
        }

        public static bool operator >(Time left, Time right)
        {
            return left.CompareTo(right) == 1 ? true : false;
        }

        public static bool operator >=(Time left, Time right)
        {
            return left.CompareTo(right) == 1 || left.CompareTo(right) == 0 ? true : false;
        }

        public static bool operator <(Time left, Time right)
        {
            return left.CompareTo(right) == -1 ? true : false;
        }

        public static bool operator <=(Time left, Time right)
        {
            return left.CompareTo(right) == -1 || left.CompareTo(right) == 0 ? true : false;
        }

        public static Time operator +(Time left, TimePeriod right)
        {
            return left.Plus(right);
        }
        public static Time operator +(Time left, int right)
        {
            return left.Plus(right);
        }

        public static Time operator -(Time left, TimePeriod right)
        {
            return left.Minus(right);
        }

        public static Time operator -(Time left, int right)
        {
            return left.Minus(right);
        }

        public Time Plus(TimePeriod t)
        {
            byte hours = Hours, minutes = Minutes, seconds = Seconds;

            seconds += (byte)((t.Interval % 3600) % 60);
            if(seconds >= 60)
            {
                minutes += (byte)Math.Floor((double)seconds / 60);
                seconds %= 60;
            }

            minutes += (byte)Math.Floor((double)(t.Interval % 3600) / 60);
            if (minutes >= 60)
            {
                hours += (byte)Math.Floor((double)minutes / 60);
                minutes %= 60;
            }

            hours += (byte)Math.Floor((double)(t.Interval / 3600));
            hours %= 24;

            return new Time(hours,minutes,seconds);
        }

        public Time Minus(TimePeriod t)
        {
            short hours = Hours, minutes = Minutes, seconds = Seconds;

            seconds -= (short)((t.Interval % 3600) % 60);
            if (seconds < 0)
            {
                minutes--;
                seconds += 60;
            }

            minutes -= (short)Math.Floor((double)(t.Interval % 3600) / 60);
            if (minutes < 0)
            {
                hours--;
                minutes += 60;
            }

            hours -= (short)Math.Floor((double)(t.Interval / 3600));
            hours %= 24;
            if (hours < 0) hours += 24;

            return new Time((byte)hours,(byte)minutes,(byte)seconds);
        }

        public Time Minus(int interval)
        {
            return Minus(new TimePeriod(interval));
        }

        public Time Plus(int interval)
        {
            return Plus(new TimePeriod(interval));
        }

        private static bool IsValidByte(byte value)
        {
            return typeof(byte).IsAssignableFrom(value.GetType());
        }
    }
}
