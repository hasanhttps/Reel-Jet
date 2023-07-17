using System;
using Reel_Jet.ViewModels.RegistrationPageModels;
using System.Windows.Controls;

namespace Reel_Jet.Views {
    public partial class LoadingPage : Page {
        public LoadingPage() {
            InitializeComponent();
            DataContext = new LoadingPageModel();
        }
    }
}
