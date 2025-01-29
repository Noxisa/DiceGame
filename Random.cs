using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace $safeprojectname$
{
    internal class Random
    {
        public int Generate(int minValue, int maxValue)
        {
            if (minValue >= maxValue) throw new ArgumentException("Min value must be less than max value.");

            int range = maxValue - minValue + 1;
            byte[] buffer = new byte[4];
            int result;

            do
            {
                RandomNumberGenerator.Fill(buffer);
                result = BitConverter.ToInt32(buffer, 0) & int.MaxValue;
            } while (result >= range * (int.MaxValue / range));

            return minValue + result % range;
        }
    }
}
