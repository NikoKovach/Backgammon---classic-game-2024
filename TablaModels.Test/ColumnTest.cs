namespace TablaModels.Test
{
     using NUnit.Framework;
     using System;
     using TablaModels.ComponentModels.Components;
     using TablaModels.ComponentModels.Components.Interfaces;
     using TablaModels.ComponentModels.Enums;

     [TestFixture]
     public class ColumnTest
     {
          [SetUp]
          public void Setup()
          {

          }

          [TestCase(1,"dark")]
          [TestCase(24,"light")]
          public void CreateAColumnWithDefaultConstructor(int colNumber,string color)
          {
               
               IColumn col = new Column(colNumber,color);

               Assert.IsInstanceOf<IColumn>( col );

          }

          [Test]
          public void SetColumnColorMethodThrowArgumentExceptionWhenColorIsDifferentFromDarkOrLight()
          {
               IColumn column = new Column(1,"Dark");

               Assert.Throws<ArgumentException>(() =>  column.SetColumnColor("White") );
          }

          [Test]
          public void PropertyIdNumberThrowArgumentExceptionValueIsNotInRange1To24()
          {
               IColumn column = new Column(1,"Dark");

               Assert.Throws<ArgumentException>(() =>  column.IdentityNumber = 25 );
          }

          [Test]
          public void PropertyIdNumberDoesNotThrowArgumentExceptionValueIsInRange1To24()
          {
               IColumn column = new Column(1,"Dark");

               Assert.DoesNotThrow(() =>  column.IdentityNumber = 24 );
          }
     }
}
