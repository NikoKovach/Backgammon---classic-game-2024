namespace TablaGameLogic.Factory
{
     using System;
     using System.Collections.Generic;

     using TablaGameLogic.Factory.Contracts;
     using TablaGameLogic.Utilities.Messages;
     using TablaModels.Components;
     using TablaModels.Components.Interfaces;
     using TablaModels.Enums;
     using TablaModels.ModelsUtilities;

     public class BoardFactory : IBoardFactory
     {
          public IBoard Create()
          {
               IDictionary<int,IColumn> columns = CreateColumns();
               
               IDictionary<int, IDice> diceSet = CreateDice();
               
               IList<IPool> whitePools = CreatePools(PoolColor.White);
               
               IList<IPool> blackPools = CreatePools(PoolColor.Black);
               
               IBoard board = new Board( columns, diceSet, whitePools, blackPools );
               
               board.DiceValueAndMovesCount = new Dictionary<int, int>();

               return board;
          }

          public IBoard Create( IDictionary<int, IColumn> columns, IDictionary<int, IDice> diceSet, IList<IPool> whitePools, IList<IPool> blackPools )
          {
               CheckInnerParametersForNullValue(columns,diceSet,whitePools,blackPools);

               IBoard board = new Board( columns, diceSet, whitePools, blackPools );
               
               board.DiceValueAndMovesCount = new Dictionary<int, int>();

               return board;
          }

          private IDictionary<int, IColumn> CreateColumns()
          {
               Dictionary<int, IColumn> dictionaryOfColumns = 
                                        new Dictionary<int, IColumn> ();

               string currentColumnColor = GameConstants.ColumnColorDark;

               for (int i = 1; i <= GameConstants.ColNumberTwentyFour; i++)
               {
                   currentColumnColor = currentColumnColor == GameConstants.ColumnColorDark      
                            ? GameConstants.ColumnColorLight 
                            : GameConstants.ColumnColorDark;           

                   dictionaryOfColumns[i] = new Column(i, currentColumnColor);
               }

               return dictionaryOfColumns;
          }

          private IDictionary<int, IDice> CreateDice()
          {
               Dictionary<int, IDice> dictionaryOfDice = new Dictionary<int, IDice>();

               for (int i = 1; i <= GameConstants.NumberOfDice; i++)
               {
                   dictionaryOfDice[i] = new Dice($"Dice{i}",i + 1, new BoardSettings      ().DiceSide);
               }

               return dictionaryOfDice;
          }

          private IList<IPool> CreatePools(PoolColor colorOfPools)
          {
               List<IPool> pools = new List<IPool>();

               for (int i = 1; i <= GameConstants.MaxPoolsNumber; i++)
               {
                   IPool pool = new Pool(i,colorOfPools,PoolState.Starting);
                   
                   pools.Add(pool);
               }

               return pools;
          }

          private void CheckInnerParametersForNullValue
               ( IDictionary<int, IColumn> columns, IDictionary<int, 
                IDice> diceSet, IList<IPool> whitePools, IList<IPool> blackPools )
          {
               ArgumentNullException.ThrowIfNull(columns);

               ArgumentNullException.ThrowIfNull(diceSet);
               
               ArgumentNullException.ThrowIfNull(whitePools);

               ArgumentNullException.ThrowIfNull(blackPools);
          }
     }
}
