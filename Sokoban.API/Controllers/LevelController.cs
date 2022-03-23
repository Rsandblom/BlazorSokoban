using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sokoban.API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorSokoban.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.IO;
using System.Text;
using Sokoban.API.Models;
using Microsoft.AspNetCore.Authorization;

namespace Sokoban.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LevelController : ControllerBase
    {
        private readonly ILevelRepository _levelRepository;

        public LevelController(ILevelRepository levelRepository)
        {
            _levelRepository = levelRepository;
        }

        [HttpGet]
        public ActionResult<List<LevelDb>> Get()
        {
            var levels = _levelRepository.GetLevels();
            var json = JsonConvert.SerializeObject(levels, Formatting.Indented);

            return Ok(json);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = Policies.Policies.CanPlayLevels)]
        public IActionResult GetLevelById(int id)
        {
            var json = JsonConvert.SerializeObject(_levelRepository.GetLevelById(id), Formatting.Indented);

            return Ok(json);
        }

        [HttpGet]
        [Route("/api/level/maxlevelid")]
        public IActionResult GetMaxLevelId()
        {
            
            return Ok(_levelRepository.GetLevelsCount());
        }
        [HttpPost]
        [Authorize(Policy = Policies.Policies.CanCreateLevels)]
        public async Task<string> AddLevel()
        {

            string jsonString;
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                jsonString = await reader.ReadToEndAsync();
            }

            Level level = new Level();
            level = JsonConvert.DeserializeObject<Level>(jsonString);
            _levelRepository.AddLevel(level);

            return "level id: " + level.LevelId.ToString() + "count: " + _levelRepository.GetLevelsCount().ToString();
            
        }
    }
}
