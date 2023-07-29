using System;
using System.Windows;
using Microsoft.Win32;
using Reel_Jet.Commands;
using System.Windows.Input;
using System.Windows.Controls;
using Reel_Jet.Views.MoviePages;
using System.Windows.Media.Imaging;
using Reel_Jet.Views.NavigationBarPages;
using Reel_Jet.Models.DatabaseNamespace;


namespace Reel_Jet.ViewModels.NavigationBarPageModels {

    public class UserAccountPageModel  {

        // Private Fields

        private Frame MainFrame;

        
        // Binding Properties


        public User EditedUser { get; set; } = Database.CurrentUser;
        public ICommand WatchListPgButtonCommand { get; set; }
        public ICommand ConfirmChangeCommand { get; set; }
        public ICommand MoviePgButtonCommand { get; set; }
        public ICommand HistoryPgCommand { get; set; }
        public ICommand EditPfpCommand { get; set; }


        // Constructor


        public UserAccountPageModel(Frame frame) {
            MainFrame = frame;
            WatchListPgButtonCommand = new RelayCommand(WatchListPage);
            HistoryPgCommand = new RelayCommand(HistoryPage);
            MoviePgButtonCommand = new RelayCommand(MovieListPage);
            ConfirmChangeCommand = new RelayCommand(ConfirmChange);
            EditPfpCommand = new RelayCommand(EditPfp);
        }

        // Functions

        private void EditPfp(object? obj) {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Image Files (*.jpg, *.jpeg, *.png, *.gif, *.bmp)|*.jpg;*.jpeg;*.png;*.gif;*.bmp|All files (*.*)|*.*";

            if (fileDialog.ShowDialog() == true) {
                EditedUser.Avatar = fileDialog.FileName;
            }
        }

        private void ConfirmChange(object? sender) {
            MessageBox.Show("Changes have been saved", "Saving", MessageBoxButton.OK, MessageBoxImage.Information);
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
    }
}