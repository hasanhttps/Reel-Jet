using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Reel_Jet.Utilities {
    public partial class CustomPasswordBox : UserControl {
        public CustomPasswordBox() {
            InitializeComponent();
        }

        private bool _isPasswordChanging;



        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register("Password", typeof(string), typeof(CustomPasswordBox),
                new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    PasswordPropertyChanged, null, false, UpdateSourceTrigger.PropertyChanged));



        private static void PasswordPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            if (d is CustomPasswordBox passwordBox) {
                passwordBox.UpdatePassword();
            }
        }



        public string Password {
            get { return (string)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            _isPasswordChanging = true;
            Password = passwordBox.Password;
            _isPasswordChanging = false;
        }



        private void UpdatePassword()
        {
            if (!_isPasswordChanging)
            {
                passwordBox.Password = Password;
            }
        }
    }
}
