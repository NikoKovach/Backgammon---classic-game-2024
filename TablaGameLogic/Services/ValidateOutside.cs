namespace TablaGameLogic.Services
{
     using System.Collections.Generic;
     using System.Linq;
     using TablaGameLogic.Services.Contracts;
     using TablaModels.ComponentModels.Components.Interfaces;
     using TablaModels.ComponentModels.Enums;

     using static TablaGameLogic.Utilities.Messages.GameConstants;

     public class ValidateOutside : ValidateBase, IValidateMove
     {
          public override bool MoveIsCorrect( IMoveParameters motion,IBoard board, 
               IPlayer player)
          {
               base.SetBoard( board );
               base.SetColor(player.MyPoolsColor);
               base.SetColumns( board.ColumnSet );
               base.SetMotionParams( motion );

               if ( base.ChipsOnTheBar()) return false;

               if ( base.ChipsInGame()) return false;

               if ( !base.ColumnIsPartOfTheBoard(MotionParams.ColumnNumber)) 
                    return false;

               if ( !this.TargetColumnIsValidAgainstColor()) return false;

               if ( !this.TargetColumnIsValidAgainstPoolCount() ) return false;

               return true;
          }

 //*****************************************************************************

          private bool TargetColumnIsValidAgainstColor()
          {
               List<int> columnsRange = GetColumnsRange();

               if ( !columnsRange.Any( x => x == MotionParams.ColumnNumber ) )
                    return false;

               return true;
          }

          protected bool TargetColumnIsValidAgainstPoolCount()
          {
               if ( !base.ColumnIsNotLock(MotionParams.ColumnNumber) ) return false;

               int chipsCount = this.Columns[ MotionParams.ColumnNumber ].PoolStack.Count;

               if ( chipsCount == 0 )  return false;

               if ( !DiceExist() ) return false;

               return true;
          }

          private bool DiceExist()
          {
               int fakeDice = (this.Color == PoolColor.White) 
                    ? MotionParams.ColumnNumber 
                    : ColNumberTwentyFour + 1 - MotionParams.ColumnNumber ;

               bool diceExist = this.Board.DiceValueAndMovesCount.ContainsKey( fakeDice );

               if ( !diceExist )
               {
                    if ( !ChechSpecialVariant(fakeDice) ) return false;
               }
               else
               {
                    this.MotionParams.UseDiceMotionCount.Add(fakeDice);
               }

               return true;
          }

          private bool ChechSpecialVariant(int fakeDice)
          {
               //Пример :
               //вземаме от колона 5.На колона5 има пулове .
               //Имаме зарове по-големи от 5 - 6 и 4. 
               // За да е валидно колони от 6 - 6 вкл. да са без пулове от съответния цвят

               List<int> colNumberRange = ( this.Color == PoolColor.White )     ?
                    this.Columns.Where( x => x.Key > fakeDice && x.Key <= ColNumberSix )
                                .Select( x => x.Key )
                                .OrderBy(x => x)
                                .ToList()                                       :
                    this.Columns.Where( x => x.Key >= ColNumberNineteen && x.Key < 
                                        (ColNumberTwentyFour + 1 - fakeDice ))
                                .Select( x => x.Key)
                                .OrderBy(x => x)
                                .ToList();

               bool diceExist = default;

               if ( ( this.Color == PoolColor.White ) )
               {
                    //colrange - 1,2,3....
                    diceExist = this.Board.DiceValueAndMovesCount
                              .Any( x => x.Key >= colNumberRange.Min() && 
                                         x.Key <= colNumberRange.Max() );
               }
               else
               {
                    //colrange - 19,20,21....
                    //            6, 5, 4....
                    diceExist = this.Board.DiceValueAndMovesCount
                             .Any( x => 
                              x.Key >= ColNumberTwentyFour + 1 - colNumberRange.Max() && 
                              x.Key <= ColNumberTwentyFour + 1 - colNumberRange.Min() );
               }

               if ( !diceExist ) return false;

               //ChechSpecialVariant ще връща  true : когато
               //diceExist == true  и колоните в дадение обхват нямат пулове със същия цвят
               bool columnWithChipsExist = this.Columns
                   .Where( x => x.Key >= colNumberRange.Min() && x.Key <= colNumberRange.Max() )
                   .Any(x => x.Value.PoolStack.Count > 0 && 
                             x.Value.PoolStack.Peek().PoolColor == this.Color);

               if ( diceExist && columnWithChipsExist ) return false;

               ChangeUseDiceMotionCount( colNumberRange );

               return true;
          }

          private void ChangeUseDiceMotionCount( List<int> colNumberRange )
          {
               int diceNumber = (this.Color == PoolColor.White)
                              ? this.Board.DiceValueAndMovesCount
                                    .First( x => x.Key >= colNumberRange.Min() &&
                                                 x.Key <= colNumberRange.Max() ).Key   
                             :  this.Board.DiceValueAndMovesCount
                                    .First(x => x.Key >= 24 + 1 - colNumberRange.Max() && 
                                                x.Key <= 24 + 1 - colNumberRange.Min()).Key;
               
               this.MotionParams.UseDiceMotionCount.Add(diceNumber);

          }

          protected List<int> GetColumnsRange()
          {
               List<int> columnsRange = (this.Color == PoolColor.White) 
               ?    this.Columns.Where( x => x.Key >= ColNumberOne && 
                                             x.Key <= ColNumberSix )
                    .Select( x => x.Key ).ToList()
               :    this.Columns.Where( x => x.Key >= ColNumberNineteen && 
                                             x.Key <= ColNumberTwentyFour )
                    .Select( x => x.Key ).ToList();

               return columnsRange;
          }
     }
}

/*
 ИЗКАРВАНЕ НА ПУЛОВЕ
    Ако няма пул на позиция отговаряща на зара,
    играчът трябва да направи легално движение на пул, 
    намиращ се на по-висока позиция. 
    Ако няма пулове на по-висока позиция,играчът е длъжен да извади пул
    намиращ се на най-високата възможна позиция, по-ниска от стойността на зара. 
    Ако играчът има пул на позиция за изкарване, 
    но има и възможно движение, той не е длъжен да изкарва пула.

//"Please enter your move type in following format :"         + NewRow + 
//"2.For 'Outside' - ( 2 ) (column number);"        + NewRow + 
//"3.For 'Move'    - ( 3 ) (column number) (places to move);";
*/
/*
 *        //protected bool ChipsInGame()
          //{
          //     var chipsSet = Color == PoolColor.White ?
          //          Board.WhitePoolsSet : Board.BlackPoolsSet;

          //     return chipsSet.Any( x => x.State == PoolState.InGame );
          //}

List<int> whiteColumnsRange = this.Columns
     .Where( x => x.Key >= 1 && x.Key <= 6 ).Select( x => x.Key ).ToList();
               
List<int> blackColumnsRange = this.Columns
     .Where( x => x.Key >= 19 && x.Key <= 24 ).Select( x => x.Key ).ToList();
 
 
               if ( this.Color == PoolColor.White )
               {
                    if ( !whiteColumnsRange.Any( x => x == MotionParams.ColumnNumber ) )
                    {
                         return false;
                    }
               }

               if ( this.Color == PoolColor.Black )
               {
                    if ( !blackColumnsRange.Any( x => x == MotionParams.ColumnNumber ) )
                    {
                         return false;
                    }
               }
 */
