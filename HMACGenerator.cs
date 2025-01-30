using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32.SafeHandles;
using System.Security.Cryptography;

namespace $safeprojectname$
{
    internal class HMACGenerator
    {
        private readonly byte[] key;

        public HMACGenerator()
        {
            key = RandomNumberGenerator.GetBytes(32); // Generuj losowy klucz
        }

        public string ComputeHMAC(string value)
        {
            using (var hmac = new HMACSHA256(key))
            {
                byte[] valueBytes = System.Text.Encoding.UTF8.GetBytes(value);
                byte[] hashBytes = hmac.ComputeHash(valueBytes);
                return BitConverter.ToString(hashBytes).Replace("-", "");
            }
        }

        public string RevealKey()
        {
            return BitConverter.ToString(key).Replace("-", "");
        }
    }
}
