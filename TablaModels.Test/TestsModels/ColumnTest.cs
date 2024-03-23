namespace TablaModels.Test.TestsModels
{
     using NUnit.Framework;
     using System;
     using TablaModels.Components;
     using TablaModels.Components.Interfaces;

     [TestFixture]
     public class ColumnTest
     {

          [TestCase(1, "dark")]
          [TestCase(24, "light")]
          public void CreateAColumnWithDefaultConstructor(int colNumber, string color)
          {

              IColumn col = new Column(colNumber, color);

              Assert.IsInstanceOf<IColumn>(col);

          }

          [Test]
          public void      SetColumnColorMethodThrowArgumentExceptionWhenColorIsDifferentFromDarkOrLight()
          {
              IColumn column = new Column(1, "Dark");

              Assert.Throws<ArgumentException>(() => column.SetColumnColor("White"));
          }

          [Test]
          public void PropertyIdNumberThrowArgumentExceptionValueIsNotInRange1To24()
          {
              IColumn column = new Column(1, "Dark");

              Assert.Throws<ArgumentException>(() => column.IdentityNumber = 25);
          }

          [Test]
          public void PropertyIdNumberDoesNotThrowArgumentExceptionValueIsInRange1To24()
          {
              IColumn column = new Column(1, "Dark");

              Assert.DoesNotThrow(() => column.IdentityNumber = 24);
          }
     }
}
