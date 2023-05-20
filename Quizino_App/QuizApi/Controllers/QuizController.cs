using Common.Loaders;
using Common.Queries;
using Common.Repositories;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Persistence.Repositories;
using System.Collections.Generic;

namespace QuizApi.Controllers
{
    [Route("api/[controller]")]
    public class QuizController : Controller
    {
        private readonly IQuizQuery _quizQuery;
        public QuizController(IQuizQuery repository)
        {
            _quizQuery = repository;
        }

        // GET: QuizController/Details/5
        [HttpGet("Active")]
        public IEnumerable<Quiz> Active()
        {
            return _quizQuery.GetAllActiveQuiz();
        }

        [HttpGet("{id}")]
        public Quiz Get(long id)
        {
            return _quizQuery.GetQuiz(id).Result;
        }
    }
}
