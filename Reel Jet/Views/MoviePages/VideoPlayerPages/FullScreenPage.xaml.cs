using System;
using System.Windows.Controls;
using Reel_Jet.Models.MovieNamespace;
using Reel_Jet.ViewModels.MoviePageModels.VideoPlayerPageModels;


namespace Reel_Jet.Views.MoviePages.VideoPlayerPages {
    public partial class FullScreenPage : Page {

        private Frame Frame;
        private Movie Movie;
        private string VideoUrl;

        public FullScreenPage(Frame frame, Movie movie, string videourl) {
            InitializeComponent();
            DataContext = new FullScreenPageModel(frame, movie, Player, videourl);
            Frame = frame;
            Movie = movie;
            VideoUrl = videourl;
        }
    }
}
