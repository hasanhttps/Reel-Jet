using System;
using System.Windows;
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
            SearchAlgorithm(title);
            if (!CheckMovieExist())
                MessageBox.Show("Video not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        // Functions

        private bool CheckMovieExist() {
            if (ScrapeFullFilmIzleNet()) return true;
            else if (ScrapeDiziBox()) return true;
            return false;
        }

        private void SearchAlgorithm(string title) {
            _search = title.ToLower();
            _search = _search.Replace(" ", "+");
            _search = _search.Replace("ı", "i");
        }

        private void FindEmbedVideoLink(string? VideoPageLink) {

            var httpClient = new HttpClient();
            var htmlDocument = new HtmlDocument();
            var html = httpClient.GetStringAsync(VideoPageLink).Result;
            htmlDocument.LoadHtml(html);

            var node2 = htmlDocument.DocumentNode.SelectSingleNode("//iframe[@src]");
            HtmlAttribute scr2 = node2.Attributes["src"];

            if (scr2.Value.Substring(0, 5) == "https") VideoUrl = scr2.Value;
            else VideoUrl = "https:" + scr2.Value;
        }

        private string? FindVideoLink(string searchlink, Predicate<HtmlNode> checkvideolink) {

            string url = searchlink + _search;
            var httpClient = new HttpClient();
            var html = httpClient.GetStringAsync(url).Result;
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);
            var nodes = htmlDocument.DocumentNode.SelectNodes("//a[@href]");
            HtmlAttribute? scr = null;

            foreach (var node in nodes) {
                try {
                    if (checkvideolink(node)) {
                        scr = node.Attributes["href"];
                        break;
                    }
                }
                catch { }
            }

            return scr!.Value;
        }


        // Scraping


        // DiziBox

        private bool ScrapeDiziBox() {
            try {
                string? VideoPageLink = FindVideoLink("https://www.dizifilmbox.pw/?s=", CheckVideoLinkDiziBox);
                FindEmbedVideoLink(VideoPageLink);
                Uri uri = new Uri(VideoUrl);
                Player.Source = uri;
                return true;
            }
            catch {
                return false;
            }
        }

        private bool CheckVideoLinkDiziBox(HtmlNode node) {
            return node.Attributes["href"]!.Value.Substring(0, 35) == "https://www.dizifilmbox.pw/filmler/";
        }

        // FullFilmIzle.net

        private bool ScrapeFullFilmIzleNet() {
            try {
                string? VideoPageLink = FindVideoLink("https://fullfilmizle.net/?s=", CheckVideoLinkFullFilmIzleNet);
                FindEmbedVideoLink(VideoPageLink);
                Uri uri = new Uri(VideoUrl);
                Player.Source = uri;
                return true;
            }
            catch {
                return false;
            }
        }

        private bool CheckVideoLinkFullFilmIzleNet(HtmlNode node) {
            return node.Attributes["href"]!.Value.Contains("-izle/");
        }


        // INotifyPropertyChanged

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string? propertyName = null) { 
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
