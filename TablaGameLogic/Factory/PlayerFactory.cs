namespace TablaGameLogic.Factory
{
    using System.Collections.Generic;
    using TablaGameLogic.Factory.Contracts;
    using TablaModels.ComponentModels.Components.Interfaces;
    using TablaModels.ComponentModels.Components.Players;

    public class PlayerFactory : IPlayerFactory
    {
        public IPlayer CreatePlayer(string name, Dictionary<int, IColumn> columnSet)
        {
            IPlayer player  = new PlayerClassicGame(name);
            player.Move = new MoveCheckersClassicGame(columnSet);

            return player;
        }
    }
}