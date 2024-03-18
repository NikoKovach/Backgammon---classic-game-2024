using System;
using System.Collections.Generic;
using System.Text;
using TablaModels.ComponentModels.Components.Interfaces;
using TablaModels.ComponentModels.Enums;

namespace TablaConsoleGame
{

     public class ArrangeOutsideScheme : IArrangeChips
     {
          public void ArrangeBlackChips( IDictionary<int, IColumn> columns, IList<IPool> blackCheckers )
          {
               /*
                    2 chips on 20st column
                    4 chips on 22th column
                    3 chips on 23th column
                    3 chips on 24th column

                    1 chip   - OutOfGame      1 chip on 17th column InGame
            */

               for (int i = 0; i < blackCheckers.Count; i++)
               {
                    if ( columns[ 1 ].PoolStack.Count < 2 )
                    {
                         columns[ 1 ].PoolStack.Push( blackCheckers[ i ] );
                         blackCheckers[ i ].State = PoolState.InGame;
                         continue;
                    }

                    if (columns[20].PoolStack.Count < 2)
                    {
                        columns[20].PoolStack.Push(blackCheckers[i]);
                        blackCheckers[i].State = PoolState.AtHome;
                        continue;
                    }

                    if (columns[22].PoolStack.Count < 4)
                    {
                        columns[22].PoolStack.Push(blackCheckers[i]);
                        blackCheckers[i].State = PoolState.AtHome;
                        continue;
                    }

                    if (columns[23].PoolStack.Count < 3)
                    {
                        columns[19].PoolStack.Push(blackCheckers[i]);
                        blackCheckers[i].State = PoolState.AtHome;
                        continue;
                    }

                    if (columns[24].PoolStack.Count < 3)
                    {
                        columns[19].PoolStack.Push(blackCheckers[i]);
                        blackCheckers[i].State = PoolState.AtHome;
                        continue;
                    }
               } 

               //columns[17].PoolStack.Push(blackCheckers[14]);
               blackCheckers[14].State = PoolState.OutOfGame;
          }

          public void ArrangeWhiteChips( IDictionary<int, IColumn> columns, IList<IPool> whiteCheckers )
          {
               /*
                    3 chips on 5th column   
                    4 chips on 3th column       OutOfGame = 2 chip
                    4 chips on 2th column        
                    2 chips on 1th column
               */

               for (int i = 0; i < whiteCheckers.Count; i++)
               {
                    

                    if (columns[5].PoolStack.Count < 3)
                    {
                        columns[5].PoolStack.Push(whiteCheckers[i]);
                        whiteCheckers[i].State = PoolState.AtHome;
                        continue;
                    }

                    if (columns[3].PoolStack.Count < 4)
                    {
                        columns[3].PoolStack.Push(whiteCheckers[i]);
                        whiteCheckers[i].State = PoolState.AtHome;
                        continue;
                    }

                    if (columns[2].PoolStack.Count < 4)
                    {
                        columns[2].PoolStack.Push(whiteCheckers[i]);
                        whiteCheckers[i].State = PoolState.AtHome;
                        continue;
                    }

                    //if (columns[1].PoolStack.Count < 2)
                    //{
                    //    columns[1].PoolStack.Push(whiteCheckers[i]);
                    //    whiteCheckers[i].State = PoolState.AtHome;
                    //    continue;
                    //}
               }

               //columns[8].PoolStack.Push(whiteCheckers[13]);
               whiteCheckers[ 13 ].State = PoolState.OutOfGame;

               whiteCheckers[ 14 ].State = PoolState.OutOfGame;
          }
     }
}

/*
                    //if (columns[1].PoolStack.Count < 2)
                    //{
                    //    columns[1].PoolStack.Push(blackCheckers[i]);
                    //    blackCheckers[i].State = PoolState.InGame;
                    //    continue;
                    //}

                    //if (columns[12].PoolStack.Count < 5)
                    //{
                    //    columns[12].PoolStack.Push(blackCheckers[i]);
                    //    blackCheckers[i].State = PoolState.InGame;
                    //    continue;
                    //}

                    //if (columns[17].PoolStack.Count < 2)
                    //{
                    //    columns[17].PoolStack.Push(blackCheckers[i]);
                    //    blackCheckers[i].State = PoolState.InGame;
                    //    continue;
                    //}

                    //if (columns[19].PoolStack.Count < 3)
                    //{
                    //    columns[19].PoolStack.Push(blackCheckers[i]);
                    //    blackCheckers[i].State = PoolState.AtHome;
                    //    continue;
                    //} 
 
 
 */