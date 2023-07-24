using System.Windows;
using System.Text.Json;
using Reel_Jet.Commands;
using System.Windows.Input;
using System.ComponentModel;
using System.Windows.Controls;
using Reel_Jet.Views.MoviePages;
using System.Collections.Generic;
using Reel_Jet.Services.WebServices;
using System.Collections.ObjectModel;
using Reel_Jet.Models.MovieNamespace;
using System.Runtime.CompilerServices;
using Reel_Jet.Models.DatabaseNamespace;
using Reel_Jet.Views.NavigationBarPages;
using static Reel_Jet.Services.WebServices.OmdbService;

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
        public ICommand? AddToWatchListCommand { get; set; }
        public MovieCollection Movie {
            get => _movie;
            set {
                _movie = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Movie> Movies { get; set; } = new();

        // Constructor

        public MoviewListPageModel(Frame frame) { 
            MainFrame = frame;

            HistoryPgButtonCommand = new RelayCommand(HistoryPage);
            WatchListPgButtonCommand = new RelayCommand(WatchListPage);
            SelectionChangedCommand = new RelayCommand(SelectionChanged);
            SearchCommand = new RelayCommand(Search);
            AddToWatchListCommand = new RelayCommand(AddToWatchList);
        }

        // Functions

        private void HistoryPage(object? sender) {
            MainFrame.Content = new HistoryPage(MainFrame);
        }

        private void WatchListPage(object? sender) {
            MainFrame.Content = new WatchListPage(MainFrame);
        }

        private void AddToWatchList(object? sender) {
            Movie movie = (sender as Movie)!;

            foreach (var item in Database.CurrentUser.MyWatchList) {
                if (item.imdbID == movie!.imdbID) {
                    MessageBox.Show("This Movie is already on your WatchList", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
            }
            Database.CurrentUser.MyWatchList.Add(movie);
            JsonHandling.WriteData(Database.Users, "users");
        }


        private async void TaskToJson(string title) {

            var jsonStr = await OmdbService.GetAllMoviesByTitle(title);
            Movie = JsonSerializer.Deserialize<MovieCollection>(jsonStr)!;

            if (Movie.Search is not null) {
                Movies.Clear();

                foreach (var result in Movie.Search) {

                    var movieCollectionJson = await OmdbService.GetConcreteMovieById(result.imdbID);
                    var movieFromCollection = JsonSerializer.Deserialize<Movie>(movieCollectionJson);

                    if (movieFromCollection.Poster == "N/A")
                        movieFromCollection.Poster = "\\Static Files\\Images\\no-poster.png";

                    if (movieFromCollection.Runtime == "N/A")
                        movieFromCollection.Runtime = "Unknown";

                    if (movieFromCollection.Rated == "N/A")
                        movieFromCollection.Rated = "Unknown";

                    if (movieFromCollection.Director == "N/A")
                        movieFromCollection.Director = "Unknown";



                    if (movieFromCollection is not null)
                        Movies.Add(movieFromCollection);
                }
                return;
            }
        }

        private void SelectionChanged(object? param) {
            Movie movie = (param as Movie)!;
            MainFrame.Content = new MoviePreviewPage(MainFrame, movie);
        }

        private void Search(object? param) {
            string text = param as string;
            TaskToJson(text);
        }


        // INotifyPropertyChanged

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null) {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}