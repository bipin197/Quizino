using Application.Users;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Persistence;
using WebApis.DataStore;

namespace Apis.Controllers
{
    [ApiController]
    [Route("api/Users")]
    public class UserController
    {
        private readonly ILogger<QuizController> _logger;

        public UserController(ILogger<QuizController> logger)
        {
            _logger = logger;
        }

        [HttpGet("Email/{emailId}")]
        public IUser GetUserByEmail(string emailId)
        {
            return new UserQueries(new TestUserLoader()).GetUser(emailId);
        }

        [HttpGet("User/{key}")]
        public IUser GetUserByKey(int key)
        {
            return new UserQueries(new TestUserLoader()).GetUser(key);
        }

        [HttpPost("User/Store")]
        public void StoreUser(IUser user)
        {
        }
    }
}
