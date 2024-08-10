using Common.Repositories;
using Common.Services;
using Domain;
using Domain.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Commands
{
    public class UpdateQuestionCommandHandler : ICommandHandler<UpdateQuestionCommand>
    {
        private readonly IRepository<Question> _repository;
        private readonly IPublishService _publishService;
        
        public UpdateQuestionCommandHandler(IRepository<Question> repository, IPublishService publishService)
        {
            _repository= repository;
            _publishService = publishService;
        }

        public async Task HandleAsync(IEnumerable<UpdateQuestionCommand> items)
        {
            try
            {
                var questions = new List<Question>();
                foreach ( var item in items)
                {
                    var question = new Question()
                    {
                        Id = item.Id,
                        QuestionId = item.Id,
                        IsNew= item.IsNew
                    };
                    if(item != null)
                    {
                        question.Text = item.Text;
                        question.Answer = item.Answer;
                        question.OptionA = item.OptionA;
                        question.OptionB = item.OptionB;
                        question.OptionC= item.OptionC;
                        question.OptionD= item.OptionD;
                        question.ApplicableCategories = item.ApplicableCategories?? "0";
                        question.HashCode = HashGenerator.GetHashForText(item.Text);
                        questions.Add(question);
                    }
                }

                await _repository.UpdateItemsAsync(questions).ConfigureAwait(false);
                _publishService.PublishMessage(new RabbitMqMessage { Body = JsonConvert.SerializeObject(questions) });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
