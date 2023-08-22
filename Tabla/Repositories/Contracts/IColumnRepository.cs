namespace Tabla.Repositories.Contracts
{
    using System.Collections.Generic;
    using Tabla.Model.Interfaces;

    public interface IColumnRepository
    {
        IList<IColumn> Columns { get; }

        void AddColumn(IColumn column);
    }
}
