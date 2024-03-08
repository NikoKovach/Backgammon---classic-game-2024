namespace TablaGameLogic.Factory.Contracts
{
    using System.Collections.Generic;
    using TablaModels.ComponentModels.Components.Interfaces;

     public interface IPlayerFactory
     {
          IPlayer CreatePlayer(string playerName,IMoveChecker moveChecker);

          IList<IPlayer> CreatePlayers( string firstPlayerName, string    secondPlayerName,IBoard board );
     }
}
