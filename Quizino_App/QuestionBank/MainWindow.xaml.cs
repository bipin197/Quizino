using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuestionBank
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel _viewModel;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = _viewModel = new MainWindowViewModel(); 
        }

        private void AddQuesButtonClick(object sender, RoutedEventArgs e)
        {
            _viewModel.Questions.Add(QuestionViewModelFactory.CreateQuestionViewModel(new Question()));
        }

        private void SaveQuesButtonClick(object sender, RoutedEventArgs e)
        {
            _viewModel.DataStore.SaveQuestions(_viewModel.Questions.Select(x => x.EntityBase).ToList());
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
