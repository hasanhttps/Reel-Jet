using System;
using System.Windows;
using System.Text.RegularExpressions;
using Reel_Jet.Services.InterfaceServices;
using static Reel_Jet.Models.DatabaseNamespace.Database;
using System.Collections.ObjectModel;
using Reel_Jet.Models.MovieNamespace;

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
                if (!Regex.IsMatch(value, "^[a-zA-Z0-9]+([._]?[a-zA-Z0-9]+)*$")) 
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

        public ObservableCollection<Movie> MyWatchList { get; set; } = new();

        // Functions

        public bool LogIn(string email,string password) {
            if (CheckUserExist(email, password)) return true;
            return false;
        }

        public void LogOut() {
            // database-dan silme
        }

        public bool SignUp(User newUser) {
            if (!CheckUserExist(newUser.Email,newUser.Password)) {
                CurrentUser = newUser;
                Users.Add(newUser);
                JsonHandling.WriteData(Users, "users");
                return true;
            }
            else {
                MessageBox.Show("This email is already in use","Error",MessageBoxButton.OK,MessageBoxImage.Error);
                return false;
            }
        }
        
    }
}