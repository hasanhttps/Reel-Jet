using System;
using System.Windows;
using System.Collections.ObjectModel;
using Reel_Jet.Models.DatabaseNamespace;
using static Reel_Jet.Models.DatabaseNamespace.Database;
using static Reel_Jet.Models.DatabaseNamespace.JsonHandling;

namespace Reel_Jet {
    public partial class App : Application {
        private void Application_Exit(object sender, ExitEventArgs e) {
            WriteData<ObservableCollection<User>>(Users, "users");
        }
    }
}
