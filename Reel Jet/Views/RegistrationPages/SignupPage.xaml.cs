using System;
using System.Windows.Controls;
using Reel_Jet.ViewModels.RegistrationPageModels;

namespace Reel_Jet.Views.RegistrationPages {
    public partial class SignupPage : Page {
        public SignupPage() {
            InitializeComponent();
            DataContext = new SignupPageModel();
        }
    }
}
