using System;
using System.Collections.Generic;
using System.Text;
using TablaModels.ComponentModels.Components.Interfaces;
using TablaModels.ComponentModels.Enums;

namespace TablaConsoleGame
{

     public class ArrangeNoMoveScheme : IArrangeChips
     {
          public void ArrangeBlackChips( IDictionary<int, IColumn> columns, IList<IPool> blackCheckers )
          {
               /*
                    2 checkers on 1st column
                    5 checkers on 12th column
                    3 checkers on 17th column
                    5 checkers on 19th column 
            */

               for (int i = 0; i < blackCheckers.Count; i++)
               {
                    if (columns[19].PoolStack.Count < 5)
                    {
                        columns[19].PoolStack.Push(blackCheckers[i]);
                        blackCheckers[i].State = PoolState.AtHome;
                        continue;
                    }

                    if (columns[17].PoolStack.Count < 3)
                    {
                        columns[17].PoolStack.Push(blackCheckers[i]);
                        blackCheckers[i].State = PoolState.InGame;
                        continue;
                    }

                    if (columns[12].PoolStack.Count < 5)
                    {
                        columns[12].PoolStack.Push(blackCheckers[i]);
                        blackCheckers[i].State = PoolState.InGame;
                        continue;
                    }

                    if (columns[1].PoolStack.Count < 2)
                    {
                        columns[1].PoolStack.Push(blackCheckers[i]);
                        blackCheckers[i].State = PoolState.InGame;
                        continue;
                    }
               } 
          }

          public void ArrangeWhiteChips( IDictionary<int, IColumn> columns, IList<IPool> whiteCheckers )
          {
               /*
                    2 checkers on 24th column
                    4 CHIPS on 13th column        beatenWhiteChips = 1 chip
                    3 checkers on 8th column
                    5 checkers on 6th column
               */

               for (int i = 0; i < whiteCheckers.Count; i++)
               {
                    if (columns[6].PoolStack.Count < 5)
                    {
                        columns[6].PoolStack.Push(whiteCheckers[i]);
                        whiteCheckers[i].State = PoolState.AtHome;
                        continue;
                    }

                    if (columns[8].PoolStack.Count < 3)
                    {
                        columns[8].PoolStack.Push(whiteCheckers[i]);
                        whiteCheckers[i].State = PoolState.InGame;
                        continue;
                    }

                    if (columns[13].PoolStack.Count < 4)
                    {
                        columns[13].PoolStack.Push(whiteCheckers[i]);
                        whiteCheckers[i].State = PoolState.InGame;
                        continue;
                    }

                    if (columns[24].PoolStack.Count < 2)
                    {
                        columns[24].PoolStack.Push(whiteCheckers[i]);
                        whiteCheckers[i].State = PoolState.InGame;
                        continue;
                    }                   
               } 
               
               whiteCheckers[ 14 ].State = PoolState.OnTheBar;
          }
     }
}
