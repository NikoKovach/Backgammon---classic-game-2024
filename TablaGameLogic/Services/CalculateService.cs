using System.Collections.Generic;
using System.Linq;
using TablaModels.ComponentModels.Components.Interfaces;
using TablaModels.ComponentModels.Enums;
using static TablaGameLogic.Utilities.Messages.GameConstants;

namespace TablaGameLogic.Services
{
     public class CalculateService
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

          public static int[] GetlInvokeMethodParams( string typeOfMove, int[] paramameters, PoolColor playerColor, IBoard board )
          {

               if ( typeOfMove.Equals( "Inside" ) )
               {
                    return paramameters;
               }

               if (typeOfMove.Equals("Move"))
               {
                    int colNumber = paramameters.First();
                    int[] diceAndMoveCount = CalculateMoveNumbers( paramameters.Last(), board );

                    int[] moveParams = new int[diceAndMoveCount.Length +1];

                    moveParams[ 0 ] = colNumber;

                    for ( int i = 0; i < diceAndMoveCount.Length; i++ )
                    {
                         moveParams[ i + 1 ] = diceAndMoveCount[ i ];
                    }

                    return moveParams;
               }

               return default;
          }

          public static void ChangeDiceValueAndMoveCount(string typeOfMove, int[] moveParams, PoolColor playerColor, IBoard board)
          {
               if (typeOfMove.Equals("Move"))
               {
                   ForMoveMotion(moveParams, board);
               }

               if ( typeOfMove.Equals( "Inside" ) )
               {
                    ForInsideMotion( moveParams, playerColor, board );
               }

               if (typeOfMove.Equals("Outside"))
               {
                   ForOutsideMotion(moveParams, playerColor, board);
               }

          }

          private static void ForMoveMotion( int[] moveParams, IBoard board)
          {
               //"3.For 'Move'    - ( 3 ) (column number) (places to move);";
               int positions = (int)moveParams[0];
               int numberOfMotions = (int)moveParams[1];

               if (numberOfMotions == 1)
               {
                   board.DiceValueAndMovesCount[positions] -= 1;
               }

               Dictionary<int, int> kvpDicesCountOfMoves = board.DiceValueAndMovesCount
               .Where(x => x.Value > 0)
               .ToDictionary(x => x.Key, x => x.Value);

               int keysSum = kvpDicesCountOfMoves.Select(x => x.Key).Sum();

               int[] keys = kvpDicesCountOfMoves.Select(x => x.Key).ToArray();

               if (numberOfMotions == 2 && kvpDicesCountOfMoves.Count == 2 && positions ==   keysSum)
               {
                   for (int i = 0; i < keys.Length; i++)
                   {
                       board.DiceValueAndMovesCount[keys[i]] -= 1;
                   }
               }

               if (numberOfMotions > 2 && positions > keysSum)
               {
                   board.DiceValueAndMovesCount[keysSum] -= numberOfMotions;
               }
          }

          private static void ForInsideMotion(int[] moveParams, PoolColor   playerColor, IBoard board)
          {
               int columnNumber = moveParams[0];

               int diceNumber = (playerColor == PoolColor.Black) ? 
                    columnNumber : (board.ColumnSet.Count + 1 - columnNumber);

               bool aDiceExist = board.DiceValueAndMovesCount
                    .Any(x => x.Key == diceNumber && x.Value > 0);

               if (aDiceExist)
               {
                   board.DiceValueAndMovesCount[diceNumber] -= 1;
               } 
          }

          private static void ForOutsideMotion(int[] moveParams, PoolColor playerColor, IBoard board)
          {   
               int column = (int)moveParams[0];

               int positions = (playerColor == PoolColor.Black) ? (24 + 1 - column) : column;

               //TODO:::
               //Белите вадят пул :пример колоната,от която се дърпа пул е №4,а зара е 5
               //колона 5 и 6 са празни
               Dictionary<int, int> kvpDicesCountOfMoves = board.DiceValueAndMovesCount
                                                    .Where(x => x.Value > 0)
                                                    .OrderBy(y => y.Key)
                                                    .ToDictionary(x => x.Key, x => x.Value);

               bool diceExists = kvpDicesCountOfMoves.ContainsKey(positions);

               if (diceExists)
               {
                   board.DiceValueAndMovesCount[positions] -= 1;
                   return;
               }

               int diseValue = kvpDicesCountOfMoves.First(x=>x.Key > positions).Key;

               board.DiceValueAndMovesCount[positions] -= 1;
          }

          public static int[] CalculateMoveNumbers(int placeToMove ,IBoard board )
          {             
               var kvp = board.DiceValueAndMovesCount
                    .Where(x => x.Value > 0)
                    .Select( x => x )
                    .ToDictionary(t => t.Key,t => t.Value);

               if ( kvp.ContainsKey(placeToMove) )
               {
                    int value = kvp
                         .Where( x => x.Key == placeToMove )
                         .Select( x => x.Key)
                         .FirstOrDefault();

                    return new int[ 1 ] { value };
               }

               if ( kvp.Count == 2 && kvp.Keys.Sum() == placeToMove )
               {
                    int[] moveNumbers = kvp.Select(x => x.Key).ToArray();
                    
                    return moveNumbers;
               }
               

               return default;
          }
     }
}
