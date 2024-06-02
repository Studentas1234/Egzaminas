using System.Collections.Generic;

namespace egzaminas
{
    public static class CombinationGenerator
    {
        private const string Characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        public static IEnumerable<string> GenerateAllCombinations()
        {
            for (int length = 1; length <= 9; length++)
            {
                foreach (var combination in GenerateCombinations(length))
                {
                    yield return combination;
                }
            }
        }

        private static IEnumerable<string> GenerateCombinations(int length)
        {
            foreach (var combination in GenerateCombinationsRecursive("", length))
            {
                yield return combination;
            }
        }

        private static IEnumerable<string> GenerateCombinationsRecursive(string prefix, int length)
        {
            if (length == 0)
            {
                yield return prefix;
                yield break;
            }

            foreach (var character in Characters)
            {
                foreach (var combination in GenerateCombinationsRecursive(prefix + character, length - 1))
                {
                    yield return combination;
                }
            }
        }
    }
}
