using System;
using Reel_Jet.Commands;
using System.Windows.Input;
using System.ComponentModel;
using System.Windows.Controls;
using Reel_Jet.Views.MoviePages;
using System.Runtime.CompilerServices;
using Reel_Jet.Models.DatabaseNamespace;
using System.Windows;
using System.Windows.Threading;
using Reel_Jet.Services.WebServices;
using Reel_Jet.Views.RegistrationPages.SignUpPages;

namespace Reel_Jet.ViewModels.RegistrationPageModels.SignUpPageModels {

    public class ValidationPageModel : INotifyPropertyChanged {


        // Private Fields

        private DispatcherTimer timer;
        private Frame MainFrame;
        private string _regcode;
        private string regCodeNumber1;
        private string regCodeNumber2;
        private string regCodeNumber3;
        private string regCodeNumber4;
        private string regCodeNumber5;
        private string regCodeNumber6;
        private int remainingSeconds = 60;


        // Binding Properties

        public ICommand ConfirmCommand { get; set; }
        public User NewUser { get; set; } = new();
        public string RegCodeFromMail { get; set; }
        public string TimerText => $"Time remaining: {remainingSeconds} seconds";
        public string RegCodeNumber1 { 
            get => regCodeNumber1;
            set {
                regCodeNumber1 = value; OnProperty(); 
            } 
        }
        public string RegCodeNumber2 { 
            get => regCodeNumber2;
            set { 
                regCodeNumber2 = value; OnProperty(); } }
        public string RegCodeNumber3 { 
            get => regCodeNumber3;
            set { 
                regCodeNumber3 = value; OnProperty(); 
            } 
        }
        public string RegCodeNumber4 { 
            get => regCodeNumber4; 
            set { regCodeNumber4 = value; OnProperty(); 
            } 
        }
        public string RegCodeNumber5 { 
            get => regCodeNumber5;
            set { regCodeNumber5 = value; OnProperty(); 
            } 
        }
        public string RegCodeNumber6 { 
            get => regCodeNumber6; 
            set { regCodeNumber6 = value; OnProperty(); 
            } 
        }
        public string RegCode {
            get => _regcode;
            set {
                _regcode = value; OnProperty();
            }
        }

        // Constructor

        public ValidationPageModel(Frame frame,User newUser) {
            MainFrame = frame;
            NewUser = newUser;
            ConfirmCommand = new RelayCommand(Confirm);
            RegCodeFromMail = Random.Shared.Next(100000, 1000000).ToString();
            SmtpService.sendMail(newUser.Email, new("Your Registration Code", RegCodeFromMail, "ReelJet"));
            StartTimer();
        }

        // Functions

        private void StartTimer() {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e) {
            remainingSeconds--;
            OnProperty(nameof(TimerText));

            if (remainingSeconds == 0)
                TimerExpired();
        }

        private void TimerExpired() {
            timer.Stop();
        }

     

        public void Confirm(object? param) {
            if (remainingSeconds == 0) {
                MessageBox.Show("Your Time Has Expired", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                MainFrame.Content = new MainSignUpPage(MainFrame);
            }
            else {
                if (!string.IsNullOrEmpty(RegCodeNumber1) && !string.IsNullOrEmpty(RegCodeNumber2) && !string.IsNullOrEmpty(RegCodeNumber3) && !string.IsNullOrEmpty(RegCodeNumber4) && !string.IsNullOrEmpty(RegCodeNumber5) && !string.IsNullOrEmpty(RegCodeNumber6)) {
                    string fullCode = RegCodeNumber1 + RegCodeNumber2 + RegCodeNumber3 + RegCodeNumber4 + RegCodeNumber5 + RegCodeNumber6;
                    if (fullCode == RegCodeFromMail) {
                        if (NewUser.SignUp(NewUser))
                            MainFrame.Content = new MovieListPage(MainFrame);

                    }
                    else
                        MessageBox.Show("Registration Code is Wrong , Try Again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                    MessageBox.Show("Fill all the required fields", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
        }

        // Property Changed

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnProperty([CallerMemberName] string name = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}