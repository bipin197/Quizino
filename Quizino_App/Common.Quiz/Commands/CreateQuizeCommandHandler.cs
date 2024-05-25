using Common.Quiz.Repositories;
using QuizModel = Domain.Quiz.Models.Quiz;
namespace Common.Quiz.Commands
{
    public class CreateQuizCommandHandler : ICommandHandler<QuizModel>
    {
        private readonly IRepository<QuizModel> _repository;
        public CreateQuizCommandHandler(IRepository<QuizModel> repository)
        {
            _repository = repository;
        }

        public async Task HandleAsync(IEnumerable<QuizModel> items)
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
