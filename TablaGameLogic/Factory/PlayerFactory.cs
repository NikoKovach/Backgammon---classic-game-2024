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
               ArgumentNullException.ThrowIfNullOrEmpty( playerName );

               ArgumentNullException.ThrowIfNull( moveChecker );

               IPlayer player  = new PlayerClassicGame(playerName,
                    moveChecker);         

               return player;
          }

          public IList<IPlayer> CreatePlayers( string firstPlayerName, string secondPlayerName )
          {
               ArgumentException.ThrowIfNullOrEmpty( firstPlayerName );

               ArgumentException.ThrowIfNullOrEmpty( secondPlayerName );

               List<IPlayer> defaultPlayers = new List<IPlayer>
               {
                    new PlayerClassicGame( firstPlayerName),
                    new PlayerClassicGame( secondPlayerName)
               };

               return defaultPlayers;
          }
     }
}