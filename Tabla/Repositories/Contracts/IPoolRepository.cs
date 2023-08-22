namespace Tabla.Repositories.Contracts
{
    using System.Collections.Generic;
    using Tabla.Model.Interfaces;

    public interface IPoolRepository
    {
        IList <IPool> PoolsForFirstPlayer { get; }

        IList <IPool>  PoolsForSecondPlayer { get; }

        void AddPool(IPool pool);
    }
}
