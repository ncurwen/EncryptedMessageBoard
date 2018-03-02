using System;
using System.Security.Cryptography;
using System.Text;

namespace EncryptedMessageBoard.Models
{
    public static class StringEncryptionUtil
    {
        // NOTE: Encryption key should be isolated from the application code and source control but for simplicity's sake I've left it here.
        private static byte[] key = new byte[8] { 1, 2, 3, 4, 5, 6, 7, 8 };

        public static string Encrypt(this string text, byte[] iv)
        {
            SymmetricAlgorithm algorithm = DES.Create();
            ICryptoTransform transform = algorithm.CreateEncryptor(key, iv);
            byte[] inputbuffer = Encoding.Unicode.GetBytes(text);
            byte[] outputBuffer = transform.TransformFinalBlock(inputbuffer, 0, inputbuffer.Length);
            return Convert.ToBase64String(outputBuffer);
        }

        public static string Decrypt(this string text, byte[] iv)
        {
            SymmetricAlgorithm algorithm = DES.Create();
            ICryptoTransform transform = algorithm.CreateDecryptor(key, iv);
            byte[] inputbuffer = Convert.FromBase64String(text);
            byte[] outputBuffer = transform.TransformFinalBlock(inputbuffer, 0, inputbuffer.Length);
            return Encoding.Unicode.GetString(outputBuffer);
        }

        public static byte[] GenerateInitializationVector()
        {
            Random random = new Random();
            byte[] iv = new byte[8];
            random.NextBytes(iv);
            return iv;
        }
    }
}
