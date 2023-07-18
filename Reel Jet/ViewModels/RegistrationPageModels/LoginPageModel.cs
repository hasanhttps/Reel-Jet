using Reel_Jet.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Reel_Jet.Views.MoviePages;
using Reel_Jet.ViewModels.MoviePageModels;

namespace Reel_Jet.ViewModels.RegistrationPageModels {
    public class LoginPageModel {

        // Private Fields

        private Frame MainFrame;

        // Binding Properties

        public ICommand? SignInCommand { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        // Constructor

        public LoginPageModel(Frame frame) {

            MainFrame = frame;
            SignInCommand = new RelayCommand(SignIn);

        }

        // Functions

        private void SignIn(object? param) {
            MainFrame.Content = new MovieListPage(MainFrame);
        }

    }
}
