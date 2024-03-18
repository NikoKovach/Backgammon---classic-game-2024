namespace TablaGameLogic.Services
{
     using System.Collections.Generic;
     using System.Linq;
     using TablaGameLogic.Services.Contracts;
     using TablaModels.ComponentModels.Components.Interfaces;

     public class ValidateMotionHasMove : ValidateMotion,IHasMoves
     {
          public  bool HasMoves( IBoard board,
               IPlayer player )
          {
               base.SetBoard( board );
               base.SetColor( player.MyPoolsColor );
               base.SetColumns( board.ColumnSet );

               if ( base.ChipsOnTheBar() ) return false;

               if ( !CaseHasMotions( ) ) return false;

               return true;
          }
//**********************************************************************
          private bool CaseHasMotions( )
          {
               IList<IColumn> columnSet = this.Columns
                    .Where(x => x.Value.PoolStack.Count > 0 && 
                                x.Value.PoolStack.Peek().PoolColor == this.Color)
                    .Select(x => x.Value)
                    .OrderBy(x => x.IdentityNumber)
                    .ToList();

               IList<int> diceSet = this.Board.DiceValueAndMovesCount
                                 .Where( x => x.Value > 0 )
                                 .Select(x => x.Key).ToList();

               List<bool> movesTypeList = new List<bool>();

               foreach ( var col in columnSet )
               {
                    foreach ( var dice in diceSet )
                    {
                         int mainTargetCol = 
                              this.CalculateTargetColumn( dice, col.IdentityNumber );

                         movesTypeList.Add(TargetColumnIsValid(mainTargetCol));
                    }
               }

               if ( movesTypeList.All( x => x == false ) )
                    return false;

               return true;
          }
     }
}

          //private bool TheBlacksHasMotions( IList<IColumn> columnSet, 
          //                                  IList<int> diceSet )
          //{
          //     List<bool> movesTypeList = new List<bool>();

          //     foreach ( var col in columnSet )
          //     {
          //          foreach ( var dice in diceSet )
          //          {
          //               int mainTargetCol = 
          //                    this.CalculateTargetColumn( dice, col.IdentityNumber );

          //               movesTypeList.Add(TargetColumnIsValid(mainTargetCol));
          //          }
          //     }

          //     if ( movesTypeList.All( x => x == false ) )
          //          return false;

          //     return true;
          //}