using NUnit.Framework;
using System.Linq;
using TablaGameLogic.Factory;
using TablaGameLogic.Factory.Contracts;
using TablaModels.Components;
using TablaModels.Components.Interfaces;
using TablaModels.Enums;

namespace TablaModels.Test.TestsTablaGameLogic
{
    [TestFixture]
     public class BoardFactoryTest
     {
          public IBoard Board { get; set; }

          [SetUp]
          public void Setup()
          {
               IBoardFactory tablaFactory = new BoardFactory();

               this.Board  = tablaFactory.Create();           
          }

          [TearDown] 
          public void Cleanup()
          {
               this.Board = default;    
          }

          [Test]
          public void BoardFactoryCreateSuccessfullyTablaBoard()
          {
               Assert.IsNotNull( this.Board );
               Assert.IsInstanceOf<Board>( this.Board );
          }

          [Test]
          public void BoardFactoryCreateColumnsInTablaBoardCorrectly()
          {
               var columns = this.Board.ColumnSet;

               Assert.AreEqual(24,columns.Count);
          }

          [TestCase( 1 )]
          [TestCase( 2 )]
          [TestCase( 12 )]
          [TestCase( 23 )]
          [TestCase( 24 )]
          public void ColumnIdNumbersAreInRangeFromOneToTwentyfour(int colId)
          {
               var columns = this.Board.ColumnSet;

               Assert.AreEqual(colId,columns[colId].IdentityNumber);
          }

          [Test]
          public void BoardFactoryCreateDicesInTablaBoardCorrectly()
          {
               var dices = this.Board.DiceSet;

               Assert.AreEqual(2,dices.Count);
               Assert.AreEqual( "Dice1", dices[1].Name );
               Assert.AreEqual( "Dice2", dices[2].Name );
          }

          [Test]
          public void BoardFactoryCreateChipsSetInTablaBoardCorrectly()
          {
               var whiteChips = this.Board.WhitePoolsSet;
               var blackChips = this.Board.BlackPoolsSet;

               Assert.AreEqual(15,whiteChips.Count);

               Assert.That(whiteChips.All(x => x.PoolColor == PoolColor.White && 
                                               x.State == PoolState.Starting));

               Assert.That(blackChips.All(x => x.PoolColor == PoolColor.Black && 
                                               x.State == PoolState.Starting));

          }
     }
}
