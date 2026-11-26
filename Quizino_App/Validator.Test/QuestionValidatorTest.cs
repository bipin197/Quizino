using Domain.Interfaces;
using Domain.Models;
using Domain.Validators;
using NUnit.Framework;
using System.Linq;

namespace Validator.Test
{
    public class QuestionValidatorTest
    {
        private IQuestion _mockQuestion;
        [SetUp]
        public void Setup()
        {
            _mockQuestion = new Question()
            {
                Id = 1,
                Text = "This is test question with Id " + 1,
                Answer = (int)AnswerOptions.A,
                OptionA = "Option A",
                OptionB = "Option B",
                OptionC = "Optionn C",
                OptionD = "Option D",
                ApplicableCategories = "0,2,3"
            };
        }

        [Test]
        public void TestEmptyId()
        {
            _mockQuestion.Id = 0;
            var questionValidator = new QuestionValidator();
            var validationResult = questionValidator.Validate(_mockQuestion);
            //Reset to 1;
            _mockQuestion.Id = 1;

            Assert.IsTrue(validationResult.Errors.Count == 1 && validationResult.Errors.Any(x => x.PropertyName == nameof(IQuestion.Id)));
        }

        [Test]
        public void TestEmptyText()
        {
            var originalQuestion = _mockQuestion.Text;
            _mockQuestion.Text = string.Empty;
            var questionValidator = new QuestionValidator();
            var validationResult = questionValidator.Validate(_mockQuestion);
            //Reset to original;
            _mockQuestion.Text = originalQuestion;

            Assert.IsTrue(validationResult.Errors.Count == 1 && validationResult.Errors.Any(x => x.PropertyName == nameof(IQuestion.Text)));
        }
    }
}