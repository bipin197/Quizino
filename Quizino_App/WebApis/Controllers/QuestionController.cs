using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Persistence.Tools;
using System;
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
        private readonly QuestionDataStore _dataStore;

        public QuestionController(ILogger<QuestionController> logger)
        {
            _logger = logger;
            _dataStore = new QuestionDataStore();
        }

        [HttpGet("{id}")]
        public Question GetQuestion(int id)
        {
            return _dataStore.GetQuestions(id);
        }

        [HttpPost("create")]
        public async Task<HttpResponseMessage> Create()
        {
            var message = new HttpResponseMessage();
            message.StatusCode = System.Net.HttpStatusCode.OK;
            var body = Request.Body;
            try
            {
                using (var sr = new StreamReader(body))
                {
                    string rawContent = await sr.ReadToEndAsync();
                    var objects = new JsonParser<Question[]>().Parse(rawContent);
                    // use raw content here

                    var isSuccessfull = _dataStore.ProcessQuestions(objects).Result;
                    if (isSuccessfull)
                    {
                        await _dataStore.SaveToDb().ConfigureAwait(false);
                    }
                }
            }
            catch (JsonException exception)
            {
                _logger.LogError(exception, "Error occurred while parsing json data");
                message.StatusCode = System.Net.HttpStatusCode.BadRequest;
            }
            catch (Exception exception)
            {
                _logger.LogCritical(exception, "Unknown error occurred while parsing json data");
                message.StatusCode = System.Net.HttpStatusCode.InternalServerError;
            }

            return message;
        }
    }
}
