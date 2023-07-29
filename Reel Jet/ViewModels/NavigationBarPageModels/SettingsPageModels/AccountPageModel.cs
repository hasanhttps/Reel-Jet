using System;
using System.Windows;
using Microsoft.Win32;
using Reel_Jet.Commands;
using System.Windows.Input;
using Reel_Jet.Models.DatabaseNamespace;


namespace Reel_Jet.ViewModels.NavigationBarPageModels.SettingsPageModels {
    public class AccountPageModel {

        // Binding Properties

        public User EditedUser { get; set; } = Database.CurrentUser;
        public ICommand ConfirmChangeCommand { get; set; }
        public ICommand EditPfpCommand { get; set; }

        // Constructor

        public AccountPageModel() {

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
    }
}
