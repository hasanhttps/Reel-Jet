using System;
using Reel_Jet.Commands;
using System.Windows.Input;
using System.ComponentModel;
using System.Windows.Controls;
using Reel_Jet.Views.MoviePages;
using System.Runtime.CompilerServices;
using Reel_Jet.Models.DatabaseNamespace;


namespace Reel_Jet.ViewModels.RegistrationPageModels.SignUpPageModels {

    public class ValidationPageModel : INotifyPropertyChanged {

        // Private Fields

        private Frame MainFrame;
        private string _regcode;
        private string regCodeNumber1;
        private string regCodeNumber2;
        private string regCodeNumber3;
        private string regCodeNumber4;
        private string regCodeNumber5;
        private string regCodeNumber6;

        // Binding Properties

        public string RegCodeNumber1 { get => regCodeNumber1; set { regCodeNumber1 = value; OnProperty(); } }
        public string RegCodeNumber2 { get => regCodeNumber2; set { regCodeNumber2 = value; OnProperty(); } }
        public string RegCodeNumber3 { get => regCodeNumber3; set { regCodeNumber3 = value; OnProperty(); } }
        public string RegCodeNumber4 { get => regCodeNumber4; set { regCodeNumber4 = value; OnProperty(); } }
        public string RegCodeNumber5 { get => regCodeNumber5; set { regCodeNumber5 = value; OnProperty(); } }
        public string RegCodeNumber6 { get => regCodeNumber6; set { regCodeNumber6 = value; OnProperty(); } }
        public ICommand ConfirmCommand { get; set; }
        public User NewUser { get; set; } = new();
        public string RegCode {
            get => _regcode;
            set {
                _regcode = value; OnProperty();
            }
        }

        // Constructor

        public ValidationPageModel(Frame frame, User newUser) {
            MainFrame = frame;
            NewUser = newUser;
            ConfirmCommand = new RelayCommand(Confirm);
        }

        // Functions

        public void Confirm(object? param) {
            NewUser.SignUp(NewUser);
            MainFrame.Content = new MovieListPage(MainFrame);
        }

        // Property Changed

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnProperty([CallerMemberName] string name = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
