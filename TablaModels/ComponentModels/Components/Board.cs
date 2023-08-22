namespace TablaModels.ComponentModels.Components
{
    using System;
    using System.Collections.Generic;

    using TablaModels.ComponentModels.Components.Interfaces;

    public class Board : IBoard
    {
        private readonly Dictionary<int, IColumn> columnSet; // keys 1 - 24
        private readonly Dictionary<int, IDice> diceSet;     //keys  1 - 2
        private List<IPool> whitePoolsSet;
        private List<IPool> blackPoolsSet;

        public Board(Dictionary<int, IColumn> columns, Dictionary<int, IDice> dice, List<IPool> whitePools, List<IPool> blackPools)
        {
            CheckInnerParameters( columns,  dice,  whitePools, blackPools);

            this.columnSet = columns;

            this.diceSet = dice;

            this.whitePoolsSet = whitePools; 

            this.blackPoolsSet = blackPools;
        }

        private void CheckInnerParameters(Dictionary<int, IColumn> columns, Dictionary<int, IDice> dice, List<IPool> whitePools, List<IPool> blackPools)
        {
            if (columns == null )
            {
                throw new ArgumentNullException($"The argument {nameof(columns)} can not be null !");
            };

            if (dice == null )
            {
                throw new ArgumentNullException($"The argument {nameof(dice)} can not be null !");
            };

            if (whitePools == null )
            {
                throw new ArgumentNullException($"The argument {nameof(whitePools)} can not be null !");
            };

            if ( blackPools == null)
            {
                throw new ArgumentNullException($"The argument {nameof(blackPools)} can not be null !");
            };
        }

        public Dictionary<int, IColumn> ColumnSet
        {
            get => this.columnSet;
        }

        public Dictionary<int, IDice> DiceSet
        {
            get => this.diceSet;
        }

        public Dictionary<int, int> ValueOfDiceAndCountOfMoves { get; set; }

        public List<IPool> WhitePoolsSet
        {
            get => this.whitePoolsSet;
        }

        public List<IPool> BlackPoolsSet
        {
            get => this.blackPoolsSet;
        }
        
    }
}
