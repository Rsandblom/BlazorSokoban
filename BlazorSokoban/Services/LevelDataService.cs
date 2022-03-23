using BlazorSokoban.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text.Json;
using System.Text;

namespace BlazorSokoban.Services
{
    public class LevelDataService : ILevelDataService
    {
        private List<Level> Levels { get; set; }
        private readonly HttpClient _httpClient;

        public LevelDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            //InitializaLevelList();
        }

        public async Task<Level> GetLevelById(int id)
        {
            var response = await _httpClient.GetAsync($"api/level/{id}");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            var level = JsonConvert.DeserializeObject<Level>(json);
            return level;
        }

        public async Task AddLevel(Level level)
        {
            var json = JsonConvert.SerializeObject(level);
            //var jsonTest = JsonConvert.SerializeObject("levelTest");
            var response = await _httpClient.PostAsync($"api/level/", new StringContent(json, Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();
        }

        public async Task<int> GetMaxLevelId()
        {
            var response = await _httpClient.GetAsync($"api/level/maxlevelid");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            var levelid = JsonConvert.DeserializeObject<int>(json);
            Console.WriteLine("levelid: " + levelid);
            return levelid;
        }

        private void InitializaLevelList()
        {
            Levels = new List<Level>()
            {
                new Level
                {
                    LevelId = 1,
                    MapGrid = new char[,]
                    {
                        {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
                        {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
                        {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
                        {' ', ' ', ' ', ' ', 'W', 'W', 'W', 'W', 'W', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
                        {' ', ' ', ' ', ' ', 'W', ' ', ' ', ' ', 'W', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
                        {' ', ' ', ' ', ' ', 'W', 'B', ' ', ' ', 'W', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
                        {' ', ' ', 'W', 'W', 'W', ' ', ' ', 'B', 'W', 'W', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
                        {' ', ' ', 'W', ' ', ' ', 'B', ' ', 'B', ' ', 'W', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
                        {'W', 'W', 'W', ' ', 'W', ' ', 'W', 'W', ' ', 'W', ' ', ' ', ' ', 'W', 'W', 'W', 'W', 'W', 'W'},
                        {'W', ' ', ' ', ' ', 'W', ' ', 'W', 'W', ' ', 'W', 'W', 'W', 'W', 'W', ' ', ' ', 'G', 'G', 'W'},
                        {'W', ' ', 'B', ' ', ' ', 'B', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', 'G', 'G', 'W'},
                        {'W', 'W', 'W', 'W', 'W', ' ', 'W', 'W', 'W', ' ', 'W', 'P', 'W', 'W', ' ', ' ', 'G', 'G', 'W'},
                        {' ', ' ', ' ', ' ', 'W', ' ', ' ', ' ', ' ', ' ', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W'},
                        {' ', ' ', ' ', ' ', 'W', 'W', 'W', 'W', 'W', 'W', 'W', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
                        {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
                        {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '}
                    }
                }

            };
        }
    }
}
