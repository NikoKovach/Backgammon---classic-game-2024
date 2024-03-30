namespace TablaGameLogic.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using TablaGameLogic.Services.Contracts;
    using TablaModels.Components.Interfaces;
    using TablaModels.Enums;

    public class ValidateMotion:ValidateBase,IValidateMove
     {
          public override bool MoveIsCorrect(IMoveParameters motion,
                                             IBoard board, IPlayer player)
          {
               base.SetBoard( board );
               base.SetColor(player.MyPoolsColor);
               base.SetColumns( board.ColumnSet );
               base.SetMotionParams( motion );

               if ( this.MotionParams.UseDiceMotionCount == null ) return false;

               if ( base.ChipsOnTheBar() ) return false;

               if ( !base.ColumnIsPartOfTheBoard(this.MotionParams.ColumnNumber) ) 
                    return false;

               if ( !base.BaseColumnIsOpen() ) return false;

               if ( !TargetColumnIsOpen() ) return false;

               return true;
          }

          protected bool TargetColumnIsOpen()
          {
               List<int> distinctDiceList = MotionParams.UseDiceMotionCount
                                                    .Distinct()
                                                    .ToList();
               if ( distinctDiceList.Count == 2 )
               {
                    if(!PathIsValidDicesAreNotSame( distinctDiceList ))
                    {
                         return false;
                    }
               }
               else if ( distinctDiceList.Count == 1 )
               {
                    int movesCount = MotionParams.UseDiceMotionCount.Count;
                    int diceNumber = distinctDiceList[ 0 ];

                    if ( !PathlIsValidOneDice( movesCount, diceNumber ) )
                    {
                         return false;
                    }
               }

               return true;
          }
 //**************************************************************************

          protected bool TargetColumnIsValid( int targetColumn )
          {
               if ( !base.ColumnIsPartOfTheBoard(targetColumn) )
               {
                    return false;
               }

               if ( !base.ColumnIsNotLock( targetColumn ) )
               {
                    return false;
               }

               return true;
          }

          protected int CalculateTargetColumn( int movesNumber, int baseColumnNumber )
          {
               return ( base.Color == PoolColor.Black ) 
                    ? baseColumnNumber  + movesNumber 
                    : baseColumnNumber - movesNumber ;
          }
          
          private bool PathIsValidDicesAreNotSame(List<int> dices)
          {
               List<bool> movesTypeList = new List<bool>();

               int baseColumn = MotionParams.ColumnNumber;
               int mainTargetCol = CalculateTargetColumn( dices.Sum(), baseColumn );

               if ( !TargetColumnIsValid(mainTargetCol))
               {
                    return false;
               }

               foreach ( var diceNumber in dices )
               {
                    int targetCol = CalculateTargetColumn( diceNumber, baseColumn );

                    movesTypeList.Add(TargetColumnIsValid(targetCol));
               }

               if ( movesTypeList.All( x => x == false ) )
               {
                    return false;
               }

               return true;
          }

          private bool PathlIsValidOneDice(int movesCount,int diceNumber)
          {
               int baseColumn = MotionParams.ColumnNumber;

               for ( int i = 1; i <= movesCount; i++ )
               {
                    int targetCol = CalculateTargetColumn( i * diceNumber, baseColumn );

                    if ( !TargetColumnIsValid(targetCol))
                    {
                         return false;
                    }
               }

               return true;
          }
     }
}

          //"Please enter your move type in following format :"         + NewRow + 
          //"1.For 'Inside'  - ( 1 ) (column number) (pool number)   ;" + NewRow + 
          //"2.For 'Outside' - ( 2 ) (column number)                 ;" + NewRow + 
          //"3.For 'Move'    - ( 3 ) (column number) (places to move);";
          //

          /*
          Пул може да бъде преместен само на отворена позиция, 
          която не е заета с капия (2 или повече противникови пула).
          Числата на двата зара дават правото на 2 отделни движения. 
          Например, ако метнеш 5 и 3, може да преместиш един пул с 5 места и друг пул с 3 места 
          или може да преместиш един пул с 8 места общо, 
          стига да има отворена позиция, където да стъпи с 5ца или с 3ка.

           */