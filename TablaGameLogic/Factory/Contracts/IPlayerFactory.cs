namespace TablaGameLogic.Factory.Contracts
{
    using System.Collections.Generic;
    using TablaModels.ComponentModels.Components.Interfaces;

    public interface IPlayerFactory
    {
        IPlayer CreatePlayer(string name, Dictionary<int, IColumn> columnSet);
    }
}
