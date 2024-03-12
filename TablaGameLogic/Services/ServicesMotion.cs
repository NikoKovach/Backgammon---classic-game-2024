namespace TablaGameLogic.Services
{
     using System;
     using System.Collections.Generic;
     using System.Linq;
     using System.Reflection;
     using TablaGameLogic.Core.Contracts;
     using TablaGameLogic.Exeptions;
     using TablaGameLogic.Services.Contracts;
     using TablaModels.ComponentModels.Components.Interfaces;
     using TablaModels.ComponentModels.Enums;

     public class ServicesMotion : IMoveService
     {
          public void ParseMove(string moveString,IMoveParameters motion)
          {    
               int[] moveArray = moveString.Split(new char[] { ' ', ',' },                                                 StringSplitOptions.RemoveEmptyEntries)
                                            .Select(x => int.Parse(x))
                                            .ToArray();

               if ( moveArray.Length < 2  )
               {
                    throw new InvalidOperationException( "" );
               }

               motion.MoveMethodName = GetMethodName( moveArray[0] );

               motion.ColumnNumber = moveArray[ 1 ];

               if ( moveArray.Length > 2 )
               {
                     motion.chipNumberOrPlaceToMove = moveArray.Skip(2).FirstOrDefault();
               }
          }

          public object[] GenerateInvokeMethodParameters(IMoveParameters motion,IBoard board,IPlayer player)
          {
               if ( motion.MoveMethodName.Equals( "Inside" ) )
               {
                    IPool pool = GetPool( board, player, motion.chipNumberOrPlaceToMove);

                    return new object[] {motion.ColumnNumber,pool};
               }

               if ( motion.MoveMethodName.Equals( "Outside" ) )
               {
                    return new object[] {motion.ColumnNumber};
               }

               return new object[] {motion.ColumnNumber,motion.UseDiceMotionCount.Sum()};
          }

          public void InvokeMoveMethod(string methodName, object[] moveParams,IPlayer CurrentPlayer)
          {
               MethodInfo moveMethodType = CurrentPlayer.Move.GetType().GetMethod     (methodName);

               moveMethodType.Invoke(CurrentPlayer.Move, moveParams); 
          }

//****************************************************************
          public bool MoveIsValid( IMoveParameters motion, IBoard board, IPlayer player )
          {
               try
               {
                    if ( motion == null || board == null || player == null )
                    {
                         throw new ArgumentNullException();
                    }

                    string className = "Validate" + motion.MoveMethodName;

                    string typeName = $"TablaGameLogic.Services.{className}";

                    Assembly assembly = Assembly.GetExecutingAssembly();

                    IValidateMove validateInstance =
                              (IValidateMove) assembly.CreateInstance(typeName);

                    
                    return validateInstance.MoveIsCorrect( motion, board, player );
               }
               catch ( ArgumentNullException nullEx)
               {
                    throw new Exception(nullEx.Message);
               }
               catch ( Exception ex)
               {
                    throw new ValidateException(ex.Message);
               }
          }
//**************************************************************

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

          //"Please enter your move type in following format :"         + NewRow + 
          //"1.For 'Inside'  - ( 1 ) (column number) (pool number)   ;" + NewRow + 
          //"2.For 'Outside' - ( 2 ) (column number)                 ;" + NewRow + 
          //"3.For 'Move'    - ( 3 ) (column number) (places to move);";