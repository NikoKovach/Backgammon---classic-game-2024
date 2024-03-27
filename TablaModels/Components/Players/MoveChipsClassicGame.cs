namespace TablaModels.Components.Players
{
    using System.Collections.Generic;
    using TablaModels.Components.Interfaces;
    using TablaModels.Enums;

    public class MoveChipsClassicGame : IMoveChips
    {
        public void Inside(int columnNumber, IPool pool,IDictionary<int, IColumn> columns)
        {
            HitAnEnemyChip(columnNumber, pool.PoolColor, columns);

            pool.State = PoolState.InGame;
            columns[columnNumber].PoolStack.Push(pool);
        }

        public void Move(int columnNumber, int positions,IDictionary<int, IColumn> columns)
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

            HitAnEnemyChip(targetColumnIndex, currentPool.PoolColor, columns);

            columns[targetColumnIndex].PoolStack.Push(currentPool);

            ChangeChipStatusToAtHome(targetColumnIndex, currentPool);
        }

        public void Outside(int columnNumber,IDictionary<int, IColumn> columns)
        {
            IPool chipGoOut = columns[columnNumber].PoolStack.Pop();
            chipGoOut.State = PoolState.OutOfGame;
        }

//*****************************************************************
        private void HitAnEnemyChip(int targetColumnIndex, 
             PoolColor currentChipColor, IDictionary<int, IColumn> columns)
        {
            int targetColumnChipsCount = columns[targetColumnIndex].PoolStack.Count;

            if (targetColumnChipsCount == 1)
            {
                PoolColor targetColumnColor = columns[targetColumnIndex]
                                             .PoolStack.Peek().PoolColor;

                if (targetColumnColor != currentChipColor)
                {
                    IPool strikeChip = columns[targetColumnIndex].PoolStack.Pop();
                    strikeChip.State = PoolState.OnTheBar;
                }
            }
        }

        private void ChangeChipStatusToAtHome(int targetColumnIndex, IPool currentPool)
        {
            if (currentPool.PoolColor == PoolColor.Black && targetColumnIndex > 18)
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


