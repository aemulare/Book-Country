using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authentication;

namespace BookCountry.Helpers
{
    /// <summary>
    /// Provides helpers methods using for authentication purposes.
    /// </summary>
    public static class EncriptionHelper
    {
        /// <summary>
        /// Generates SHA256 has for the specified plain string (password).
        /// </summary>
        /// <param name="plainText">Plain text.</param>
        /// <returns>SHA256 hash.</returns>
        public static string Sha256Hash(string plainText)
        {
            using(var alg = SHA256.Create())
            {
                var hash = alg.ComputeHash(Encoding.Unicode.GetBytes(plainText));
                return Base64UrlTextEncoder.Encode(hash);
            }
        }
    }
}
