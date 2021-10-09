using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuestionApi.Store;

namespace QuestionApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class QuestionController : ControllerBase
    {
        private readonly ILogger<QuestionController> _logger;
        public QuestionController(ILogger<QuestionController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{id}")]
        public Question Get(int id)
        {
            var question = MockQuestionStore.GetItem(id);
            if(question == null)
            {
                _logger.LogError("No Question found with id {0}", id);
                return new Question { Text = @"No Such Question Exist with id " + id };
            }

            return question;
        }
    }
}
