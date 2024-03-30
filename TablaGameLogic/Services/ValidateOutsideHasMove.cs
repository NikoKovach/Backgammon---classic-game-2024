namespace TablaGameLogic.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using TablaGameLogic.Services.Contracts;
    using TablaModels.Components.Interfaces;
    using TablaModels.Enums;
    using static TablaGameLogic.Utilities.Messages.GameConstants;

    public class ValidateOutsideHasMove : ValidateOutside, IHasMoves
     {
          public bool HasMoves( IBoard board, IPlayer player )
          {
               base.SetBoard( board );
               base.SetColor( player.MyPoolsColor );
               base.SetColumns( board.ColumnSet );

               if ( base.ChipsOnTheBar() ) return false;

               if ( this.ChipsInGame() ) return false;

               if ( !CaseHasMotions( player) ) return false;

               return true;
          }

//***********************************************************
          private bool CaseHasMotions(IPlayer player)
          {
               IDictionary<int, int> diceSet = base.GetDiceSet();
               
               List<int> targetColumnNumbers = ( this.Color == PoolColor.White )
                    ? diceSet.Select( x => x.Key ).ToList()
                    : diceSet.Select( x => ColNumberTwentyFour + 1 - x.Key).ToList();

               List<bool> hasRightMoveList = new List<bool>();

               foreach ( var colNumber in targetColumnNumbers )
               {
                    int chipCount = this.Columns[ colNumber ].PoolStack.Count;

                    bool colIsNotLock = base.ColumnIsNotLock( colNumber );

                    if ( chipCount == 0 || colIsNotLock == false )
                    {
                         hasRightMoveList.Add(HasHigherPositionWithSameChip(colNumber));  
                    }
               }

               if ( hasRightMoveList.Any( x => x == true ) )
               {
                    bool result = new ValidateMotionHasMove().HasMoves(this.Board,player);

                    if ( !result ) return false; 
               }

               return true;
          }

          private bool HasHigherPositionWithSameChip( int colNumber )
          {
               List<IColumn> colSet = new List<IColumn>();

               List<int> rangeOfCol = ( this.Color == PoolColor.White )
                             ? this.Columns
                                   .Where( x => x.Key > colNumber &&
                                           x.Key <= ColNumberSix )
                                   .Select( x => x.Key )
                                   .OrderBy( x => x )
                                   .ToList()
                             : this.Columns
                                   .Where( x => x.Key >= ColNumberNineteen &&
                                                x.Key < colNumber )
                                   .Select( x => x.Key )
                                   .OrderBy( x => x )
                                   .ToList();

               foreach ( var item in rangeOfCol )
               {
                    colSet.Add( this.Columns[item] );
               }

               return colSet.Any( x => x.PoolStack.Count > 0 &&
                               x.PoolStack.Peek().PoolColor == this.Color );
          }
     }
}

//IDictionary<int, int> diceSet =
//this.Board.DiceValueAndMovesCount
// .Where(x => x.Value > 0)
// .ToDictionary(x => x.Key,x => x.Value);

//List<int> rangeOfCol = default;

//if ( this.Color == PoolColor.White )
//{
//     rangeOfCol = this.Columns
//                      .Where( x => x.Key > colNumber &&
//                              x.Key <= ColNumberSix )
//                      .Select( x => x.Key)
//                      .OrderBy( x => x )
//                      .ToList();  

//}

//if ( this.Color == PoolColor.Black )
//{
//     rangeOfCol = this.Columns.Where( x => x.Key >= ColNumberNineteen &&
//                                           x.Key < colNumber )
//                              .Select( x => x.Key )
//                              .OrderBy( x => x )
//                              .ToList();
//}