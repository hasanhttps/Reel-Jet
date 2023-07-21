using System;
using System.ComponentModel;
using System.Windows.Controls;
using Reel_Jet.Services.WebServices;
using System.Runtime.CompilerServices;
using Reel_Jet.Models.MovieNamespace;


namespace Reel_Jet.ViewModels.MoviePageModels {
    public class MoviePreviewPageModel : INotifyPropertyChanged {

        // Private Fields

        private Frame MainFrame;
        private ShortMovieInfo _movieInfo;
        private Movie _movie;

        // Binding Properties

        public string trailerLink { get; set; }
        public Movie Movie {
            get => _movie;
            set {
                _movie = value;
                OnPropertyChanged();
            }
        }
        public ShortMovieInfo MovieInfo { 
            get => _movieInfo;
            set {
                _movieInfo = value;
            }
        }

        // Constructor

        public MoviePreviewPageModel(Frame frame, ShortMovieInfo movieInfo) { 
            MainFrame = frame;
            MovieInfo = movieInfo;
            ChangeShortMovieFul();
            trailerLink = "https://www.youtube.com/results?search_query=" + movieInfo.Title + " trailer";
        }

        // Functions

        private async void ChangeShortMovieFul() {
            var jsonStr = await OmdbService.GetConcreteMovieById(MovieInfo.imdbID);
            Movie = System.Text.Json.JsonSerializer.Deserialize<Movie>(jsonStr);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null) { 
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}