using Application.Quiz;
using Common.Loaders;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Persistence.CosmoDb;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApis.DataStore;

namespace Apis.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuizController : Controller
    {
        private readonly ILogger<QuizController> _logger;
        private readonly QuizDataStore _quizDataStore;
        private readonly IQuizLoader _quizLoader;

        public QuizController(ILogger<QuizController> logger)
        {
            _logger = logger;
            _quizDataStore = new QuizDataStore();
            _quizLoader = new CosmoDbQuizLoader();
        }

        [HttpGet("active")]
        public IEnumerable<IQuiz> Get()
        {
            var quizQuery = new QuizQueries(_quizLoader);

            return quizQuery.GetAllActiveQuizForPeriod(DateTime.Now, DateTime.Now.AddHours(1));
        }

        [HttpGet("{key}")]
        public IQuiz GetQuiz(int key)
        {
            var quizQuery = new QuizQueries(_quizLoader);

            return quizQuery.GetQuiz(key).Result;
        }

        [HttpGet("question/{quizKey}/{questionNumber}")]
        public IQuestion GetQuestion(long quizKey, int questionNumber)
        {
            var quizQuery = new QuizQueries(_quizLoader);

            return quizQuery.GetQuestion(quizKey, questionNumber);
        }

        [HttpPost("create")]
        public async Task<IQuiz> Create(int category)
        {
            var quiz = await _quizDataStore.CreateQuiz(DateTime.Now, category);
            
            return quiz;
        }
    }
}
