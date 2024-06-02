using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace egzaminas
{
    public class PasswordManager
    {
        private const string Salt = "staticSalt";
        private const string FilePath = "password.txt";
        private string encodedPassword;

        public void EncodePassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var saltedPassword = password + Salt;
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));
                encodedPassword = Convert.ToBase64String(hashedBytes);
                File.WriteAllText(FilePath, encodedPassword);
            }
        }

        public string BruteForceDecodePassword(int threadCount)
        {
            encodedPassword = File.ReadAllText(FilePath);
            return BruteForce.Decode(encodedPassword, threadCount);
        }
    }
}
