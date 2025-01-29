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
        private readonly byte[] secretKey;

        public HMACGenerator()
        {
            secretKey = GenerateSecretKey();
        }

        public byte[] GenerateSecretKey()
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                byte[] key = new byte[32];
                rng.GetBytes(key);
                return key;
            }
        }

        public string ComputeHMAC(int message)
        {
            using (var hmac = new HMACSHA256(secretKey))
            {
                byte[] messageBytes = Encoding.UTF8.GetBytes(message.ToString());
                byte[] hash = hmac.ComputeHash(messageBytes);
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }

        public string RevealKey()
        {
            return BitConverter.ToString(secretKey).Replace("-", "").ToLower();
        }
    }
}
