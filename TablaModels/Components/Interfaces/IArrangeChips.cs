namespace TablaModels.Components.Interfaces
{
     using System.Collections.Generic;

     public interface IArrangeChips
     {
          void ArrangeWhiteChips(IDictionary<int, IColumn> columns,
               IList<IPool> checkers);

          void ArrangeBlackChips(IDictionary<int, IColumn> columns,
               IList<IPool> checkers);
     }
}