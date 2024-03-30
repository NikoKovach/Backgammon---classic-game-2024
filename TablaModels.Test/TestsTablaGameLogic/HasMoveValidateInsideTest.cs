namespace TablaModels.Test.TestsTablaGameLogic
{
     using NUnit.Framework;
     using System.Linq;
     using TablaGameLogic.Factory;
     using TablaGameLogic.Services;
     using TablaModels.Components.Interfaces;
     using TablaModels.Components.Players;
     using TablaModels.Enums;

     [TestFixture]
     public class HasMoveValidateInsideTest
     {
          private static IBoard board;

          [TestCaseSource(nameof(CaseHasChipsOnTheBarReturnFalseParams))]
          public void CaseHasChipsOnTheBarMethod_ShouldReturnFalse(IBoard board,
               PoolColor color,string name)
          {
               //Arrange
               IPlayer player = new PlayerClassicGame( name );
               player.State = PlayerState.NormalState;
               player.MyPoolsColor = color;

               var validateInHasMove = new ValidateInsideHasMove();

               //Act
               bool result = validateInHasMove.HasMoves( board, player );
               //Assert
               Assert.That( result == false );
          }

          [TestCaseSource( nameof( CaseHasChipsOnTheBarReturnTrueParams ) )]
          public void CaseHasChipsOnTheBarMethod_ShouldReturnTrue( IBoard board,
               PoolColor color, string name )
          {
               //Arrange
               IPlayer player = new PlayerClassicGame( name );
               player.State = PlayerState.NormalState;
               player.MyPoolsColor = color;

               var validateInHasMove = new ValidateInsideHasMove();
               //Act
               bool result = validateInHasMove.HasMoves( board, player );
               //Assert
               Assert.That( result == true );
          }

          //**********************************************************
          private static object[] CaseHasChipsOnTheBarReturnFalseParams()
          {
               SetToDefault();

               Setup();

               ArrangeBoardReturnFalse();

               object[] methodParams = new object[]
               {
                    //board,player
                    new object[]{ board,PoolColor.Black,"BlackJack"},
                    new object[]{ board,PoolColor.White,"WhiteJack"},
               };

               return methodParams;
          }

          private static object[] CaseHasChipsOnTheBarReturnTrueParams()
          {
               SetToDefault();

               Setup();

               ArrangeBoardReturnTrue();

               object[] methodParams = new object[]
               {
                    //board,player
                    new object[]{ board,PoolColor.Black,"BlackJack"},
                    new object[]{ board,PoolColor.White,"WhiteJack"},
               };

               return methodParams;
          }

          private static void ArrangeBoardReturnFalse()
          {
               board.BlackPoolsSet
                    .First( x => x.IdentityNumber == 15 )
                    .State = PoolState.OnTheBar;
               
                board.WhitePoolsSet
                    .First( x => x.IdentityNumber == 15 )
                    .State = PoolState.OnTheBar;

               for ( int i = 1; i <= 2; i++ )
               {
                    IPool firstWhiteSetAtHome = board.WhitePoolsSet[ i - 1];
                    firstWhiteSetAtHome.State = PoolState.AtHome;

                    IPool secondWhiteSetAtHome = board.WhitePoolsSet[ i + 2];
                    secondWhiteSetAtHome.State = PoolState.AtHome;

                    board.ColumnSet[2].PoolStack.Push( firstWhiteSetAtHome );  
                    board.ColumnSet[4].PoolStack.Push( secondWhiteSetAtHome );  

                    IPool firstBlackSetAtHome = board.BlackPoolsSet[ i - 1];
                    firstBlackSetAtHome.State = PoolState.AtHome;

                    IPool secondBlackSetAtHome = board.BlackPoolsSet[ i + 2];
                    secondBlackSetAtHome.State = PoolState.AtHome;

                    board.ColumnSet[23].PoolStack.Push( firstBlackSetAtHome );  
                    board.ColumnSet[21].PoolStack.Push( secondBlackSetAtHome );  
               }

               board.DiceValueAndMovesCount[ 4 ] = 1;
               board.DiceValueAndMovesCount[ 2 ] = 1;
          }

          private static void ArrangeBoardReturnTrue()
          {
               board.BlackPoolsSet
                    .First( x => x.IdentityNumber == 15 )
                    .State = PoolState.OnTheBar;
               
                board.WhitePoolsSet
                    .First( x => x.IdentityNumber == 15 )
                    .State = PoolState.OnTheBar;

               for ( int i = 1; i <= 2; i++ )
               {
                    IPool firstWhiteSetAtHome = board.WhitePoolsSet[ i - 1];
                    firstWhiteSetAtHome.State = PoolState.AtHome;

                    board.ColumnSet[2].PoolStack.Push( firstWhiteSetAtHome );  
                    
                    IPool firstBlackSetAtHome = board.BlackPoolsSet[ i - 1];
                    firstBlackSetAtHome.State = PoolState.AtHome;

                    board.ColumnSet[23].PoolStack.Push( firstBlackSetAtHome );       
               }

               board.DiceValueAndMovesCount[ 3 ] = 1;
               board.DiceValueAndMovesCount[ 2 ] = 1;
          }

          private static void Setup()
          {
               board = new BoardFactory().Create();
          }

          private static void SetToDefault()
          {
               board  = default;
          }

     }
}
