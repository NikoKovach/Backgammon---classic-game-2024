namespace TablaGameLogic.Services
{
     using System.Collections.Generic;
     using System.Linq;
     using TablaGameLogic.Services.Contracts;
     using TablaModels.ComponentModels.Components.Interfaces;
     using TablaModels.ComponentModels.Enums;
     using static TablaGameLogic.Utilities.Messages.GameConstants;

     public static class ServiceCalculate
     {
          public static void SetDiceValueAndMovesCount(IBoard board)
          {
               int[] dice = new int[ NumberOfDice];

               for ( int i = 0; i < dice.Length; i++ )
               {
                    dice[i] = board.DiceSet[i + 1].ValueOfOneDice;
               }

               board.DiceValueAndMovesCount.Clear();

               if (dice[0] != dice[1] )
               {
                   board.DiceValueAndMovesCount[dice[0]] = 1;
                   board.DiceValueAndMovesCount[dice[1]] = 1;
               }

               if (dice[0] == dice[1] )
               {
                   board.DiceValueAndMovesCount[dice[0]] = 4;
               }
          }

          public static void CalculateUseDiceMotionCount( IMoveParameters motion, PoolColor playerColor, IBoard board )
          {

               if ( motion.MoveMethodName.Equals( "Inside" ) )
               {
                    CalculateInsideMoveCount( motion, playerColor, board );
               }

               if ( motion.MoveMethodName.Equals( "Move" ) )
               {
                    motion.UseDiceMotionCount = 
                         CalculateMoveNumbers( motion.chipNumberOrPlaceToMove, board );
               }
          }

          public static void ChangeDiceValueAndMoveCount
               ( IMoveParameters motion, IBoard board)
          {
               IList<int> presentMoves = motion.UseDiceMotionCount;

               foreach ( var item in presentMoves )
               {
                    board.DiceValueAndMovesCount[ item ] -= 1;
               }
          }

//******************************************************************

          private static void CalculateInsideMoveCount(IMoveParameters motion, PoolColor   playerColor, IBoard board)
          {
               int columnNumber = motion.ColumnNumber;

               int diceNumber = (playerColor == PoolColor.Black) ? 
                    columnNumber : (board.ColumnSet.Count + 1 - columnNumber);

               bool aDiceExist = board.DiceValueAndMovesCount
                    .Any(x => x.Key == diceNumber && x.Value > 0);

               if (aDiceExist)
               {
                   motion.UseDiceMotionCount.Add(diceNumber);
               } 
          }

          private static List<int> CalculateMoveNumbers(int placeToMove ,IBoard board )
          {
               bool diceExist = board.DiceValueAndMovesCount
                    .Any( x => x.Key == placeToMove && x.Value > 0 );

               if ( diceExist )
               {
                    return board.DiceValueAndMovesCount
                              .Where( x => x.Key == placeToMove && x.Value > 0 )
                              .Select(x => x.Key)
                              .ToList();
               }

               var dices = board.DiceValueAndMovesCount
                    .Where(x => x.Value > 0)
                    .ToDictionary(t => t.Key,t => t.Value) ;

               if ( dices.Count == 2 && dices.Keys.Sum() == placeToMove )
               {
                    return  dices.Select(x => x.Key).ToList();
               }

               if ( dices.Count == 1 )
               {
                    if ( placeToMove % dices.First().Key == 0 )
                    {
                         int number = placeToMove / dices.First().Key;
                         int diceValue = dices.First().Key;

                         return Enumerable.Repeat(diceValue, number).ToList();
                    }   
               }

               return default;
          }     

     }
}
