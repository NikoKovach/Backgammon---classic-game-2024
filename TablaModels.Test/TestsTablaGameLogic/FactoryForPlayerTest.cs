namespace TablaModels.Test.TestsTablaGameLogic
{
     using System;
     using System.Collections.Generic;
     using Moq;
     using NUnit.Framework;
     
     using TablaGameLogic.Factory;
     using TablaGameLogic.Factory.Contracts;
     using TablaModels.Components.Interfaces;

     [TestFixture]
     public class FactoryForPlayerTest
     {
          [Test]
          public void CreatePlayerMethodSouldCreateObjectCorrectly()
          {
               string name = "Boris";
               IMoveChips moveChipsMoq = new Mock<IMoveChips>().Object;

               //Act
               IPlayer player = new PlayerFactory().CreatePlayer( name, moveChipsMoq );

               //Assert
               Assert.IsNotNull(player);

               Assert.AreEqual( name, player.Name );
          }

          [Test]
          public void CreatePlayerMethodSouldThrowArgumentExceptionWhenNameIsNullOrEmpty()
          {
               string name = null;
               IMoveChips moveChipsMoq = new Mock<IMoveChips>().Object;

               Assert.Throws<ArgumentNullException>( () => 
                    new PlayerFactory().CreatePlayer( name, moveChipsMoq ) );
          }

          [Test]
          public void CreatePlayerMethodSouldThrowArgumentExceptionWhenMoveCheckerIsNull()
          {
               string name = "Boris";
               IMoveChips moveChipsMoq = null;

               Assert.Throws<ArgumentNullException>( () => 
                    new PlayerFactory().CreatePlayer( name, moveChipsMoq ) );
          }

          
          [Test]
          public void CreatePlayersMethodSouldCreateCollectionOfPlayersCorrectly()
          {
               //Arrange
               string firstName = "Boris";
               string secondName = "Decho";

               //Act
               IList<IPlayer> players = new PlayerFactory()
                    .CreatePlayers( firstName, secondName );

               //Assert
               Assert.IsNotNull(players);

               Assert.AreEqual( firstName, players[0].Name );

               Assert.AreEqual( secondName, players[1].Name );
          }

          [TestCase(null,"Decho")]
          [TestCase("Boris",null)]
          public void CreatePlayersMethodSouldThrowArgumentNullExceptionWhenParametersAreNull
               (string firstName,string secondName )
          {

               Assert.Throws<ArgumentNullException>( () => 
                    new PlayerFactory().CreatePlayers( firstName, secondName ) );
          }

          [TestCase("","Decho")]
          [TestCase("Boris","")]
          public void CreatePlayersMethodSouldThrowArgumentExceptionWhenParametersAreEmpty
               (string firstName,string secondName )
          {

               Assert.Throws<ArgumentException>( () => 
                    new PlayerFactory().CreatePlayers( firstName, secondName ) );
          }
     }
}
