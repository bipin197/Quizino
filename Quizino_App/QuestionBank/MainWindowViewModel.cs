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
        private readonly IDictionary<DataLoadingModes, IDataStore> _dataStores;
        public DataLoadingModes DataLoadingMode { get; set; }

        public MainWindowViewModel()
        {
            Questions = new ObservableCollection<QuestionViewModel>();
            _dataStores = new Dictionary<DataLoadingModes, IDataStore>
            {
                { DataLoadingModes.CloudDataBase, new CosmoDBQuestionDataStore()},
                { DataLoadingModes.JsonFiles, new JsonQuestionDataStore()}
            };
        }

        internal void LoadData()
        {
            var questions = new List<IQuestion>();
            var dataStore = _dataStores[DataLoadingMode];
            foreach (var question in dataStore.LoadQuestions())
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

        internal void SaveQuestions(IEnumerable<IQuestion> questions)
        {
            var dataStore = _dataStores[DataLoadingMode];
            dataStore.SaveQuestions(questions.Cast<Question>());
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

        public List<DataLoadingModes> AvailableDataLoadingModes => new List<DataLoadingModes>()
            {
                DataLoadingModes.None,
                DataLoadingModes.JsonFiles,
                DataLoadingModes.CloudDataBase
            };
    }
}
