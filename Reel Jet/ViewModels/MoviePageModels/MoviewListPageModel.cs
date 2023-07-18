using System;
using Reel_Jet.Commands;
using System.Windows.Input;
using System.Windows.Controls;
using Reel_Jet.Views.MoviePages;
using Reel_Jet.Views.NavigationBarPages;

namespace Reel_Jet.ViewModels.MoviePageModels {
    public class MoviewListPageModel {

        // Private Fields

        private Frame MainFrame;

        // Binding Properties

        public ICommand? HistoryPgButtonCommand { get; set; }
        public ICommand? WatchListPgButtonCommand { get; set; }

        // Constructor

        public MoviewListPageModel(Frame frame) { 
            MainFrame = frame;

            HistoryPgButtonCommand = new RelayCommand(HistoryPage);
            WatchListPgButtonCommand = new RelayCommand(WatchListPage);
        }

        // Functions

        private void HistoryPage(object? sender) {
            MainFrame.Content = new HistoryPage(MainFrame);
        }

        private void WatchListPage(object? sender) {
            MainFrame.Content = new WatchListPage(MainFrame);
        }

    }
}
