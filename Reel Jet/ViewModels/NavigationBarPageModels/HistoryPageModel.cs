using Reel_Jet.Commands;
using Reel_Jet.Views.MoviePages;
using Reel_Jet.Views.NavigationBarPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Reel_Jet.ViewModels.NavigationBarPageModels {
    public class HistoryPageModel {

        // Private Fields

        private Frame MainFrame;

        // Binding Properties

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
