using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Utilities
{
    public class NumberHandler
    {
        //TODO: make it random
        public static IEnumerable<long> GenerateRandomKeys(int count, int min, int max)
        {
            var random = new Random();
            var createdNumbers = new List<long>();
            for (var createdNumberCount = 0; createdNumberCount < count;)
            {
                var number = random.Next(min, max);
                if (!createdNumbers.Contains(number))
                {
                    createdNumbers.Add(number);
                    createdNumberCount++;
                }
            }

            return createdNumbers;
        }

        public static long GetNextKey(IEnumerable<IEntityBase> enumerable)
        {
            if (enumerable.Any())
            {
                return enumerable.Max(x => x.Key) + 1;
            }

            return 1;
        }
    }
}
