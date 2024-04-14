using Common.Repositories;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Commands
{
    public class CreateQuizCommandHandler : ICommandHandler<Quiz>
    {
        private readonly IRepository<Quiz> _repository;
        public CreateQuizCommandHandler(IRepository<Quiz> repository)
        {
            _repository = repository;
        }

        public async Task HandleAsync(IEnumerable<Quiz> items)
        {
            try
            {
                await _repository.AddItemsAsync(items).ConfigureAwait(false);
                _repository.Save();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
        }
    }
}
