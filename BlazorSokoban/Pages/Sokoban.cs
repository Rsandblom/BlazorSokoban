using BlazorSokoban.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorSokoban.Services;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorSokoban.Pages
{
    public partial class Sokoban
    {
        [Inject]
        public ILevelDataService LevelDataService { get; set; }
        public string GameWon { get; set; } = "";
        public Level Level { get; set; }
        public bool LevelIsLoading { get; set; } = false;
        [Range(1, 3, ErrorMessage = "Level is not available")]
        public int LevelNumber { get; set; } = 1;
        public GameState GameState { get; set; } = new GameState();

        [CascadingParameter]
        Task<AuthenticationState> authenticationStateTask { get; set; }

        protected async override Task OnInitializedAsync()
        {   
        }

        public async Task LoadLevel()
        {
            await InitializeLevel(LevelNumber);
            InitializaGameState();
        }

        public async Task InitializeLevel(int id)
        {
            Level = null;
            LevelIsLoading = true;
            Level = await LevelDataService.GetLevelById(id);   
        }

        public void InitializaGameState()
        {
            GameState.MoveColumn = 0;
            GameState.MoveRow = 0;
            GameState.PlayerCurrentTileType = ' ';
            GameState.GoalArea = new List<TilePosition>();

            for (int row = 0; row < Level.MapGrid.GetLength(0); row++)
            {
                for (int col = 0; col < Level.MapGrid.GetLength(1); col++)
                {
                    if (Level.MapGrid[row, col] == 'G')
                        GameState.GoalArea.Add(new TilePosition() { Row = row, Column = col });
                    if (Level.MapGrid[row, col] == 'P')
                    {
                        GameState.PlayerPositionRow = row;
                        GameState.PlayerPositionColumn = col;
                    }
                }
            }
        }
        public void ReadKey(KeyboardEventArgs e)
        {
            DirectionOfMove(e.Key);
            AttemptMove();
        }

        public void DirectionOfMove(string arrowDirection)
        {
            switch (arrowDirection)
            {
                case "ArrowUp":
                    GameState.MoveRow = -1;
                    GameState.MoveColumn = 0;
                    break;
                case "ArrowDown":
                    GameState.MoveRow = 1;
                    GameState.MoveColumn = 0;
                    break;
                case "ArrowLeft":
                    GameState.MoveRow = 0;
                    GameState.MoveColumn = -1;
                    break;
                case "ArrowRight":
                    GameState.MoveRow = 0;
                    GameState.MoveColumn = 1;
                    break;
            }
        }

        public void AttemptMove()
        {
            var typeOfTileAhead = Level.MapGrid[GameState.PlayerPositionRow + GameState.MoveRow, GameState.PlayerPositionColumn + GameState.MoveColumn];

            switch (typeOfTileAhead)
            {
                case 'W':
                    break;
                case ' ':
                case 'G':
                    MovePlayer(typeOfTileAhead);
                    break;
                case 'B':
                    AttemptToPushBox();
                    break;
            }
        }

        public void MovePlayer(char typeOfTileAheadOfPlayer)
        {
            Level.MapGrid[GameState.PlayerPositionRow, GameState.PlayerPositionColumn] = GameState.PlayerCurrentTileType;
            Level.MapGrid[GameState.PlayerPositionRow += GameState.MoveRow, GameState.PlayerPositionColumn += GameState.MoveColumn] = 'P';
            GameState.PlayerCurrentTileType = typeOfTileAheadOfPlayer;
        }

        public void AttemptToPushBox()
        {
            var typeOfTileAheadOfBox = Level.MapGrid[GameState.PlayerPositionRow + (2 * GameState.MoveRow), GameState.PlayerPositionColumn + (2 * GameState.MoveColumn)];

            if (typeOfTileAheadOfBox == 'W' || typeOfTileAheadOfBox == 'B')
                return;

            MoveBox();
            var typeOfTileAheadOfPlayer = Level.MapGrid[GameState.PlayerPositionRow + GameState.MoveRow, GameState.PlayerPositionColumn + GameState.MoveColumn];
            MovePlayer(typeOfTileAheadOfPlayer);
            CheckIfGameIsFinished();
        }

        public void MoveBox()
        {
             Level.MapGrid[GameState.PlayerPositionRow + GameState.MoveRow, GameState.PlayerPositionColumn + GameState.MoveColumn] =
                (GameState.GoalArea.Any(t => t.Row == GameState.PlayerPositionRow + GameState.MoveRow && t.Column == GameState.PlayerPositionColumn + GameState.MoveColumn)) ? 'G' : ' ';

            Level.MapGrid[GameState.PlayerPositionRow + (2 * GameState.MoveRow), GameState.PlayerPositionColumn + (2 * GameState.MoveColumn)] = 'B';
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

        public void CheckIfGameIsFinished()
        {
            var gameIsFinished = true;
            for (var index = 0; index < GameState.GoalArea.Count; index++)
            {
                var goalTile = GameState.GoalArea[index];
                if (Level.MapGrid[goalTile.Row, goalTile.Column] != 'B')
                    gameIsFinished = false;
            }

            if (gameIsFinished)
                GameWon = "Game Won!!!";
        }

        /*
         * 
         * (authenticationState.User.Identity.Name == "alice")
         * */
    }
}

