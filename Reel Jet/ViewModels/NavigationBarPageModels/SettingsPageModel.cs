using System;
using Reel_Jet.Commands;
using System.Windows.Input;
using System.Windows.Controls;
using Reel_Jet.Views.NavigationBarPages.SettingsPages;


namespace Reel_Jet.ViewModels.NavigationBarPageModels {
    public class SettingsPageModel {

        // Private Fields

        private Frame MainFrame;
        private Frame SettingsFrame;

        // Binding Properties

        public ICommand? AccountPgButtonCommand { get; set; }
        public ICommand? ClearCacheDataButtonCommand { get; set; }

        // Constructor

        public SettingsPageModel(Frame frame, Frame settingsframe) { 
            MainFrame = frame;
            SettingsFrame = settingsframe;

            ClearCacheDataButtonCommand = new RelayCommand(ClearCacheData);
            AccountPgButtonCommand = new RelayCommand(AccountPage);
        }

        // Functions

        private void AccountPage(object? sender) {
            SettingsFrame.Content = new AccountPage();
        }

        private void ClearCacheData(object? sender) {

        }
    }
}
