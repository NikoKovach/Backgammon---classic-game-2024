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

               if ( !base.ChipsOnTheBar() ) return false;

               if ( !CaseHasChipsOnTheBar() )  return false; // Test

               return true;
          }
//****************************************************************
          private bool CaseHasChipsOnTheBar( )
          {
               IDictionary<int,int> diceSet = base.GetDiceSet();

               List<bool> hasAnyMoveList = new List<bool>();

               foreach ( var item in diceSet )
               {
                    int colNumber = GetColNumber( item.Key );

                    hasAnyMoveList.Add(base.ColumnIsNotLock( colNumber ));
               }

               if ( hasAnyMoveList.All(x => x == false) )
               {
                    return false;
               }

               return true;
          }

          private int GetColNumber(int diceValue)
          {
               int colNumber = default;

               if ( this.Color == PoolColor.Black  )
               {
                    colNumber = diceValue;
               }
               else if ( this.Color == PoolColor.White  )
               {
                    colNumber = ColNumberTwentyFour + ColNumberOne - diceValue;
               }

               return colNumber;
          }
     }
}

