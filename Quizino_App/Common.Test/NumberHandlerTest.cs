using Common.Utilities;
using NUnit.Framework;
using System.Linq;

namespace Common.Test
{ 
    public class NumberHandlerTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void NumberHandlerCountCheckSmalls()
        {
            var numbers = NumberHandler.GenerateRandomKeys(10, 1, 100);
            var count = numbers.Distinct().Count();
            Assert.IsTrue(count == numbers.Count());
        }

        [Test]
        public void NumberHandlerCountCheckMedium()
        {
            var numbers = NumberHandler.GenerateRandomKeys(10, 1, 1000);
            var count = numbers.Distinct().Count();
            Assert.IsTrue(count == numbers.Count());
        }
    }
}