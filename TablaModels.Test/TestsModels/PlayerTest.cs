namespace TablaModels.Test.TestsModels
{
     using NUnit.Framework;
     using TablaModels.Components.Interfaces;
     using TablaModels.Components.Players;
     using Moq;
     using TablaModels.Enums;
     using System;

     [TestFixture]
     public class PlayerTest
     {
          [Test]
          public void CtorWithOneParameterNameCreateObjectPlayer()
          {
               //Arrange
               string name = "Xxx";
               //Act
               IPlayer player = new PlayerClassicGame( name );
               //Assert

               Assert.IsNotNull( player );

               Assert.AreEqual( name, player.Name );
          }

          [Test]
          public void CtorWithTwoParametersNameAndMoveChipCreateObjectPlayer()
          {
               //Arrange
               string name = "Xxx";
               var moveChipMoq = new Mock<IMoveChips>().Object;

               //Act
               IPlayer player = new PlayerClassicGame( name, moveChipMoq );
               //Assert
               Assert.IsNotNull( player );

               Assert.AreEqual( name, player.Name );

               Assert.IsNotNull( player.Move );
               Assert.IsInstanceOf<IMoveChips>( player.Move );
          }

          [TestCase( PoolColor.White )]
          [TestCase( PoolColor.Black )]
          public void PropMyPoolsColorSetValueCorrectly( PoolColor color )
          {
               //Arrange     
               IPlayer player = new PlayerClassicGame( "Xxx" );
               //Act
               player.MyPoolsColor = color;
               //Assert
               Assert.AreEqual( color, player.MyPoolsColor );
          }

          [TestCase( PlayerState.NormalState)]
          [TestCase(PlayerState.Winner)]
          public void PropStateSetValueCorrectly(PlayerState state)
          {
               //Arrange     
               IPlayer player = new PlayerClassicGame( "Xxx" );
               //Act
               player.State = state;
               //Assert
               Assert.AreEqual( state, player.State );
          }

          [Test]
          [Repeat(50)]
          public void RollADiceMethodReturnValueInRangeFromOneToSix()
          {
               IPlayer player = new PlayerClassicGame( "Xxx" );
               //Act
               int value = player.RollADice();
               //Assert
               Assert.True(value >= 1 && value <= 6);
          }

          [TestCaseSource(nameof(DivideCases))]
          public void ArrangingTheChipsMethodThrowArgumentNullExceptionWhenInnerParametersAreNull(IBoard board, IArrangeChips arrangeChips)
          {
               IPlayer player = new PlayerClassicGame( "Xxx" );

               Assert.Throws<ArgumentNullException>
                    (
                         () => player.ArrangingTheCheckers(board,arrangeChips)
                    );
          }

          [TestCaseSource(nameof(DivideCasesParamsNotNull))]
          public void ArrangingTheChipsMethodDoesNotThrowArgumentNullExceptionWhenInnerParametersAreNotNull(IBoard board, IArrangeChips arrangeChips)
          {
               IPlayer player = new PlayerClassicGame( "Xxx" );

               Assert.DoesNotThrow
                    (
                         () => player.ArrangingTheCheckers(board,arrangeChips)
                    );
          }

          [Test]
          public void ArrangingTheCheckersCalls_ArrangeChips_ArrangeWhiteChipsMethodOnce
               ()
          {
               var boardMock = new Mock<IBoard>();
               var arrangeChipsMock = new Mock<IArrangeChips>();

               IPlayer player = new PlayerClassicGame( "Xxx" );
               player.MyPoolsColor = PoolColor.White;
               player.ArrangingTheCheckers( boardMock.Object, arrangeChipsMock.Object );

               arrangeChipsMock.Verify(x => x.ArrangeWhiteChips(boardMock.Object.ColumnSet,boardMock.Object.WhitePoolsSet),Times.Once);

          }

          [Test]
          public void ArrangingTheCheckersCalls_ArrangeChips_ArrangeBlackChipsMethodOnce
               ()
          {
               var boardMock = new Mock<IBoard>();
               var arrangeChipsMock = new Mock<IArrangeChips>();

               IPlayer player = new PlayerClassicGame( "Xxx" );
               player.MyPoolsColor = PoolColor.Black;
               player.ArrangingTheCheckers( boardMock.Object, arrangeChipsMock.Object );

               arrangeChipsMock.Verify(x => x.ArrangeBlackChips(boardMock.Object.ColumnSet,boardMock.Object.BlackPoolsSet),Times.Once);

          }
//**************************************************************************
          private static object[] DivideCases()
          {
               var boardMock = new Mock<IBoard>().Object;
               var arrangeChipsMock = new Mock<IArrangeChips>().Object;

               object[] arrangeTheChipsParams =
               {
                    new object[] { null, arrangeChipsMock },
                    new object[] { boardMock, null },

               };

              return arrangeTheChipsParams;
          }

          private static object[] DivideCasesParamsNotNull()
          {
               var boardMock = new Mock<IBoard>().Object;
               var arrangeChipsMock = new Mock<IArrangeChips>().Object;

               object[] arrangeTheChipsParams =
               {
                    new object[] { boardMock, arrangeChipsMock },

               };

              return arrangeTheChipsParams;
          }

          //private static object[] CasePlayerIsCreate()
          //{
          //     object[] playersArr =
          //     {
          //          new object[] { new PlayerClassicGame( "Xxx" ) },

          //     };

          //    return playersArr;
          //}
     }
}
