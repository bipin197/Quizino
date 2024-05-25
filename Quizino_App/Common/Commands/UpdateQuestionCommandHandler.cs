using Common.Repositories;
using Domain;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Commands
{
    public class UpdateQuestionCommandHandler : ICommandHandler<UpdateQuestionCommand>
    {
        private readonly IRepository<Question> _repository;
        
        public UpdateQuestionCommandHandler(IRepository<Question> repository)
        {
            _repository= repository;
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
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
