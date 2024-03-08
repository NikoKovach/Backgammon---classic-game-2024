namespace TablaGameLogic.Factory
{
     using System.Collections.Generic;
     using TablaGameLogic.Factory.Contracts;
     using TablaModels.ComponentModels.Components.Interfaces;
     using TablaModels.ComponentModels.Components.Players;
     public class PlayerFactory : IPlayerFactory
     {
          public IPlayer CreatePlayer(string playerName,IMoveChecker moveChecker)
          {
               IPlayer player  = new PlayerClassicGame(playerName,
                    moveChecker);         

               return player;
          }

          public IList<IPlayer> CreatePlayers( string firstPlayerName, string secondPlayerName,IBoard board )
          {
               IMoveChecker moves = new MoveCheckersClassicGame( board.ColumnSet );

               List<IPlayer> defaultPlayers = new List<IPlayer>
               {
                    new PlayerClassicGame( firstPlayerName,moves),
                    new PlayerClassicGame( secondPlayerName,moves)
               };

               return defaultPlayers;
          }
     }
}

//player.Move = new MoveCheckersClassicGame(columnSet);