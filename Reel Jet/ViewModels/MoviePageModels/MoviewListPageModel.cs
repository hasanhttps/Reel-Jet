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
using System.Collections.ObjectModel;

namespace Reel_Jet.ViewModels.MoviePageModels {
    public class MoviewListPageModel : INotifyPropertyChanged {

        // Private Fields

        private Frame MainFrame;
        private MovieCollection _movie;

        // Binding Properties

        public ICommand? HistoryPgButtonCommand { get; set; }
        public ICommand? WatchListPgButtonCommand { get; set; }
        public ICommand? SelectionChangedCommand { get; set; }
        public ICommand? SearchCommand { get; set; }
        public ObservableCollection<Movie> Movies { get; set; }
        public MovieCollection MovieCollection {
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
            SelectionChangedCommand = new RelayCommand(SelectionChanged);
            SearchCommand = new RelayCommand(Search);

            TaskToJson();
            ChangeShortMovieFul();
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
            MovieCollection = System.Text.Json.JsonSerializer.Deserialize<MovieCollection>(jsonStr);
        }

        private void SelectionChanged(object? param) {
            Movie movie = (Movie)param!;
            MainFrame.Content = new MoviePreviewPage(MainFrame, movie);
        }

        private void Search(object? param) {
            string text = param as string;
            MessageBox.Show(text);
            TaskToJson();
            ChangeShortMovieFul();
        }

        private async void ChangeShortMovieFul() {
            foreach(var movie in MovieCollection.Search) {
                var jsonStr = await OmdbService.GetConcreteMovieById(movie.imdbID);
                Movies.Add(System.Text.Json.JsonSerializer.Deserialize<Movie>(jsonStr)!);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null) {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}