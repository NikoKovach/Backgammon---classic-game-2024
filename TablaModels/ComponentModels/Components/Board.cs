namespace TablaModels.ComponentModels.Components
{
     using System;
     using System.Collections.Generic;

     using TablaModels.ComponentModels.Components.Interfaces;

     public class Board : IBoard
     {
          /// <summary>
          /// Keys in range [1 - 24]
          /// </summary>
          private readonly Dictionary<int, IColumn> columnSet;
          /// <summary>
          /// Keys in range [1 - 2]
          /// </summary>
          private readonly Dictionary<int, IDice> diceSet;
          private readonly List<IPool> whitePoolsSet;
          private readonly List<IPool> blackPoolsSet;

          public Board( Dictionary<int, IColumn> columns, Dictionary<int, IDice> dice, List<IPool> whitePools, List<IPool> blackPools )
          {
               CheckInnerParameters( columns, dice, whitePools, blackPools );

               this.columnSet = columns;

               this.diceSet = dice;

               this.whitePoolsSet = whitePools;

               this.blackPoolsSet = blackPools;
          }

          private void CheckInnerParameters( Dictionary<int, IColumn> columns, Dictionary<int, IDice> dice, List<IPool> whitePools, List<IPool> blackPools )
          {
               if ( columns == null )
               {
                    throw new ArgumentNullException( $"The argument {nameof( columns )} can not be      null !" );
               };

               if ( dice == null )
               {
                    throw new ArgumentNullException( $"The argument {nameof( dice )} can not be     null !" );
               };

               if ( whitePools == null )
               {
                    throw new ArgumentNullException( $"The argument {nameof( whitePools )} can not      be null !" );
               };

               if ( blackPools == null )
               {
                    throw new ArgumentNullException( $"The argument {nameof( blackPools )} can not      be null !" );
               };
          }

          public IDictionary<int, IColumn> ColumnSet => this.columnSet;

          public IDictionary<int, IDice> DiceSet => this.diceSet;

          public IDictionary<int, int> DiceValueAndMovesCount { get; set; }

          public IList<IPool> WhitePoolsSet => this.whitePoolsSet;

          public IList<IPool> BlackPoolsSet => this.blackPoolsSet;

          //public IList<IPool> BeatenWhitePoolList { get; set; }

          //public IList<IPool> BeatenBlackPoolList { get; set; }
    }
}
