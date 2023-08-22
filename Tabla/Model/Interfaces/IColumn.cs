
namespace Tabla.Model.Interfaces
{
    using System;
    using System.Collections.Generic;

    public interface IColumn
    {
        int IdentityNumber { get; }

        string Color { get; }

        Stack<IPool> PoolStack { get; }

        bool AddPool(IPool pool);

        IPool RemovePool();

        IPool GetPool();
    }
}
