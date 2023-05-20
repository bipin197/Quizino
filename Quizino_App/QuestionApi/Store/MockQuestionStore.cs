using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuestionApi.Store
{
    public static class MockQuestionStore
    {
        private readonly static IList<Question> _questions;
        static MockQuestionStore()
        {
            _questions = new List<Question>();
            Initialize();
        }

        private static void Initialize()
        {
            for (int i = 1; i < 1000; i++)
            {
                var question = new Question
                {
                    Id = i,
                    Answer = i % 4 + 1,
                    OptionA = "Option A",
                    OptionB = "Option B",
                    OptionC = "Option C",
                    OptionD = "Option D",
                    ApplicableCategories = "1,2,3,4"
                };

                _questions.Add(question);
            }
        }

        public static IEnumerable<Question> GetItems(Func<Question, bool> predicate) => _questions.Where(predicate);
        public static Question GetItem(int id) => _questions.FirstOrDefault(x => x.Id == id);
    }
}
