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
        /// <summary>
        /// Godziny.
        /// </summary>
        public readonly byte Hours { get; }

        /// <summary>
        /// Minuty.
        /// </summary>
        public readonly byte Minutes { get; }

        /// <summary>
        /// Sekundy.
        /// </summary>
        public readonly byte Seconds { get; }


        /// <summary>
        /// Inicjalizuje nową instancję klasy <see cref="Time"/> z podanymi wartościami godzin, minut i sekund.
        /// </summary>
        /// <param name="h">Liczba reprezentująca godziny.</param>
        /// <param name="m">Liczba reprezentująca minuty.</param>
        /// <param name="s">Liczba reprezentująca sekundy.</param>
        /// <exception cref="ArgumentException">Wyrzucane, gdy jedna lub więcej wartości nie są prawidłowymi liczbami typu byte.</exception>
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

        /// <summary>
        /// Inicjalizuje nową instancję klasy <see cref="Time"/> z podanymi wartościami godzin i minut. Sekundy są ustawione na 0.
        /// </summary>
        /// <param name="h">Liczba reprezentująca godziny.</param>
        /// <param name="m">Liczba reprezentująca minuty.</param>
        public Time(byte h, byte m): this(h,m,0) { }

        /// <summary>
        /// Inicjalizuje nową instancję klasy <see cref="Time"/> z podaną wartością godzin. Minuty i sekundy są ustawione na 0.
        /// </summary>
        /// <param name="h">Liczba reprezentująca godziny.</param>
        public Time(byte h) : this(h, 0, 0) { }

        /// <summary>
        /// Inicjalizuje nową instancję klasy <see cref="Time"/> na podstawie podanego ciągu znaków reprezentującego czas w formacie "hh:mm:ss".
        /// </summary>
        /// <param name="s">Ciąg znaków reprezentujący czas w formacie "hh:mm:ss".</param>
        /// <exception cref="ArgumentOutOfRangeException">Wyrzucane, gdy podany format jest nieprawidłowy.</exception>
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

        /// <summary>
        /// Zwraca reprezentację czasu jako ciąg znaków w formacie "hh:mm:ss".
        /// </summary>
        /// <returns>Reprezentacja czasu jako ciąg znaków w formacie "hh:mm:ss".</returns>
        public override string ToString() => $"{Hours:00}:{Minutes:00}:{Seconds:00}";

        /// <summary>
        /// Określa, czy bieżący obiekt klasy <see cref="Time"/> jest równy podanemu obiektowi <paramref name="other"/>.
        /// </summary>
        /// <param name="other">Obiekt klasy <see cref="Time"/> do porównania z bieżącym obiektem.</param>
        /// <returns>True, jeśli bieżący obiekt klasy <see cref="Time"/> jest równy podanemu obiektowi <paramref name="other"/>; w przeciwnym razie false.</returns>
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

        /// <summary>
        /// Porównuje bieżący obiekt klasy <see cref="Time"/> z innym obiektem <paramref name="other"/> i zwraca wartość wskazującą na kolejność.
        /// </summary>
        /// <param name="other">Obiekt klasy <see cref="Time"/> do porównania z bieżącym obiektem.</param>
        /// <returns>
        /// Liczbową wartość wskazującą na wzajemną kolejność obiektów:
        ///   - Wartość większa niż zero wskazuje, że bieżący obiekt jest większy niż <paramref name="other"/>.
        ///   - Wartość mniejsza niż zero wskazuje, że bieżący obiekt jest mniejszy niż <paramref name="other"/>.
        ///   - Wartość równa zero wskazuje, że obiekty są sobie równe.
        /// </returns>
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

        /// <summary>
        /// Porównuje dwa obiekty klasy <see cref="Time"/> pod względem równości.
        /// </summary>
        /// <param name="left">Pierwszy obiekt do porównania.</param>
        /// <param name="right">Drugi obiekt do porównania.</param>
        /// <returns>True, jeśli obiekty są sobie równe; w przeciwnym razie false.</returns>
        public static bool operator ==(Time left, Time right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Porównuje dwa obiekty klasy <see cref="Time"/> pod względem nierówności.
        /// </summary>
        /// <param name="left">Pierwszy obiekt do porównania.</param>
        /// <param name="right">Drugi obiekt do porównania.</param>
        /// <returns>True, jeśli obiekty nie są sobie równe; w przeciwnym razie false.</returns>
        public static bool operator !=(Time left, Time right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Porównuje dwa obiekty klasy <see cref="Time"/> pod względem większości.
        /// </summary>
        /// <param name="left">Pierwszy obiekt do porównania.</param>
        /// <param name="right">Drugi obiekt do porównania.</param>
        /// <returns>True, jeśli pierwszy obiekt jest większy niż drugi; w przeciwnym razie false.</returns>
        public static bool operator >(Time left, Time right)
        {
            return left.CompareTo(right) == 1 ? true : false;
        }

        /// <summary>
        /// Porównuje dwa obiekty klasy <see cref="Time"/> pod względem większości lub równości.
        /// </summary>
        /// <param name="left">Pierwszy obiekt do porównania.</param>
        /// <param name="right">Drugi obiekt do porównania.</param>
        /// <returns>True, jeśli pierwszy obiekt jest większy lub równy drugiemu; w przeciwnym razie false.</returns>
        public static bool operator >=(Time left, Time right)
        {
            return left.CompareTo(right) == 1 || left.CompareTo(right) == 0 ? true : false;
        }

        /// <summary>
        /// Porównuje dwa obiekty klasy <see cref="Time"/> pod względem mniejszości.
        /// </summary>
        /// <param name="left">Pierwszy obiekt do porównania.</param>
        /// <param name="right">Drugi obiekt do porównania.</param>
        /// <returns>True, jeśli pierwszy obiekt jest mniejszy niż drugi; w przeciwnym razie false.</returns>
        public static bool operator <(Time left, Time right)
        {
            return left.CompareTo(right) == -1 ? true : false;
        }

        /// <summary>
        /// Porównuje dwa obiekty klasy <see cref="Time"/> pod względem mniejszości lub równości.
        /// </summary>
        /// <param name="left">Pierwszy obiekt do porównania.</param>
        /// <param name="right">Drugi obiekt do porównania.</param>
        /// <returns>True, jeśli pierwszy obiekt jest mniejszy lub równy drugiemu; w przeciwnym razie false.</returns>
        public static bool operator <=(Time left, Time right)
        {
            return left.CompareTo(right) == -1 || left.CompareTo(right) == 0 ? true : false;
        }

        /// <summary>
        /// Dodaje obiekt klasy <see cref="Time"/> i obiekt klasy <see cref="TimePeriod"/>.
        /// </summary>
        /// <param name="left">Obiekt <see cref="Time"/> do którego będzie dodawane.</param>
        /// <param name="right">Obiekt <see cref="TimePeriod"/> do dodania.</param>
        /// <returns>Nowy obiekt <see cref="Time"/> będący sumą <paramref name="left"/> i <paramref name="right"/>.</returns>
        public static Time operator +(Time left, TimePeriod right)
        {
            return left.Plus(right);
        }
        public static Time operator +(Time left, int right)
        {
            return left.Plus(right);
        }

        /// <summary>
        /// Odejmuje od obiektu klasy <see cref="Time"/> obiekt klasy <see cref="TimePeriod"/>.
        /// </summary>
        /// <param name="left">Obiekt <see cref="Time"/> od którego będzie odejmowane.</param>
        /// <param name="right">Obiekt <see cref="TimePeriod"/> do odjęcia.</param>
        /// <returns>Nowy obiekt <see cref="Time"/> będący różnicą między <paramref name="left"/> a <paramref name="right"/>.</returns>
        public static Time operator -(Time left, TimePeriod right)
        {
            return left.Minus(right);
        }

        public static Time operator -(Time left, int right)
        {
            return left.Minus(right);
        }

        /// <summary>
        /// Dodaje obiekt klasy <see cref="Time"/> i obiekt klasy <see cref="TimePeriod"/>.
        /// </summary>
        /// <param name="t">Obiekt <see cref="TimePeriod"/>, który zostanie dodany do obiektu typu <see cref="Time"/></param>
        /// <returns>Nowy obiekt <see cref="Time"/> będący sumą <see cref="Time"/> i <see cref="TimePeriod"/>.</returns>
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

        /// <summary>
        /// Odejmuje obiekt klasy <see cref="TimePeriod"/> od obiektu klasy <see cref="Time"/>.
        /// </summary>
        /// <param name="t">Obiekt <see cref="TimePeriod"/>, który zostanie odjęty do obiektu typu <see cref="Time"/></param>
        /// <returns>Nowy obiekt <see cref="Time"/> będący różnicą <see cref="Time"/> i <see cref="TimePeriod"/>.</returns>
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

        private Time Minus(int interval)
        {
            return Minus(new TimePeriod(interval));
        }

        private Time Plus(int interval)
        {
            return Plus(new TimePeriod(interval));
        }

        private static bool IsValidByte(byte value)
        {
            return typeof(byte).IsAssignableFrom(value.GetType());
        }
    }
}
