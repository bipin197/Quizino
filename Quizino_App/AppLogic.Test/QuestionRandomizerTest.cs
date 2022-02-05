using NUnit.Framework;
using System.Linq;

namespace AppLogic.Test
{
    public class QuestionRandomizerTest
    {
        [Test]
        public void TestZeroQuestionRandomization()
        {
            var questionIds = QuestionRandomizer.GetRandomQuestionIds(0);
            Assert.AreEqual(questionIds.Count(), 0);
        }
    }
}
