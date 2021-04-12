using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Persistence.Tools;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using WebApis.DataStore;

namespace WebApis.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuestionController : Controller
    {
        private readonly ILogger<QuestionController> _logger;

        public QuestionController(ILogger<QuestionController> logger)
        {
            _logger = logger;
        }

        [HttpPost("create")]
        public async Task<HttpResponseMessage> Create()
        {
            var message = new HttpResponseMessage();
            message.StatusCode = System.Net.HttpStatusCode.OK;
            var body = Request.Body;
            using (var sr = new StreamReader(body))
            {
                string rawContent = await sr.ReadToEndAsync();
                var objects = new JsonParser<Question[]>().Parse(rawContent);
                // use raw content here

                var dataStore = new QuestionDataStore();
                var isSuccessfull = dataStore.ProcessQuestions(objects).Result;
                if(isSuccessfull)
                {
                    await dataStore.SaveToDb().ConfigureAwait(false);
                }
            }
            return message;
        }
    }
}
