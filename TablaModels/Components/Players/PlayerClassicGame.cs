namespace TablaModels.Components.Players
{
    using System;
    using TablaModels.Components.Interfaces;
    using TablaModels.Enums;
    using TablaModels.ModelsUtilities;
    using static TablaModels.ModelsUtilities.Messages.ExceptionMessages;

    public class PlayerClassicGame : IPlayer
    {
        public PlayerClassicGame(string name)
        {
            Name = name;

            State = PlayerState.NormalState;
        }

        public PlayerClassicGame(string name, IMoveChips moveChip)
           : this(name)
        {
            Move = moveChip;
        }

        public string Name { get; set; }

        public PoolColor MyPoolsColor { get; set; }

        public PlayerState State { get; set; }

        public IMoveChips Move { get; set; }

        public virtual void ArrangingTheCheckers(IBoard board, IArrangeChips arrangeChips)
        {
            if (board == null)
            {
                throw new ArgumentNullException(string.Format(InvalidBoardParameter, nameof(board)));
            }

            if (arrangeChips == null)
            {
                throw new ArgumentNullException(string.Format(InvalidBoardParameter, nameof(arrangeChips)));
            }

            if (MyPoolsColor == PoolColor.White)
            {
                arrangeChips.ArrangeWhiteChips(board.ColumnSet, board.WhitePoolsSet);
            }
            else
            {
                arrangeChips.ArrangeBlackChips(board.ColumnSet, board.BlackPoolsSet);
            };
        }

        public int RollADice()
        {
            Random rnd = new Random();

            int number = rnd.Next
                (TableGlobalConstants.MinDiceValue, TableGlobalConstants.MaxDiceValue + 1);

            return number;
        }
    }
}
