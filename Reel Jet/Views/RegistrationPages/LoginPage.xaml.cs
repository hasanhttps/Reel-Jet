﻿using System;
using System.Windows.Input;
using System.Windows.Controls;
using Reel_Jet.ViewModels.RegistrationPageModels;

namespace Reel_Jet.Views.RegistrationPages {
    public partial class LoginPage : Page {
        private Frame MainFrame;
        public LoginPage(Frame frame) {
            InitializeComponent();
            MainFrame = frame;
            DataContext = new LoginPageModel(frame);
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e) {
            MainFrame.Content = new SignupPage();
        }
    }
}
