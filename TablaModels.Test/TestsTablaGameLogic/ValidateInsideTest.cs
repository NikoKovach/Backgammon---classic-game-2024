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
     public class ValidateInsideTest
     {
          private static List<IPlayer> players;
          private static IMoveParameters motion;
          private static IBoard board;

          [TestCaseSource(nameof(ChipsOnTheBarFalse))]
          public void ChipsOnTheBarMethodInValidateInsideClassShouldReturnFalse
               (int colNumber,int chipNumber,IMoveParameters motion,IBoard board, IPlayer player)
          {
               //Arrange
               motion.ColumnNumber = colNumber;
               motion.chipNumberOrPlaceToMove = chipNumber;

               var validateIn = new ValidateInside();

               bool result = validateIn
                             .MoveIsCorrect( motion, board, player );

               Assert.That( result == false );
          }

          [TestCaseSource(nameof(ColumnIsPartOfTheBoardFalse))]
          public void ColumnIsPartOfTheBoardMethodInAllValidateClassesShouldReturnFalse
               (int colNumber,int chipNumber,IMoveParameters motion,IBoard board, IPlayer player)
          {
               //Arrange
               motion.ColumnNumber = colNumber;
               motion.chipNumberOrPlaceToMove = chipNumber;

               var validateIn = new ValidateInside();

               bool result = validateIn
                             .MoveIsCorrect( motion, board, player );

               Assert.That( result == false );
          }

          [TestCaseSource(nameof(BaseColumnIsOpenFalse))]
          public void BaseColumnIsOpenMethodInAllValidateClassesShouldReturnFalse
               (int colNumber,int chipNumber,IMoveParameters motion,IBoard board, IPlayer player)
          {
               //Arrange
               motion.ColumnNumber = colNumber;
               motion.chipNumberOrPlaceToMove = chipNumber;

               var validateIn = new ValidateInside();

               bool result = validateIn
                             .MoveIsCorrect( motion, board, player );

               Assert.That( result == false );
          }

          [TestCaseSource(nameof(ValidateInsideTrue))]
          public void ValidateInsideShouldReturnTrue
               (int colNumber,int chipNumber,IMoveParameters motion,IBoard board, IPlayer player)
          {
               //Arrange
               motion.ColumnNumber = colNumber;
               motion.chipNumberOrPlaceToMove = chipNumber;

               var validateIn = new ValidateInside();

               bool result = validateIn
                             .MoveIsCorrect( motion, board, player );

               Assert.That( result == true );
          }
//*******************************************************************

          private static object[] ChipsOnTheBarFalse()
          {
               SetToDefault();
               Setup();

               object[] methodParams = new object[]
               {
                    //colNumber,chipNumber,motion,board,player,beatenChip
                    new object[]{ 1,15,motion,board,players[1]},
                    new object[]{24,15,motion,board,players[0]}
               };

               return methodParams;
          }

          private static object[] ColumnIsPartOfTheBoardFalse()
          {
               SetToDefault();
               Setup();

               object[] methodParams = new object[]
               {
                    //colNumber,chipNumber,motion,board,player
                    new object[]{ 0,15,motion,board,players[1]},
                    new object[]{25,15,motion,board,players[0]},
                    new object[]{-5,15,motion,board,players[0]}
               };

               return methodParams;
          }

          private static object[] BaseColumnIsOpenFalse()
          {
               SetToDefault();
               Setup();

               for ( int i = 1; i <= 2; i++ )
               {
                    IPool whiteChip = board.WhitePoolsSet[ i - 1 ];
                    board.ColumnSet[ 1 ].PoolStack.Push(whiteChip);

                    IPool blackChip = board.BlackPoolsSet[ i - 1 ];
                    board.ColumnSet[ 24 ].PoolStack.Push(blackChip);
               }

               object[] methodParams = new object[]
               {
                    //colNumber,chipNumber,motion,board,player
                    new object[]{ 1,15,motion,board,players[1]},
                    new object[]{24,15,motion,board,players[0]},
               };

               return methodParams;
          }

          private static object[] ValidateInsideTrue()
          {
               SetToDefault();
               Setup();

               board.BlackPoolsSet
                    .First( x => x.IdentityNumber == 15 )
                    .State = PoolState.OnTheBar;
               
                board.WhitePoolsSet
                    .First( x => x.IdentityNumber == 15 )
                    .State = PoolState.OnTheBar;

               IPool whiteChip = board.WhitePoolsSet[ 0 ];
               board.ColumnSet[ 2 ].PoolStack.Push(whiteChip);

               IPool blackChip = board.BlackPoolsSet[ 0 ];
               board.ColumnSet[ 23 ].PoolStack.Push(blackChip);

               object[] methodParams = new object[]
               {
                    //colNumber,chipNumber,motion,board,player
                    new object[]{ 1,15,motion,board,players[1]},
                    new object[]{24,15,motion,board,players[0]},

                    new object[]{ 2,15,motion,board,players[1]},
                    new object[]{23,15,motion,board,players[0]},
               };

               return methodParams;
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

