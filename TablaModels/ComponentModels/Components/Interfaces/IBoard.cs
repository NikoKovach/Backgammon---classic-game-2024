namespace TablaModels.ComponentModels.Components.Interfaces
{
    using System.Collections.Generic;

    public interface IBoard
    {
        Dictionary<int, IColumn> ColumnSet { get; }

        Dictionary<int, IDice> DiceSet { get; }

        List<IPool> WhitePoolsSet { get; }

        List<IPool> BlackPoolsSet { get; }

        Dictionary<int, int> ValueOfDiceAndCountOfMoves { get; set; }
    }
}