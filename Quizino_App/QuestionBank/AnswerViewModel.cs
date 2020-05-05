using Domain.Interfaces;

namespace QuestionBank
{
    public class AnswerViewModel : EntityBaseViewModel<IAnswer>
    {
        public AnswerViewModel(IAnswer entityBase) : base(entityBase)
        {
        }

        public AnswerOptions CorrectAnswer
        {
            get => EntityBase.CorrectAnswer;
            set => EntityBase.CorrectAnswer = value;
        }
    }
}
