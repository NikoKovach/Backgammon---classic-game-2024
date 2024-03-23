namespace TablaGameLogic.Factory.Contracts
{
    using System.Collections.Generic;
    using TablaModels.Components.Interfaces;

    public interface IPlayerFactory
     {
          IPlayer CreatePlayer(string playerName,IMoveChips moveChecker);

          IList<IPlayer> CreatePlayers( string firstPlayerName, string    secondPlayerName,IBoard board );
     }
}
