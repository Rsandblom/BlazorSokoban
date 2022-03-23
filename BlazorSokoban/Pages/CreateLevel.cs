using BlazorSokoban.Models;
using BlazorSokoban.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorSokoban.Pages
{
    public partial class CreateLevel
    {
        [Inject]
        public ILevelDataService LevelDataService { get; set; }

        public Level Level { get; set; }

        public int NextLevelId { get; set; }

        protected async override Task OnInitializedAsync()
        {
            var max = await LevelDataService.GetMaxLevelId();

            NextLevelId = max + 1;
            InitializeLevel();


        }

        public void InitializeLevel()
        {

            //Level = await LevelDataService.GetLevelById(id);
            Level = new Level
            {
                LevelId = NextLevelId,
                MapGrid = new char[,]
                {
            {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
            {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
            {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
            {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
            {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
            {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
            {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
            {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
            {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
            {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
            {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
            {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
            {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
            {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
            {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
            {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '}
                     }
            };
        }

        public string TypeOfTileClass(char classType)
        {
            string classTypeName;
            switch (classType)
            {
                case 'W':
                    classTypeName = "wall";
                    break;
                case 'B':
                    classTypeName = "box";
                    break;
                case 'G':
                    classTypeName = "goal";
                    break;
                case ' ':
                    classTypeName = "empty";
                    break;
                case 'P':
                    classTypeName = "player";
                    break;
                default:
                    classTypeName = "";
                    break;
            }

            return classTypeName;
        }

        public void ChangeTileType(int row, int col)
        {
            char tileType = Level.MapGrid[row, col];

            switch (tileType)
            {
                case ' ':
                    Level.MapGrid[row, col] = 'W';
                    break;
                case 'W':
                    Level.MapGrid[row, col] = 'B';
                    break;
                case 'B':
                    Level.MapGrid[row, col] = 'G';
                    break;
                case 'G':
                    Level.MapGrid[row, col] = 'P';
                    break;
                case 'P':
                    Level.MapGrid[row, col] = ' ';
                    break;
            }
        }

        public void SaveLevel()
        {
            LevelDataService.AddLevel(Level);
        }
    }
}
