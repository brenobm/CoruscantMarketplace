using System;
using System.Collections.Generic;
using System.Linq;

namespace CoruscantMarketplace.FakeData
{
    public static class UtilHelper
    {
        private static Random random = new Random();

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static int RandomInt()
        {
            return random.Next();
        }

        public static int RandomInt(int max)
        {
            return random.Next(1, max);
        }

        public static byte RandomByte()
        {
            return Convert.ToByte(random.Next(0, 255));
        }

        public static byte RandomByte(byte max)
        {
            return Convert.ToByte(random.Next(1, max));
        }

        public static decimal RandomDecimal()
        {
            return Convert.ToDecimal(random.NextDouble());
        }

        public static IEnumerable<int> RandomIntList(int quantidade, int max)
        {
            return Enumerable.Range(0, max).OrderBy(x => random.Next()).Take(quantidade);
        }
    }
}
