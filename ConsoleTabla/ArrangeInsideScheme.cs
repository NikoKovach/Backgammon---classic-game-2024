using System;
using System.Collections.Generic;
using System.Text;
using TablaModels.Components.Interfaces;
using TablaModels.Enums;

namespace TablaConsoleGame
{

     public class ArrangeInsideScheme : IArrangeChips
     {
          public void ArrangeBlackChips( IDictionary<int, IColumn> columns, IList<IPool> blackCheckers )
          {
               /*
                * Black
                    2 checkers on 1st column
                    5 checkers on 12th column
                    2 checkers on 17th column

                    2 checkers on 19th column
                    2 checkers on 20th column
                                                       2 checkers on 22th column
               */
               /*
                *White
                    2 chips on 24th column
                    4 chips on 13th column       
                    3 chips on 8th column
                    4 chips on 6th column       beatenWhiteChips= 2 chip
               */
               for (int i = 0; i < blackCheckers.Count; i++)
               {

                    if (columns[1].PoolStack.Count < 2)
                    {
                        columns[1].PoolStack.Push(blackCheckers[i]);
                        blackCheckers[i].State = PoolState.InGame;
                        continue;
                    }

                    if (columns[12].PoolStack.Count < 5)
                    {
                        columns[12].PoolStack.Push(blackCheckers[i]);
                        blackCheckers[i].State = PoolState.InGame;
                        continue;
                    }

                    if (columns[17].PoolStack.Count < 2)
                    {
                        columns[17].PoolStack.Push(blackCheckers[i]);
                        blackCheckers[i].State = PoolState.InGame;
                        continue;
                    }

                    if (columns[19].PoolStack.Count < 2)
                    {
                        columns[19].PoolStack.Push(blackCheckers[i]);
                        blackCheckers[i].State = PoolState.AtHome;
                        continue;
                    }

                    if (columns[20].PoolStack.Count < 2)
                    {
                        columns[20].PoolStack.Push(blackCheckers[i]);
                        blackCheckers[i].State = PoolState.AtHome;
                        continue;
                    }

                    //if ( columns[ 22 ].PoolStack.Count < 2 )
                    //{
                    //     columns[ 22 ].PoolStack.Push( blackCheckers[ i ] );
                    //     blackCheckers[ i ].State = PoolState.AtHome;
                    //     continue;
                    //}
               }
               blackCheckers[ 13 ].State = PoolState.OnTheBar;
               blackCheckers[ 14 ].State = PoolState.OnTheBar;
          }

          public void ArrangeWhiteChips( IDictionary<int, IColumn> columns, IList<IPool> whiteCheckers )
          {
               /*
                    2 chips on 24th column
                    4 chips on 13th column       
                    3 chips on 8th column
                    4 chips on 6th column       beatenWhiteChips= 2 chip
               */

               for (int i = 0; i < whiteCheckers.Count; i++)
               {
                    if (columns[6].PoolStack.Count < 4)
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

                    //whiteCheckers[ i ].State = PoolState.OnTheBar;
               }
               whiteCheckers[ 13 ].State = PoolState.OnTheBar;
               whiteCheckers[ 14 ].State = PoolState.OnTheBar;

          }
     }
}
