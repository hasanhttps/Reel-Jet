using System;
using System.Windows.Controls;
using Reel_Jet.ViewModels.RegistrationPageModels;
using static Reel_Jet.Models.DatabaseNamespace.Database;

namespace Reel_Jet.Views.RegistrationPages {
    public partial class SignupPage : Page {
        private Frame MainFrame;

        public SignupPage(Frame frame) {
            InitializeComponent();
            MainFrame = frame;
            DataContext = new SignupPageModel(MainFrame);
        }

        private void TextBlock_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            MainFrame.Content = new LoginPage(MainFrame);
        }

        private void SetErrorTxtBoxes() {
            ErrorLabels["NameLabel"] = NameTxtBoxTip;
            ErrorLabels["SurnameLabel"] = SurnameTxtBoxTip;
            ErrorLabels["UsernameLabel"] = UsernameTxtBoxTip;
            ErrorLabels["AgeLabel"] = AgeTxtBoxTip;
            ErrorLabels["EmailLabel"] = EmailTxtBoxTip;
            ErrorLabels["PasswordLabel"] = PasswordTxtBoxTip;
            ErrorLabels["RegistrationCodeLabel"] = RegistrationCodeTxtBoxTip;
            ErrorLabels["ConfirmPasswordLabel"] = ConfirmTxtBoxTip;
        }
    }
}
