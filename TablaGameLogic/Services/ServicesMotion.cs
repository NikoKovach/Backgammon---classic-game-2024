namespace TablaGameLogic.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using TablaGameLogic.Services.Contracts;
    using TablaModels.Components.Interfaces;
    using TablaModels.Enums;
    using static TablaGameLogic.Utilities.Messages.ExceptionMessages;

    public class ServicesMotion : IMoveService
     {
          private static IDictionary<string,IValidateMove> defaultValidateList 
               = GetDefaultValidateList(); 
          private static IDictionary<string,IHasMoves> defaultHasAnyMoveList 
               = GetDefaultHasMoveServiceList();

          public ServicesMotion() : this(defaultValidateList,defaultHasAnyMoveList)
          { }

          public ServicesMotion(IDictionary<string,IValidateMove> validateList,
               IDictionary<string,IHasMoves> hasAnyMoveList)
          {
               ArgumentNullException.ThrowIfNull(validateList);

               ArgumentNullException.ThrowIfNull(hasAnyMoveList);

               this.ValidateList = validateList ;

               this.HasAnyMoveList = hasAnyMoveList;    
          }

          public IDictionary<string,IValidateMove> ValidateList { get; set; }

          public IDictionary<string,IHasMoves> HasAnyMoveList { get; set; }

//**************************************************************************
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

          public object[] GenerateInvokeMethodParameters(IMoveParameters motion,
               IBoard board,IPlayer player)
          {
               if ( motion.MoveMethodName.Equals( "Inside" ) )
               {
                    IPool pool = GetPool( board, player, motion.chipNumberOrPlaceToMove);

                    return new object[] {motion.ColumnNumber,pool,board.ColumnSet};
               }

               if ( motion.MoveMethodName.Equals( "Outside" ) )
               {
                    return new object[] {motion.ColumnNumber,board.ColumnSet};
               }

               return new object[] {motion.ColumnNumber
                    ,motion.UseDiceMotionCount.Sum(),board.ColumnSet};
          }

          public void InvokeMoveMethod(string methodName, object[] moveParams,
               IPlayer CurrentPlayer)
          {
               MethodInfo moveMethodType = CurrentPlayer.Move.GetType().GetMethod     (methodName);

               moveMethodType.Invoke(CurrentPlayer.Move, moveParams); 
          }

          public bool MoveIsValid( IMoveParameters motion, IBoard board,
               IPlayer player )
          {
               try
               {
                    if ( motion == null || board == null || player == null )
                    {
                         throw new ArgumentNullException(ParameterNullException);
                    }

                    IValidateMove validateInstance = 
                         this.ValidateList[motion.MoveMethodName];
                    
                    if ( validateInstance == null )
                    {
                         throw new NullReferenceException(
                              string.Format(NullReferenceValidateMove,
                              nameof(validateInstance)));
                    }

                    return validateInstance.MoveIsCorrect( motion, board, player );
               }
               catch ( ArgumentNullException nullEx)
               {
                    throw new Exception(nullEx.Message);
               }
               catch ( NullReferenceException nullRefEx)
               {
                    throw new Exception(nullRefEx.Message);
               }
          }

          public bool PlayerHasMoves(IBoard board, IPlayer player)
          {
               string moveType =  this.ValidateList
                                      .First().Value
                                      .GetMoveType(board,player);

               bool hasValidMove = this.HasAnyMoveList[ moveType ]
                                       .HasMoves(board,player);

               if ( !hasValidMove ) return false;
 
               return true;
          }

//***************************************************************************
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
                         { "Move"   , new ValidateMotion()   }
               };

               return validateList;
          }

          private static IDictionary<string,IHasMoves> GetDefaultHasMoveServiceList()
          {
               IDictionary<string,IHasMoves> hasMovesList = 
                    new Dictionary<string,IHasMoves>() 
               {
                         { "Inside",  new ValidateInsideHasMove()   },
                         { "Outside", new ValidateOutsideHasMove()  },
                         { "Move"   , new ValidateMotionHasMove()   }
               };

               return hasMovesList;
          }
     }
}

               //this.ValidateList = validateList ?? 
               //     throw new ArgumentNullException
               //     (
               //          string.Format(InvalidMoveConfirmationParameter,nameof(validateList))
               //     );

               //this.HasAnyMoveList = hasAnyMoveList ?? 
               //     throw new ArgumentNullException
               //     (
               //          string.Format(InvalidMoveConfirmationParameter,nameof(hasAnyMoveList))
               //     );