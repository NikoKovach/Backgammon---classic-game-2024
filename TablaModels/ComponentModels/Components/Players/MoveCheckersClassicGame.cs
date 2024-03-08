namespace TablaModels.ComponentModels.Components.Players
{
     using System.Collections.Generic;

     using TablaModels.ComponentModels.Components.Interfaces;
     using TablaModels.ComponentModels.Enums;

     public class MoveCheckersClassicGame : IMoveChecker
     {
          private IDictionary<int, IColumn> columns;

          public MoveCheckersClassicGame(IDictionary<int, IColumn> columnSet)
          {
              this.columns = columnSet;
          }

          public void Inside(int columnNumber, IPool pool)
          {
               HitAnEnemyChecker(columnNumber, pool.PoolColor,columns);

               pool.State = PoolState.InGame;
               columns[columnNumber].PoolStack.Push(pool);
          }

          public void Move(int columnNumber, int positions)
          {          
              IPool currentPool = columns[columnNumber].PoolStack.Pop();

              int targetColumnIndex = default;

              if (currentPool.PoolColor == PoolColor.Black)
              {
                  targetColumnIndex = columnNumber + positions;
              }
              else if (currentPool.PoolColor == PoolColor.White)
              {
                  targetColumnIndex = columnNumber - positions;
              }

              HitAnEnemyChecker(targetColumnIndex, currentPool.PoolColor,columns);

              columns[targetColumnIndex].PoolStack.Push(currentPool);

              ChangeCheckerStatusToAtHome(targetColumnIndex, currentPool);
          }

          public void Outside(int columnNumber)
          {
              IPool checkerGoOut  = columns[columnNumber].PoolStack.Pop();
              checkerGoOut.State = PoolState.OutOfGame;
          }

          private void HitAnEnemyChecker(int targetColumnIndex, PoolColor currentCheckerColor,IDictionary<int, IColumn> columns)
        {
            int targetColumnCheckersCount = columns[targetColumnIndex].PoolStack.Count;

            if (targetColumnCheckersCount == 1 )
            {
                PoolColor targetColumnColor = columns[targetColumnIndex].PoolStack.Peek().PoolColor;

                if (targetColumnColor != currentCheckerColor)
                {
                    IPool strikeChecker = columns[targetColumnIndex].PoolStack.Pop();
                    strikeChecker.State = PoolState.OnTheBar;
                }
            }
        }

          private void ChangeCheckerStatusToAtHome(int targetColumnIndex, IPool currentPool)
          {
               if (currentPool.PoolColor == PoolColor.Black  && targetColumnIndex > 18)
               {
                   currentPool.State = PoolState.AtHome;
               }

               if (currentPool.PoolColor == PoolColor.White && targetColumnIndex < 7)
               {
                   currentPool.State = PoolState.AtHome;
               }
          }
     }
}


