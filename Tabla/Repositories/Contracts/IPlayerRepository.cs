
namespace Tabla.Repositories.Contracts
{
    using System.Collections.Generic;
    using Tabla.PlayerFolder.Contracts;

    public interface IPlayerRepository
    {
        IList< IPlayer> Players { get; }

        void AddPlayer(IPlayer person);
    }
}
