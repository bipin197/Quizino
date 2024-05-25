using Domain.Quiz.Interfaces;

namespace Persistent.Quiz
{
    public class TestQuizRepository
    {
        private static TestQuizRepository _instance;
        private static IList<IQuiz> _repository;
        public static TestQuizRepository GetInstance()
        {
            if (_instance == null)
            {
                _instance = new TestQuizRepository();
                _instance.Initialize();
            }

            return _instance;
        }

        public IQuiz GetQuiz(int id)
        {
            return _repository.FirstOrDefault(z => z.Id == id);
        }

        public IList<IQuiz> GetAllActiveQuiz()
        {
            return _repository.Where(z => z.IsActive).ToList();
        }

        public IList<IQuiz> GetAllActiveQuizForAPeriod(DateTime start, DateTime end)
        {
            return _repository.Where(z => z.IsActive && z.CreationTime >= start && z.DeactivationTime <= end).ToList();
        }

        private void Initialize()
        {
            _repository = new List<IQuiz>();
        }
    }
}
