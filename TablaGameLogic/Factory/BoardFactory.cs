namespace TablaGameLogic.Factory
{
    using System.Collections.Generic;

    using TablaGameLogic.Factory.Contracts;
    using TablaModels.ComponentModels;
    using TablaModels.ComponentModels.Components;
    using TablaModels.ComponentModels.Components.Interfaces;
    using TablaModels.ComponentModels.Enums;
    using static TablaModels.ComponentModels.TableGlobalConstants;

    public class BoardFactory : IBoardFactory
    {
          public IBoard Create()
          {
               Dictionary<int,IColumn> columns = CreateColumns();
               
               Dictionary<int, IDice> diceSet = CreateDice();
               
               List<IPool> whitePools = CreatePools(PoolColor.White);
               
               List<IPool> blackPools = CreatePools(PoolColor.Black);
               
               IBoard board = new Board( columns, diceSet, whitePools, blackPools );
               
               board.DiceValueAndMovesCount = new Dictionary<int, int>();

               return board;
          }

        private Dictionary<int, IColumn> CreateColumns()
        {
            Dictionary<int, IColumn> dictionaryOfColumns = new Dictionary<int, IColumn>();

            ColumnColor currentColumnColor = ColumnColor.Dark;

            for (int i = 1; i <= ColumnNumber; i++)
            {
                currentColumnColor = currentColumnColor == ColumnColor.Dark ? ColumnColor.Light : ColumnColor.Dark;           

                dictionaryOfColumns[i] = new Column(i, currentColumnColor);
            }

            return dictionaryOfColumns;
        }

        private Dictionary<int, IDice> CreateDice()
        {
            Dictionary<int, IDice> dictionaryOfDice = new Dictionary<int, IDice>();

            for (int i = 1; i <= 2; i++)
            {
                dictionaryOfDice[i] = new Dice($"Dice{i}",i + 1, new BoardSettings().DiceSide);
            }

            return dictionaryOfDice;
        }

        private List<IPool> CreatePools(PoolColor colorOfPools)
        {
            List<IPool> pools = new List<IPool>();

            for (int i = 1; i <= MaxPoolsNumber; i++)
            {
                IPool pool = new Pool(colorOfPools,PoolState.Starting,i);
                
                pools.Add(pool);
            }

            return pools;
        }
    }
}
