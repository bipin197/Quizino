using Common.Utilities;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuestionApi.Store;
using System.Collections.Generic;
using System.Linq;

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
        public Question Get(int id)
        {
            var question = _dataStore.GetQuestion(id);
            if(question == null)
            {
                _logger.LogError("No Question found with id {0}", id);
                return new Question { Text = @"No Such Question Exist with id " + id };
            }

            return question;
        }

        [HttpPost("Search")]
        //[Authorize("read:questions")]
        [AllowAnonymous]
        public IEnumerable<Question> GetQuestion([FromBody] Criteria criteria)
        {
            if(criteria == null)
            {
                _logger.LogError("Empty or invalid criteria");
                return new List<Question>();
            }
            var questions = _dataStore.GetQuestion(criteria);
            if (questions == null || !questions.Any())
            {
                _logger.LogError("No Question found with provided criteria {0}", criteria);
                return new List<Question>();
            }

            return questions;
        }

        [HttpPost("Update")]
        //[Authorize("read:questions")]
        [AllowAnonymous]
        public IEnumerable<Question> UpdateQuestion([FromBody] Criteria criteria)
        {
            if (criteria == null)
            {
                _logger.LogError("Empty or invalid criteria");
                return new List<Question>();
            }
            var questions = _dataStore.GetQuestion(criteria);
            if (questions == null || !questions.Any())
            {
                _logger.LogError("No Question found with provided criteria {0}", criteria);
                return new List<Question>();
            }

            return questions;
        }

        /// <summary>
        /// Only for testing
        /// </summary>
        /// <param name="numberOfItems"></param>
        /// <returns></returns>
        [HttpGet("FirstFive")]
        [Authorize("read:questions")]
        public IEnumerable<Question> GetFirstFiveItems()
        {
            var question = _dataStore.GetQuestions(new long[] { 1, 2, 3, 4, 5 });
            if (question == null)
            {
                _logger.LogError("No Question found");
                return new List<Question>();
            }

            return question;
        }

        ///// <summary>
        ///// Create Default data if doesn't exist only for testing
        ///// </summary>
        ///// <param name="numberOfItems"></param>
        ///// <returns></returns>
        //[HttpPost("CreateDefaultData")]
        //[AllowAnonymous]
        //public void CreateDefaultData()
        //{
        //    var jsonRepo = new JsonRepository<Question>();
        //    var command = HttpContext.RequestServices.GetService<CreateQuestionCommand>();
        //    _dataStore.AddQuestions(jsonRepo.GetAllItems(), command);
        //}
    }
}
