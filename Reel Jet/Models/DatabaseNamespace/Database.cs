﻿using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using static Reel_Jet.Models.DatabaseNamespace.JsonHandling;

namespace Reel_Jet.Models.DatabaseNamespace {
    public static class Database {

        // Properties

        public static ObservableCollection<User> Users { get; set; } = new();

        // Constructor
        
        static Database() {
            Users = ReadData<ObservableCollection<User>>("users")!;
        }

        // Functions

        public static bool CheckUserExist(string email, string password) {
            foreach (var user in Users) {
                if (user.Email == email && user.Password == password)
                    return true;
            }
            return false;

        }
    }
}
