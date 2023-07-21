using System.Windows;
using Reel_Jet.Commands;
using System.Windows.Input;
using System.ComponentModel;
using System.Windows.Controls;
using Reel_Jet.Views.MoviePages;
using System.Collections.Generic;
using Reel_Jet.Services.WebServices;
using Reel_Jet.Models.MovieNamespace;
using System.Runtime.CompilerServices;
using Reel_Jet.Views.NavigationBarPages;
using static Reel_Jet.Services.WebServices.OmdbService;


namespace Reel_Jet.ViewModels.MoviePageModels {
    public class MoviewListPageModel : INotifyPropertyChanged {

        // Private Fields

        private Frame MainFrame;
        private MovieCollection _movie;
        private ShortMovieInfo _movieInfo;

        // Binding Properties

        public ICommand? HistoryPgButtonCommand { get; set; }
        public ICommand? WatchListPgButtonCommand { get; set; }
        public ICommand? SelectionChangedCommand { get; set; }
        public ICommand? SearchCommand { get; set; }
        public MovieCollection Movie {
            get => _movie;
            set { 
                _movie = value;
                OnPropertyChanged();
            }
        }
        public ShortMovieInfo MovieInfo {
            get { return _movieInfo; }
            set { _movieInfo = value; }
        }


        // Constructor

        public MoviewListPageModel(Frame frame) { 
            MainFrame = frame;

            HistoryPgButtonCommand = new RelayCommand(HistoryPage);
            WatchListPgButtonCommand = new RelayCommand(WatchListPage);
            SelectionChangedCommand = new RelayCommand(SelectionChanged);
            SearchCommand = new RelayCommand(Search);

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
        }

        private void SelectionChanged(object? param) {
            MovieInfo = (ShortMovieInfo)param!;
            MainFrame.Content = new MoviePreviewPage(MainFrame, MovieInfo);
        }

        private void Search(object? param) {
            string text = param as string;
            MessageBox.Show(text);
            TaskToJson();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null) {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}