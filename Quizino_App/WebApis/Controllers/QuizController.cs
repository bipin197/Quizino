using Application.Quiz;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Persistence;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApis.DataStore;

namespace Apis.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuizController
    {
        private readonly ILogger<QuizController> _logger;
        private readonly QuizDataStore _quizDataStore;

        public QuizController(ILogger<QuizController> logger)
        {
            _logger = logger;
            _quizDataStore = new QuizDataStore();
        }

        [HttpGet("active")]
        public IEnumerable<IQuiz> Get()
        {
            var quizQuery = new QuizQueries(new TestQuizLoader());

            return quizQuery.GetAllActiveQuizForPeriod(DateTime.Now, DateTime.Now.AddHours(1));
        }

        [HttpGet("{key}")]
        public IQuiz GetQuiz(int key)
        {
            var quizQuery = new QuizQueries(new TestQuizLoader());

            return quizQuery.GetQuiz(key);
        }

        [HttpGet("{quizKey}/{questionNumber}")]
        public IQuestion GetQuestion(long quizKey, int questionNumber)
        {
            var quizQuery = new QuizQueries(new TestQuizLoader());

            return quizQuery.GetQuestion(quizKey, questionNumber);
        }

        [HttpGet("Create")]
        public async Task<IQuiz> Create()
        {
            var quiz = await _quizDataStore.CreateQuiz(DateTime.Now);
            
            return quiz;
        }
    }
}
