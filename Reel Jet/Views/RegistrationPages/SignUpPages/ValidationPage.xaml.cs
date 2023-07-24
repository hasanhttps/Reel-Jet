﻿using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Controls;
using Reel_Jet.Models.DatabaseNamespace;
using Reel_Jet.ViewModels.RegistrationPageModels.SignUpPageModels;


namespace Reel_Jet.Views.RegistrationPages.SignUpPages {
    public partial class ValidationPage : Page {
        private Frame MainFrame;
        public ValidationPage(Frame frame, User newUser) {
            InitializeComponent();
            MainFrame = frame;
            DataContext = new ValidationPageModel(MainFrame, newUser);
        }
    }
}