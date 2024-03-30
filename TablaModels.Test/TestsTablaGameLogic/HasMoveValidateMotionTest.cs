namespace TablaModels.Test.TestsTablaGameLogic
{
     using NUnit.Framework;
     using TablaGameLogic.Factory;
     using TablaGameLogic.Services;
     using TablaModels.Components.Interfaces;
     using TablaModels.Components.Players;
     using TablaModels.Enums;

     [TestFixture]
     public class HasMoveValidateMotionTest
     {
          private static IBoard board;

          [TestCaseSource(nameof(CaseHasMotionsReturnFalseParams))]
          public void CaseHasMotionsMethod_ShouldReturnFalse(IBoard board,
               PoolColor color,string name)
          {
               //Arrange
               IPlayer player = new PlayerClassicGame( name );
               player.State = PlayerState.NormalState;
               player.MyPoolsColor = color;

               var validateHasMove = new ValidateMotionHasMove();
               //Act
               bool result = validateHasMove.HasMoves( board, player );
               //Assert
               Assert.That( result == false );
          }

//*******************************************************

          private static object[] CaseHasMotionsReturnFalseParams()
          {
               SetToDefault();

               Setup();

               ArrangeBoardCaseReturnFalse();

               object[] methodParams = new object[]
               {
                    //board,player
                    new object[]{ board,PoolColor.Black,"BlackJack"},
                    new object[]{ board,PoolColor.White,"WhiteJack"},
               };

               return methodParams;
          }

          private static void ArrangeBoardCaseReturnFalse()
          {
               var columns = board.ColumnSet;
               var blackChips = board.BlackPoolsSet;
               var whiteChips = board.WhitePoolsSet;

               for (int i = 0; i < blackChips.Count ; i++)
               {
                    if ( columns[ 5 ].PoolStack.Count < 2 )
                    {
                         columns[ 5 ].PoolStack.Push( blackChips[ i ] );
                         blackChips[ i ].State = PoolState.InGame;
                         continue;
                    }

                    if ( columns[ 3 ].PoolStack.Count < 2 )
                    {
                         columns[ 3 ].PoolStack.Push( blackChips[ i ] );
                         blackChips[ i ].State = PoolState.InGame;
                         continue;
                    }                   
               } 

               for (int i = 0; i < whiteChips.Count; i++)
               {
                    if ( columns[ 6 ].PoolStack.Count < 2 )
                    {
                         columns[ 6 ].PoolStack.Push( whiteChips[ i ] );
                         whiteChips[ i ].State = PoolState.AtHome;
                         continue;
                    }

                    if ( columns[ 8 ].PoolStack.Count < 2 )
                    {
                         columns[ 8 ].PoolStack.Push( whiteChips[ i ] );
                         whiteChips[ i ].State = PoolState.InGame;
                         continue;
                    }
               } 

               //board.DiceValueAndMovesCount[ 4 ] = 1;
               board.DiceValueAndMovesCount[ 3 ] = 4;
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
