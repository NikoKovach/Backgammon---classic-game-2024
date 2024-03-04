namespace TablaModels.ComponentModels.Components.Players
{
     using System;
     using System.Collections.Generic;
     using TablaModels.ComponentModels.Components.Interfaces;
     using TablaModels.ComponentModels.Enums;

     using static TablaModels.ModelsUtilities.Messages.ExceptionMessages;

     public class PlayerClassicGame : Player , IPlayer
     {       
          public PlayerClassicGame(string name)
              :base(name)
          { }

          public PlayerClassicGame(string name,IMoveChecker moveChecker)
              :base(name,moveChecker)
          { }
        
          public override void ArrangingTheCheckers(IBoard board)
          {
               if (board == null )
               {
                   throw new ArgumentNullException(string.Format(InvalidBoardParameter,nameof       (board)));
               }
               
               if (this.MyPoolsColor == PoolColor.White)
               {
                   ArrangeWhitePools(board.ColumnSet,board.WhitePoolsSet);
               }
               else 
               {
                   ArrangeBlackPools(board.ColumnSet, board.BlackPoolsSet);
               };
          }

          private void ArrangeWhitePools(Dictionary<int,IColumn> columns,List<IPool> whiteCheckers)
        {
            /*
                    2 checkers on 24th column
                    5 checkers on 13th column
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

                if (columns[13].PoolStack.Count < 5)
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
        }     

          private void ArrangeBlackPools(Dictionary<int, IColumn> columns, List<IPool> blackCheckers)
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
    }
}
