using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
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

        public async Task<AlbumDao?> GetAlbum(string mbid)
        {
            var content = await RequestUri<AlbumResultDao>(new Dictionary<string, string>()
                {
                    {"method", "album.getinfo"},
                    {"mbid", mbid}
                }
            );
            return content?.album;
        }

        public async Task<AlbumSearchDao[]?> SearchAlbums(string album, int limit, int page)
        {
            var content = await RequestUri<AlbumsSearchResultsDao>(new Dictionary<string, string>()
                {
                    {"method", "album.search"},
                    {"album", album},
                    {"limit", limit.ToString()},
                    {"page", page.ToString()}
                }
            );
            return content?.results?.albummatches?.album;
        }

        public async Task<ArtistCorrectionDao?> ArtistCorrection(string artist)
        {
            var content = await RequestUri<CorrectionsResultDao>(new Dictionary<string, string>()
                {
                    {"method", "artist.getcorrection"},
                    {"artist", artist},
                }
            );
            return content?.corrections?.correction?.artist;
        }

        public async Task<ArtistCorrectionDao?> GetArtist(string mbid)
        {
            var content = await RequestUri<CorrectionsResultDao>(new Dictionary<string, string>()
                {
                    {"method", "artist.getinfo"},
                    {"mbid", mbid},
                }
            );
            return content?.corrections?.correction?.artist;
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

        /*

  async getArtist(mbid: string): Promise<any> {
    return this.get('', {
      method: 'artist.getinfo',
      mbid,
    })
  }

  async getArtistSearch(
    artist: string,
    limit: number,
    page: number,
  ): Promise<any> {
    return this.get('', {
      method: 'artist.search',
      limit,
      page,
      artist,
    })
  }
        */
    }

}