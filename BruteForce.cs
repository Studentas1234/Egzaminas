using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace egzaminas
{
    public class BruteForce
    {
        private const string Salt = "staticSalt";
        private const string Characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        public static string Decode(string encodedPassword, int threadCount)
        {
            string decodedPassword = null;
            bool found = false;

            var options = new ParallelOptions { MaxDegreeOfParallelism = Math.Min(threadCount, Environment.ProcessorCount) };

            Parallel.ForEach(CombinationGenerator.GenerateAllCombinations(), options, (password, state) =>
            {
                if (found)
                {
                    state.Stop();
                    return;
                }

                if (VerifyPassword(password, encodedPassword))
                {
                    decodedPassword = password;
                    found = true;
                    state.Stop();
                }
            });

            return decodedPassword;
        }

        private static bool VerifyPassword(string password, string encodedPassword)
        {
            using (var sha256 = SHA256.Create())
            {
                var saltedPassword = password + Salt;
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));
                var hashedPassword = Convert.ToBase64String(hashedBytes);
                return hashedPassword == encodedPassword;
            }
        }
    }
}
