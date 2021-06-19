using Domain.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Persistence.Test
{
    public class QuestionCosmoDbRepositoryTest
    {
        [Test]
        public async Task TestCreateDeleteEntity()
        {
            var cosmoDbContext = new CosmoDBRepository<Question>(GetDbConnectionBundle());
            await cosmoDbContext.EnsureDeletedAsync();
            await cosmoDbContext.EnsureCreatedAsync();
            var question = GetMockQuestion(1);
            await cosmoDbContext.AddItemAsync(question);
            await cosmoDbContext.SaveAsync();

            var foundQuestion = cosmoDbContext.GetItem(x => x.Id == 1);
            await ClearDataAsync(cosmoDbContext, new[] { question });
            Assert.IsNotNull(foundQuestion);
        }

        [Test]
        public async Task TestMultiCreateDeleteEntity()
        {
            var cosmoDbContext = new CosmoDBRepository<Question>(GetDbConnectionBundle());
            await cosmoDbContext.EnsureDeletedAsync();
            await cosmoDbContext.EnsureCreatedAsync();
            var questions = new List<Question>();
            for(var id = 1; id < 11; id++)
            {
                questions.Add(GetMockQuestion(id));
            }

            await cosmoDbContext.AddItemsAsync(questions);
            await cosmoDbContext.SaveAsync();


            var foundQuestion = cosmoDbContext.GetItem(x => x.Id == new Random().Next(1, 10));
            await ClearDataAsync(cosmoDbContext, questions);
            Assert.IsNotNull(foundQuestion);
        }

        [Test]
        public async Task TestMultiUpdateEntity()
        {
            var cosmoDbContext = new CosmoDBRepository<Question>(GetDbConnectionBundle());
            await cosmoDbContext.EnsureDeletedAsync();
            await cosmoDbContext.EnsureCreatedAsync();

            var questions = new List<Question>();
            for (var id = 1; id < 11; id++)
            {
                questions.Add(GetMockQuestion(id));
            }

            await cosmoDbContext.AddItemsAsync(questions);
            await cosmoDbContext.SaveAsync();

            foreach (var question in questions)
            {
                question.Answer = (int)AnswerOptions.C;
            }
            
            cosmoDbContext.UpdateItems(questions);
            await cosmoDbContext.SaveAsync();

            var foundQuestion = cosmoDbContext.GetItem(x => x.Id == new Random().Next(1, 10));
            await ClearDataAsync(cosmoDbContext, questions);
            Assert.AreEqual(foundQuestion.Answer, 2);
        }

        [Test]
        public async Task TestUpdateEntity()
        {
            var cosmoDbContext = new CosmoDBRepository<Question>(GetDbConnectionBundle());
            await cosmoDbContext.EnsureDeletedAsync();
            await cosmoDbContext.EnsureCreatedAsync();
            var question = GetMockQuestion(1);
            await cosmoDbContext.AddItemAsync(question);
            await cosmoDbContext.SaveAsync();

            question.Answer = (int)AnswerOptions.B;
            cosmoDbContext.UpdateItem(question);
            await cosmoDbContext.SaveAsync();

            var foundQuestion = cosmoDbContext.GetItem(x => x.Id == 1);
            await ClearDataAsync(cosmoDbContext, new[] { question });
            Assert.AreEqual(foundQuestion.Answer, 1);
        }

        private async Task ClearDataAsync(CosmoDBRepository<Question> cosmoDbRepository, IEnumerable<Question> questions)
        {
            foreach (var question in questions)
            {
                cosmoDbRepository.RemoveItem(question);
            }

            await cosmoDbRepository.SaveAsync();
        }

        private Question GetMockQuestion(int id)
        {
            return new Question()
            {
                Id = id,
                Text = "This is test question with Id " + id,
                Answer = (int)AnswerOptions.A,
                OptionA = "Option A",
                OptionB = "Option B",
                OptionC = "Optionn C",
                OptionD = "Option D",
                ApplicableCategories = "0,2,3"
            };
        }

        private DbConnectionBundle GetDbConnectionBundle()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var dbConnectionBundle = new DbConnectionBundle
            {
                EndpointConnection = new EndpointConnection
                {
                    Endpoint = config["EndPoint"],
                    Key = config["Key"]
                },
                DatabaseId = config["DatabaseId"],
                CollectionId = config["MyCollection"]
            };

            return dbConnectionBundle;

        }
    }
}
