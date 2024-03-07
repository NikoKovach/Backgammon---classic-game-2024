namespace TablaModels.ComponentModels.Components.Players
{
     using System.Collections.Generic;

     using TablaModels.ComponentModels.Components.Interfaces;
     using TablaModels.ComponentModels.Enums;

     public class MoveCheckersClassicGame : IMoveChecker
     {
          private Dictionary<int, IColumn> columns;

          public MoveCheckersClassicGame(Dictionary<int, IColumn> columnSet)
          {
              this.columns = columnSet;
          }

          public void Inside(int columnNumber, IPool pool)
          {
               HitAnEnemyChecker(columnNumber, pool.PoolColor);

               pool.State = PoolState.InGame;
               this.columns[columnNumber].PoolStack.Push(pool);
          }

        public void Move(int columnNumber, int positions)
        {          
            IPool currentPool = this.columns[columnNumber].PoolStack.Pop();

            int targetColumnIndex = default;

            if (currentPool.PoolColor == PoolColor.Black)
            {
                targetColumnIndex = columnNumber + positions;
            }
            else if (currentPool.PoolColor == PoolColor.White)
            {
                targetColumnIndex = columnNumber - positions;
            }

            HitAnEnemyChecker(targetColumnIndex, currentPool.PoolColor);

            this.columns[targetColumnIndex].PoolStack.Push(currentPool);

            ChangeCheckerStatusToAtHome(targetColumnIndex, currentPool);
        }

        public void Outside(int columnNumber)
        {
            IPool checkerGoOut  = this.columns[columnNumber].PoolStack.Pop();
            checkerGoOut.State = PoolState.OutOfGame;
        }

        private void HitAnEnemyChecker(int targetColumnIndex, PoolColor currentCheckerColor)
        {
            int targetColumnCheckersCount = this.columns[targetColumnIndex].PoolStack.Count;

            if (targetColumnCheckersCount == 1 )
            {
                PoolColor targetColumnColor = this.columns[targetColumnIndex].PoolStack.Peek().PoolColor;

                if (targetColumnColor != currentCheckerColor)
                {
                    IPool strikeChecker = this.columns[targetColumnIndex].PoolStack.Pop();
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


