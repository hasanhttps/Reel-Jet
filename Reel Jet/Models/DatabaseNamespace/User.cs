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
        private string _phone;
        private int? _age;

        // Properties


		public string Name {
			get { return _name; }
			set { 
                _name = value; OnProperty();
            }
		}

        public string Surname {
            get { return _surname; }
            set {
                 _surname = value; OnProperty(); 
            }
        }
		public int? Age {
			get { return _age; }
			set { 
                if (value < 6)
                    MessageBox.Show("Invalid Age", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                else
                    _age = value; OnProperty();
            }
        }

        public string Username {
            get { return _username; }
            set { 
                if (!Regex.IsMatch(value, "^[a-z0-9_-]{3,15}$")) 
                    MessageBox.Show("Invalid Username", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                else
                    _username = value; OnProperty();
            }
        }

        public string PhoneNumber {
            get { return _phone; }
            set {
                if (!Regex.IsMatch(value!, "^[\\+]?[(]?[0-9]{3}[)]?[-\\s\\.]?[0-9]{3}[-\\s\\.]?[0-9]{2}[-\\s\\.]?[0-9]{2}$")) 
                    MessageBox.Show("Invalid Phone Number", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                _phone = value;
            }
        }


        // Functions

        public bool LogIn(string email,string password) {
            if (CheckUserExist(email, password)) return true;
            return false;
        }

        public void LogOut() {
            // database-dan silme
        }

        public void SignUp(User newUser) {
            if (!CheckUserExist(newUser.Email,newUser.Password)) {
                Database.Users.Add(newUser);
                JsonHandling.WriteData(Database.Users, "users");
            }
            else
                MessageBox.Show("This email is already in use","Error",MessageBoxButton.OK,MessageBoxImage.Error);
        }
        
    }
}
