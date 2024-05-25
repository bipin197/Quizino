using System;
using System.Collections.Generic;
using System.Linq;

namespace AppLogic
{
    public class QuestionRandomizer
    {
        public static IEnumerable<long> GetRandomKeysFromAvailableKeys(IEnumerable<long> activeAvailableKeys, int requestedNumbers)
        {
            if (activeAvailableKeys == null || !activeAvailableKeys.Any())
            {
                return Enumerable.Empty<long>();
            }
            
            int minRange = 1; // Minimum number in the range
            var maxRange = (int)activeAvailableKeys.Max(x => x); // Change it later
            int count = requestedNumbers; // Number of random numbers to generate



            var generatedNumbers = new HashSet<long>();
            Random random = new Random();

            while (generatedNumbers.Count < count)
            {
                int randomNumber = random.Next(minRange, maxRange + 1);
                if (!generatedNumbers.Contains(randomNumber) && activeAvailableKeys.Contains(randomNumber))
                {
                    generatedNumbers.Add(randomNumber);
                }
            }

            return generatedNumbers.Select(x => x);
        }
    }
}
