using System;
using System.Windows;
using Reel_Jet.Commands;
using System.Windows.Input;
using System.ComponentModel;
using System.Windows.Controls;
using Reel_Jet.Views.MoviePages;
using Reel_Jet.Services.WebServices;
using Reel_Jet.Models.MovieNamespace;
using System.Runtime.CompilerServices;
using Reel_Jet.Views.NavigationBarPages;
using static Reel_Jet.Models.DatabaseNamespace.Database;


namespace Reel_Jet.ViewModels.MoviePageModels {
    public class MoviePreviewPageModel : INotifyPropertyChanged {

        // Private Fields

        private Frame MainFrame;
        private Movie? _movie;

        // Binding Properties

        public ICommand? WatchListPgButtonCommand { get; set; }
        public ICommand? SettingsPgButtonCommand { get; set; }
        public ICommand? HistoryPgButtonCommand { get; set; }
        public ICommand? ProfilePgButtonCommand { get; set; }
        public ICommand? VideoPlayerPageCommand { get; set; }
        public ICommand? MovieListPageCommand { get; set; }
        public string trailerLink { get; set; }
        public Movie Movie {
            get => _movie!;
            set {
                _movie = value;
            }
        }

        // Constructor

        public MoviePreviewPageModel(Frame frame, Movie movie) { 
            MainFrame = frame;
            Movie = movie;
            trailerLink = "https://www.youtube.com/results?search_query=" + movie.Title + " trailer";

            WatchListPgButtonCommand = new RelayCommand(WatchListPage);
            VideoPlayerPageCommand = new RelayCommand(VideoPlayerPage);
            SettingsPgButtonCommand = new RelayCommand(SettingsPage);
            HistoryPgButtonCommand = new RelayCommand(HistoryPage);
            ProfilePgButtonCommand = new RelayCommand(ProfilePage);
            MovieListPageCommand = new RelayCommand(MovieListPage);
        }

        // Functions

        private void MovieListPage(object? param) {
            MainFrame.Content = new MovieListPage(MainFrame);
        }

        private void HistoryPage(object? sender) {
            MainFrame.Content = new HistoryPage(MainFrame);
        }

        private void WatchListPage(object? sender) {
            MainFrame.Content = new WatchListPage(MainFrame);
        }

        private void SettingsPage(object? sender) {
            MainFrame.Content = new SettingsPage(MainFrame);
        }

        private void ProfilePage(object? sender) {
            MainFrame.Content = new UserAccountPage(MainFrame);
        }
        
        private void VideoPlayerPage(object? param) {
            
            bool isContain = false;

            MainFrame.Content = new VideoPlayerPage(MainFrame, Movie);

            foreach(var movie in CurrentUser.HistoryList) {
                if (movie.Title == Movie.Title && movie.imdbID == Movie.imdbID)
                    isContain = true;
            }

            if (!isContain) 
                CurrentUser.HistoryList.Add(Movie);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string? propertyName = null) { 
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}