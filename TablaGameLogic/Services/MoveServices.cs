namespace TablaGameLogic.Services
{
     using System;
     using System.Collections.Generic;
     using System.Linq;
     using System.Reflection;
     using TablaGameLogic.Services.Contracts;
     using TablaModels.ComponentModels.Components.Interfaces;
     using TablaModels.ComponentModels.Enums;

     using static TablaGameLogic.Services.CalculateService;

     public class MoveServices : IMoveService
     {
          //"Please enter your move type in following format :"         + NewRow + 
          //"1.For 'Inside'  - ( 1 ) (column number) (pool number)   ;" + NewRow + 
          //"2.For 'Outside' - ( 2 ) (column number)                 ;" + NewRow + 
          //"3.For 'Move'    - ( 3 ) (column number) (places to move);";

          public KeyValuePair<string,int[]> ParseMove(string moveString)
          {    
               int[] moveArray = moveString
                                   .Split(new char[] { ' ', ',' },                                                StringSplitOptions.RemoveEmptyEntries)
                                   .Select(x => int.Parse(x))
                                   .ToArray();

               string moveMethodName = GetMethodName( moveArray[0] );

               int[]paramsList = moveArray.Skip(1).ToArray();

               return new KeyValuePair<string,int[]>(moveMethodName, paramsList);
          }

          public object[] GetInvokeMethodParameters(string moveMethod,int[] moveParams,IBoard board,IPlayer player)
          {
               object[] parametersArray = new object[ moveParams.Length ];

               for ( int i = 0; i < parametersArray.Length; i++ )
               {
                    parametersArray[ i ] = moveParams[ i ];
               }

               if ( moveMethod.Equals("Inside") )
               {
                    IPool pool = GetPool( board, player, moveParams.Last() );

                    parametersArray[ parametersArray.Length - 1 ] = pool;

                    return parametersArray.ToArray();
               }

               return parametersArray.ToArray();
          }

          public void InvokeMoveMethod(string moveType,object[] moveParams,IPlayer CurrentPlayer)
          {
               MethodInfo moveMethodType = CurrentPlayer.Move.GetType().GetMethod     (moveType);

               moveMethodType.Invoke(CurrentPlayer.Move, moveParams); 
          }

          private string GetMethodName( int moveType )
          {
               string moveName = string.Empty;

               switch ( moveType )
               {
                    case 1:
                         moveName = "Inside";
                         break;
                    case 2:
                         moveName = "OutSide";
                         break;
                    case 3:
                         moveName = "Move";
                         break;
               }

               return moveName;
          }

          private IPool GetPool( IBoard board, IPlayer player,int poolNumber )
          {
               PoolColor color = player.MyPoolsColor;

               IList<IPool> chipList = 
                      player.MyPoolsColor == PoolColor.White ?
                      board.WhitePoolsSet : board.BlackPoolsSet;

               return chipList.FirstOrDefault( x => x.IdentityNumber == poolNumber );
          }
     }
}

//if (moveType.Equals("Move"))
          //{
          //    int numberOfMoves = CalculateTheNumberOfMoves(moveParameters[1],       this.TablaBoard.ValueOfDiceAndCountOfMoves);

          //    moveParameters.Add(numberOfMoves);
          //}

          ///// <summary>
          ///// Old name : CalculateTheNumberOfMoves
          ///// </summary>
          ///// <param name="positions"></param>
          ///// <param name="valueOfDiceAndCountOfMoves"></param>
          ///// <returns></returns>
          //public int ChangeMovesNumbers(int positions,IDictionary<int,int>       diceValueAndMovesCount)
          //{
          //     int number = 0;

          //     var kvpDicesCountOfMoves = diceValueAndMovesCount
          //         .Where(x => x.Value > 0)
          //         .ToDictionary(x => x.Key, x => x.Value);

          //     int KeysSum = kvpDicesCountOfMoves.Select(x => x.Key).Sum();

          //     if (kvpDicesCountOfMoves.Count == 2 && (positions < KeysSum))
          //     {
          //         number = 1;
          //     }

          //     if (kvpDicesCountOfMoves.Count == 2 && (positions == KeysSum))
          //     {
          //         number = 2;
          //     }

          //     if (kvpDicesCountOfMoves.Count == 1 && (positions >= kvpDicesCountOfMoves.First().Key)   && (positions % kvpDicesCountOfMoves.First().Key == 0))
          //     {
          //         number = positions / kvpDicesCountOfMoves.First().Key;
          //     }

          //     return number;
          //}

          //private object[] GetParameters( int[] moveArray,
          //     IBoard board, IPlayer player )
          //{
          //     try
          //     {
          //          int[] moveParameters = moveArray.Skip( 1 ).ToArray();

          //          object[] parametersArray = new object[ moveParameters.Length ];

          //          for ( int i = 0; i < parametersArray.Length; i++ )
          //          {
          //               parametersArray[ i ] = moveParameters[ i ];
          //          }

          //          IPool pool = default;

          //          if ( moveArray[ 0 ] == 1 )
          //          {
          //               pool = GetPool( board, player, moveArray.Last() );
          //               parametersArray[ parametersArray.Length - 1 ] = pool;
          //               return parametersArray.ToArray();
          //          }

          //          return parametersArray.ToArray();

          //     }
          //     catch ( Exception ex )
          //     {
          //          throw new InvalidOperationException( ex.Message );
          //     }
          //}