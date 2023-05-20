using Common.Loaders;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace QuestionApi.Controllers
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
        [Authorize("read:quizzes")]
        public IEnumerable<Quiz> Active()
        {
            return _quizQuery.GetAllActiveQuiz();
        }

        [HttpGet("{id}")]
        [Authorize("read:quizzes")]
        public Quiz Get(long id)
        {
            return _quizQuery.GetQuiz(id).Result;
        }
    }
}
