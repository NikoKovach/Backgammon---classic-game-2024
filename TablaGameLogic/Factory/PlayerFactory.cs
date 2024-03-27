namespace TablaGameLogic.Factory
{
     using System;
     using System.Collections.Generic;
     using TablaGameLogic.Factory.Contracts;
     using TablaModels.Components.Interfaces;
     using TablaModels.Components.Players;

     public class PlayerFactory : IPlayerFactory
     {
          public IPlayer CreatePlayer(string playerName,IMoveChips moveChecker)
          {
               if ( string.IsNullOrEmpty(playerName) )
               {
                    throw new ArgumentNullException(nameof(playerName));
               }

               if ( moveChecker == null )
               {
                    throw new ArgumentNullException(nameof(moveChecker));
               }

               IPlayer player  = new PlayerClassicGame(playerName,
                    moveChecker);         

               return player;
          }

          public IList<IPlayer> CreatePlayers( string firstPlayerName, string secondPlayerName,IBoard board )
          {
               if ( string.IsNullOrEmpty(firstPlayerName) )
               {
                    throw new ArgumentNullException(nameof(firstPlayerName));
               }

               if ( string.IsNullOrEmpty(secondPlayerName) )
               {
                    throw new ArgumentNullException(nameof(secondPlayerName));
               }

               if ( board == null )
               {
                    throw new ArgumentNullException(nameof(board));
               }

               IMoveChips moves = new MoveChipsClassicGame( );

               List<IPlayer> defaultPlayers = new List<IPlayer>
               {
                    new PlayerClassicGame( firstPlayerName,moves),
                    new PlayerClassicGame( secondPlayerName,moves)
               };

               return defaultPlayers;
          }
     }
}