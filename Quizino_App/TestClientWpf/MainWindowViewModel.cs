using Domain.Interfaces;
using Domain.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace TestClientWpf
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<IQuiz> Quizzes { get; set; }
        public event EventHandler<EventArgs> Loaded;
        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindowViewModel()
        {
            Quizzes = new ObservableCollection<IQuiz>();
        }

        public void Load()
        {
            RunAsync();
        }

        private async void RunAsync()
        {
            // Update port # in the following line.
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://quizino.azurewebsites.net");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {

                HttpResponseMessage response = await client.GetAsync("api/quiz/active");
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var quizes = JsonConvert.DeserializeObject<Quiz[]>(result);
                    foreach(var quiz in quizes)
                    {
                        Quizzes.Add(quiz);
                    }
                }      

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
            if(Loaded != null)
            {
                Loaded.Invoke(this, new EventArgs());
            }

            OnPropertyChanged(nameof(Quizzes));
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
