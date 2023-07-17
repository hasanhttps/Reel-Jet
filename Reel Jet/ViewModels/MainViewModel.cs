using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reel_Jet.Views;
using System.Threading.Tasks;
using System.Windows.Controls;
using Reel_Jet.Views.RegistrationPages;
using Reel_Jet.Views.MoviePages;

namespace Reel_Jet.ViewModels {
    public class MainViewModel {
        private Frame MainFrame;
        public MainViewModel(Frame frame) { 
            MainFrame = frame;
            MainFrame.Content = new MovieListPage();
        }

    }
}
