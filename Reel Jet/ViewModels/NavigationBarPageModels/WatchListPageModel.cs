using System;
using System.Windows;
using Reel_Jet.Commands;
using System.Windows.Input;
using System.Windows.Controls;
using Reel_Jet.Views.MoviePages;
using System.Collections.ObjectModel;
using Reel_Jet.Models.MovieNamespace;
using Reel_Jet.Models.DatabaseNamespace;
using Reel_Jet.Views.NavigationBarPages;


namespace Reel_Jet.ViewModels.NavigationBarPageModels {
    public class WatchListPageModel {

        // Private Fields

        private Frame MainFrame;

        // Binding Properties

        public ObservableCollection<Movie> MyWatchList { get; set; } = Database.CurrentUser.MyWatchList;
        public ICommand? MovieListPgButtonCommand { get; set; }
        public ICommand? HistoryPgButtonCommand { get; set; }
        public ICommand RemoveFromWatchListCommand { get; set; }
        public ShortMovieInfo MovieInfo { get; set; }

        // Constructor

        public WatchListPageModel(Frame frame) {
            MainFrame = frame;
            MovieListPgButtonCommand = new RelayCommand(MovieListPage);
            HistoryPgButtonCommand = new RelayCommand(HistoryPage);
            RemoveFromWatchListCommand = new RelayCommand(RemoveFromWatchList);
        }

        // Functions

        private void MovieListPage(object? sender) {
            MainFrame.Content = new MovieListPage(MainFrame);
        }

        private void HistoryPage(object? sender) {
            MainFrame.Content = new HistoryPage(MainFrame);
        }

        private void RemoveFromWatchList(object? sender) {
            Movie a = (sender as Movie)!;
            MyWatchList.Remove(a);
            JsonHandling.WriteData(Database.Users, "users");
        }
    }
}