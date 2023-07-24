using System;
using System.Net.Http;
using HtmlAgilityPack;
using System.ComponentModel;
using System.Windows.Controls;
using Microsoft.Web.WebView2.Wpf;
using System.Runtime.CompilerServices;

namespace Reel_Jet.ViewModels.MoviePageModels {
    class VideoPlayerPageModel : INotifyPropertyChanged {

        // Private Fields

        private Frame MainFrame;
        private WebView2 Player;
        private string _videoUrl;
        private string _search;

        // Binding Properties

        public string VideoUrl {
            get => _videoUrl;
            set {
                _videoUrl = value;
            }
        }

        // Constructor

        public VideoPlayerPageModel(Frame frame, string title, WebView2 player) {
            MainFrame = frame;
            Player = player;
            _search = title.ToLower();
            _search = _search.Replace(" ", "+");
            ScrapeDiziBox();
        }

        // Functions

        private void ScrapeDiziBox() {
            string url = "https://www.dizifilmbox.pw/?s=" + _search;
            var httpClient = new HttpClient();
            var html = httpClient.GetStringAsync(url).Result;
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            var nodes = htmlDocument.DocumentNode.SelectNodes("//a[@href]");
            HtmlAttribute scr = null;

            foreach (var node in nodes) {
                try {

                    if (node.Attributes["href"]!.Value.Substring(0, 35) == "https://www.dizifilmbox.pw/filmler/") {
                        scr = node.Attributes["href"];
                        break;
                    }
                }
                catch {
                }
            }
            html = httpClient.GetStringAsync(scr.Value).Result;
            htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);
            var node2 = htmlDocument.DocumentNode.SelectSingleNode("//iframe[@src]");
            HtmlAttribute scr2 = node2.Attributes["src"];
            if (scr2.Value.Substring(0, 5) == "https") VideoUrl = scr2.Value;
            else VideoUrl = "https:" + scr2.Value;
            Uri uri = new Uri(VideoUrl);
            Player.Source = uri;
        }

        // INotifyPropertyChanged

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string? propertyName = null) { 
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
