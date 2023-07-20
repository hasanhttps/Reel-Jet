using System;
using System.Windows.Controls;
using static Reel_Jet.Models.DatabaseNamespace.Database;
using Reel_Jet.ViewModels.RegistrationPageModels.SignUpPageModels;

namespace Reel_Jet.Views.RegistrationPages.SignUpPages {
    public partial class MainSignUpPage : Page {
        private Frame MainFrame;
        public MainSignUpPage(Frame frame) {
            InitializeComponent();
            MainFrame = frame;
            DataContext = new MainSignupPageModel(MainFrame, Frame2);
        }
    }
}
