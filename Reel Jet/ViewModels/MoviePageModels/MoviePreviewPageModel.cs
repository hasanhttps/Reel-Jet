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


namespace Reel_Jet.ViewModels.MoviePageModels {
    public class MoviePreviewPageModel : INotifyPropertyChanged {

        // Private Fields

        private Frame MainFrame;
        private Movie _movie;

        // Binding Properties

        public ICommand? VideoPlayerPageCommand { get; set; }
        public ICommand? MovieListPageCommand { get; set; }
        public string trailerLink { get; set; }
        public Movie Movie {
            get => _movie;
            set {
                _movie = value;
            }
        }

        // Constructor

        public MoviePreviewPageModel(Frame frame, Movie movie) { 
            MainFrame = frame;
            Movie = movie;
            trailerLink = "https://www.youtube.com/results?search_query=" + movie.Title + " trailer";

            MovieListPageCommand = new RelayCommand(MovieListPage);
            VideoPlayerPageCommand = new RelayCommand(VideoPlayerPage);
        }

        // Functions

        private void MovieListPage(object? param) {
            MainFrame.Content = new MovieListPage(MainFrame);
        }
        
        private void VideoPlayerPage(object? param) {
            MainFrame.Content = new VideoPlayerPage(MainFrame, Movie.Title);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string? propertyName = null) { 
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}