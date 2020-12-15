using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using HotMusicReviews.Models;

namespace HotMusicReviews.Services
{

    public class LastFmService
    {
        public HttpClient Client { get; }

        public LastFmService(HttpClient client)
        {
            Client = client;
        }

        public async Task<string?> GetAlbum(string mbid)
        {
            var response = await Client.GetAsync("&method=album.getinfo&sort=created&direction=desc");

            response.EnsureSuccessStatusCode();

            // using var responseStream = await response.Content.ReadAsStreamAsync();

            return await response.Content.ReadAsStringAsync();

            // return await JsonSerializer.DeserializeAsync<string>(responseStream);
        }

        /*
async getAlbum(mbid: string): Promise<any> {
    return this.get('', {
      method: 'album.getinfo',
      mbid,
    })
  }

  async getAlbumSearch(
    album: string,
    limit: number,
    page: number,
  ): Promise<any> {
    return this.get('', {
      method: 'album.search',
      limit,
      page,
      album,
    })
  }

  async getArtistCorrection(artist: string): Promise<any> {
    return this.get('', {
      method: 'artist.getcorrection',
      artist,
    })
  }

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