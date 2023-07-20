using System;
using System.Windows;
using System.Text.RegularExpressions;
using Reel_Jet.Services.InterfaceServices;
using static Reel_Jet.Models.DatabaseNamespace.Database;

#nullable disable


namespace Reel_Jet.Models.DatabaseNamespace {
    public class User : Person, IAuthLoginService, IAuthLogOutService, IAuthSignUpService {

        // Private Fields

		private string _name;
        private string _surname;
        private string _username;
        private int? _age;

        // Properties


		public string Name {
			get { return _name; }
			set { 
                if(String.IsNullOrEmpty(value)) 
                    MessageBox.Show("Invalid Name", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                else 
                    _name = value; OnProperty();
            }
		}

        public string Surname {
            get { return _surname; }
            set {
                if (String.IsNullOrEmpty(value)) 
                    MessageBox.Show("Invalid Surname", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                else
                    _surname = value; OnProperty(); 
            }
        }
		public int? Age {
			get { return _age; }
			set { 
                if ((value < 6) || value == null)
                    MessageBox.Show("Invalid Age", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                else
                    _age = value; OnProperty();
            }
        }

        public string Username {
            get { return _username; }
            set { 
                if (!Regex.IsMatch(value, "^[a-z0-9_-]{3,15}$") || String.IsNullOrEmpty(value)) 
                    MessageBox.Show("Invalid Username", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                else
                    _username = value; OnProperty();
            }
        }

        // Functions

        public void LogIn() {
            // database-dan yoxlanis
        }

        public void LogOut() {
            // database-dan silme
        }

        public void SignUp() {
            if (Name != null) {
                if (Surname != null) {
                    if (Age != null) {
                        if (Username != null) {
                            if (Password != null) {

                            }
                        }
                    }
                }
            } 
        }
        
    }
}
