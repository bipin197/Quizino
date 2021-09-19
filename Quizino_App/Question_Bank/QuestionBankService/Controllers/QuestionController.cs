using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using QuestionBankService.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QuestionBankService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        // GET: api/<QuestionController>
        [HttpGet]
        public IEnumerable<Question> Get()
        {
            // First 10 question
            return MockQuestionDataStore.GetItems(x => x.Id < 10);
        }

        // GET api/<QuestionController>/5
        [HttpGet("{id}")]
        public Question Get(int id)
        {
            return MockQuestionDataStore.GetItem(id);
        }

        // POST api/<QuestionController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<QuestionController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<QuestionController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
