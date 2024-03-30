namespace TablaModels.Test.TestsTablaGameLogic
{
     using NUnit.Framework;
     using System;
     using TablaGameLogic.Factory;
     using TablaGameLogic.Services;
     using TablaModels.Components.Interfaces;
     using TablaModels.Components.Players;
     using TablaModels.Enums;

     [TestFixture]
     public class HasMoveValidateOutsideTest
     {
          private IPlayer player;
          private IBoard board;

          [Test]
          public void ForWhitePlayer_CaseHasMotionsMethod_ShouldReturnFalse()
          {
               //Arrange
               SetGameForWhitePlayer();

               var validateHasMove = new ValidateOutsideHasMove();
               //Act
               bool result = validateHasMove.HasMoves( board, player );
               //Assert
               Assert.That( result == false );
          }

          [Test]
          public void ForBlackPlayer_CaseHasMotionsMethod_ShouldReturnFalse()
          {
               //Arrange
               SetGameForBlackPlayer();

               var validateHasMove = new ValidateOutsideHasMove();
               //Act
               bool result = validateHasMove.HasMoves( board, player );
               //Assert
               Assert.That( result == false );
          }

          //***************************************************

          private void SetGameForWhitePlayer()
          {
               SetToDefault();

               string name = "WhiteJack";

               this.player = new PlayerClassicGame( name );
               this.player.State = PlayerState.NormalState;
               this.player.MyPoolsColor = PoolColor.White;

               this.board = new BoardFactory().Create();


               ArrangeBoardForWhitePlayer();

          }

          private void SetGameForBlackPlayer()
          {
               SetToDefault();

               string name = "BlackJack";

               this.player = new PlayerClassicGame( name );
               this.player.State = PlayerState.NormalState;
               this.player.MyPoolsColor = PoolColor.Black;

               this.board = new BoardFactory().Create();


               ArrangeBoardForBlackPlayer();
          }

          private void ArrangeBoardForWhitePlayer()
          {
               var columns = board.ColumnSet;
               var blackChips = board.BlackPoolsSet;
               var whiteChips = board.WhitePoolsSet;

               int chipsCount = 0;

               for ( int i = 1; i <= 3; i++ )
               {
                    for ( int w = 1; w <= 2; w++ )
                    {
                         IPool blackChip = blackChips[ chipsCount ];
                         blackChip.State = PoolState.InGame;

                         columns[ i ].PoolStack.Push(blackChip);

                         chipsCount++;
                    }
               }

               chipsCount = 0;

               for ( int w = 1; w <= 4; w++ )
               {
                    IPool whiteChip = whiteChips[ chipsCount ];
                    whiteChip.State = PoolState.AtHome;

                    columns[ 6 ].PoolStack.Push(whiteChip);

                    chipsCount++;
               }

               board.DiceValueAndMovesCount[ 4 ] = 1;
               board.DiceValueAndMovesCount[ 3 ] = 1;
          }

          private void ArrangeBoardForBlackPlayer()
          {
               var columns = board.ColumnSet;
               var blackChips = board.BlackPoolsSet;
               var whiteChips = board.WhitePoolsSet;

               int chipsCount = 0;

               for ( int i = 1; i <= 3; i++ )
               {
                    for ( int w = 1; w <= 2; w++ )
                    {
                         IPool whiteChip = whiteChips[ chipsCount ];
                         whiteChip.State = PoolState.InGame;

                         columns[ 25 - i ].PoolStack.Push( whiteChip );

                         chipsCount++;
                    }
               }

               chipsCount = 0;

               for ( int w = 1; w <= 4; w++ )
               {
                    IPool blackChip = blackChips[ chipsCount ];
                    blackChip.State = PoolState.AtHome;

                    columns[ 20 ].PoolStack.Push( blackChip );

                    chipsCount++;
               }

               board.DiceValueAndMovesCount[ 4 ] = 1;
               board.DiceValueAndMovesCount[ 3 ] = 1;
          }

          private void SetToDefault()
          {
               this.player = default;
               this.board  = default;
          }

     }
}

               

               