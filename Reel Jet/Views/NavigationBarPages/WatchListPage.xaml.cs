using System;
using System.Windows.Controls;
using Reel_Jet.ViewModels.NavigationBarPageModels;

namespace Reel_Jet.Views.NavigationBarPages {
    public partial class WatchListPage : Page {
        public WatchListPage() {
            InitializeComponent();
            DataContext = new WatchListPageModel();
        }
    }
}
