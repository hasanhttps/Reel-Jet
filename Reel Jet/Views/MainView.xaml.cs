using System;
using System.Windows;
using Reel_Jet.ViewModels;

namespace Reel_Jet.Views {
    public partial class MainView : Window {
        public MainView() {
            InitializeComponent();
            DataContext = new MainViewModel(MainFrame);
        }
    }
}
