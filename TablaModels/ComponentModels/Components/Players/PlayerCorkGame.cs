namespace TablaModels.ComponentModels.Components.Players
{
    using System;
    using System.Collections.Generic;
    using TablaModels.ComponentModels.Components.Interfaces;
    using TablaModels.ComponentModels.Enums;

    using static TablaModels.ModelsUtilities.Messages.ExceptionMessages;

    public class PlayerCorkGame : PlayerClassicGame, IPlayer
    {
        public PlayerCorkGame(string name)
           : base(name)
        {
        }

        public override void ArrangingTheCheckers(IBoard board,IArrangeChips arrangeChips)
        {
            if (board == null)
            {
                throw new ArgumentNullException(string.Format(InvalidBoardParameter, nameof(board)));
            }

            if (this.MyPoolsColor == PoolColor.White)
            {
                ArrangeWhitePools(board.ColumnSet[24], board.WhitePoolsSet);
            }
            else
            {
                ArrangeBlackPools(board.ColumnSet[12], board.BlackPoolsSet);
            };
        }

        private void ArrangeWhitePools(IColumn column,IList<IPool> whiteCheckers)
        {
            for (int i = 0; i < whiteCheckers.Count; i++)
            {
                column.PoolStack.Push(whiteCheckers[i]);
                whiteCheckers[i].State = PoolState.InGame;
            }
        }

        private void ArrangeBlackPools(IColumn column, IList<IPool> blackCheckers)
        {
            for (int i = 0; i < blackCheckers.Count; i++)
            {
                column.PoolStack.Push(blackCheckers[i]);
                blackCheckers[i].State = PoolState.InGame;
            }
        }
    }
}
