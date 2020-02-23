using Application.Quiz;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Persistence;
using System;
using System.Collections.Generic;

namespace Apis.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuizController
    {
        private readonly ILogger<QuizController> _logger;

        public QuizController(ILogger<QuizController> logger)
        {
            _logger = logger;
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
    }
}
