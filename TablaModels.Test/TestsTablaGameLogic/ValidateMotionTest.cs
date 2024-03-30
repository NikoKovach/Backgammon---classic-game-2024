namespace TablaModels.Test.TestsTablaGameLogic
{
     using NUnit.Framework;
     using System.Collections.Generic;
     using System.Linq;
     using TablaGameLogic.Factory;
     using TablaGameLogic.Services;
     using TablaGameLogic.Services.Contracts;
     using TablaModels.Components.Interfaces;
     using TablaModels.Components.Players;
     using TablaModels.Enums;

     [TestFixture]
     public class ValidateMotionTest
     {
          private static List<IPlayer> players;
          private static IMoveParameters motion;
          private static IBoard board;

          [TestCaseSource(nameof(ChipsOnTheBarFalse))]
          public void ChipsOnTheBarMethodInValidateMotionClassShouldReturnFalse
               (int colNumber,int placeToMove,IMoveParameters motion,IBoard board, IPlayer player)
          {
               //Arrange
               motion.ColumnNumber = colNumber;
               motion.chipNumberOrPlaceToMove = placeToMove;

               var validateMove = new ValidateMotion();

               bool result = validateMove
                             .MoveIsCorrect( motion, board, player );

               Assert.That( result == false );
          }


          [TestCaseSource(nameof(TargetColumnReturnFalse))]
          public void TargetColumnIsOpenMethodInValidateMotionClassShouldReturnFalse
               (int colNumber,int placeToMove,IMoveParameters motion,IBoard board, IPlayer player)
          {
               //Arrange
               motion.ColumnNumber = colNumber;
               motion.chipNumberOrPlaceToMove = placeToMove;
               motion.UseDiceMotionCount.Clear();
               motion.UseDiceMotionCount.Add( placeToMove );

               var validateMove = new ValidateMotion();

               bool result = validateMove
                             .MoveIsCorrect( motion, board, player );

               Assert.That( result == false );
          }


          [TestCaseSource(nameof(TargetColumnReturnTrue))]
          public void TargetColumnIsOpenMethodInValidateMotionClassShouldReturnTrue
               (int colNumber,int placeToMove,IMoveParameters motion,IBoard board, IPlayer player)
          {
               //Arrange
               motion.ColumnNumber = colNumber;
               motion.chipNumberOrPlaceToMove = placeToMove;
               motion.UseDiceMotionCount.Clear();
               motion.UseDiceMotionCount.Add( placeToMove );

               var validateMove = new ValidateMotion();

               bool result = validateMove
                             .MoveIsCorrect( motion, board, player );

               Assert.That( result == true );
          }

//***************************************************

          private static object[] ChipsOnTheBarFalse()
          {
               SetToDefault();
               Setup();

               board.BlackPoolsSet
                    .First( x => x.IdentityNumber == 15 )
                    .State = PoolState.OnTheBar;
               
                board.WhitePoolsSet
                    .First( x => x.IdentityNumber == 15 )
                    .State = PoolState.OnTheBar;

               object[] methodParams = new object[]
               {
                    //colNumber,chipNumber,motion,board,player,beatenChip
                    new object[]{ 1,15,motion,board,players[1]},
                    new object[]{24,15,motion,board,players[0]}
               };

               return methodParams;
          }

          private static object[] TargetColumnReturnFalse()
          {
               SetToDefault();
               Setup();

               ArrangeTheChipsMoveOne();

               object[] methodParams = new object[]
               {
                    //colNumber,placeToMove,motion,board,player,beatenChip
                    new object[]{ 1,1,motion,board,players[1]},
                    new object[]{24,3,motion,board,players[0]},               
               };

               return methodParams;
          }

          private static object[] TargetColumnReturnTrue()
          {
               SetToDefault();
               Setup();

               ArrangeTheChipsMoveTwo();

               object[] methodParams = new object[]
               {
                    //colNumber,placeToMove,motion,board,player,beatenChip
                    new object[]{ 1,1,motion,board,players[1]},
                    new object[]{24,3,motion,board,players[0]},               
               };

               return methodParams;
          }

          private static void ArrangeTheChipsMoveOne()
          { 
               for ( int i = 1; i <= 2; i++ )
               {
                    IPool blackChip = board.BlackPoolsSet[ i - 1 ];
                    IPool whiteBlockChip = board.WhitePoolsSet[ i + 2 ];
                    board.ColumnSet[ 1 ].PoolStack.Push(blackChip);
                    board.ColumnSet[ 2 ].PoolStack.Push(whiteBlockChip);

                    IPool whiteChip = board.WhitePoolsSet[ i - 1 ];
                    IPool blackBlockChip = board.BlackPoolsSet[ i + 1 ];
                    board.ColumnSet[ 24 ].PoolStack.Push(whiteChip);
                    board.ColumnSet[ 21 ].PoolStack.Push(blackBlockChip);
               }
          }

          private static void ArrangeTheChipsMoveTwo()
          { 
               for ( int i = 1; i <= 2; i++ )
               {
                    IPool blackChip = board.BlackPoolsSet[ i - 1 ];
                    board.ColumnSet[ 1 ].PoolStack.Push(blackChip);

                    IPool whiteChip = board.WhitePoolsSet[ i - 1 ];
                    board.ColumnSet[ 24 ].PoolStack.Push(whiteChip);
               }
          }

          private static void Setup()
          { 
               players = new List<IPlayer>()
               {
                    new PlayerClassicGame( "White" )
                    {
                         MyPoolsColor = PoolColor.White,
                         State = PlayerState.NormalState,
                    },
                    new PlayerClassicGame( "Black" )
                    {
                         MyPoolsColor = PoolColor.Black,
                         State = PlayerState.NormalState,
                    },
               };

               board = new BoardFactory().Create();

               motion = new MoveParameters();
          }

          private static void SetToDefault()
          {
               players = default;

               board  = default;

               motion = default;
          }

         
     }
}
