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

        // Constructor

        public MoviePreviewPageModel(Frame frame, Movie movie) { 
            MainFrame = frame;
            Movie = movie;
            trailerLink = "https://www.youtube.com/results?search_query=" + movie.Title + " trailer";
        }

        // Functions

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null) { 
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}