using System.Collections.Generic;
using TablaModels.Components.Interfaces;
using TablaModels.Enums;

namespace TablaModels.Components.Players
{
     public class ArrangeChipsCorkGame : IArrangeChips
     {
          public void ArrangeBlackChips( IDictionary<int, IColumn> columns, IList<IPool> blackChips )
          {
               for (int i = 0; i < blackChips.Count; i++)
               {
                    columns[12].PoolStack.Push(blackChips[i]);
                    blackChips[i].State = PoolState.InGame;
               }
          }

          public void ArrangeWhiteChips( IDictionary<int, IColumn> columns, IList<IPool> whiteChips )
          {
               for (int i = 0; i < whiteChips.Count; i++)
               {
                    columns[24].PoolStack.Push(whiteChips[i]);
                    whiteChips[i].State = PoolState.InGame;
               }
          }
     }
}
