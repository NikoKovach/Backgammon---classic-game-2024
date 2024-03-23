namespace TablaGameLogic.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using TablaGameLogic.Services.Contracts;
    using TablaModels.Components.Interfaces;
    using TablaModels.Enums;
    using static TablaGameLogic.Utilities.Messages.GameConstants;

    public class ValidateInsideHasMove : ValidateInside,IHasMoves
     {
          public  bool HasMoves( IBoard board,IPlayer player )
          {
               base.SetBoard( board );
               base.SetColor( player.MyPoolsColor );
               base.SetColumns( board.ColumnSet );

               if ( !ChipsOnTheBar() ) return false;

               if ( !CaseHasChipsOnTheBar() )  return false;

               return true;
          }
//****************************************************************
          private bool CaseHasChipsOnTheBar( )
          {
               IDictionary<int,int> diceSet = this.Board.DiceValueAndMovesCount
                                             .Where(x => x.Value > 0)
                                             .ToDictionary(x => x.Key,x => x.Value);

               List<bool> hasAnyMoveList = new List<bool>();

               if ( this.Color == PoolColor.Black  )
               {
                    foreach ( var item in diceSet )
                    {
                         hasAnyMoveList.Add(ColumnIsNotLock( item.Key ));
                    }

                    if ( hasAnyMoveList.All(x => x == false) )
                    {
                         return false;
                    }
               }

               if ( this.Color == PoolColor.White  )
               {
                    foreach ( var item in diceSet )
                    {
                         hasAnyMoveList.Add(ColumnIsNotLock( ColNumberTwentyFour + ColNumberOne - item.Key ));
                    }

                    if ( hasAnyMoveList.All(x => x == false) )
                    {
                         return false;
                    }
               }

               return true;
          }
     }
}
