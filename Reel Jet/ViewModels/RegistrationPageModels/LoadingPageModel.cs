using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using Reel_Jet.Views.RegistrationPages;

namespace Reel_Jet.ViewModels.RegistrationPageModels {
    public class LoadingPageModel {

        // Private Fields

        private Frame MainFrame;

        // Constructor

        public LoadingPageModel(Frame frame) { 
            MainFrame = frame;

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(6.25);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object? sender, EventArgs e) {
            // Stop the timer
            
            ((DispatcherTimer)sender!).Stop();
            MainFrame.Content = new LoginPage(MainFrame);
        }

    }
}