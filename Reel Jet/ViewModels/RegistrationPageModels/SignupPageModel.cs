using System;
using System.Windows;
using Reel_Jet.Commands;
using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Reel_Jet.Models.DatabaseNamespace;
using System.Windows.Controls;

#nullable disable

namespace Reel_Jet.ViewModels.RegistrationPageModels {
    public class SignupPageModel {

        // Private Fields

        private Frame MainFrame;
        private string confirmPassword;
        private string regCode;

        // Binding Properties

        public User newUser { get; set; } = new();
        public ICommand SignUpCommand { get; set; }
        public string ConfirmPassword {
            get => confirmPassword;
            set {
                if (String.IsNullOrEmpty(value))
                    MessageBox.Show("Invalid Confirm Password", "Avoid", MessageBoxButton.OK, MessageBoxImage.Warning);

                confirmPassword = value; OnProperty();
            }
        }
        public string RegCode {
            get => regCode; 
            set {
                if (String.IsNullOrEmpty(value)) 
                    MessageBox.Show("Invalid Registration Code", "Avoid", MessageBoxButton.OK, MessageBoxImage.Warning);

                regCode = value; OnProperty();
            }
        }

        // Constructor

        public SignupPageModel(Frame frame) {
            MainFrame = frame;
            SignUpCommand = new RelayCommand(SignUp);
        }

        // Functions

        public void SignUp(object? param) {
            if (ConfirmPassword == newUser.Password) 
                newUser.SignUp(); // serti
        }

        // Property Changed

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnProperty([CallerMemberName] string? name = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}