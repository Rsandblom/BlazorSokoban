using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorSokoban.Models
{
    public class GameState
    {
        public int PlayerPositionRow { get; set; }
        public int PlayerPositionColumn { get; set; }
        public int MoveRow { get; set; }
        public int MoveColumn { get; set; }
        public char PlayerCurrentTileType { get; set; }
        public List<TilePosition> GoalArea { get; set; }
    }
}
