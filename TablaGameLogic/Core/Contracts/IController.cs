namespace TablaGameLogic.Core.Contracts
{
     using System.Collections.Generic;

     using TablaGameLogic.Services.Contracts;
     using TablaModels.ComponentModels.Components.Interfaces;

     public interface IController
     {
          IBoard TablaBoard { get; }

          IList<IPlayer> Players { get; set; }

          IPlayer CurrentPlayer { get; set; }

          int CurrentPlayerMovesNumber { get; }

          IMoveService MoveService { get; }

          IMoveParameters MoveParams { get; set; }

          string PlayersChooseAColor(int color);

          string ArrangingTheCheckersToPlay();

          string InitialInfoCurrentPlayerMoves();

          void RollDice();

          string CurrentPlayerMakesMove(string moveString);

          void CurrentPlayerFirstSet();

          void ChangeCurrentPlayer();

          void ClearBoardFromCheckers();
     }
}

//void CreatePlayers( string firstPlayerName, string secondPlayerName );