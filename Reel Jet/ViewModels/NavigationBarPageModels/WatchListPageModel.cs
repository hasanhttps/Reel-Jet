using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using Reel_Jet.Views.MoviePages;
using Reel_Jet.Commands;

namespace Reel_Jet.ViewModels.NavigationBarPageModels {
    public class WatchListPageModel {

        // Private Fields

        private Frame MainFrame;

        // Binding Properties

        public ICommand? MovieListPgButtonCommand { get; set; }
        public ICommand? HistoryPgButtonCommand { get; set; }

        // Constructor

        public WatchListPageModel(Frame frame) {
            MainFrame = frame;
            MovieListPgButtonCommand = new RelayCommand(MovieListPage);
            HistoryPgButtonCommand = new RelayCommand(HistoryPage);
        }

        // Functions

        private void MovieListPage(object? sender) {
            MainFrame.Content = new MovieListPage(MainFrame);
        }
        private void HistoryPage(object? sender) {
            MainFrame.Content = new HistoryPage(MainFrame);
        }
    }
}
