namespace TablaModels.ComponentModels.Components.Interfaces
{
     using System.Collections.Generic;

     public interface IBoard
     {
          IDictionary<int, IColumn> ColumnSet { get; }

          IDictionary<int, IDice> DiceSet { get; }

          IList<IPool> WhitePoolsSet { get; }

          IList<IPool> BlackPoolsSet { get; }

          //IList<IPool> BeatenWhitePoolList { get; set; }

          //IList<IPool> BeatenBlackPoolList { get; set; }

          IDictionary<int, int> DiceValueAndMovesCount { get; set; }
     }
}