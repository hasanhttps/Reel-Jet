using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Reel_Jet.ViewModels.MoviePageModels {
    public class FullScreenPageModel {

        // Binding Properties

        public string VideoUrl { get; set; }

        // Constructor

        public FullScreenPageModel(string videourl) { 
            VideoUrl = videourl;
        }
    }
}
