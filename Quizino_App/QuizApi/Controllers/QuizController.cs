using Common.Quiz.Commands;
using Common.Quiz.Queries;
using Domain.Quiz.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace QuizApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class QuizController : Controller
    {
        private readonly IQuizQuery _quizQuery;
        public QuizController(IQuizQuery repository)
        {
            _quizQuery = repository;
        }

        // GET: QuizController/Details/5
        [HttpGet("Active")]
        [Authorize("read:quizes")]
        public IEnumerable<Quiz> GetActive()
        {
            return _quizQuery.GetAllActiveQuiz();
        }

        [HttpGet("{id}")]
        [Authorize("read:quizes")]
        public Quiz Get(long id)
        {
            return _quizQuery.GetQuiz(id).Result;
        }

        [HttpPost("Create")]
        [Authorize("write:quizes")]
        public bool CreateQuizes([FromBody] CreateQuizCommand createQuizCommand)
        {
            return true;
        }
    }
}
