using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Text;

namespace TimePeriodNamespace
{
    /// <summary>
    /// Reprezentuje okres czasu w sekundach.
    /// </summary>
    public struct TimePeriod :IEquatable<TimePeriod>, IComparable<TimePeriod>
    {
        /// <summary>
        /// Reprezentuje długość interwału czasowego w sekundach.
        /// </summary>
        public readonly long Interval { get; }

        /// <summary>
        /// Inicjalizuje nową instancję klasy <see cref="TimePeriod"/> na podstawie liczby sekund.
        /// </summary>
        /// <param name="seconds">Liczba sekund.</param>
        /// <exception cref="ArgumentException">Wyrzucany, gdy wartość <paramref name="seconds"/> nie jest poprawną liczbą całkowitą.</exception>
        public TimePeriod(long seconds) 
        {
            if (!IsValidLong(seconds)) throw new ArgumentException("Value is not valid byte values.");
            Interval = seconds; 
        }

        /// <summary>
        /// Inicjalizuje nową instancję klasy <see cref="TimePeriod"/> na podstawie liczby godzin, minut i sekund.
        /// </summary>
        /// <param name="hours">Liczba godzin.</param>
        /// <param name="minutes">Liczba minut.</param>
        /// <param name="seconds">Liczba sekund.</param>
        /// <exception cref="ArgumentException">Wyrzucany, gdy wartości <paramref name="hours"/>, <paramref name="minutes"/> lub <paramref name="seconds"/> nie są poprawnymi liczbami całkowitymi.</exception>
        public TimePeriod(int hours, int minutes, int seconds) 
        {
            if (!IsValidInt(hours) || !IsValidInt(minutes) || !IsValidInt(seconds)) throw new ArgumentException("One or more values are not valid byte values.");
            Interval = hours * 3600 + minutes * 60 + seconds; 
        }

        /// <summary>
        /// Inicjalizuje nową instancję klasy <see cref="TimePeriod"/> na podstawie liczby godzin, minut i sekund.
        /// </summary>
        /// <param name="hours">Liczba godzin.</param>
        /// <param name="minutes">Liczba minut.</param>
        /// <exception cref="ArgumentException">Wyrzucany, gdy wartości <paramref name="hours"/> lub <paramref name="minutes"/> nie są poprawnymi liczbami całkowitymi.</exception>
        public TimePeriod(int hours, int minutes) : this(hours, minutes, 0) { }

        /// <summary>
        /// Inicjalizuje nową instancję klasy <see cref="TimePeriod"/> na podstawie dwóch obiektów typu <see cref="Time"/>.
        /// </summary>
        /// <param name="first">Pierwszy obiekt typu <see cref="Time"/>.</param>
        /// <param name="second">Drugi obiekt typu <see cref="Time"/>.</param>
        /// <exception cref="ArgumentException">Wyrzucany, gdy jeden lub oba obiekty typu <see cref="Time"/> nie są poprawne.</exception>
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

        /// <summary>
        /// Określa, czy bieżący obiekt <see cref="TimePeriod"/> jest równy innemu obiektowi <see cref="TimePeriod"/>.
        /// </summary>
        /// <param name="other">Inny obiekt <see cref="TimePeriod"/> do porównania.</param>
        /// <returns>True, jeśli bieżący obiekt jest równy obiektowi <paramref name="other"/>; w przeciwnym razie false.</returns>
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

        /// <summary>
        /// Porównuje dwa obiekty typu <see cref="TimePeriod"/> pod kątem równości.
        /// </summary>
        /// <param name="left">Pierwszy obiekt typu <see cref="TimePeriod"/> do porównania.</param>
        /// <param name="right">Drugi obiekt typu <see cref="TimePeriod"/> do porównania.</param>
        /// <returns>True, jeśli obiekty są równe; w przeciwnym razie false.</returns>
        public static bool operator ==(TimePeriod left, TimePeriod right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Porównuje dwa obiekty typu <see cref="TimePeriod"/> pod kątem nierówności.
        /// </summary>
        /// <param name="left">Pierwszy obiekt typu <see cref="TimePeriod"/> do porównania.</param>
        /// <param name="right">Drugi obiekt typu <see cref="TimePeriod"/> do porównania.</param>
        /// <returns>True, jeśli obiekty są nierówne; w przeciwnym razie false.</returns>
        public static bool operator !=(TimePeriod left, TimePeriod right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Zwraca ciąg reprezentujący bieżący obiekt <see cref="TimePeriod"/>.
        /// </summary>
        /// <returns>Ciąg reprezentujący bieżący obiekt <see cref="TimePeriod"/> w formacie hh:mm:ss.</returns>
        public override string ToString()
        {
            int h, m, s;

            h = (int)Math.Floor((double)(Interval / 3600));
            m = (int)Math.Floor((double)((Interval % 3600) / 60));
            s = (int)((Interval % 3600) % 60);

            return $"{h:00}:{m:00}:{s:00}";
        }

        /// <summary>
        /// Porównuje bieżący obiekt <see cref="TimePeriod"/> z innym obiektem <see cref="TimePeriod"/>.
        /// </summary>
        /// <param name="other">Inny obiekt <see cref="TimePeriod"/> do porównania.</param>
        /// <returns>
        /// Liczba całkowita wskazująca relację pomiędzy bieżącym obiektem a obiektem <paramref name="other"/>.
        /// Wartość większa niż zero oznacza, że bieżący obiekt jest większy od obiektu <paramref name="other"/>.
        /// Wartość zero oznacza, że oba obiekty są równe.
        /// Wartość mniejsza niż zero oznacza, że bieżący obiekt jest mniejszy od obiektu <paramref name="other"/>.
        /// </returns>
        public int CompareTo(TimePeriod other)
        {
            if (Interval > other.Interval) return 1;
            else if (Interval == other.Interval) return 0;
            else return -1;
        }

        /// <summary>
        /// Porównuje dwa obiekty typu <see cref="TimePeriod"/> pod kątem większości.
        /// </summary>
        /// <param name="left">Pierwszy obiekt typu <see cref="TimePeriod"/> do porównania.</param>
        /// <param name="right">Drugi obiekt typu <see cref="TimePeriod"/> do porównania.</param>
        /// <returns>True, jeśli pierwszy obiekt jest większy od drugiego; w przeciwnym razie false.</returns>
        public static bool operator >(TimePeriod left, TimePeriod right)
        {
            if(left.CompareTo(right) == 1) return true;
            else return false;
        }

        /// <summary>
        /// Porównuje dwa obiekty typu <see cref="TimePeriod"/> pod kątem większości lub równości.
        /// </summary>
        /// <param name="left">Pierwszy obiekt typu <see cref="TimePeriod"/> do porównania.</param>
        /// <param name="right">Drugi obiekt typu <see cref="TimePeriod"/> do porównania.</param>
        /// <returns>True, jeśli pierwszy obiekt jest większy lub równy drugiemu; w przeciwnym razie false.</returns>
        public static bool operator >=(TimePeriod left, TimePeriod right)
        {
            if (left.CompareTo(right) == 1 || left.CompareTo(right) == 0) return true;
            else return false;
        }

        /// <summary>
        /// Porównuje dwa obiekty typu <see cref="TimePeriod"/> pod kątem mniejszości.
        /// </summary>
        /// <param name="left">Pierwszy obiekt typu <see cref="TimePeriod"/> do porównania.</param>
        /// <param name="right">Drugi obiekt typu <see cref="TimePeriod"/> do porównania.</param>
        /// <returns>True, jeśli pierwszy obiekt jest mniejszy od drugiego; w przeciwnym razie false.</returns>
        public static bool operator <(TimePeriod left, TimePeriod right)
        {
            if (left.CompareTo(right) == -1) return true;
            else return false;
        }

        /// <summary>
        /// Porównuje dwa obiekty typu <see cref="TimePeriod"/> pod kątem mniejszości lub równości.
        /// </summary>
        /// <param name="left">Pierwszy obiekt typu <see cref="TimePeriod"/> do porównania.</param>
        /// <param name="right">Drugi obiekt typu <see cref="TimePeriod"/> do porównania.</param>
        /// <returns>True, jeśli pierwszy obiekt jest mniejszy lub równy drugiemu; w przeciwnym razie false.</returns>
        public static bool operator <=(TimePeriod left, TimePeriod right)
        {
            if (left.CompareTo(right) == -1 || left.CompareTo(right) == 0) return true;
            else return false;
        }

        /// <summary>
        /// Dodaje dwa obiekty typu <see cref="TimePeriod"/> i zwraca wynik jako nowy obiekt <see cref="TimePeriod"/>.
        /// </summary>
        /// <param name="left">Pierwszy obiekt typu <see cref="TimePeriod"/>.</param>
        /// <param name="right">Drugi obiekt typu <see cref="TimePeriod"/>.</param>
        /// <returns>Nowy obiekt <see cref="TimePeriod"/> reprezentujący sumę dwóch obiektów.</returns>
        public static TimePeriod Plus(TimePeriod left, TimePeriod right)
        {
            return new TimePeriod(left.Interval + right.Interval);
        }

        /// <summary>
        /// Odejmuje dwa obiekty typu <see cref="TimePeriod"/> i zwraca wynik jako nowy obiekt <see cref="TimePeriod"/>.
        /// </summary>
        /// <param name="left">Pierwszy obiekt typu <see cref="TimePeriod"/>.</param>
        /// <param name="right">Drugi obiekt typu <see cref="TimePeriod"/>.</param>
        /// <returns>Nowy obiekt <see cref="TimePeriod"/> reprezentujący różnicę między dwoma obiektami.</returns>
        public static TimePeriod Minus(TimePeriod left, TimePeriod right)
        {
            return new TimePeriod(left.Interval - right.Interval);
        }

        /// <summary>
        /// Dodaje dwa obiekty typu <see cref="TimePeriod"/> i zwraca wynik jako nowy obiekt <see cref="TimePeriod"/>.
        /// </summary>
        /// <param name="left">Pierwszy obiekt typu <see cref="TimePeriod"/>.</param>
        /// <param name="right">Drugi obiekt typu <see cref="TimePeriod"/>.</param>
        /// <returns>Nowy obiekt <see cref="TimePeriod"/> reprezentujący sumę dwóch obiektów.</returns>
        public static TimePeriod operator +(TimePeriod left, TimePeriod right) => Plus(left, right);

        /// <summary>
        /// Odejmuje dwa obiekty typu <see cref="TimePeriod"/> i zwraca wynik jako nowy obiekt <see cref="TimePeriod"/>.
        /// </summary>
        /// <param name="left">Pierwszy obiekt typu <see cref="TimePeriod"/>.</param>
        /// <param name="right">Drugi obiekt typu <see cref="TimePeriod"/>.</param>
        /// <returns>Nowy obiekt <see cref="TimePeriod"/> reprezentujący różnicę między dwoma obiektami.</returns>
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
