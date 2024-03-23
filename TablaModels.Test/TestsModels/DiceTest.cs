namespace TablaModels.Test.TestsModels
{
     using NUnit.Framework;
     using System;
     using TablaModels.Components;
     using TablaModels.Components.Interfaces;

     [TestFixture]
     public class DiceTest
     {

          [Test]
          public void CreateADiceObjectWithDefaultConstructor()
          {
              IDice dice = new Dice();

              Assert.IsNotNull(dice);
          }

          [TestCase(1, "Dice1")]
          [TestCase(2, "Dice2")]
          [TestCase(3, "First")]
          [TestCase(4, "Second")]
          [TestCase(5, "Any")]
          [TestCase(6, "Bobo")]
          public void CreateADiceObjectWithParamConstructorAndValidParamsValues(int dieValue,    string name)
          {
              IDice dice = new Dice(name, dieValue, 1);

              Assert.IsNotNull(dice);
              Assert.AreEqual(name, dice.Name);
              Assert.AreEqual(dieValue, dice.ValueOfOneDice);
          }

          [TestCase(1, "Dice1", 100)]
          [TestCase(2, "Dice2", 200)]
          [TestCase(3, "First", 275)]
          [TestCase(4, "Second", 300)]
          [TestCase(5, "Any", 400)]
          [TestCase(6, "Bobo", 600)]
          public void CreateADiceObjectWithParamConstructorAndValidParamsValuesAfterWeAddParameterDiceSide(int dieValue, string name, int boardWidth)
          {
               int diceSide = (int)(boardWidth * 0.09);

               IDice dice = new Dice(name, dieValue, diceSide);

               Assert.IsNotNull(dice);
               Assert.AreEqual(name, dice.Name);
               Assert.AreEqual(dieValue, dice.ValueOfOneDice);
               Assert.AreEqual(diceSide, dice.DiceSide);
          }

          [TestCase(0)]
          [TestCase(7)]
          [TestCase(-6)]
          public void ConstructorCanNotCreateDiceThrowExceptionWhenWeSetInvavidIntValue(int      intNumber)
          {
              string dieName = "First";

              Assert.Throws<ArgumentException>(() => new Dice(dieName, intNumber, 1));
          }

          [TestCase(null)]
          [TestCase("")]
          [TestCase(" ")]
          [TestCase("\n")]
          public void PropertyNameThrowExceptionWhenWeSetInvalidValue(string valueOfName)
          {
              IDice dice = new Dice();

              Assert.Throws<ArgumentException>(() => dice.Name = valueOfName);
          }

          [TestCase(0)]
          [TestCase(7)]
          [TestCase(-6)]
          public void PropertyValueOfOneDiceThrowExceptionWhenWeSetInvavidIntValue(int      intNumber)
          {
              IDice dice = new Dice();

              Assert.Throws<ArgumentException>(() => dice.ValueOfOneDice = intNumber);
          }

          [TestCase(0)]
          [TestCase((int)0.14)]
          [TestCase(-6)]
          public void PropertyDiceSideThrowExceptionWhenWeSetInvavidIntValue(int diceSide)
          {
              IDice dice = new Dice();

              Assert.Throws<ArgumentException>(() => dice.DiceSide = diceSide);
          }
     }
}
