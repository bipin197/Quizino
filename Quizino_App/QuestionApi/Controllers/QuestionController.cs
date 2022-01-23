using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Persistence.Tools;
using QuestionApi.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace QuestionApi.Controllers
{
    [ApiController]
    //[Authorize]
    [Route("api/[controller]")]
    public class QuestionController : ControllerBase
    {
        private readonly ILogger<QuestionController> _logger;
        private readonly QuestionDataStore _dataStore;
        public QuestionController(ILogger<QuestionController> logger)
        {
            _logger = logger;
            _dataStore = new QuestionDataStore();
        }

        [HttpGet("{id}")]
        public Question Get(int id)
        {
            var question = _dataStore.GetQuestion(id);
            if(question == null)
            {
                _logger.LogError("No Question found with id {0}", id);
                return new Question { Text = @"No Such Question Exist with id " + id };
            }

            return question;
        }

        /// <summary>
        /// Only for testing
        /// </summary>
        /// <param name="numberOfItems"></param>
        /// <returns></returns>
        [HttpGet("FirstFive")]
        public IEnumerable<Question> GetFirstFiveItems()
        {
            var question = _dataStore.GetQuestions(new long[] { 1, 2, 3, 4, 5 });
            if (question == null)
            {
                _logger.LogError("No Question found");
                return new List<Question>();
            }

            return question;
        }

        [HttpPost("Create")]
        public async Task<HttpResponseMessage> Create()
        {
            var message = new HttpResponseMessage();
            message.StatusCode = System.Net.HttpStatusCode.OK;
            var body = Request.Body;
            try
            {
                using (var sr = new StreamReader(body))
                {
                    var rawContent = await sr.ReadToEndAsync();
                    if(string.IsNullOrEmpty(rawContent))
                    {
                        _logger.LogWarning("Empty payload");
                        message.StatusCode = System.Net.HttpStatusCode.NoContent;
                        return message;
                    }
                    var objects = new JsonParser<Question[]>().Parse(rawContent);
                    // use raw content here

                    await _dataStore.ProcessQuestions(objects);
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

        [HttpPut("Update")]
        public HttpResponseMessage Update([FromBody] Question[] questions)
        {
            var message = "Unknow Error occurred";
            var responseMessage = new HttpResponseMessage
            {
                ReasonPhrase = message,
                StatusCode = System.Net.HttpStatusCode.NoContent
            };
            if (!questions.Any())
            {
                return responseMessage;
            }

            return responseMessage;
        }
    }
}
