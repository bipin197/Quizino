using Domain.Interfaces;
using System;
using System.Collections.ObjectModel;

namespace QuestionBank
{
    public class QuizViewModel : EntityBaseViewModel<IQuiz>
    {
        public QuizViewModel(IQuiz entityBase) : base(entityBase)
        {
            Questions = new ObservableCollection<QuestionViewModel>();
        }

        public string Id
        {
            get => EntityBase.Name;
            set => EntityBase.Name = value;
        }

        public bool IsActive
        {
            get => EntityBase.IsActive;
            set => EntityBase.IsActive = value;
        }

        public DateTime CreationTime
        {
            get => EntityBase.CreationTime;
            set => EntityBase.CreationTime = value;
        }

        public DateTime DeactivationTime
        {
            get => EntityBase.DeactivationTime;
            set => EntityBase.DeactivationTime = value;
        }

        public Categories Category
        {
            get => EntityBase.Category;
            set => EntityBase.Category = value;
        }

        public ObservableCollection<QuestionViewModel> Questions { get; }
    }
}
