using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AppLogic
{
    public class QuestionRandomizer
    {
        public static IEnumerable<long> GetRandomKeysFromAvailableKeys(ILogger logger, Func<IEnumerable<long>> getAvailableKeys, int requestedNumbers)
        {
            if (getAvailableKeys == null)
            {
                return Enumerable.Empty<long>();
            }

            var availableKeys = getAvailableKeys();

            int minRange = 1; // Minimum number in the range
            int maxRange = 100; // Maximum number in the range
            int count = requestedNumbers; // Number of random numbers to generate

            if (maxRange - minRange + 1 < count)
            {
                logger.Error("Error: Range is smaller than the count of numbers to generate.");          
            }

            var generatedNumbers = new HashSet<long>();
            Random random = new Random();

            while (generatedNumbers.Count < count)
            {
                int randomNumber = random.Next(minRange, maxRange + 1);
                if (!generatedNumbers.Contains(randomNumber) && availableKeys.Contains(randomNumber))
                {
                    generatedNumbers.Add(randomNumber);
                }
            }

            return generatedNumbers.Select(x => x);
        }
    }
}
