using Domain.Interfaces;

namespace QuestionBank
{
    public static class QuestionViewModelFactory
    {
        private static int _id = 1;
        public static QuestionViewModel CreateQuestionViewModel(IQuestion question)
        {
            if(question.Key == 0)
            {
                question.Key = _id++;
            }

            return new QuestionViewModel(question);
        }
    }
}
