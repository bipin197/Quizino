using System;
using System.Collections.Generic;
using System.Text;

namespace AppLogic
{
    public class QuestionRandomizer
    {
        private static readonly List<long> _immediateBucket;
        private static readonly List<long> _weekOldBucket;
        private static readonly List<long> _fornightOldBucket;
        private static readonly List<long> _monthOldBucket;
        static QuestionRandomizer()
        {
            _immediateBucket = new List<long>();
            _weekOldBucket = new List<long>();
            _fornightOldBucket = new List<long>();
            _monthOldBucket = new List<long>();
            Initialize();
        }

        /// <summary>
        /// Bucket should be populated with real time data from Question API.
        /// </summary>
        private static void Initialize()
        {
            for (int i = 1; i < 120; i++)
            {
                _monthOldBucket.Add(i);
                _weekOldBucket.Add(120 + i);
                _fornightOldBucket.Add(240 + i);
                _immediateBucket.Add(360+ i);
            }
        }

        public static IEnumerable<long> GetRandomQuestionIds(int numberOfQuestions)
        {
            var random = new Random();
            var numbers = new List<long>();
            for (int i = 0; i < numberOfQuestions;)
            {
                var nextNumber = random.Next(1, 554);
                if (!numbers.Contains(nextNumber) && CanNumberBeUsed(nextNumber))
                {
                    numbers.Add(nextNumber);
                    i++;
                    continue;
                }
            }

            return numbers;
        }

        private static bool CanNumberBeUsed(long number)
        {
            if (!_monthOldBucket.Contains(number)
                && !_fornightOldBucket.Contains(number)
                && !_weekOldBucket.Contains(number)
                && !_immediateBucket.Contains(number))
            {
                //TODO: something went wrong refresh cached question Ids
                _immediateBucket.Add(number);
                return true;
            }

            if (_monthOldBucket.Contains(number))
            {
                _immediateBucket.Add(number);
                _monthOldBucket.Remove(number);
                return true;
            }

            if (_fornightOldBucket.Contains(number))
            {
                _immediateBucket.Add(number);
                _fornightOldBucket.Remove(number);
                return true;
            }

            if (_weekOldBucket.Contains(number))
            {
                _immediateBucket.Add(number);
                _weekOldBucket.Remove(number);
                return true;
            }

            if (_immediateBucket.Contains(number))
            {
                _immediateBucket.Add(number);
                return false;
            }

            return true;
        }
    }
}
