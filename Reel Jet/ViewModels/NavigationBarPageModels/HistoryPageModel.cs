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
        public ICommand? SelectionChangedCommand { get; set; }
        public ICommand? SettingsPgButtonCommand { get; set; }
        public ICommand? ProfilePgButtonCommand { get; set; }
        public ICommand? MoviePgButtonCommand { get; set; }

        // Constructor

        public HistoryPageModel(Frame frame) { 
            MainFrame = frame;

            SelectionChangedCommand = new RelayCommand(SelectionChanged);
            WatchListPgButtonCommand = new RelayCommand(WatchListPage);
            SettingsPgButtonCommand = new RelayCommand(SettingsPage);
            ProfilePgButtonCommand = new RelayCommand(ProfilePage);
            MoviePgButtonCommand = new RelayCommand(MovieListPage);
        }

        // Functions

        private void SelectionChanged(object? param) {
            Movie movie = (Movie)param!;
            MainFrame.Content = new VideoPlayerPage(MainFrame, movie);
        }

        private void WatchListPage(object? sender) {
            MainFrame.Content = new WatchListPage(MainFrame);
        }

        private void MovieListPage(object? sender) {
            MainFrame.Content = new MovieListPage(MainFrame);
        }

        private void ProfilePage(object? sender) {
            MainFrame.Content = new UserAccountPage(MainFrame);
        }

        private void SettingsPage(object? sender) {
            MainFrame.Content = new SettingsPage(MainFrame);
        }
    }
}
