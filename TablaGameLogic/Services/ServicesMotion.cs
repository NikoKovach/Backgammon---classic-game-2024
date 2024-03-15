namespace TablaGameLogic.Services
{
     using System;
     using System.Collections.Generic;
     using System.Linq;
     using System.Reflection;

     using TablaGameLogic.Core.Contracts;
     using TablaGameLogic.Services.Contracts;
     using TablaModels.ComponentModels.Components.Interfaces;
     using TablaModels.ComponentModels.Enums;

     public class ServicesMotion : IMoveService
     {
          private static IDictionary<string,IValidateMove> defaultValidateList = GetDefaultValidateList(); 

          public ServicesMotion() : this(defaultValidateList)
          { }

          public ServicesMotion(IDictionary<string,IValidateMove> validateList)
          {
               this.ValidateList = validateList;
          }

          public IDictionary<string,IValidateMove> ValidateList { get; set; }

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

          public bool MoveIsValid( IMoveParameters motion, IBoard board, IPlayer player )
          {
               try
               {
                    if ( motion == null || board == null || player == null )
                    {
                         throw new ArgumentNullException();
                    }

                    IValidateMove validateInstance = this.ValidateList[motion.MoveMethodName];

                    if ( validateInstance == null )
                    {
                         throw new InvalidOperationException();
                    }

                    return validateInstance.MoveIsCorrect( motion, board, player );
               }
               catch ( ArgumentNullException nullEx)
               {
                    throw new Exception(nullEx.Message);
               }
               catch ( InvalidOperationException invalidEx)
               {
                    throw new Exception(invalidEx.Message);
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
                         moveName = "Outside";
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

          private static IDictionary<string,IValidateMove> GetDefaultValidateList()
          {
               IDictionary<string,IValidateMove> validateList = 
                    new Dictionary<string,IValidateMove>() 
               {
                         { "Inside",  new ValidateInside() },
                         { "Outside", new ValidateOutside()},
                         { "Move"   , new ValidateMove()   }
               };

               return validateList;
          }
     }
}

  //public bool MoveIsValid( IMoveParameters motion, IBoard board, IPlayer player )
          //{
          //     try
          //     {
          //          if ( motion == null || board == null || player == null )
          //          {
          //               throw new ArgumentNullException();
          //          }

          //          string className = "Validate" + motion.MoveMethodName;

          //          IValidateMove validateInstance = null;

          //          if ( className.Equals("ValidateInside") )
          //          {
          //               validateInstance = new ValidateInside();
          //          }

          //          if ( className.Equals("ValidateMove") )
          //          {
          //               validateInstance = new ValidateMove();
          //          }

          //          if ( className == "ValidateOutside" )
          //          {
          //               validateInstance = new ValidateOutside();
          //          }

          //          return validateInstance.MoveIsCorrect( motion, board, player );
          //     }
          //     catch ( ArgumentNullException nullEx)
          //     {
          //          throw new Exception(nullEx.Message);
          //     }
          //     catch ( Exception ex)
          //     {
          //          throw new ValidateException(ex.Message);
          //     }
          //}