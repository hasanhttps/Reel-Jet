using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using static Reel_Jet.Models.DatabaseNamespace.JsonHandling;

namespace Reel_Jet.Models.DatabaseNamespace {
    public static class Database {

        // Properties

        public static ObservableCollection<User> Users { get; set; }
        public static Dictionary<string, TextBlock> ErrorLabels { get; set; } = new();
        public static User CurrentUser { get; set; } = new();

        // Constructor

        static Database() {
            Users = ReadData<ObservableCollection<User>>("users")!;
            if (Users == null)
                Users = new();
        }

        // Functions

        public static bool CheckUserExist(string email, string password) {
            foreach (var user in Users) {
                if (user.Email == email && user.Password == password) {
                    CurrentUser = user;
                    return true;
                }
            }
            return false;

        }
    }
}