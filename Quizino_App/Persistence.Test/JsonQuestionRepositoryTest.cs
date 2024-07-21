using Domain.Models;
using NUnit.Framework;
using Persistence.Repositories;
using System;
using System.Linq;

namespace Persistence.Test
{
    public class JsonQuestionRepositoryTest
    {
        [Test]
        public void TestDefaultDataCount()
        {
            var repo = new JsonRepository<Question>();
            var items = repo.GetItems(x => x.Id > 0);
            Assert.Equals(555, items.Count());
        }

        [Test]
        public void TestAllDefaultQuestionHasKey()
        {
            var repo = new JsonRepository<Question>();
            var items = repo.GetItems(x => x.Id <= 0);
            Assert.Equals(0, items.Count());
        }

        [Test]
        public void TestAllDefaultQuestionHasAnswer()
        {
            var repo = new JsonRepository<Question>();
            var items = repo.GetItems(x => x.Answer < 0 || x.Answer > 3);
            Assert.Equals(0, items.Count());
        }

        [Test]
        public void TestAllDefaultQuestionText()
        {
            var repo = new JsonRepository<Question>();
            var items = repo.GetItems(x => string.IsNullOrEmpty(x.Text));
            Assert.Equals(0, items.Count());
        }
    }
}
