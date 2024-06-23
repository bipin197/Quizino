using Common.Quiz.Repositories;
using QuizModel = Domain.Quiz.Models.Quiz;
namespace Common.Quiz.Commands
{
    public class CreateQuizCommandHandler : ICommandHandler<CreateQuizCommand>
    {
        private readonly IRepository<QuizModel> _repository;
        public CreateQuizCommandHandler(IRepository<QuizModel> repository)
        {
            _repository = repository;
        }

        public async Task HandleAsync(IEnumerable<CreateQuizCommand> items)
        {
            try
            {
                var quizes = new List<QuizModel>();
                foreach (var item in items)
                {
                    
                }

                await _repository.AddItemsAsync(quizes).ConfigureAwait(false);
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
