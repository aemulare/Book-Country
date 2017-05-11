using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace BookCountry.Models.ViewModels
{
    /// <summary>
    /// Login view model.
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// Email address used as user ID.
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Password.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// Determines whether a login/password should be remembered.
        /// </summary>
        [Display(Name="Remember me?")]
        public bool RememberMe { get; set; }



        public string Hash => new string(HashPassword(Password.ToCharArray(), "salt".ToCharArray()));

        private static HashAlgorithm HashAlgorithm { get; } = new HMACSHA256();


        /// <summary>
        /// Creates a hash for the clear text password.
        /// </summary>
        /// <param name="clearText">Clear password text.</param>
        /// <param name="salt">Random salt data.</param>
        /// <returns>Returns hash array for the clear text password.</returns>
        private static byte[] HashPassword(char[] clearText, byte[] salt)
        {
            byte[] hash;
            int length;
            var utf8 = Encoding.UTF8;
            length = clearText.Length;
            for(var i = 0; i < clearText.Length; i++)
            {
                if(clearText[i] == '\0')
                {
                    length = i;
                    break;
                }
            }
            var data = new byte[salt.Length + utf8.GetMaxByteCount(length)];
            try
            {
                int byteCount;
                byteCount = utf8.GetBytes(clearText, 0, length, data, salt.Length);
                Array.Copy(salt, 0, data, 0, salt.Length);
                hash = HashAlgorithm.ComputeHash(data, 0, salt.Length + byteCount);
            }
            finally
            {
                Array.Clear(data, 0, data.Length);
            }
            return hash;
        }



        /// <summary>
        /// Create hash for the clear text password.
        /// </summary>
        /// <param name="clearPassword">Clear password text.</param>
        /// <param name="salt">Random salt data.</param>
        /// <returns>Returns hash array for the clear text password.</returns>
        private static char[] HashPassword(char[] clearPassword, char[] salt)
        {
            byte[] hashBinary = null;
            try
            {
                hashBinary = HashPassword(clearPassword, Convert.FromBase64CharArray(salt, 0, salt.Length));
                var hash = new char[ComputeBase64Length(hashBinary.Length)];
                Convert.ToBase64CharArray(hashBinary, 0, hashBinary.Length, hash, 0);

                return hash;
            }
            finally
            {
                if(hashBinary != null)
                    Array.Clear(hashBinary, 0, hashBinary.Length);
            }
        }



        private static int ComputeBase64Length(int binaryLength)
        {
            if(binaryLength == 0)
                return 0;
            return ((binaryLength - 1) / 3 + 1) * 4;
        }
    }
}
