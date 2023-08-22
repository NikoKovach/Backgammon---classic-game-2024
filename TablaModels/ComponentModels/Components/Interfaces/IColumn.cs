namespace TablaModels.ComponentModels.Components.Interfaces
{
    using System.Collections.Generic;
    using TablaModels.ComponentModels.Enums;

    public interface IColumn
    {
        int IdentityNumber { get; }

        int ColumnBase { get;  }

        int ColumnHeight { get; }

        ColumnColor Color {get; set;}

        Stack<IPool> PoolStack { get; }
    }
}
