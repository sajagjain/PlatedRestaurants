using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace restaurantBAL
{
    public class HashPassword
    {
        public static string ComputeHash(string input, HashAlgorithm algorithm)
        {
            try { 
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);

            Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);

            return BitConverter.ToString(hashedBytes);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
