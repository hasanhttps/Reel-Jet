using System;
using System.Windows.Controls;
using Reel_Jet.ViewModels.MoviePageModels;


namespace Reel_Jet.Views.MoviePages {
    public partial class FullScreenPage : Page {
        public FullScreenPage(string videourl) {
            InitializeComponent();
            DataContext = new FullScreenPageModel(videourl);
        }
    }
}
