
namespace Tabla.GameLogic.Contracts
{
    using System.Collections.Generic;

    using Tabla.PlayerFolder.Contracts;
    using Tabla.TablaFolder.ContractTabla;

    public interface IBaseLogic
    {
        IPlayer CurrentPlayer { get;}

        ITabla Tabla { get; }

        IList<int> MoveParametersList { get; }
    }
}
