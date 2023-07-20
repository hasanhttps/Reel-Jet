using System.Windows;
using Reel_Jet.Commands;
using System.Windows.Input;
using System.Windows.Controls;
using Reel_Jet.Views.MoviePages;
using Reel_Jet.Models.DatabaseNamespace;
using Reel_Jet.ViewModels.MoviePageModels;
using static Reel_Jet.Models.DatabaseNamespace.Database;

namespace Reel_Jet.ViewModels.RegistrationPageModels {
    public class LoginPageModel {

        // Private Fields

        private Frame MainFrame;

        // Binding Properties

        public ICommand? SignInCommand { get; set; }

        public User NewUser { get; set; } = new();

        // Constructor

        public LoginPageModel(Frame frame) {

            MainFrame = frame;
            SignInCommand = new RelayCommand(SignIn);

        }

        // Functions

        private void SignIn(object? param) {
            if (!string.IsNullOrEmpty(NewUser.Email) && !string.IsNullOrEmpty(NewUser.Password)) 
                if (NewUser.LogIn(NewUser.Email, NewUser.Password)) 
                    MainFrame.Content = new MovieListPage(MainFrame);
                else 
                    MessageBox.Show("This account doesn't exist!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);     
            else
                MessageBox.Show("Fill all the required fields", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

    }
}
