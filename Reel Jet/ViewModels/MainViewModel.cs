using System;
using Reel_Jet.Views;
using System.Windows;
using Reel_Jet.Commands;
using System.Windows.Input;
using System.Windows.Controls;


namespace Reel_Jet.ViewModels {
    public class MainViewModel {

        // Private Fields

        private Frame MainFrame;
        private WindowState prevstate;

        // Binding Properties

        public ICommand? KeyDownCommand { get; set; }

        // Constructor

        public MainViewModel(Frame frame) { 
            MainFrame = frame;
            MainFrame.Content = new LoadingPage(frame);

            KeyDownCommand = new RelayCommand(KeyDown);
        }

        // Functions

        private void KeyDown(object? sender) { 
            //Window window = (sender as Window)!;

            //if (e.Key == Key.F11) {
            //    if (window.WindowStyle == WindowStyle.SingleBorderWindow) {
            //        window.WindowStyle = WindowStyle.None;
            //        prevstate = window.WindowState;
            //        window.WindowState = WindowState.Maximized;
            //        window.Visibility = Visibility.Collapsed;
            //        window.Topmost = true;
            //        window.WindowStyle = WindowStyle.None;
            //        window.Visibility = Visibility.Visible;
            //    }
            //    else {
            //        window.WindowState = prevstate;
            //        window.WindowStyle = WindowStyle.SingleBorderWindow;
            //    }
            //}
        }
    }
}
