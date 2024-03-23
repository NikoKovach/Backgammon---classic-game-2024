namespace TablaModels.Components
{
    using System;
    using System.Collections.Generic;
    using TablaModels.Components.Interfaces;
    using TablaModels.ModelsUtilities.Messages;

    public class Board : IBoard
    {
        /// <summary>
        /// Keys in range [1 - 24]
        /// </summary>
        private readonly IDictionary<int, IColumn> columnSet;

        /// <summary>
        /// Keys in range [1 - 2]
        /// </summary>
        private readonly IDictionary<int, IDice> diceSet;
        private readonly IList<IPool> whitePoolsSet;
        private readonly IList<IPool> blackPoolsSet;

        public Board(IDictionary<int, IColumn> columns, IDictionary<int, IDice> dice, IList<IPool> whitePools, IList<IPool> blackPools)
        {
            CheckInnerParameters(columns, dice, whitePools, blackPools);

            columnSet = columns;

            diceSet = dice;

            whitePoolsSet = whitePools;

            blackPoolsSet = blackPools;
        }

        public IDictionary<int, IColumn> ColumnSet => columnSet;

        public IDictionary<int, IDice> DiceSet => diceSet;

        public IDictionary<int, int> DiceValueAndMovesCount { get; set; }

        public IList<IPool> WhitePoolsSet => whitePoolsSet;

        public IList<IPool> BlackPoolsSet => blackPoolsSet;

        private void CheckInnerParameters(IDictionary<int, IColumn> columns, IDictionary<int, IDice> dice, IList<IPool> whitePools, IList<IPool> blackPools)
        {
            if (columns == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionMessages.BoardArgumentNullException, nameof(columns)));
            };

            if (dice == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionMessages.BoardArgumentNullException, nameof(dice)));
            };

            if (whitePools == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionMessages.BoardArgumentNullException, nameof(whitePools)));
            };

            if (blackPools == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionMessages.BoardArgumentNullException, nameof(blackPools)));
            };
        }

    }
}
