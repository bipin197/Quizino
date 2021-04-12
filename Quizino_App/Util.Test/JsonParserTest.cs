using Domain.Models;
using NUnit.Framework;
using Persistence.Tools;
using System.IO;
using System.Linq;

namespace Util.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestDataStringWithMultipleObjects()
        {
            var data = File.ReadAllText(@"MultiDataSingleString.json");

            var parser = new JsonParser<Question[]>();
            var parsedData =parser.Parse(data);

            Assert.AreEqual(parsedData.Length, 2);
        }

        [Test]
        public void TestAnswerStringWithMultipleObjects()
        {
            var data = File.ReadAllText(@"SingleDataSingleString.json");

            var parser = new JsonParser<Question[]>();
            var parsedData = parser.Parse(data).FirstOrDefault();

            Assert.AreEqual(parsedData.Answer, 3);
        }

        [Test]
        public void TestOptionAWithMultipleObjects()
        {
            var data = File.ReadAllText(@"SingleDataSingleString.json");

            var parser = new JsonParser<Question[]>();
            var parsedData = parser.Parse(data).FirstOrDefault();

            Assert.AreEqual(parsedData.OptionA, "stop");
        }
    }
}