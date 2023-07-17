using System;
using System.Windows.Controls;
using Reel_Jet.ViewModels.RegistrationPageModels;

namespace Reel_Jet.Views.RegistrationPages {
    public partial class LoginPage : Page {
        public LoginPage(Frame frame) {
            InitializeComponent();
            DataContext = new LoginPageModel(frame);
        }
    }
}
