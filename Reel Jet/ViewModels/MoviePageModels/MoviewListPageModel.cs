using System.IO;
using Newtonsoft.Json;
using System.Text.Json;
using Reel_Jet.Commands;
using System.Windows.Input;
using System.Windows.Controls;
using Reel_Jet.Views.MoviePages;
using System.Collections.Generic;
using Reel_Jet.Models.MovieNamespace;
using Reel_Jet.Views.NavigationBarPages;
using static Reel_Jet.Services.WebServices.OmdbService;
using System.Collections.ObjectModel;
using Reel_Jet.Services.WebServices;
using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Reel_Jet.ViewModels.MoviePageModels {
    public class MoviewListPageModel : INotifyPropertyChanged {

        // Private Fields

        private Frame MainFrame;
        private MovieCollection _movie; 

        // Binding Properties

        public ICommand? HistoryPgButtonCommand { get; set; }
        public ICommand? WatchListPgButtonCommand { get; set; }
        public MovieCollection Movie {
            get => _movie;
            set { 
                _movie = value;
                OnPropertyChanged();
            }
        }

        // Constructor

        public MoviewListPageModel(Frame frame) { 
            MainFrame = frame;

            HistoryPgButtonCommand = new RelayCommand(HistoryPage);
            WatchListPgButtonCommand = new RelayCommand(WatchListPage);
            TaskToJson();
        }

        // Functions

        private void HistoryPage(object? sender) {
            MainFrame.Content = new HistoryPage(MainFrame);
        }

        private void WatchListPage(object? sender) {
            MainFrame.Content = new WatchListPage(MainFrame);
        }

        private async void TaskToJson() {
            var jsonStr = await OmdbService.GetAllMoviesByTitle("Fight Club");
            Movie = System.Text.Json.JsonSerializer.Deserialize<MovieCollection>(jsonStr);
            MessageBox.Show(Movie.Search.Count.ToString());
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null) {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}