using Domain.Interfaces;
using Domain.Models;
using QuestionBank.DataStore;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace QuestionBank
{
    public class MainWindowViewModel
    {
        public ObservableCollection<QuestionViewModel> Questions { get; }
        public QuestionDataStore DataStore { get; }

        public MainWindowViewModel()
        {
            Questions = new ObservableCollection<QuestionViewModel>();
            DataStore = new QuestionDataStore();
            LoadData();
        }

        private void LoadData()
        {
            var questions = DataStore.LoadQuestions();
            foreach (var question in questions)
            {
                var viewModel = QuestionViewModelFactory.CreateQuestionViewModel(question);
                viewModel.IsReadOnly = true;
                Questions.Add(viewModel);
            }

            if(questions.Any())
            {
                var maxKey = questions.Max(x => x.Key);
                QuestionViewModelFactory.SetHighestKey(maxKey);
            }
        }

        public List<Categories> Categories
        {
            get => new List<Categories>() 
            { 
                Domain.Interfaces.Categories.GeneralAwareness,
                Domain.Interfaces.Categories.Politics,
                Domain.Interfaces.Categories.Entertainment,
                Domain.Interfaces.Categories.History,
                Domain.Interfaces.Categories.Sports,
                Domain.Interfaces.Categories.Science,
                Domain.Interfaces.Categories.Research,
                Domain.Interfaces.Categories.Technology,
                Domain.Interfaces.Categories.Geography
            };
        }

        public List<Answer> Options
        {
            get => new List<Answer>()
            {
                new Answer{ CorrectAnswer = AnswerOptions.A },
                new Answer{ CorrectAnswer = AnswerOptions.B },
                new Answer{ CorrectAnswer = AnswerOptions.C },
                new Answer{ CorrectAnswer = AnswerOptions.D }
            };
        }
    }
}
