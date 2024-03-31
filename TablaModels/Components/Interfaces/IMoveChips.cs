namespace TablaModels.Components.Interfaces
{
     using System.Collections.Generic;

     public interface IMoveChips
     {
         void Inside(int columnNumber, IPool pool,IDictionary<int, IColumn> columns);

         void Outside(int columnNumber,IDictionary<int, IColumn> columns);

         void Move(int columnNumber, int positions,IDictionary<int, IColumn> columns);
     }
}
