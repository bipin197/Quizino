using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuestionApi.Store;
using System.Collections.Generic;

namespace QuestionApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class QuizController : Controller
    {
        private readonly QuizDataStore _quizDataStore;
        public QuizController(QuizDataStore quizDataStore)
        {
            _quizDataStore = quizDataStore;
        }

        // GET: QuizController/Details/5
        [HttpGet("Active")]
        [Authorize("read:quizzes")]
        public IEnumerable<Quiz> Active()
        {
            return _quizDataStore.GetAllActiveQuiz();
        }

        [HttpGet("{id}")]
        [Authorize("read:quizzes")]
        public Quiz Get(long id)
        {
            return _quizDataStore.GetQuiz(id);
        }
    }
}
