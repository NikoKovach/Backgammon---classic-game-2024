namespace TablaModels.Test.TestsTablaGameLogic
{
     using NUnit.Framework;
     using System;
     using System.Collections.Generic;
     using System.Linq;
     using TablaGameLogic.Core;
     using TablaGameLogic.Core.Contracts;
     using TablaGameLogic.Services;
     using TablaModels.Components.Interfaces;
     using TablaModels.Components.Players;
     using TablaModels.Enums;

     using static TablaGameLogic.Utilities.Messages.OutputMessages;

     [TestFixture]
     public class ControllerTest
     {
          private IController controller;
          private IBoard board;

          [TestCase((int)PoolColor.White,"White Jack","Black Jack")]
          public void PlayersChooseAColorMethodReturnCorrectString(int color,
               string firstName,string secondName)
          {
               SetControllerDefaultValue();

               CreateControllerAndPlayers();

               string expectedString = 
                    $"Player {firstName} chose the '{PoolColor.White}' color." 
                    + NewRow + 
                    $"Player {secondName} chose the '{PoolColor.Black} color.";

               string result = this.controller.PlayersChooseAColor( color );

               //Assert
               Assert.AreEqual( expectedString, result );
          }

          [Test]
          public void ArrangingTheCheckersToPlayMethod_ShouldReturnCorrectString()
          {
               SetControllerDefaultValue();

               CreateControllerAndPlayers();
               //Act
               this.controller.PlayersChooseAColor( 1 );
               string result = this.controller.ArrangingTheCheckersToPlay();
               string expectedString = NewRow + "Both players arranged his checkers." 
                                     + NewRow;
               //Assert
               Assert.AreEqual( expectedString, result );
          }

          [Test]
          public void CurrentPlayerFirstSetMethod_SetWhitePlayerToBeCurrentPlayer()
          {
               SetControllerDefaultValue();
               CreateControllerAndPlayers();

               this.controller.PlayersChooseAColor( 1 );

               controller.TablaBoard.DiceSet[1].ValueOfOneDice = 4;
               controller.TablaBoard.DiceSet[2].ValueOfOneDice = 1;
               //Act
               this.controller.CurrentPlayerFirstSet();

               //Assert
               Assert.True( controller.CurrentPlayer.MyPoolsColor == PoolColor.White );
          }

          [Test]
          public void CurrentPlayerFirstSetMethod_SetBlackPlayerToBeCurrentPlayer()
          {
               SetControllerDefaultValue();
               CreateControllerAndPlayers();

               this.controller.PlayersChooseAColor( 1 );

               controller.TablaBoard.DiceSet[1].ValueOfOneDice = 3;
               controller.TablaBoard.DiceSet[2].ValueOfOneDice = 6;
               //Act
               this.controller.CurrentPlayerFirstSet();

               //Assert
               Assert.True( controller.CurrentPlayer.MyPoolsColor == PoolColor.Black );
          }

          [Test]
          public void ChangeCurrentPlayerMethod_SetBlackPlayerToBeCurrent()
          {
               SetControllerDefaultValue();
               CreateControllerAndPlayers();

               controller.Players[ 0 ].MyPoolsColor = PoolColor.White;
               controller.Players[ 1 ].MyPoolsColor = PoolColor.Black;

               controller.CurrentPlayer = controller.Players[ 0 ];

               //Act
               controller.ChangeCurrentPlayer();

               //Assert
               Assert.True( controller.CurrentPlayer.MyPoolsColor == PoolColor.Black );
          }

          [Test]
          public void ChangeCurrentPlayerMethod_SetWhitePlayerToBeCurrent()
          {
               SetControllerDefaultValue();
               CreateControllerAndPlayers();

               controller.Players[ 0 ].MyPoolsColor = PoolColor.White;
               controller.Players[ 1 ].MyPoolsColor = PoolColor.Black;

               controller.CurrentPlayer = controller.Players[ 1 ];

               //Act
               controller.ChangeCurrentPlayer();

               //Assert
               Assert.True( controller.CurrentPlayer.MyPoolsColor == PoolColor.White );
          }

          [Test]
          public void TestRollDiceMethod_SetDiceValueBetweenOneAndSix()
          {
               SetControllerDefaultValue();
               CreateControllerAndPlayers();

               controller.Players[ 0 ].MyPoolsColor = PoolColor.White;
               controller.Players[ 1 ].MyPoolsColor = PoolColor.Black;

               controller.CurrentPlayer = controller.Players[ 0 ];

               //Act
               controller.RollDice();

               //Assert
               Assert.That( this.board.DiceSet[ 1 ].ValueOfOneDice >= 1 && 
                            this.board.DiceSet[ 1 ].ValueOfOneDice <=6);

               Assert.That( this.board.DiceSet[ 2 ].ValueOfOneDice >= 1 && 
                            this.board.DiceSet[ 2 ].ValueOfOneDice <=6);
          }

          [Test]
          public void ClearBoardFromCheckersMethod_SouldClearTheColumnsFromChips()
          {
               SetControllerDefaultValue();
               CreateControllerAndPlayers();

               controller.Players[ 0 ].MyPoolsColor = PoolColor.White;
               controller.Players[ 1 ].MyPoolsColor = PoolColor.Black;

               controller.ArrangingTheCheckersToPlay();

               //Act
               controller.ClearBoardFromCheckers();

               //Assert
               Assert.That(board.ColumnSet
                                .All(x => x.Value.PoolStack.Count == 0) == true);
          }

          [Test]
          public void ClearBoardFromCheckersMethod_SetWhiteChipsStatusToStarting()
          {
               SetControllerDefaultValue();
               CreateControllerAndPlayers();

               controller.Players[ 0 ].MyPoolsColor = PoolColor.White;
               controller.Players[ 1 ].MyPoolsColor = PoolColor.Black;

               controller.ArrangingTheCheckersToPlay();

               //Act
               controller.ClearBoardFromCheckers();

               //Assert
               Assert.That( board.WhitePoolsSet
                                .All( x => x.State == PoolState.Starting ) == true );

               //Assert.That( board.BlackPoolsSet
               //                 .All( x => x.State == PoolState.AtHome ) == true );
          }

          [Test]
          public void ClearBoardFromCheckersMethod_SetBlackChipsStatusToStarting()
          {
               SetControllerDefaultValue();
               CreateControllerAndPlayers();

               controller.Players[ 0 ].MyPoolsColor = PoolColor.White;
               controller.Players[ 1 ].MyPoolsColor = PoolColor.Black;

               controller.ArrangingTheCheckersToPlay();

               //Act
               controller.ClearBoardFromCheckers();

               //Assert
               Assert.That( board.BlackPoolsSet
                                .All( x => x.State == PoolState.Starting) == true );
          }

          [TestCase(PoolColor.White,"2 2")]
          [TestCase(PoolColor.Black,"2 22")]
          public void 
               PlayerTakesOutTheLastChipAndWinsTheGame_CurrentPlayerMakesMoveMethod
               (PoolColor color,string moveString)
          {
               ///Outide and Game has a winner
               SetControllerDefaultValue();
               CreateControllerAndPlayers();
               ArrangeTakesOutTheLastChip();

               IPlayer player = controller.Players
                                          .FirstOrDefault( x => x.MyPoolsColor ==
                                                               color );
               this.controller.CurrentPlayer = player;

               ServiceCalculate.SetDiceValueAndMovesCount( this.board );
               //Act
               this.controller.CurrentPlayerMakesMove(moveString);

               //Assert
               Assert.That( this.controller.CurrentPlayer.State == PlayerState.Winner );
          }

//***************************************************************

          private void ArrangeTakesOutTheLastChip()
          {
               for ( int i = 0; i < board.WhitePoolsSet.Count - 1; i++ )
               {
                    this.board.WhitePoolsSet[ i ].State = PoolState.OutOfGame;
                    this.board.BlackPoolsSet[ i ].State = PoolState.OutOfGame;
               }

               this.board.WhitePoolsSet[ 14 ].State = PoolState.AtHome;
               this.board.BlackPoolsSet[ 14 ].State = PoolState.AtHome;

               this.board.ColumnSet[ 2 ].PoolStack.Push( this.board.WhitePoolsSet[ 14 ] );
               this.board.ColumnSet[ 22 ].PoolStack.Push( this.board.BlackPoolsSet[ 14 ] );

               controller.TablaBoard.DiceSet[1].ValueOfOneDice = 3;
               controller.TablaBoard.DiceSet[2].ValueOfOneDice = 2;

               this.controller.Players[ 0 ].MyPoolsColor = PoolColor.White;
               this.controller.Players[ 1 ].MyPoolsColor = PoolColor.Black;
          }

          private void CreateControllerAndPlayers()
          {
               this.controller = new Controller();
               this.board = this.controller.TablaBoard;

               controller.TablaBoard.DiceSet[1].ValueOfOneDice = 4;
               controller.TablaBoard.DiceSet[2].ValueOfOneDice = 1;

               string firstName = "White Jack";
               string secondName = "Black Jack";

               this.controller.Players = new List<IPlayer>();

               controller.Players.Add( new PlayerClassicGame( firstName ) );
               controller.Players.Add( new PlayerClassicGame( secondName ) );
          }

          private void SetControllerDefaultValue()
          {
               this.controller = default;
          }
     }
}
