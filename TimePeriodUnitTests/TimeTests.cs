using TimePeriodNamespace;

namespace TimePeriodUnitTests
{
    [TestClass]
    public class TimeTests
    {
        [TestMethod]
        [DataTestMethod, TestCategory("Constructors")]
        [DataRow((byte)12, (byte)13, (byte)14)]
        public void Constructor_3params(byte h, byte m, byte s)
        {
            Time t = new Time(h,m,s);
            Assert.AreEqual(t.Hours, h);
            Assert.AreEqual(t.Minutes, m);
            Assert.AreEqual(t.Seconds, s); 
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow((byte)12, (byte)13)]
        public void Constructor_2params(byte h, byte m)
        {
            Time t = new Time(h, m);
            Assert.AreEqual(t.Hours, h);
            Assert.AreEqual(t.Minutes, m);
            Assert.AreEqual(t.Seconds, 0);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow((byte)12)]
        public void Constructor_1param(byte h)
        {
            Time t = new Time(h);
            Assert.AreEqual(t.Hours, h);
            Assert.AreEqual(t.Minutes, 0);
            Assert.AreEqual(t.Seconds, 0);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow("23:29:39")]
        public void Constructor_stringparam(string s)
        {
            Time t = new Time(s);
            Assert.AreEqual(t.Hours, 23);
            Assert.AreEqual(t.Minutes, 29);
            Assert.AreEqual(t.Seconds, 39);
        }

        [DataTestMethod, TestCategory("ToString")]
        [DataRow("23:29:39")]
        public void ToString_MethodTest(string s)
        {
            Time t = new Time(s);
            Assert.AreEqual(t.ToString(), s);
        }

        [DataTestMethod, TestCategory("IComparable")]
        [DataRow((byte)24, (byte)12, (byte)12, (byte)0, (byte)12, (byte)12)]
        public void Equals_MethodTest(byte h, byte m, byte s, byte hh, byte mm, byte ss)
        {
            Time t = new Time(h, m, s);
            Time t1 = new Time(hh, mm, ss);
            Assert.IsTrue(t.Equals(t1));
        }

        [DataTestMethod, TestCategory("IEquatable")]
        [DataRow((byte)24, (byte)12, (byte)12, (byte)0, (byte)12, (byte)12, 0)]
        [DataRow((byte)23, (byte)11, (byte)12, (byte)23, (byte)12, (byte)12, -1)]
        [DataRow((byte)24, (byte)12, (byte)12, (byte)24, (byte)12, (byte)11, 1)]
        public void Equatable_MethodTest(byte h, byte m, byte s, byte hh, byte mm, byte ss, int result)
        {
            Time t = new Time(h, m, s);
            Time t1 = new Time(hh, mm, ss);
            Assert.AreEqual(t.CompareTo(t1), result);
        }

        [DataTestMethod, TestCategory("Operators")]
        [DataRow((byte)24, (byte)12, (byte)12, (byte)0, (byte)12, (byte)12)]
        public void EqualOperator(byte h, byte m, byte s, byte hh, byte mm, byte ss)
        {
            Time t = new Time(h, m, s);
            Time t1 = new Time(hh, mm, ss);
            Assert.IsTrue(t==t1);
        }

        [DataTestMethod, TestCategory("Operators")]
        [DataRow((byte)24, (byte)13, (byte)12, (byte)0, (byte)12, (byte)12)]
        public void NotEqualOperator(byte h, byte m, byte s, byte hh, byte mm, byte ss)
        {
            Time t = new Time(h, m, s);
            Time t1 = new Time(hh, mm, ss);
            Assert.IsTrue(t != t1);
        }

        [DataTestMethod, TestCategory("Operators")]
        [DataRow((byte)12, (byte)12, (byte)12, (byte)11, (byte)12, (byte)12)]
        [DataRow((byte)23, (byte)13, (byte)12, (byte)23, (byte)12, (byte)12)]
        [DataRow((byte)23, (byte)13, (byte)22, (byte)23, (byte)13, (byte)12)]
        public void GreaterOperator(byte h, byte m, byte s, byte hh, byte mm, byte ss)
        {
            Time t = new Time(h, m, s);
            Time t1 = new Time(hh, mm, ss);
            Assert.AreEqual(t.CompareTo(t1), 1);
            Assert.IsTrue(t > t1);
        }

        [DataTestMethod, TestCategory("Operators")]
        [DataRow((byte)12, (byte)12, (byte)12, (byte)11, (byte)12, (byte)12)]
        [DataRow((byte)23, (byte)13, (byte)12, (byte)23, (byte)12, (byte)12)]
        [DataRow((byte)23, (byte)13, (byte)22, (byte)23, (byte)13, (byte)12)]
        public void LessOperator(byte h, byte m, byte s, byte hh, byte mm, byte ss)
        {
            Time t = new Time(h, m, s);
            Time t1 = new Time(hh, mm, ss);
            Assert.AreEqual(t1.CompareTo(t), -1);
            Assert.IsFalse(t < t1);
        }

        [DataTestMethod, TestCategory("Operators")]
        [DataRow((byte)12, (byte)12, (byte)12, (byte)11, (byte)12, (byte)12)]
        [DataRow((byte)23, (byte)23, (byte)23, (byte)23, (byte)23, (byte)23)]
        public void GreaterOrEqualOperator(byte h, byte m, byte s, byte hh, byte mm, byte ss)
        {
            Time t = new Time(h, m, s);
            Time t1 = new Time(hh, mm, ss);
            Assert.AreEqual(t.CompareTo(t1), 1);
            Assert.IsTrue(t >= t1);
        }

        [DataTestMethod, TestCategory("Operators")]
        [DataRow((byte)12, (byte)12, (byte)12, (byte)11, (byte)12, (byte)12)]
        [DataRow((byte)23, (byte)23, (byte)23, (byte)23, (byte)23, (byte)23)]
        public void LessOrEqualOperator(byte h, byte m, byte s, byte hh, byte mm, byte ss)
        {
            Time t = new Time(h, m, s);
            Time t1 = new Time(hh, mm, ss);
            Assert.AreEqual(t1.CompareTo(t), -1);
            Assert.IsFalse(t <= t1);
        }

        [DataTestMethod, TestCategory("Operators")]
        [DataRow((byte)12, (byte)12, (byte)12, 3671, (byte)13, (byte)13, (byte)23)]
        [DataRow((byte)23, (byte)58, (byte)59, 3671, (byte)1, (byte)0, (byte)10)] // 23 59 10,,,    00 00 10  01 00 10
        public void OperatorPlus(byte h, byte m, byte s, int interval, byte expextedh, byte expectedm, byte expecteds)
        {
            Time t = new Time(h, m, s);
            TimePeriod tp = new(interval);
            Assert.AreEqual(t+tp,new Time(expextedh, expectedm, expecteds));
        }

        [DataTestMethod, TestCategory("Operators")]
        [DataRow((byte)12, (byte)12, (byte)12, 3671, (byte)11, (byte)11, (byte)1)]
        [DataRow((byte)23, (byte)1, (byte)1, 3600 + 60 + 11, (byte)21, (byte)59, (byte)50)] // 21 59 50
        public void OperatorMinus(byte h, byte m, byte s, int interval, byte expextedh, byte expectedm, byte expecteds)
        {
            Time t = new Time(h, m, s);
            TimePeriod tp = new(interval);
            Assert.AreEqual(t - tp, new Time(expextedh, expectedm, expecteds));
        }
    }

    [TestClass]
    public class TimePeriodTests
    {
        [TestMethod]
        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(1,1,11,3671)]
        public void Constructor_3params(int h, int m, int s, int interval)
        {
            TimePeriod t = new(h,m,s);
            Assert.AreEqual(t.Interval, interval);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(1, 2, 3720)]
        public void Constructor_2params(int h, int m, int interval)
        {
            TimePeriod t = new(h, m);
            Assert.AreEqual(t.Interval, interval);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(3671)]
        public void Constructor_1param(int interval)
        {
            TimePeriod t = new(interval);
            Assert.AreEqual(t.Interval, interval);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow((byte)12, (byte)33, (byte)45, (byte)11, (byte)34, (byte)01, 82816)]
        [DataRow((byte)8, (byte)30, (byte)58, (byte)7, (byte)34, (byte)55, 83037)]
        public void Constructor_2params_Time_Time(byte h, byte m, byte s, byte hh, byte mm, byte ss, int expected)
        {
            Time t = new Time(h, m, s);
            Time t1 = new Time(hh, mm, ss);
            TimePeriod tp = new(t, t1);
            Assert.AreEqual(tp.Interval, expected);
        }

        [DataTestMethod, TestCategory("ToString")]
        [DataRow((byte)8, (byte)30, (byte)58, (byte)7, (byte)34, (byte)55, "23:3:57")]
        public void ToString_TestMethod(byte h, byte m, byte s, byte hh, byte mm, byte ss, string expected)
        {
            Time t = new Time(h, m, s);
            Time t1 = new Time(hh, mm, ss);
            TimePeriod tp = new(t, t1);
            Assert.AreEqual(tp.ToString(), expected);
        }

        [DataTestMethod, TestCategory("IComparable")]
        [DataRow(13, 13, 13, 13, 12, 13, 1)]
        [DataRow(13, 13, 13, 13, 13, 15, -1)]
        [DataRow(13, 13, 13, 13, 13, 13, 0)]
        public void GreaterThan(int h, int m, int s, int hh, int mm, int ss, int result)
        {
            TimePeriod t = new(h, m, s);
            TimePeriod t1 = new(hh, mm, ss);
            Assert.AreEqual(t.CompareTo(t1), result);
        }

        [DataTestMethod, TestCategory("Operators")]
        [DataRow(13, 14, 13, 13, 13, 13)]
        public void GreaterOperator(int h, int m, int s, int hh, int mm, int ss)
        {
            TimePeriod t = new(h, m, s);
            TimePeriod t1 = new(hh, mm, ss);
            Assert.IsTrue(t > t1);
        }

        [DataTestMethod, TestCategory("Operators")]
        [DataRow(13, 13, 13, 14, 13, 13)]
        public void LessOperator(int h, int m, int s, int hh, int mm, int ss)
        {
            TimePeriod t = new(h, m, s);
            TimePeriod t1 = new(hh, mm, ss);
            Assert.IsTrue(t < t1);
        }

        [DataTestMethod, TestCategory("Operators")]
        [DataRow(13, 13, 13, 13, 13, 13)]
        [DataRow(13, 14, 13, 13, 13, 13)]
        public void GreaterOrEqualOperator(int h, int m, int s, int hh, int mm, int ss)
        {
            TimePeriod t = new(h, m, s);
            TimePeriod t1 = new(hh, mm, ss);
            Assert.IsTrue(t >= t1);
        }

        [DataTestMethod, TestCategory("Operators")]
        [DataRow(13, 13, 13, 13, 13, 13)]
        [DataRow(13, 13, 13, 14, 13, 13)]
        public void LessOrEqualOperator(int h, int m, int s, int hh, int mm, int ss)
        {
            TimePeriod t = new(h, m, s);
            TimePeriod t1 = new(hh, mm, ss);
            Assert.IsTrue(t <= t1);
        }

        [DataTestMethod, TestCategory("Operators")]
        [DataRow(13, 13, 59, 13, 13, 13)]
        public void PlusOperator(int h, int m, int s, int hh, int mm, int ss)
        {
            TimePeriod t = new(h, m, s);
            TimePeriod t1 = new(hh, mm, ss);
            Assert.AreEqual(t + t1, new TimePeriod(26, 27, 12));
        }

        [DataTestMethod, TestCategory("Operators")]
        [DataRow(13, 13, 59, 13, 13, 13)]
        public void MinusOperator(int h, int m, int s, int hh, int mm, int ss)
        {
            TimePeriod t = new(h, m, s);
            TimePeriod t1 = new(hh, mm, ss);
            Assert.AreEqual(t - t1, new TimePeriod(0, 0, 46));
        }
    }
}