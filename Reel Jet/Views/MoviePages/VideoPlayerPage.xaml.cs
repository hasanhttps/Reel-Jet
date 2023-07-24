using System;
using System.Windows.Controls;
using Reel_Jet.ViewModels.MoviePageModels;

namespace Reel_Jet.Views.MoviePages {

    public partial class VideoPlayerPage : Page {
        public VideoPlayerPage(Frame frame, string title) {
            InitializeComponent();
            DataContext = new VideoPlayerPageModel(frame, title, Player);
        }
    }
}
