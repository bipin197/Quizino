using Common.Repositories;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Common.Commands
{
    public class UpdateQuestionCommand : ICommand<Question>
    {
        private readonly IRepository<Question> _repository;
        
        public UpdateQuestionCommand(IRepository<Question> repository)
        {
            _repository= repository;
        }

        public async Task Handle(IEnumerable<Question> items)
        {
            try
            {
                await _repository.UpdateItemsAsync(items).ConfigureAwait(false);
                _repository.Save();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
