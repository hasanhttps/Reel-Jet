using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;


namespace Reel_Jet.Models.DatabaseNamespace {
    public abstract class Person : INotifyPropertyChanged {
		
		// Private Fields

		private string _email;
		private string _password;

		// Properties

		public string Email {
			get { return _email; }
			set { 
				if (!Regex.IsMatch(value, "[^@ \\t\\r\\n]+@[^@ \\t\\r\\n]+\\.[^@ \\t\\r\\n]+") || String.IsNullOrEmpty(value)) 
					MessageBox.Show("Invalid Email", "Warning", MessageBoxButton.OK,MessageBoxImage.Warning); 
				else
					_email = value; OnProperty();
			}
		}


		public string Password {
			get { return _password; }
			set { 
				if (!Regex.IsMatch(value, "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{8,}$") || String.IsNullOrEmpty(value)) 
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
