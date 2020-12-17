using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using HotMusicReviews.Models;

namespace HotMusicReviews.Services
{

    public class LastFmService
    {
        public HttpClient Client { get; }

        public LastFmSettings Settings { get; }

        public LastFmService(HttpClient client, LastFmSettings settings)
        {
            Client = client;
            Settings = settings;
        }

        public async Task<AlbumDetails?> GetAlbum(string mbid)
        {
            var content = await RequestUri<AlbumDetailsResult>(new Dictionary<string, string>()
                {
                    {"method", "album.getinfo"},
                    {"mbid", mbid}
                }
            );
            return content?.album;
        }

        public async Task<AlbumSearch[]?> SearchAlbums(string album, int limit, int page)
        {
            var content = await RequestUri<AlbumsSearchResult>(new Dictionary<string, string>()
                {
                    {"method", "album.search"},
                    {"album", album},
                    {"limit", limit.ToString()},
                    {"page", page.ToString()}
                }
            );
            return content?.results?.albummatches?.album;
        }

        public async Task<PerformerCorrection?> PerformerCorrection(string performer)
        {
            var content = await RequestUri<PerformerCorrectionResult>(new Dictionary<string, string>()
                {
                    {"method", "artist.getcorrection"},
                    {"artist", performer},
                }
            );
            return content?.corrections?.correction?.artist;
        }

        public async Task<PerformerDetails?> GetPerformer(string mbid)
        {
            var content = await RequestUri<PerformerDetailsResult>(new Dictionary<string, string>()
                {
                    {"method", "artist.getinfo"},
                    {"mbid", mbid},
                }
            );
            return content?.artist;
        }

        public async Task<PerformerSearch[]?> SearchPerformer(string performer, int limit, int page)
        {
            var content = await RequestUri<PerformerSearchResult>(new Dictionary<string, string>()
                {
                    {"method", "artist.search"},
                    {"artist", performer},
                    {"limit", limit.ToString()},
                    {"page", page.ToString()}
                }
            );
            return content?.results?.artistmatches?.artist;
        }

        private async Task<T?> RequestUri<T>(Dictionary<string, string> arguments)
        {
            var argumentList = new List<string>();
            argumentList.Add($"?api_key={Settings.ApiKey}&format=json&sort=created&direction=desc");

            foreach (var entry in arguments)
            {
                argumentList.Add($"{entry.Key}={entry.Value}");
            }

            var uri = String.Join("&", argumentList);

            var response = await Client.GetAsync(uri);

            response.EnsureSuccessStatusCode();

            using var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<T>(responseStream);
        }
    }
}