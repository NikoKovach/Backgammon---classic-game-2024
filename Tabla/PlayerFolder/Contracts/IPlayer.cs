
namespace Tabla.PlayerFolder.Contracts
{
    using System.Collections.Generic;

    using Tabla.Enums;
    using Tabla.Model.Interfaces;

    public interface IPlayer
    {
        string Name { get; }

        Color Color { get; }

        IList<IPool> MyPools { get; }

        void SetMyPools(IList<IPool> basicPools);

        IList<IPool> OutPools { get; }

        IList<IPool> BitenPools { get; }

        void AddToOutList(IPool pool);

        void AddToBitenList(IPool pool);

        IPool RemoveFromBitenList();
    }
}
