﻿using RestSharp;
using System.Windows;
using System.Text.Json;
using Reel_Jet.Commands;
using System.Windows.Input;
using System.ComponentModel;
using System.Windows.Controls;
using Reel_Jet.Views.MoviePages;
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

        public ICommand? WatchListPgButtonCommand { get; set; }
        public ICommand? SelectionChangedCommand { get; set; }
        public ICommand? SettingsPgButtonCommand { get; set; }
        public ICommand? HistoryPgButtonCommand { get; set; }
        public ICommand? ProfilePgButtonCommand { get; set; }
        public ICommand? SearchCommand { get; set; }
        public ICommand? AddToWatchListCommand { get; set; }
        public ICommand? WatchTrailerFromListCommand { get; set; }
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

            WatchTrailerFromListCommand = new RelayCommand(SelectionChanged);
            SelectionChangedCommand = new RelayCommand(SelectionChanged);
            WatchListPgButtonCommand = new RelayCommand(WatchListPage);
            AddToWatchListCommand = new RelayCommand(AddToWatchList);
            SettingsPgButtonCommand = new RelayCommand(SettingsPage);
            HistoryPgButtonCommand = new RelayCommand(HistoryPage);
            ProfilePgButtonCommand = new RelayCommand(ProfilePage);
            SearchCommand = new RelayCommand(Search);

            // Popular Movies

            Popular();

        }

        // Functions

        private void HistoryPage(object? sender) {
            MainFrame.Content = new HistoryPage(MainFrame);
        }
        private void WatchListPage(object? sender) {
            MainFrame.Content = new WatchListPage(MainFrame);
        }
        private void ProfilePage(object? sender) {
            MainFrame.Content = new UserAccountPage(MainFrame);
        }
        private void SettingsPage(object? sender) {
            MainFrame.Content = new SettingsPage(MainFrame);
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

        private async void Popular() {
            var options = new RestClientOptions("https://api.themoviedb.org/3/movie/popular?language=en-US&page=1");
            var client = new RestClient(options);
            var request = new RestRequest("");
            request.AddHeader("accept", "application/json");
            request.AddHeader("Authorization", "Bearer eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiIwN2I3OWYxNmU2NWFmMGY1YTBjNGY4ZGFkZDdkMDhjNCIsInN1YiI6IjY0YjA0MzFjMjBlY2FmMDBjNmY2MWQ1ZSIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.VErhjbegJJ2tyZVP-GiDRN_gTcH_MYVhQ1wThi0Ytb0");
            var response = await client.GetAsync(request);
            PopularMovies popularMovies = JsonSerializer.Deserialize<PopularMovies>(response.Content!)!;


            for(int i = 0; i < popularMovies.results.Count; i++)  {
                var jsonStr = await GetConcreteMovieByTitle(popularMovies.results[i].title);
                Movie movie = JsonSerializer.Deserialize<Movie>(jsonStr)!;
                movie.Year = popularMovies.results[i].release_date.Substring(0, 4);

                Movies.Add(movie);
            }
        }


        private async void TaskToJson(string title) {

            var jsonStr = await OmdbService.GetAllMoviesByTitle(title);
            Movie = JsonSerializer.Deserialize<MovieCollection>(jsonStr)!;

            if (Movie.Search is not null) {
                Movies.Clear();

                foreach (var result in Movie.Search) {

                    var movieCollectionJson = await OmdbService.GetConcreteMovieById(result.imdbID);
                    var movieFromCollection = JsonSerializer.Deserialize<Movie>(movieCollectionJson);

                    if (movieFromCollection!.Poster == "N/A")
                        movieFromCollection.Poster = "\\Static Files\\Images\\no-poster.png";

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
            string text = (param as string)!;
            TaskToJson(text);
        }


        // INotifyPropertyChanged

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}