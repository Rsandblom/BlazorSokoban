using BlazorSokoban.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorSokoban.Services
{
    public interface ILevelDataService
    {
        Task<Level> GetLevelById(int id);
        Task AddLevel(Level level);
        Task<int> GetMaxLevelId();
    }
}
