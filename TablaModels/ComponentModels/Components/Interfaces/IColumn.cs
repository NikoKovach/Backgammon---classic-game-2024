namespace TablaModels.ComponentModels.Components.Interfaces
{
     using System.Collections.Generic;
     using TablaModels.ComponentModels.Enums;

     public interface IColumn
     {
          int IdentityNumber { get; set; }

          int ColumnBase { get;  }

          int ColumnHeight { get; }

          ColumnColor Color { get; }

          Stack<IPool> PoolStack { get; }

          void SetColumnColor( string color );
     }
}
