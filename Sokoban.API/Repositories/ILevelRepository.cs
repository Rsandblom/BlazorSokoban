using BlazorSokoban.Models;
using Sokoban.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sokoban.API.Repositories
{
    public interface ILevelRepository
    {
        int GetLevelsCount();

        List<LevelDb> GetLevels();
        Level GetLevelById(int id);
        public void AddLevel(Level level);
    }
}
