using System;
using Reel_Jet.Commands;
using System.Windows.Input;
using System.Windows.Controls;
using Reel_Jet.Views.MoviePages;
using System.Collections.ObjectModel;
using Reel_Jet.Models.MovieNamespace;
using Reel_Jet.Views.NavigationBarPages;
using static Reel_Jet.Models.DatabaseNamespace.Database;


namespace Reel_Jet.ViewModels.NavigationBarPageModels {
    public class HistoryPageModel {

        // Private Fields

        private Frame MainFrame;

        // Binding Properties

        public ObservableCollection<Movie> History { get; set; } = CurrentUser.HistoryList; 
        public ICommand? WatchListPgButtonCommand { get; set; }
        public ICommand? MoviePgButtonCommand { get; set; }

        // Constructor

        public HistoryPageModel(Frame frame) { 
            MainFrame = frame;
            WatchListPgButtonCommand = new RelayCommand(WatchListPage);
            MoviePgButtonCommand = new RelayCommand(MovieListPage);
        }

        // Functions

        private void WatchListPage(object? sender) {
            MainFrame.Content = new WatchListPage(MainFrame);
        }

        private void MovieListPage(object? sender) {
            MainFrame.Content = new MovieListPage(MainFrame);
        }
    }
}
