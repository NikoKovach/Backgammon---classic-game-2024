using System.Collections.Generic;

namespace TablaModels.ComponentModels.Components.Interfaces
{
    public interface IArrangeChips
    {
          void ArrangeWhiteChips(IDictionary<int, IColumn> columns, 
               IList<IPool> checkers);

          void ArrangeBlackChips(IDictionary<int, IColumn> columns, 
               IList<IPool> checkers);
    }
}