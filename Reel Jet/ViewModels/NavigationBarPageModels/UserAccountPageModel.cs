using System;
using System.Windows;
using Microsoft.Win32;
using Reel_Jet.Commands;
using System.Windows.Input;
using System.Windows.Controls;
using Reel_Jet.Views.MoviePages;
using Reel_Jet.Views.NavigationBarPages;
using Reel_Jet.Models.DatabaseNamespace;


namespace Reel_Jet.ViewModels.NavigationBarPageModels {

    public class UserAccountPageModel  {

        // Private Fields

        private Frame MainFrame;
        
        // Binding Properties

        public User EditedUser { get; set; } = new();
        public ICommand WatchListPgButtonCommand { get; set; }
        public ICommand SettingsPgButtonCommand { get; set; }
        public ICommand ConfirmChangeCommand { get; set; }
        public ICommand MoviePgButtonCommand { get; set; }
        public ICommand HistoryPgCommand { get; set; }
        public ICommand EditPfpCommand { get; set; }


        // Constructor


        public UserAccountPageModel(Frame frame) {
            MainFrame = frame;

            setUser();

            WatchListPgButtonCommand = new RelayCommand(WatchListPage);
            SettingsPgButtonCommand = new RelayCommand(SettingsPage);
            MoviePgButtonCommand = new RelayCommand(MovieListPage);
            ConfirmChangeCommand = new RelayCommand(ConfirmChange);
            HistoryPgCommand = new RelayCommand(HistoryPage);
            EditPfpCommand = new RelayCommand(EditPfp);
        }

        // Functions


        private void setUser() {

            EditedUser.Avatar = Database.CurrentUser.Avatar;
            EditedUser.Name = Database.CurrentUser.Name;
            EditedUser.Surname = Database.CurrentUser.Surname;
            EditedUser.Age = Database.CurrentUser.Age;
            EditedUser.Username = Database.CurrentUser.Username;
            EditedUser.PhoneNumber = Database.CurrentUser.PhoneNumber;
            EditedUser.Email = Database.CurrentUser.Email;
            EditedUser.Password = Database.CurrentUser.Password;
        }

        private void ConfirmChange(object? sender) {

            if (string.IsNullOrEmpty(EditedUser.Name) 
                || string.IsNullOrEmpty(EditedUser.Surname) 
                || EditedUser.Age == null 
                || string.IsNullOrEmpty(EditedUser.Username) 
                || string.IsNullOrEmpty(EditedUser.PhoneNumber) 
                || string.IsNullOrEmpty(EditedUser.Password)) 
                
                MessageBox.Show("Fill all the required fields,Try Again", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);

            else {

                 Database.CurrentUser.Avatar      = EditedUser.Avatar      ;
                 Database.CurrentUser.Name        = EditedUser.Name        ;
                 Database.CurrentUser.Surname     = EditedUser.Surname     ;
                 Database.CurrentUser.Age         = EditedUser.Age         ;
                 Database.CurrentUser.Username    = EditedUser.Username    ;
                 Database.CurrentUser.PhoneNumber = EditedUser.PhoneNumber ;
                 Database.CurrentUser.Password    = EditedUser.Password    ;

                 JsonHandling.WriteData(Database.Users, "users");
            }
        }

        private void EditPfp(object? obj) {

            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Image Files (*.jpg, *.jpeg, *.png, *.gif, *.bmp)|*.jpg;*.jpeg;*.png;*.gif;*.bmp|All files (*.*)|*.*";

            if (fileDialog.ShowDialog() == true) {
                EditedUser.Avatar = fileDialog.FileName;
            }
        }

        private void MovieListPage(object? sender) {
            MainFrame.Content = new MovieListPage(MainFrame);
        }

        private void HistoryPage(object? sender) {
            MainFrame.Content = new HistoryPage(MainFrame);
        }

        private void WatchListPage(object? sender) {
            MainFrame.Content = new WatchListPage(MainFrame);
        }

        private void SettingsPage(object? sender) {
            MainFrame.Content = new SettingsPage(MainFrame);
        }
    }
}