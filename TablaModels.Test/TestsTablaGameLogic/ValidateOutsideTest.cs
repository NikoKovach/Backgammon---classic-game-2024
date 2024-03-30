namespace TablaModels.Test.TestsTablaGameLogic
{
     using NUnit.Framework;
     using System.Collections.Generic;
     using TablaGameLogic.Factory;
     using TablaGameLogic.Services;
     using TablaGameLogic.Services.Contracts;
     using TablaModels.Components.Interfaces;
     using TablaModels.Components.Players;
     using TablaModels.Enums;

     [TestFixture]
     public class ValidateOutsideTest
     {
          private static List<IPlayer> players;
          private static IMoveParameters motion;
          private static IBoard board;

          [TestCaseSource(nameof(ChipsInGameFalse))]
          public void ChipsInGameMethodInValidateOutsideClassShouldReturnFalse
               (int colNumber,IMoveParameters motion,IBoard board, IPlayer player)
          {
               //Arrange
               motion.ColumnNumber = colNumber;

               var validate = new ValidateOutside();

               bool result = validate
                             .MoveIsCorrect( motion, board, player );

               Assert.That( result == false );
          }

          [TestCaseSource(nameof(TargetColumnIsValidFalse))]
          public void TargetColumnIsValidAgainstColorMethod_ShouldReturnFalse
               (int colNumber,IMoveParameters motion,IBoard board, IPlayer player)
          {
               //Arrange
               motion.ColumnNumber = colNumber;

               var validate = new ValidateOutside();

               bool result = validate
                             .MoveIsCorrect( motion, board, player );

               Assert.That( result == false );
          }

          [TestCaseSource(nameof(TargetColumnIsValidTrue))]
          public void TargetColumnIsValidAgainstColorMethod_ShouldReturnTrue
               (int colNumber,IMoveParameters motion,IBoard board, IPlayer player)
          {
               //Arrange
               motion.ColumnNumber = colNumber;

               var validate = new ValidateOutside();

               bool result = validate
                             .MoveIsCorrect( motion, board, player );

               Assert.That( result == true );
          }

          [TestCaseSource( nameof( ColumnIsValidAgainstChipCountFalse ) )]
          public void TargetColumnIsValidAgainstPoolCountMethod_ShouldReturnFalse
               ( int colNumber, IMoveParameters motion, IBoard board, IPlayer player )
          {
               //Arrange
               motion.ColumnNumber = colNumber;

               var validate = new ValidateOutside();

               bool result = validate
                             .MoveIsCorrect( motion, board, player );

               Assert.That( result == false );
          }

          [TestCaseSource( nameof( ColumnIsValidAgainstChipCountTrue ) )]
          public void TargetColumnIsValidAgainstPoolCountMethod_ShouldReturnTrue
               ( int colNumber, IMoveParameters motion, IBoard board, IPlayer player )
          {
               //Arrange
               motion.ColumnNumber = colNumber;

               var validate = new ValidateOutside();

               bool result = validate
                             .MoveIsCorrect( motion, board, player );

               Assert.That( result == true );
          }

          //***********************************************************

          private static object[] ChipsInGameFalse()
          {
               SetToDefault();

               Setup();

               ArrangeChipsInGameFalse();

               object[] methodParams = new object[]
               {
                    //colNumber,motion,board,player,beatenChip
                    new object[]{ 6,motion,board,players[0]},
                    new object[]{20,motion,board,players[1]},    
               };

               return methodParams;
          }

          private static object[] TargetColumnIsValidFalse()
          {
               SetToDefault();

               Setup();

               ArrangeTargetColumnIsValid();

               object[] methodParams = new object[]
               {
                    //colNumber,motion,board,player,beatenChip
                    new object[]{ 7,motion,board,players[0]},
                    new object[]{12,motion,board,players[1]},    
               };

               return methodParams;
          }

          private static object[] TargetColumnIsValidTrue()
          {
               SetToDefault();

               Setup();

               ArrangeTargetColumnIsValid();

               object[] methodParams = new object[]
               {
                    //colNumber,motion,board,player,beatenChip
                    new object[]{ 6,motion,board,players[0]},
                    new object[]{20,motion,board,players[1]},    
               };

               return methodParams;
          }

          private static object[] ColumnIsValidAgainstChipCountFalse()
          {
               SetToDefault();

               Setup();

               ArrangeColumnIsValidAgainstChipCount();

               object[] methodParams = new object[]
               {
                    //colNumber,motion,board,player,beatenChip

                    new object[]{ 3,motion,board,players[0]},
                    new object[]{22,motion,board,players[1]},
               };

               return methodParams;
          }

          private static object[] ColumnIsValidAgainstChipCountTrue()
          {
               SetToDefault();

               Setup();

               ArrangeColumnIsValidAgainstChipCountSecond();

               object[] methodParams = new object[]
               {
                    //colNumber,motion,board,player,beatenChip

                    new object[]{ 3,motion,board,players[0]},
                    new object[]{22,motion,board,players[1]},
               };

               return methodParams;
          }

          private static void ArrangeTargetColumnIsValid()
          {
               for ( int i = 1; i <= 2; i++ )
               {
                    IPool whiteChipAtHome = board.WhitePoolsSet[ i-1 ];
                    whiteChipAtHome.State = PoolState.AtHome;
                    board.ColumnSet[ 6 ].PoolStack.Push( whiteChipAtHome );

                    IPool blackChipAtHome = board.BlackPoolsSet[ i-1];
                    blackChipAtHome.State = PoolState.AtHome;
                    board.ColumnSet[ 20 ].PoolStack.Push( blackChipAtHome );   
               }

               board.DiceValueAndMovesCount[ 6 ] = 1;
               board.DiceValueAndMovesCount[ 5 ] = 1;
          }

          private static void ArrangeChipsInGameFalse()
          {
               IPool whiteChipAtHome = board.WhitePoolsSet[ 0 ];
               whiteChipAtHome.State = PoolState.AtHome;
               board.ColumnSet[ 6 ].PoolStack.Push( whiteChipAtHome );

               IPool whiteChipInGame = board.WhitePoolsSet[ 14 ];
               whiteChipInGame.State = PoolState.InGame;
               board.ColumnSet[ 8 ].PoolStack.Push( whiteChipInGame );

               IPool blackChipAtHome = board.BlackPoolsSet[ 0 ];
               blackChipAtHome.State = PoolState.AtHome;
               board.ColumnSet[ 20 ].PoolStack.Push( blackChipAtHome );

               IPool blackChipInGame = board.BlackPoolsSet[ 14 ];
               blackChipInGame.State = PoolState.InGame;
               board.ColumnSet[ 18 ].PoolStack.Push( blackChipInGame );

          }

          private static void ArrangeColumnIsValidAgainstChipCount()
          {
               for ( int i = 1; i <= 2; i++ )
               {
                    IPool whiteChipSetOne = board.WhitePoolsSet[ i - 1 ];
                    whiteChipSetOne.State = PoolState.AtHome;
                    IPool whiteChipSetTwo = board.WhitePoolsSet[ i + 1 ];
                    whiteChipSetTwo.State = PoolState.AtHome;

                    board.ColumnSet[ 6 ].PoolStack.Push( whiteChipSetOne );
                    board.ColumnSet[ 3 ].PoolStack.Push(whiteChipSetTwo );

                    IPool blackChipSetOne = board.BlackPoolsSet[ i - 1];
                    blackChipSetOne.State = PoolState.AtHome;
                    IPool blackChipSetTwo = board.BlackPoolsSet[ i + 1];
                    blackChipSetTwo.State = PoolState.AtHome;

                    board.ColumnSet[ 19].PoolStack.Push( blackChipSetOne );
                    board.ColumnSet[ 22].PoolStack.Push( blackChipSetTwo );
                    
               }

               board.DiceValueAndMovesCount[ 4 ] = 1;
               board.DiceValueAndMovesCount[ 5 ] = 1;
          }

          private static void ArrangeColumnIsValidAgainstChipCountSecond()
          {
               for ( int i = 1; i <= 2; i++ )
               {
                    IPool whiteChipSetTwo = board.WhitePoolsSet[ i + 1 ];
                    whiteChipSetTwo.State = PoolState.AtHome;
                    board.ColumnSet[ 3 ].PoolStack.Push(whiteChipSetTwo );

                    IPool blackChipSetTwo = board.BlackPoolsSet[ i + 1];
                    blackChipSetTwo.State = PoolState.AtHome;
                    board.ColumnSet[ 22].PoolStack.Push( blackChipSetTwo );  
               }

               board.DiceValueAndMovesCount[ 4 ] = 1;
               board.DiceValueAndMovesCount[ 5 ] = 1;
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
