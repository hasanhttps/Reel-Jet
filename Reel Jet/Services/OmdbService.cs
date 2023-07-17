using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Reel_Jet.Services {
    public static class OmdbService {

        private static readonly HttpClient _client = new HttpClient();

        private static readonly string _key = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build()["OmdbKey"]!;

        public static async Task<string> GetAllMoviesByTitle(string title) {
            string url = $"http://www.omdbapi.com/?apikey={_key}&s={title}";
            var task = await _client.GetStringAsync(url);
            return task;
        }

        public static async Task<string> GetConcreteMovieById(string imdbId) {
            string url = $"http://www.omdbapi.com/?apikey={_key}&i={imdbId}&plot=full";
            var task = await _client.GetStringAsync(url);
            return task;
        }

        public static async Task<string> GetConcreteMovieByTitle(string title) {
            string url = $"http://www.omdbapi.com/?apikey={_key}&t={title}&plot=full";
            var task = await _client.GetStringAsync(url);
            return task;
        }

    }
}
