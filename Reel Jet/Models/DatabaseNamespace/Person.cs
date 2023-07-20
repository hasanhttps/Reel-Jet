using System;
using System.Windows;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Runtime.CompilerServices;


namespace Reel_Jet.Models.DatabaseNamespace {
    public abstract class Person : INotifyPropertyChanged {
		
		// Private Fields

		private string _email;
		private string _password;

		// Properties

		public string Email {
			get { return _email; }
			set { 
				if (!Regex.IsMatch(value, "[^@ \\t\\r\\n]+@[^@ \\t\\r\\n]+\\.[^@ \\t\\r\\n]+")) 
					MessageBox.Show("Invalid Email", "Warning", MessageBoxButton.OK,MessageBoxImage.Warning); 
				else
					_email = value; OnProperty();
			}
		}


		public string Password {
			get { return _password; }
			set { 
				if (!Regex.IsMatch(value, "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{8,}$")) 
					MessageBox.Show("Invalid Password", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
				else
					_password = value; OnProperty();
            }
		}

		// Property Changed

        public event PropertyChangedEventHandler ?PropertyChanged;

        public void OnProperty([CallerMemberName] string ?name = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}