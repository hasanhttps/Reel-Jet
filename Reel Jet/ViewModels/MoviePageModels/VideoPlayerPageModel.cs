using System;
using System.Windows;
using System.Net.Http;
using HtmlAgilityPack;
using Reel_Jet.Commands;
using System.Windows.Input;
using System.ComponentModel;
using System.Windows.Controls;
using Reel_Jet.Views.MoviePages;
using Microsoft.Web.WebView2.Wpf;
using Microsoft.Web.WebView2.Core;
using System.Collections.ObjectModel;
using Reel_Jet.Models.MovieNamespace;
using System.Runtime.CompilerServices;
using Reel_Jet.Views.NavigationBarPages;


namespace Reel_Jet.ViewModels.MoviePageModels {
    class VideoPlayerPageModel : INotifyPropertyChanged {

        // Private Fields

        private Frame MainFrame;
        private WebView2 Player;
        private Movie Movie;
        private string? _videoPgUrl;
        private string? _videoUrl;
        private string? _search;

        // Binding Properties

        public ICommand? WatchListPgButtonCommand { get; set; }
        public ICommand? FullScreenButtonCommand { get; set; }
        public ICommand? SelectionChangedCommand { get; set; }
        public ICommand? SettingsPgButtonCommand { get; set; }
        public ICommand? HistoryPgButtonCommand { get; set; }
        public ICommand? ProfilePgButtonCommand { get; set; }
        public ICommand? MovieListPageCommand { get; set; }
        public ObservableCollection<Option>? Options { get; set; }
        public string? VideoUrl {
            get => _videoUrl;
            set {
                _videoUrl = value;
            } 
        }

        // Constructor

        public VideoPlayerPageModel(Frame frame, Movie movie, WebView2 player) {

            MainFrame = frame;
            Player = player;
            Movie = movie;

            // It is blocking ads from webview2 for opening new window
            WebView2_CoreWebView2InitializationCompleted();

            SelectionChangedCommand = new RelayCommand(SelectionChanged);
            FullScreenButtonCommand = new RelayCommand(FullScreenPage);
            WatchListPgButtonCommand = new RelayCommand(WatchListPage);
            SettingsPgButtonCommand = new RelayCommand(SettingsPage);
            HistoryPgButtonCommand = new RelayCommand(HistoryPage);
            MovieListPageCommand = new RelayCommand(MovieListPage);
            ProfilePgButtonCommand = new RelayCommand(ProfilePage);

            SearchAlgorithm(movie.Title);
            if (!CheckMovieExist())
                MessageBox.Show("Video not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

        }


        // Functions


        private void HistoryPage(object? sender) {
            MainFrame.Content = new HistoryPage(MainFrame);
        }

        private void WatchListPage(object? sender) {
            MainFrame.Content = new WatchListPage(MainFrame);
        }

        private void MovieListPage(object? sender) {
            MainFrame.Content = new MovieListPage(MainFrame);
        }

        private void ProfilePage(object? sender) {
            MainFrame.Content = new UserAccountPage(MainFrame);
        }

        private void SettingsPage(object? sender) { 
            MainFrame.Content = new SettingsPage(MainFrame);
        }

        private void FullScreenPage(object? sender) {

            MainFrame.Content = new FullScreenPage(VideoUrl!);
        }

        private void SelectionChanged(object? sender) {
            string option = (sender as Option)!.option;
            if (option != null) {
                int count = 0;
                foreach(var op in Options!) {
                    count++;
                    if (op.option == option) break;
                }

                try {
                    FindEmbedVideoLink(_videoPgUrl + count.ToString() + "/");
                    Uri uri = new Uri(VideoUrl!);
                    Player.Source = uri;
                }
                catch (Exception e) {
                    if (e.Message == "Trailer Link" && option.ToLower() == "fragman") {
                        Uri uri = new Uri(VideoUrl!);
                        Player.Source = uri;
                    }
                }
            }
        }

        private void WebView2_CoreWebView2InitializationCompleted() {
            if (Player != null && Player.CoreWebView2 != null) {
                // Get the CoreWebView2 instance from the WebView2 control
                CoreWebView2 coreWebView2 = Player.CoreWebView2;

                // Handle the NewWindowRequested event
                coreWebView2.NewWindowRequested += CoreWebView2_NewWindowRequested!;
            }
        }

        private void CoreWebView2_NewWindowRequested(object sender, CoreWebView2NewWindowRequestedEventArgs e) {
            // Cancel the new window request
            e.Handled = true;
        }

        private bool CheckMovieExist() {
            if (ScrapeDiziBox()) return true;
            else if (ScrapeFullFilmIzleNet()) return true;
            else if (ScrapeFullHdIzle()) return true;
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

            Options = new();
            var node2 = htmlDocument.DocumentNode.SelectSingleNode("//iframe[@src]");
            var nodes = htmlDocument.DocumentNode.SelectNodes("//div[@class='keremiya_part']//span");
            foreach(var n in nodes) {
                Option option = new Option();
                option.option = n.InnerText;
                Options.Add(option);
            }

            HtmlAttribute scr2 = node2.Attributes["src"];

            if (scr2.Value.Substring(0, 5) == "https") VideoUrl = scr2.Value;
            else VideoUrl = "https:" + scr2.Value;

            if (VideoUrl.Contains("youtube.com"))
                throw new Exception("Trailer Link");
        }

        private string? FindVideoLink(string searchlink) {
            string url = searchlink + _search;
            var httpClient = new HttpClient();
            var html = httpClient.GetStringAsync(url).Result;
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            HtmlAttribute? scr = null;
            HtmlNodeCollection htmlNodes = htmlDocument.DocumentNode.SelectNodes("//div[@class='movie-details existing-details']");

            foreach (var node in htmlNodes) {
                try {
                    if (node.InnerText.Contains(Movie.Year)) {
                        var doc = new HtmlDocument();
                        doc.LoadHtml(node.InnerHtml);

                        var nameDiv = doc.DocumentNode.SelectSingleNode("//div[@class='name']");
                        var anchorElement = nameDiv.SelectSingleNode("a");
                        scr = anchorElement.Attributes["href"];
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
                string? VideoPageLink = FindVideoLink("https://www.dizifilmbox.pw/?s=");
                FindEmbedVideoLink(VideoPageLink);
                _videoPgUrl = VideoPageLink;
                Uri uri = new Uri(VideoUrl!);
                Player.Source = uri;
                return true;
            }
            catch {
                return false;
            }
        }

        // FullFilmIzle.net

        private bool ScrapeFullFilmIzleNet() {
            try {
                string? VideoPageLink = FindVideoLink("https://fullfilmizle.net/?s=");
                FindEmbedVideoLink(VideoPageLink);
                _videoPgUrl = VideoPageLink;
                Uri uri = new Uri(VideoUrl!);
                Player.Source = uri;
                return true;
            }
            catch {
                return false;
            }
        }

        // FullHdIzle.me

        private bool ScrapeFullHdIzle() {
            try {
                string? VideoPageLink = FindVideoLink("https://www.fullhdizle.me/?s=");
                FindEmbedVideoLink(VideoPageLink);
                _videoPgUrl = VideoPageLink;
                Uri uri = new Uri(VideoUrl!);
                Player.Source = uri;
                return true;
            }
            catch {
                return false;
            }
        }


        // INotifyPropertyChanged

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string? propertyName = null) { 
            PropertyChanged!.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}