using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace BusinessLayer
{
    public class EncryptionHelper
    {
        private RNGCryptoServiceProvider _cryptoServiceProvider = null;
        private const int SALT_SIZE = 24;

        public EncryptionHelper()
        {
            _cryptoServiceProvider = new RNGCryptoServiceProvider();
        }

        private string GetSaltString()
        {
            byte[] saltBytes = new byte[SALT_SIZE];
            _cryptoServiceProvider.GetNonZeroBytes(saltBytes);
            return Encoding.UTF8.GetString(saltBytes);
        }

        private string GetPasswordHash(string message)
        {
            SHA512 sha = new SHA512CryptoServiceProvider();
            byte[] dataBytes = Encoding.UTF8.GetBytes(message);
            byte[] resultBytes = sha.ComputeHash(dataBytes);
            return Encoding.UTF8.GetString(resultBytes);
        }

        public string GeneratePasswordHash(string password, ref string salt)
        {
            salt = salt == "" ? GetSaltString() : salt;
            string finalString = password + salt;
            return GetPasswordHash(finalString);
        }
    }
}
