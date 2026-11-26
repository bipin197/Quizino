using Common.Repositories;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Commands
{
    public class CreateQuestionCommandHandler : ICommandHandler<Question>
    {
        private readonly ICachedRepository<Question> _repository;

        public CreateQuestionCommandHandler(ICachedRepository<Question> repository)
        {
            _repository = repository;
        }

        public async Task HandleAsync(IEnumerable<Question> items)
        {
            try
            {
                await _repository.AddItemsAsync(items).ConfigureAwait(false);
                _repository.Save();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

        }

        public async Task Handle(Question item)
        {
            await _repository.AddItemAsync(item).ConfigureAwait(false);
            await _repository.SaveAsync().ConfigureAwait(false);
        }
    }
}
