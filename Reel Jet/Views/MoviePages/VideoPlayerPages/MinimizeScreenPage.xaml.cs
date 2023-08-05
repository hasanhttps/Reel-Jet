using System;
using System.Windows.Controls;
using Microsoft.Web.WebView2.Wpf;
using System.Collections.ObjectModel;
using Reel_Jet.Models.MovieNamespace;
using Reel_Jet.ViewModels.MoviePageModels;
using Reel_Jet.ViewModels.MoviePageModels.VideoPlayerPageModels;


namespace Reel_Jet.Views.MoviePages.VideoPlayerPages {
    public partial class MinimizeScreenPage : Page {
        public MinimizeScreenPage(Frame frame, Movie movie, ObservableCollection<Option> options, string videoUrl, string videoPgUrl) {

            InitializeComponent();

            FullScreenPage fullScreenPage = new FullScreenPage(frame, movie, videoUrl);
            PlayerFrame.Content = fullScreenPage;

            DataContext = new MinimizeScreenPageModel(frame, movie, (fullScreenPage.DataContext as FullScreenPageModel)!.getPlayer(), 
                PlayerFrame, options, videoUrl, videoPgUrl);
        }
    }
}
