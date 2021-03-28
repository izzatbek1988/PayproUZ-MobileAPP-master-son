using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Paypro_Mobile.Models
{
    public class SHA1
    {
        public static string GenerateSHA1(string value)
        {
            var data = Encoding.ASCII.GetBytes(value);
            var hashData = new SHA1Managed().ComputeHash(data);
            var hash = string.Empty;
            foreach (var b in hashData)
            {
                hash += b.ToString("X2");
            }
            return hash.ToLower();
        }
    }
}
