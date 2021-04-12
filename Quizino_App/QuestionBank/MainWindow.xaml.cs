using Domain.Models;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;

namespace QuestionBank
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private MainWindowViewModel _viewModel;

        public event PropertyChangedEventHandler PropertyChanged;
        public bool HasChanges { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = _viewModel = new MainWindowViewModel(); 
        }

        private void AddQuesButtonClick(object sender, RoutedEventArgs e)
        {
            _viewModel.Questions.Add(QuestionViewModelFactory.CreateQuestionViewModel(new Question() { IsNew = true }));
            HasChanges = true;
            NotifyPropertyChanged(nameof(HasChanges));
        }

        private void SaveQuesButtonClick(object sender, RoutedEventArgs e)
        {
            var dataToSave = _viewModel.Questions.Where(x => !x.IsReadOnly || _viewModel.DataLoadingMode == DataLoadingModes.JsonFiles);
            if (!dataToSave.Any())
            {
                MessageBox.Show("Data is up to date", "No Unsaved Data", MessageBoxButton.OK);
                return;
            }

            _viewModel.SaveQuestions(dataToSave.Select(x => x.EntityBase));

            foreach(var data in dataToSave)
            {
                data.IsReadOnly = true;
            }

            HasChanges = false;
            NotifyPropertyChanged(nameof(HasChanges));
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void LoadQuestions(object sender, RoutedEventArgs e)
        {
            _viewModel.LoadData();
        }
    }
}
