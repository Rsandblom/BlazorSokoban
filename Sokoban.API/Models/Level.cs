using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorSokoban.Models
{
    public class Level
    {
        public int LevelId { get; set; }
        public char[,] MapGrid { get; set; }
        
    }
}
