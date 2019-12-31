using Lab1_1.Observer;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_1.Facade
{
    public class Lobby
    {
        private static string requestUri = "https://localhost:44393/api/player/";
        private static string gmRequestUri = "/api/gamecontroller/";
        public int maxLobbyPlayers = 2;
        public Uri url;

        public HttpClient client { get; set; }
        public Lobby(HttpClient client)
        {
            this.client = client;
        }
        public async Task<bool> LobbyIsFullAsync()
        {
            ICollection<Player> playersInLobby = await GetAllPlayersAsync(client.BaseAddress.PathAndQuery);

            if (playersInLobby.Count == maxLobbyPlayers)
                return true;

            return false;
        }

        public async Task<List<Unit>> GetMap(long id)
        {
            List<Unit> map = null;
            HttpResponseMessage response = await client.GetAsync(gmRequestUri + $"{id}");
            if (response.IsSuccessStatusCode)
            {
                map = await response.Content.ReadAsAsync<List<Unit>>();
            }
            return map;
        }
        public async Task<HttpStatusCode> UpdatePlayerAsync(Player player)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync(
                requestUri + $"{player.id}", player);
            response.EnsureSuccessStatusCode();

            return response.StatusCode;
        }
        public async Task<GameState> UpdateMap(long id, List<Unit> map)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                gmRequestUri + $"{id}", map);
            response.EnsureSuccessStatusCode();

            GameState gs = await response.Content.ReadAsAsync<GameState>();

            return gs;
        }
        private async Task<ICollection<Player>> GetAllPlayersAsync(string path)
        {
            ICollection<Player> players = null;
            HttpResponseMessage response = await client.GetAsync(requestUri);
            if (response.IsSuccessStatusCode)
            {
                players = await response.Content.ReadAsAsync<ICollection<Player>>();
            }
            return players;
        }
        static void ShowPlayer(Player player)
        {
            Console.WriteLine("Player name: " + player.Name);
        }
        public async Task<Uri> CreatePlayerAsync(Player player)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                requestUri, player);
            response.EnsureSuccessStatusCode();

            Player player2 = await response.Content.ReadAsAsync<Player>();
            if (player2 != null)
            {
                ShowPlayer(player2);
            }
            url = response.Headers.Location;

            return response.Headers.Location;
        }
        public async Task<Player> GetPlayerAsync(string path)
        {
            Player player = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                player = await response.Content.ReadAsAsync<Player>();
            }
            return player;
        }

    }
}
