namespace TablaModels.Test.TestsModels
{
     using NUnit.Framework;
     using System;
     using System.Collections.Generic;
     using TablaModels.Components;
     using TablaModels.Components.Interfaces;

     [TestFixture]
     public class BoardTest
     {
          public static object[] DivideCases()
          {
               var columns = new Dictionary<int, IColumn>();
               var dices = new Dictionary<int, IDice>();
               var whiteChips = new List<IPool>();
               var blackChips = new List<IPool>();

               object[] ctorParams =
               {
                    new object[] { null, dices, whiteChips,blackChips },
                    new object[] { columns, null, whiteChips,blackChips },
                    new object[] { columns, dices, null,blackChips },
                    new object[] { columns, dices, whiteChips,null },
               };

              return ctorParams;
          }

          [Test]
          public void ConstructorCreateTablaBoard()
          {
               var columns = new Dictionary<int, IColumn>();
               var dices = new Dictionary<int, IDice>();
               var whiteChips = new List<IPool>();
               var blackChips = new List<IPool>();

               var board = new Board(columns, dices, whiteChips, blackChips);

               Assert.IsInstanceOf<IBoard>(board);
          }

          [TestCaseSource(nameof(DivideCases))]
          public void ConstructorThrowArgumentNullExceptionWhenCreateTablaBoard   (IDictionary<int, IColumn> columns, IDictionary<int, IDice> dices, IList<IPool>    whiteChips, IList<IPool> blackChips)
          {
               Assert.Throws<ArgumentNullException>
                    (
                         () => new Board(columns, dices, whiteChips, blackChips)
                    );
          }

          [Test]
          public void ConstructorDoesNotThrowArgumentNullExceptionWhenCreateTablaBoard()
          {
               var columns = new Dictionary<int, IColumn>();
               var dices = new Dictionary<int, IDice>();
               var whiteChips = new List<IPool>();
               var blackChips = new List<IPool>();

               var board = new Board(columns, dices, whiteChips, blackChips);
               Assert.DoesNotThrow
                    (
                         () => new Board(columns, dices, whiteChips, blackChips)
                    );
          }

     }
}
