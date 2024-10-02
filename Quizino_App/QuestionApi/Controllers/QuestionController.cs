using Common.Commands;
using Common.Utilities;
using Domain.ReadModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuestionApi.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace QuestionApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class QuestionController : ControllerBase
    {
        private readonly ILogger<QuestionController> _logger;
        private readonly QuestionDataStore _dataStore;
        public QuestionController(ILogger<QuestionController> logger, QuestionDataStore questionDataStore)
        {
            _logger = logger;
            _dataStore = questionDataStore;
        }

        [HttpGet("{id}")]
        [Authorize("read:questions")]
        public QuestionReadModel Get(int id)
        {
            var question = _dataStore.GetQuestion(id);
            if(question == null)
            {
                _logger.LogError("No Question found with id {0}", id);
                return new QuestionReadModel { Text = @"No Such Question Exist with id " + id };
            }

            return question;
        }

        [HttpPost("Search")]
        [Authorize("read:questions")]
        public QuestionSearchResult GetQuestion([FromBody] ReadOnlyCriteria criteria)
        {
            if(criteria == null)
            {
                _logger.LogError("Empty or invalid criteria");
                return new QuestionSearchResult { Questions = Enumerable.Empty<QuestionReadModel>(), TotalQuestions = 0 };
            }
            var questions = _dataStore.GetQuestion(criteria);
            if (questions == null || !questions.Any())
            {
                _logger.LogError("No Question found with provided criteria {0}", criteria);
                return new QuestionSearchResult { Questions = Enumerable.Empty<QuestionReadModel>(), TotalQuestions = 0 };
            }

            return new QuestionSearchResult { Questions = questions, TotalQuestions = questions.Count() }; ;
        }

        [HttpGet("hash/{hash}")]
        [Authorize("read:questions")]
        public QuestionReadModel GetFromHashCode(string hash)
        {
            var question = _dataStore.GetQuestionFromHash(hash);
            if (question == null)
            {
                _logger.LogError("No Question found with hash {0}", hash);
                return new QuestionReadModel { Text = @"No Such Question Exist with id " + hash };
            }

            return question;
        }

        [HttpPost("Update")]
        [Authorize("write:questions")]
        public HttpResponseMessage UpdateQuestion([FromBody] UpdateQuestionCommand[] updateQuestionCommands)
        {
            if(updateQuestionCommands == null)
            {
                _logger.LogError("Empty or invalid criteria");
                return new HttpResponseMessage(HttpStatusCode.BadRequest) { ReasonPhrase = "Invalid Data"};
            }
            try
            {
                _dataStore.UpdateQuestions(updateQuestionCommands).ConfigureAwait(false);
            }
            catch(Exception e)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest) { ReasonPhrase = "Error occurred while procession questions: "+ e.Message };
            }


            return new HttpResponseMessage(HttpStatusCode.OK) { ReasonPhrase = "Questions Updated successfully" };
        }

        /// <summary>
        /// Only for testing
        /// </summary>
        /// <param name="numberOfItems"></param>
        /// <returns></returns>
        [HttpGet("FirstFive")]
        [Authorize("read:questions")]
        public IEnumerable<QuestionReadModel> GetFirstFiveItems()
        {
            var question = _dataStore.GetQuestions(new long[] { 1, 2, 3, 4, 5 });
            if (question == null)
            {
                _logger.LogError("No Question found");
                return new List<QuestionReadModel>();
            }

            return question;
        }

        /// <summary>
        /// Get random given random active questions
        /// </summary>
        /// <param name="numberOfQuestions"></param>
        /// <returns></returns>
        [HttpGet("ActiveRandom")]
        [Authorize("read:questions")]
        public IEnumerable<long> GetActiveRandomQuestionKeys(int numberOfQuestions)
        {
            var keys = _dataStore.GetRandomActiveQuestionKeys(numberOfQuestions);
            if (keys == null || !keys.Any())
            {
                _logger.LogError("No Question found");
                return new List<long>();
            }

            return keys;
        }

        /// <summary>
        /// Get random given random active questions
        /// </summary>
        /// <param name="numberOfQuestions"></param>
        /// <returns></returns>
        [HttpGet("ActiveRandomHashes")]
        [Authorize("read:questions")]
        public IEnumerable<string> GetActiveRandomQuestionHashes(int numberOfQuestions)
        {
            var hashes = _dataStore.GetRandomActiveQuestionHashes(numberOfQuestions);
            if (hashes == null || !hashes.Any())
            {
                _logger.LogError("No Question found");
                return new List<string>();
            }

            return hashes;
        }

        /// <summary>
        /// Create Default data if doesn't exist only for testing
        /// </summary>
        /// <param name="numberOfItems"></param>
        /// <returns></returns>
        //[HttpPost("CreateDefaultData")]
        //[Authorize("write:questions")]
        //public async Task CreateDefaultData()
        //{
        //    var jsonRepo = new JsonRepository<Question>();
        //    var updateQuestionsCommand = new List<UpdateQuestionCommand>();
        //    foreach (var question in jsonRepo.GetAllItems())
        //    {
        //        var updateQuestionCommand = new UpdateQuestionCommand
        //        {
        //            Id = 0,
        //            Text = question.Text,
        //            OptionA = question.OptionA,
        //            OptionB = question.OptionB,
        //            OptionC = question.OptionC,
        //            OptionD = question.OptionD,
        //            Answer = question.Answer,
        //            ApplicableCategories = question.ApplicableCategories,
        //            IsNew = question.IsNew
        //        };

        //        updateQuestionsCommand.Add(updateQuestionCommand);
        //    }

        //    await _dataStore.UpdateQuestions(updateQuestionsCommand).ConfigureAwait(false);
        //}
    }
}
